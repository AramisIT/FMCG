using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using pdtExternalStorage;

namespace WMS_client
    {
    public class ServerInteraction : IRemoteCommunications
        {

        public bool GetCarsForAcceptance(out System.Data.DataTable table)
            {
            throw new NotImplementedException();
            }

        public bool GetPlaceDataFromCode(string barcode, out string type, out long id)
            {
            throw new NotImplementedException();
            }

        public bool GetPermitInstallPalletManually()
            {
            throw new NotImplementedException();
            }

        public bool GetDataAboutMovingPallet(int palletId, out string goods, out string date, out double boxCount, out double bottleCount)
            {
            throw new NotImplementedException();
            }

        public bool GetDataForInventory(out double count, out string nomenclature, out string date, out long palletId, out string measure, out long docId, out long lineNumber, out long cellId, out string cell)
            {
            throw new NotImplementedException();
            }

        /// <summary>К-сть документів, що чекають обробки</summary>
        /// <param name="acceptanceDocCount">К-сть документів "Прийманя"</param>
        /// <param name="inventoryDocCount">К-сть документів "Інветаризація"</param>
        /// <param name="selectionDocCount">К-сть документів "Відбір"</param>
        /// <param name="movementDocCount">К-сть документів "Переміщення"</param>
        public bool GetCountOfDocuments(out string acceptanceDocCount, out string inventoryDocCount,
                                        out string selectionDocCount, out string movementDocCount)
            {
            PerformQuery("GetCountOfDocuments");

            if (IsExistParameters)
                {
                acceptanceDocCount = Parameters[0].ToString();
                inventoryDocCount = Parameters[1].ToString();
                selectionDocCount = Parameters[2].ToString();
                movementDocCount = Parameters[3].ToString();
                return true;
                }

            acceptanceDocCount = 0.ToString();
            inventoryDocCount = 0.ToString();
            selectionDocCount = 0.ToString();
            movementDocCount = 0.ToString();

            return false;
            }

        public bool GetSelectionRowInfo(long contractor, out long id, out long palletId, out string goods, out string date, out double boxCount, out double unitCount, out int baseCount, out string cell)
            {
            throw new NotImplementedException();
            }

        public bool GetAdditionalInfoAboutAccepnedGoods(double count, long goods, long car, long party, out long incomeDoc, out string date, out long cellId, out string cell, out long palett)
            {
            throw new NotImplementedException();
            }

        public void SetInventory(long docId, long lineNumber, long count)
            {
            throw new NotImplementedException();
            }

        public void SetMoving(long palletId, long newPos, bool isCell)
            {
            throw new NotImplementedException();
            }

        public void SetAcceptanceData(long nomenclature, long party, double boxCount, double bottleCount, long planId, long previousPallet, long cellId, bool isCell)
            {
            throw new NotImplementedException();
            }

        public void SetSelectionData(long docId, long palletId, long cellId)
            {
            throw new NotImplementedException();
            }

        public bool CheckBarcodeForExistUser(string barcode)
            {
            throw new NotImplementedException();
            }

        public bool CheckBarcodeForExistGoods(string barcode, out long id, out string description)
            {
            throw new NotImplementedException();
            }

        public bool CheckPalletBarcodeForMoving(string barcode, bool cellIsAccepted, out long palletId, out bool isCell)
            {
            throw new NotImplementedException();
            }

        public bool CheckInventoryPallet(string barcode, long cellId, out bool result)
            {
            throw new NotImplementedException();
            }

        public bool CheckCellFormShipment(string barcode, out long id)
            {
            throw new NotImplementedException();
            }

        private void PerformQuery(string QueryName, params object[] parameters)
            {
            Parameters = null;
            if (!WMSClient.Current.OnLine && WMSClient.Current.MainForm.IsMainThread)
                {
                WMSClient.Current.ShowMessage("Нет подключения к серверу");
                return;
                }

            Parameters = WMSClient.Current.PerformQuery(QueryName, parameters);
            }

        private bool IsExistParameters
            {
            get { return Parameters != null && Parameters.Length > 0 && Parameters[0] != null; }
            }

        private object[] Parameters;

        public bool GetTareTable(out System.Data.DataTable tareTable)
            {
            PerformQuery("GetTareTable");

            if (IsExistParameters)
                {
                tareTable = Parameters[0] as DataTable;
                return tareTable is DataTable;
                }

            tareTable = null;
            return false;
            }

        public bool GetStickerData(long acceptanceId, long stickerId, out string nomenclatureDescription, out string trayDescription, out long trayId, out int unitsPerBox, out long cellId, out string cellDescription)
            {
            PerformQuery("GetStickerData", acceptanceId, stickerId);

            if (IsExistParameters)
                {
                nomenclatureDescription = Parameters[0] as string;
                trayDescription = Parameters[1] as string;
                trayId = Convert.ToInt64(Parameters[2]);
                unitsPerBox = Convert.ToInt32(Parameters[3]);
                cellId = Convert.ToInt64(Parameters[4]);
                cellDescription = Parameters[5] as string;

                return true;
                }

            nomenclatureDescription = null;
            trayDescription = null;
            trayId = 0;
            unitsPerBox = 0;
            cellDescription = null;
            cellId = 0;

            return false;
            }

        public bool GetAcceptanceId(long stickerId, out long acceptanceId)
            {
            PerformQuery("GetAcceptanceId", stickerId);

            if (IsExistParameters)
                {
                acceptanceId = Convert.ToInt64(Parameters[0]);
                return true;
                }

            acceptanceId = 0;

            return false;
            }

        #region IRemoteCommunications Members




        #endregion
        }
    }
