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
        private void performQuery(string QueryName, params object[] parameters)
            {
            queryResultParameters = null;
            if (!WMSClient.Current.OnLine && WMSClient.Current.MainForm.IsMainThread)
                {
                "Нет подключения к серверу".Warning();
                return;
                }

            queryResultParameters = WMSClient.Current.PerformQuery(QueryName, parameters);
            }

        private bool IsExistParameters
            {
            get { return queryResultParameters != null && queryResultParameters.Length > 0 && queryResultParameters[0] != null; }
            }

        public bool remoteFunctionBoolResult
            {
            get { return (bool)queryResultParameters[1]; }
            }

        public object remoteFunctionResult
            {
            get { return queryResultParameters[1]; }
            }

        private bool success
            {
            get
                {
                return IsExistParameters && queryResultParameters[0] is bool && (bool)queryResultParameters[0];
                }
            }

        private object[] queryResultParameters;

        /// <summary>К-сть документів, що чекають обробки</summary>
        /// <param name="acceptanceDocCount">К-сть документів "Прийманя"</param>
        /// <param name="inventoryDocCount">К-сть документів "Інветаризація"</param>
        /// <param name="selectionDocCount">К-сть документів "Відбір"</param>
        /// <param name="movementDocCount">К-сть документів "Переміщення"</param>
        public bool GetCountOfDocuments(out string acceptanceDocCount, out string inventoryDocCount,
                                        out string selectionDocCount, out string movementDocCount)
            {
            performQuery("GetCountOfDocuments");

            if (success)
                {
                acceptanceDocCount = queryResultParameters[2].ToString();
                inventoryDocCount = queryResultParameters[3].ToString();
                selectionDocCount = queryResultParameters[4].ToString();
                movementDocCount = queryResultParameters[5].ToString();
                return true;
                }

            acceptanceDocCount = 0.ToString();
            inventoryDocCount = 0.ToString();
            selectionDocCount = 0.ToString();
            movementDocCount = 0.ToString();

            return false;
            }

        public bool GetTareTable(out DataTable tareTable)
            {
            performQuery("GetTareTable");

            if (success)
                {
                tareTable = queryResultParameters[2] as DataTable;
                return (bool)queryResultParameters[1];
                }

            tareTable = null;
            return false;
            }

        public bool GetStickerData(long acceptanceId, long stickerId, out long nomenclatureId, out string nomenclatureDescription, out long trayId,
            out int totalUnitsQuantity, out int unitsPerBox,
            out long cellId, out string cellDescription, out bool currentAcceptance,
            out int wareBarcodesCount)
            {
            performQuery("GetStickerData", acceptanceId, stickerId);

            if (IsExistParameters)
                {
                nomenclatureId = Convert.ToInt64(queryResultParameters[0]);
                nomenclatureDescription = queryResultParameters[1] as string;
                trayId = Convert.ToInt64(queryResultParameters[2]);
                totalUnitsQuantity = Convert.ToInt32(queryResultParameters[3]);
                unitsPerBox = Convert.ToInt32(queryResultParameters[4]);
                cellId = Convert.ToInt64(queryResultParameters[5]);
                cellDescription = queryResultParameters[6] as string;
                currentAcceptance = (bool)queryResultParameters[7];
                wareBarcodesCount = Convert.ToInt32(queryResultParameters[8]);
                return true;
                }

            wareBarcodesCount = 0;
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
            performQuery("GetAcceptanceId", stickerId);

            if (IsExistParameters)
                {
                acceptanceId = Convert.ToInt64(queryResultParameters[0]);
                return true;
                }

            acceptanceId = 0;

            return false;
            }

        public bool WriteStickerFact(long acceptanceId, long stickerId, bool palletChanged, long cellId, long previousStickerId, long trayId, long linerId, int linersQuantity, int packsCount, int unitsCount)
            {
            performQuery("WriteStickerFact", acceptanceId, stickerId, palletChanged, cellId, previousStickerId, trayId, linerId, linersQuantity, packsCount, unitsCount);

            return IsExistParameters && queryResultParameters[0] is bool && (bool)queryResultParameters[0];
            }

        private const string QUERY_NOT_SERVED_MESSAGE = "Запит не виконано";
        public bool ComplateAcceptance(long acceptanceId, bool forceCompletion, out string errorMessage)
            {
            performQuery("ComplateAcceptance", acceptanceId, forceCompletion);
            if (IsExistParameters && queryResultParameters[0] is bool && queryResultParameters.Length > 1)
                {
                errorMessage = queryResultParameters[1].ToString();
                return (bool)queryResultParameters[0];
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
            out DateTime productionDate, out long partyId,
            out int totalUnitsQuantity)
            {
            performQuery("GetPalletBalance", stickerId);

            if (IsExistParameters)
                {
                nomenclatureId = Convert.ToInt64(queryResultParameters[0]);
                nomenclatureDescription = queryResultParameters[1] as string;
                trayId = Convert.ToInt64(queryResultParameters[2]);
                linerId = Convert.ToInt64(queryResultParameters[3]);
                linersAmount = Convert.ToByte(queryResultParameters[4]);
                unitsPerBox = Convert.ToInt32(queryResultParameters[5]);
                cellId = Convert.ToInt64(queryResultParameters[6]);
                cellDescription = queryResultParameters[7] as string;
                previousPalletCode = Convert.ToInt64(queryResultParameters[8]);
                productionDate = queryResultParameters[9].ToString().ToDateTime();
                partyId = Convert.ToInt64(queryResultParameters[10]);
                totalUnitsQuantity = Convert.ToInt32(queryResultParameters[11]);
                return true;
                }

            nomenclatureId = 0;
            nomenclatureDescription = null;
            trayId = 0;
            unitsPerBox = 0;
            cellDescription = null;
            cellId = 0;
            previousPalletCode = 0;
            totalUnitsQuantity = 0;
            linerId = 0;
            linersAmount = 0;
            productionDate = DateTime.MinValue;
            partyId = 0;
            return false;
            }

        public bool GetNewInventoryId(out long documentId)
            {
            performQuery("GetNewInventoryId");

            if (IsExistParameters)
                {
                documentId = Convert.ToInt64(queryResultParameters[0]);
                return documentId > 0;
                }

            documentId = 0;
            return false;
            }

        public bool WriteInventoryResult(long documentId, DataTable resultTable)
            {
            performQuery("WriteInventoryResult", documentId, resultTable);
            return success;
            }

        public bool ComplateInventory(long documentId, bool forceCompletion, out string errorMessage)
            {
            performQuery("ComplateInventory", documentId, forceCompletion);
            if (IsExistParameters && queryResultParameters[0] is bool && queryResultParameters.Length > 1)
                {
                errorMessage = queryResultParameters[1].ToString();
                return (bool)queryResultParameters[0];
                }
            else
                {
                errorMessage = QUERY_NOT_SERVED_MESSAGE;
                return false;
                }
            }

        public bool GetNewMovementId(out long documentId)
            {
            performQuery("GetNewMovementId");

            if (IsExistParameters)
                {
                documentId = Convert.ToInt64(queryResultParameters[0]);
                return documentId > 0;
                }

            documentId = 0;
            return false;
            }

        public bool WriteMovementResult(long documentId, DataTable resultTable)
            {
            performQuery("WriteMovementResult", documentId, resultTable);
            return success;
            }

        public bool ComplateMovement(long documentId, bool forceCompletion, out string errorMessage)
            {
            performQuery("ComplateMovement", documentId, forceCompletion);
            if (IsExistParameters && queryResultParameters[0] is bool && queryResultParameters.Length > 1)
                {
                errorMessage = queryResultParameters[1].ToString();
                return (bool)queryResultParameters[0];
                }
            else
                {
                errorMessage = QUERY_NOT_SERVED_MESSAGE;
                return false;
                }
            }

        public DataTable GetPickingDocuments()
            {
            performQuery("GetPickingDocuments");
            if (IsExistParameters && queryResultParameters[0] is DataTable)
                {
                return queryResultParameters[0] as DataTable;
                }
            else
                {
                return new DataTable();
                }
            }

        public bool GetPickingTask(long documentId, long palletId, int predefinedTaskLineNumber,
            int currentLineNumber,
            out long stickerId,
            out long wareId, out string wareDescription,
            out long cellId, out string cellDescription,
            out long partyId, out DateTime productionDate,
            out int unitsPerBox, out int unitsToPick,
            out int lineNumber)
            {
            performQuery("GetPickingTask", documentId, palletId, predefinedTaskLineNumber, currentLineNumber);

            if (success)
                {
                stickerId = Convert.ToInt64(queryResultParameters[2]);
                wareId = Convert.ToInt64(queryResultParameters[3]);
                wareDescription = queryResultParameters[4].ToString();
                cellId = Convert.ToInt64(queryResultParameters[5]);
                cellDescription = queryResultParameters[6].ToString();
                partyId = Convert.ToInt64(queryResultParameters[7]);
                productionDate = queryResultParameters[8].ToString().ToDateTime();
                unitsPerBox = Convert.ToInt32(queryResultParameters[9]);
                unitsToPick = Convert.ToInt32(queryResultParameters[10]);
                lineNumber = Convert.ToInt32(queryResultParameters[11]);
                return remoteFunctionBoolResult;
                }

            stickerId = wareId = cellId = partyId = unitsToPick = unitsPerBox = lineNumber = 0;
            wareDescription = cellDescription = string.Empty;
            productionDate = DateTime.MinValue;

            return false;
            }

        public bool WritePickingResult(long documentId, int currentLineNumber, DataTable resultTable, long partyId, out int sameWareNextTaskLineNumber)
            {
            performQuery("WritePickingResult", documentId, currentLineNumber, resultTable, partyId);
            sameWareNextTaskLineNumber = success ? Convert.ToInt32(queryResultParameters[1]) : 0;

            return success;
            }

        public bool PrintStickers(DataTable result)
            {
            performQuery("PrintStickers", result);
            return success;
            }

        public bool ReadConsts(out DataTable constsTable)
            {
            performQuery("ReadConsts");
            constsTable = success ? queryResultParameters[1] as DataTable : null;
            return success;
            }

        public bool CreatePickingDocuments()
            {
            performQuery("CreatePickingDocuments");
            return success;
            }

        public string GetUserName(int userId)
            {
            performQuery("GetUserName", userId);
            if (!success) return string.Empty;

            return queryResultParameters[1] as string;
            }

        public DataTable GetWares(string barcode, SelectionFilters selectionFilter)
            {
            performQuery("GetWares", barcode, (int)selectionFilter);
            if (!success) return null;

            return queryResultParameters[1] as DataTable;
            }

        public bool SetBarcode(string barcode, long stickerId, out bool recordWasAdded)
            {
            performQuery("SetBarcode", barcode, stickerId);
            if (!success)
                {
                recordWasAdded = false;
                return false;
                }

            recordWasAdded = (bool)queryResultParameters[1];
            return true;
            }

        public void SetPalletStatus(long stickerId, bool fullPallet)
            {
            performQuery("SetPalletStatus", stickerId, fullPallet);
            }

        public DataTable GetParties(long wareId, SelectionFilters selectionFilter)
            {
            performQuery("GetParties", wareId, (int)selectionFilter);

            if (success)
                {
                return queryResultParameters[1] as DataTable;
                }
            return null;
            }

        public DataTable GetWaresInKegs(SelectionFilters selectionFilter)
            {
            performQuery("GetWaresInKegs", (int)selectionFilter);

            if (success)
                {
                return queryResultParameters[1] as DataTable;
                }
            return null;
            }

        public long CreateNewSticker(long wareId, DateTime expirationDate, int unitsQuantity, int boxesCount, long linerId, int linersCount)
            {
            performQuery("CreateNewSticker", wareId, expirationDate, unitsQuantity, boxesCount, linerId, linersCount);

            if (success)
                {
                return Convert.ToInt64(remoteFunctionResult);
                }
            return 0;
            }

        public long CreateNewAcceptance()
            {
            performQuery("CreateNewAcceptance");

            if (success)
                {
                return Convert.ToInt64(queryResultParameters[1]);
                }
            return 0;
            }
        }
    }
