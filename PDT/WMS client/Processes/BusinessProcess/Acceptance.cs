using System.Linq;
using WMS_client.Processes.BaseScreen;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    /// <summary>Приймання товару</summary>
    public class Acceptance : Process<AcceptanceData>
        {
        private const string INVALID_BARCODE_MSG = "Відсканований штрих-код не вірний";

        /// <summary>Приймання товару</summary>
        public Acceptance(WMSClient MainProcess)
            : base(MainProcess)
            {
            IsLoad = true;
            DrawControls();
            }

        #region Override methods
        public override sealed void DrawControls()
            {
            if (IsLoad)
                {
                DataTable table;
                List<TableData> listOfElements = new List<TableData>();

                if (GetCarsForAcceptance(out table))
                    {
                    listOfElements.AddRange(
                        from DataRow row in table.Rows
                        select
                            new TableData(
                            Convert.ToInt64(row[ID_COLUMN_NAME]),
                            row[DESCRIPTION_COLUMN_NAME].ToString(),
                            string.Empty));
                    }
                MainProcess.Process = new SelectTableList(
                    MainProcess, selectCar, Data.Topic, "Машина", listOfElements, "До вибору процесів (Esc)", true);
                }
            }

        public override void OnBarcode(string Barcode) {}
        #endregion

        #region Stages
        private void selectCar(long selectedIndex, string description)
            {
            Data.Car = selectedIndex;
            readUserBarcode();
            }

        private void readUserBarcode()
            {
            MainProcess.ClearControls();
            MainProcess.Process = new ReadBarcode(
                MainProcess, "Відскануйте свій штрих-код", string.Empty, afterReadUserBarcode);
            }

        private void afterReadUserBarcode(string barcode)
            {
            if (CheckBarcodeForExistUser(barcode))
                {
                Data.UserBarcode = barcode;
                readGoodsBarcode();
                }
            else
                {
                ShowMessage(INVALID_BARCODE_MSG);
                readUserBarcode();
                }
            }

        private void readGoodsBarcode()
            {
            MainProcess.ClearControls();
            MainProcess.Process = new ReadBarcode(MainProcess, "Відскануйте товар", "Відміна", afterReadGoodsBarcode);
            }

        private void afterReadGoodsBarcode(string barcode)
            {
            long goodsId;
            string goodsDesc;

            if (CheckBarcodeForExistGoods(barcode, out goodsId, out goodsDesc))
                {
                //Перелік необхідної інформації для продовження:
                // - Комірка
                // - Номенклатура
                // - Дата 
                // - К-сть ящиків, банок
                // - Партія

                //Note: тимчасово (так як нема QRcode)
                Random rand = new Random();
                Data.BoxCount = rand.Next(0, 100);
                Data.BottleCount = rand.Next(0, 100);
                Data.Party = rand.Next(1, 5);
                Data.Goods = new KeyValuePair<long, string>(goodsId, goodsDesc);

                long incomeDoc;
                string date;
                long cellId;
                string cell;
                long palett;

                PerformQuery("GetAdditionalInfoAboutAccepnedGoods", Data.BoxCount, Data.Goods.Key, Data.Car, Data.Party);

                if (GetAdditionalInfoAboutAccepnedGoods(Data.BoxCount, Data.Goods.Key, Data.Car, Data.Party,
                                                        out incomeDoc, out date, out cellId, out cell, out palett))
                    {
                    Data.ConsignmentNote = incomeDoc;
                    Data.Date = date;
                    Data.Place = new PlaningData<long>(palett);
                    Data.Cell = new KeyValuePair<long, string>(cellId, cell);

                    checkCell();
                    }
                }
            else
                {
                ShowMessage(INVALID_BARCODE_MSG);
                readGoodsBarcode();
                }
            }

        private void checkCell()
            {
            Data.PermitInstallPalletManually = GetPermitInstallPalletManually();
            MainProcess.ClearControls();
            MainProcess.Process = new Placing(MainProcess, Data, finish);
            }

        private void finish()
            {
            save();
            readGoodsBarcode();
            }

        private void save()
            {
            SetAcceptanceData(
                Data.Goods.Key,
                Data.Party,
                Data.BoxCount,
                Data.BottleCount,
                Data.ConsignmentNote,
                Data.Place.Fact,
                Data.Cell.Key,
                Data.IsCell);
            }
        #endregion
        }
    }