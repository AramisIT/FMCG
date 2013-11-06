using System.Linq;
using WMS_client.HelperClasses;
using WMS_client.Processes.BaseScreen;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    /// <summary>Відвантаження</summary>
    public class Selection : Process<SelectionData>
        {
        /// <summary>Відвантаження</summary>
        public Selection()
            : base()
            {
            Data = new SelectionData("Відбір товару");
            isLoading = true;
            DrawControls();
            }

        #region Override methods
        public override sealed void DrawControls()
            {
            if (isLoading)
                {
                selectDoc();
                }
            }

        public override void OnBarcode(string Barcode) {}
        #endregion

        #region Stages
        private void selectDoc()
            {
            MainProcess.ToDoCommand = Data.Topic;
            PerformQuery("GetContractorsForSelection");
            List<TableData> listOfElements = new List<TableData>();

            if (IsAnswerIsTrue)
                {
                DataTable table = Parameters[1] as DataTable;

                if (table != null)
                    {
                    listOfElements.AddRange(
                        from DataRow row in table.Rows
                        select new TableData(
                            Convert.ToInt64(row[ID_COLUMN_NAME]),
                            row[DESCRIPTION_COLUMN_NAME].ToString(),
                            string.Empty));
                    }
                }

            MainProcess.Process = new SelectTableList(
                MainProcess, afterDocumentIsSelected, Data.Topic, "Відбір", listOfElements,
                "До вибору процесів (Esc)", true);
            }

        private void afterDocumentIsSelected(long selectedIndex, string description)
            {
            Data.Contractor = new KeyValuePair<long, string>(selectedIndex, description);
            showRow();
            }

        private void showRow()
            {
            long id;
            long palletId;
            string goods;
            string date;
            double boxCount;
            double unitCount;
            int baseCount;
            string cell;

            if (GetSelectionRowInfo(
                Data.Contractor.Key, out id, out palletId, out goods, out date, out boxCount, out unitCount,
                out baseCount, out cell))
                {
                    Data.DocId = id;
                    Data.PalletId = palletId;
                    Data.GoodsDescription = goods;
                    Data.Date = date;
                    Data.BoxCount = boxCount;
                    Data.UnitCount = unitCount;
                    Data.BoxSize = baseCount;
                    Data.Cell = cell;

                MainProcess.ClearControls();
                MainProcess.Process = new SelectedGoodsInfo(MainProcess, Data, afterScanPallet);
                }
            else
                {
                ShowMessage("Операцію завершено");
                selectDoc();
                }
            }

        private void afterScanPallet(string barcode)
            {
            long palletId;

            if (BarcodeWorker.GetPalletId(barcode, out palletId))
                {
                if (Data.PalletId == palletId)
                    {
                    MainProcess.ClearControls();
                    MainProcess.Process = new SelectedGoodsInfo(MainProcess, Data, afterEnterCounts, true);
                    }
                else
                    {
                    ShowMessage("Відсканована паллета не співпадає з очікуваною");
                    }
                }
            }

        private void afterEnterCounts(string barcode)
            {
            //todo: Комірка "Викуп/..."
            MainProcess.ClearControls();
            string cell = string.Concat("Комірка: ", Data.Cell);
            MainProcess.Process = new ReadBarcode(MainProcess, cell, "Відсткануйте комірку", string.Empty, afterSelectCellForPllet);
            }

        private void afterSelectCellForPllet(string barcode)
            {
            long cellId;

            if (CheckCellFormShipment(barcode, out cellId))
                {
                //save
                SetSelectionData(Data.DocId, Data.PalletId, cellId);
                //next
                MainProcess.ClearControls();
                Data = new SelectionData("Відвантаження");
                showRow();
                }
            }
        #endregion
        }
    }