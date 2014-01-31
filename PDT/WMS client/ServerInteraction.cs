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
                "Нет подключения к серверу".Warning();
                return;
                }

            Parameters = WMSClient.Current.PerformQuery(QueryName, parameters);
            }

        private bool IsExistParameters
            {
            get { return Parameters != null && Parameters.Length > 0 && Parameters[0] != null; }
            }

        private bool success
            {
            get
                {
                return IsExistParameters && Parameters[0] is bool && (bool)Parameters[0];
                }
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

        public bool GetStickerData(long acceptanceId, long stickerId, out long nomenclatureId, out string nomenclatureDescription, out long trayId,
            out int totalUnitsQuantity, out int unitsPerBox,
            out long cellId, out string cellDescription, out bool currentAcceptance)
            {
            PerformQuery("GetStickerData", acceptanceId, stickerId);

            if (IsExistParameters)
                {
                nomenclatureId = Convert.ToInt64(Parameters[0]);
                nomenclatureDescription = Parameters[1] as string;
                trayId = Convert.ToInt64(Parameters[2]);
                totalUnitsQuantity = Convert.ToInt32(Parameters[3]);
                unitsPerBox = Convert.ToInt32(Parameters[4]);
                cellId = Convert.ToInt64(Parameters[5]);
                cellDescription = Parameters[6] as string;
                currentAcceptance = (bool)Parameters[7];
                return true;
                }

            nomenclatureId = 0;
            nomenclatureDescription = null;
            trayId = 0;
            totalUnitsQuantity = 0;
            unitsPerBox = 0;
            cellDescription = null;
            cellId = 0;
            currentAcceptance = false;

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



        public bool WriteStickerFact(long acceptanceId, long stickerId, bool palletChanged, long cellId, long trayId, long linerId, int linersQuantity, int packsCount, int unitsCount)
            {
            PerformQuery("WriteStickerFact", acceptanceId, stickerId, palletChanged, cellId, trayId, linerId, linersQuantity, packsCount, unitsCount);

            return IsExistParameters && Parameters[0] is bool && (bool)Parameters[0];
            }


        private const string QUERY_NOT_SERVED_MESSAGE = "Запит не виконано";
        public bool ComplateAcceptance(long acceptanceId, bool forceCompletion, out string errorMessage)
            {
            PerformQuery("ComplateAcceptance", acceptanceId, forceCompletion);
            if (IsExistParameters && Parameters[0] is bool && Parameters.Length > 1)
                {
                errorMessage = Parameters[1].ToString();
                return (bool)Parameters[0];
                }
            else
                {
                errorMessage = QUERY_NOT_SERVED_MESSAGE;
                return false;
                }
            }

        public bool GetPalletBalance(long stickerId,
            out long nomenclatureId,
            out string nomenclatureDescription,
            out long trayId,
            out long linerId, out byte linersAmount,
            out int unitsPerBox,
            out long cellId, out string cellDescription,
            out long previousPalletCode,
            out DateTime productionDate, out long partyId)
            {
            PerformQuery("GetPalletBalance", stickerId);

            if (IsExistParameters)
                {
                nomenclatureId = Convert.ToInt64(Parameters[0]);
                nomenclatureDescription = Parameters[1] as string;
                trayId = Convert.ToInt64(Parameters[2]);
                linerId = Convert.ToInt64(Parameters[3]);
                linersAmount = Convert.ToByte(Parameters[4]);
                unitsPerBox = Convert.ToInt32(Parameters[5]);
                cellId = Convert.ToInt64(Parameters[6]);
                cellDescription = Parameters[7] as string;
                previousPalletCode = Convert.ToInt64(Parameters[8]);
                productionDate = Parameters[9].ToString().ToDateTime();
                partyId = Convert.ToInt64(Parameters[10]);
                return true;
                }

            nomenclatureId = 0;
            nomenclatureDescription = null;
            trayId = 0;
            unitsPerBox = 0;
            cellDescription = null;
            cellId = 0;
            previousPalletCode = 0;
            linerId = 0;
            linersAmount = 0;
            productionDate = DateTime.MinValue;
            partyId = 0;
            return false;
            }

        public bool GetNewInventoryId(long userId, out long documentId)
            {
            PerformQuery("GetNewInventoryId", userId);

            if (IsExistParameters)
                {
                documentId = Convert.ToInt64(Parameters[0]);
                return documentId > 0;
                }

            documentId = 0;
            return false;
            }

        public bool WriteInventoryResult(long documentId, DataTable resultTable)
            {
            PerformQuery("WriteInventoryResult", documentId, resultTable);
            return success;
            }

        public bool ComplateInventory(long documentId, bool forceCompletion, out string errorMessage)
            {
            PerformQuery("ComplateInventory", documentId, forceCompletion);
            if (IsExistParameters && Parameters[0] is bool && Parameters.Length > 1)
                {
                errorMessage = Parameters[1].ToString();
                return (bool)Parameters[0];
                }
            else
                {
                errorMessage = QUERY_NOT_SERVED_MESSAGE;
                return false;
                }
            }

        public bool GetNewMovementId(long userId, out long documentId)
            {
            PerformQuery("GetNewMovementId", userId);

            if (IsExistParameters)
                {
                documentId = Convert.ToInt64(Parameters[0]);
                return documentId > 0;
                }

            documentId = 0;
            return false;
            }

        public bool WriteMovementResult(long documentId, DataTable resultTable)
            {
            PerformQuery("WriteMovementResult", documentId, resultTable);
            return success;
            }

        public bool ComplateMovement(long documentId, bool forceCompletion, out string errorMessage)
            {
            PerformQuery("ComplateMovement", documentId, forceCompletion);
            if (IsExistParameters && Parameters[0] is bool && Parameters.Length > 1)
                {
                errorMessage = Parameters[1].ToString();
                return (bool)Parameters[0];
                }
            else
                {
                errorMessage = QUERY_NOT_SERVED_MESSAGE;
                return false;
                }
            }

        public DataTable GetPickingDocuments()
            {
            PerformQuery("GetPickingDocuments");
            if (IsExistParameters && Parameters[0] is DataTable)
                {
                return Parameters[0] as DataTable;
                }
            else
                {
                return new DataTable();
                }
            }

        public bool GetPickingTask(int userId, long documentId, long palletId, int predefinedTaskLineNumber,
            int currentLineNumber,
            out long stickerId,
            out long wareId, out string wareDescription,
            out long cellId, out string cellDescription,
            out long partyId, out DateTime productionDate,
            out int unitsPerBox, out int unitsToPick,
            out int lineNumber)
            {
            PerformQuery("GetPickingTask", userId, documentId, palletId, predefinedTaskLineNumber, currentLineNumber);

            if (IsExistParameters)
                {
                stickerId = Convert.ToInt64(Parameters[0]);
                wareId = Convert.ToInt64(Parameters[1]);
                wareDescription = Parameters[2].ToString();
                cellId = Convert.ToInt64(Parameters[3]);
                cellDescription = Parameters[4].ToString();
                partyId = Convert.ToInt64(Parameters[5]);
                productionDate = Parameters[6].ToString().ToDateTime();
                unitsPerBox = Convert.ToInt32(Parameters[7]);
                unitsToPick = Convert.ToInt32(Parameters[8]);
                lineNumber = Convert.ToInt32(Parameters[9]);
                return true;
                }

            stickerId = wareId = cellId = partyId = unitsToPick = unitsPerBox = lineNumber = 0;
            wareDescription = cellDescription = string.Empty;
            productionDate = DateTime.MinValue;

            return false;
            }

        public bool WritePickingResult(long documentId, int currentLineNumber, DataTable resultTable, long partyId, out int sameWareNextTaskLineNumber)
            {
            PerformQuery("WritePickingResult", documentId, currentLineNumber, resultTable, partyId);
            sameWareNextTaskLineNumber = success ? Convert.ToInt32(Parameters[1]) : 0;

            return success;
            }

        public bool PrintStickers(DataTable result)
            {
            PerformQuery("PrintStickers", result);
            return success;
            }

        public bool ReadConsts(out DataTable constsTable)
            {
            PerformQuery("ReadConsts");
            constsTable = success ? Parameters[1] as DataTable : null;
            return success;
            }

        public bool CreatePickingDocuments()
            {
            PerformQuery("CreatePickingDocuments");
            return success;
            }

        public string GetUserName(int userId)
            {
            PerformQuery("GetUserName", userId);
            if (!success) return string.Empty;

            return Parameters[1] as string;
            }

        public DataTable GetWares(string barcode)
            {
            PerformQuery("GetWares", barcode);
            if (!success) return null;

            return Parameters[1] as DataTable;
            }

        public bool SetBarcode(string barcode, long stickerId, out bool recordWasAdded)
            {
            PerformQuery("SetBarcode", barcode, stickerId);
            if (!success)
                {
                recordWasAdded = false;
                return false;
                }

            recordWasAdded = (bool)Parameters[1];
            return true;
            }

        public void SetPalletStatus(long stickerId, bool fullPallet)
            {
            PerformQuery("SetPalletStatus", stickerId, fullPallet);
            }
        }
    }
