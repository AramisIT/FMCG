using System;
using System.Collections.Generic;
using System.Data;
using Aramis.Common_classes;
using Aramis.DatabaseConnector;
using Aramis.Platform;
using AramisWpfComponents.Excel;
using AtosFMCG.TouchScreen.Controls;

namespace AtosFMCG.HelperClasses.PDT
    {
    public static class ReceiveMessages
        {
        internal static PDTCommunication Сommunication
            {
            get
                {
                return communication;
                }
            }

        private static readonly PDTCommunication communication;

        static ReceiveMessages()
            {
            communication = new PDTCommunication();
            }

        public static object[] ReceiveMessage(string procedure, object[] parameters, int userId)
            {
            switch (procedure)
                {
                case "GetCountOfDocuments":
                    return GetCountOfDocuments();
                case "GetContractorsForSelection":
                    return GetContractorsForSelection();
                case "GetTareTable":
                    return GetTareTable(parameters);
                case "GetStickerData":
                    return GetStickerData(parameters);
                case "GetAcceptanceId":
                    return GetAcceptanceId(parameters);
                case "WriteStickerFact":
                    return WriteStickerFact(parameters);
                case "ComplateAcceptance":
                    return ComplateAcceptance(parameters);
                case "ComplateInventory":
                    return ComplateInventory(parameters);
                case "GetPalletBalance":
                    return GetPalletBalance(parameters);
                case "GetNewInventoryId":
                    return GetNewInventoryId(parameters);
                case "WriteInventoryResult":
                    return new object[] { communication.WriteInventoryResult(Convert.ToInt64(parameters[0]), parameters[1] as DataTable) };

                case "GetNewMovementId":
                    return GetNewMovementId(parameters);
                case "ComplateMovement":
                    return ComplateMovement(parameters);
                case "WriteMovementResult":
                    return new object[] { communication.WriteMovementResult(Convert.ToInt64(parameters[0]), parameters[1] as DataTable) };

                case "WritePickingResult":
                    int sameWareNextTaskLineNumber;
                    var writePickingResultResult = communication.WritePickingResult(Convert.ToInt64(parameters[0]),
                        Convert.ToInt32(parameters[1]), parameters[2] as DataTable, Convert.ToInt64(parameters[3]), out sameWareNextTaskLineNumber);
                    return new object[] { writePickingResultResult, sameWareNextTaskLineNumber };

                case "GetPickingTask":
                    return GetPickingTask(parameters);

                case "GetPickingDocuments":
                    return new object[] { communication.GetPickingDocuments() };

                case "GetPDTFiles":
                    return new object[] { PlatformMethods.GetPDTFiles().ToTable() };

                case "GetPDTFileBlock":
                    return new object[] { PlatformMethods.GetPDTFileBlock(new Guid(parameters[0].ToString()), 
                    Convert.ToInt32(parameters[1]), Convert.ToInt32(parameters[2])) };

                case "PrintStickers":
                    return new object[] { communication.PrintStickers(parameters[0] as DataTable) };

                case "CreatePickingDocuments":
                    return new object[] { communication.CreatePickingDocuments() };

                case "ReadConsts":
                    DataTable constsTable;
                    communication.ReadConsts(out constsTable);
                    return new object[] { true, constsTable };

                case "GetUserName":
                    return new object[] { true, communication.GetUserName(Convert.ToInt32(parameters[0])) };

                case "GetWares":
                    return new object[] { true, communication.GetWares(parameters[0] as string) };

                case "SetBarcode":
                    bool recordWasAdded;
                    var setBarcodeResult = communication.SetBarcode(parameters[0] as string, Convert.ToInt64(parameters[1]), out recordWasAdded);
                    return new object[] { setBarcodeResult, recordWasAdded };

                case "SetPalletStatus":
                    communication.SetPalletStatus(Convert.ToInt64(parameters[0]), (bool)parameters[1]);
                    break;
                }

            return new object[0];
            }

        private static object[] GetPickingTask(object[] parameters)
            {
            long stickerId;
            long wareId;
            string wareDescription;
            long cellId;
            string cellDescription;
            long partyId;
            DateTime productionDate;
            int unitsPerBox;
            int unitsToPick;
            int lineNumber;

            if (!communication.GetPickingTask(Convert.ToInt32(parameters[0]), Convert.ToInt64(parameters[1]), Convert.ToInt64(parameters[2]), Convert.ToInt32(parameters[3]), Convert.ToInt32(parameters[4]),
                out stickerId,
                out wareId, out wareDescription,
                out cellId, out cellDescription,
                out partyId, out productionDate,
                out unitsPerBox, out unitsToPick,
                out lineNumber))
                {
                return new object[] { };
                }

            return new object[] { stickerId, wareId, wareDescription, cellId, cellDescription, partyId, productionDate.ConvertToStringDateOnly(), 
            unitsPerBox, unitsToPick, lineNumber};
            }

        private static object[] ComplateMovement(object[] parameters)
            {
            string message;
            bool result = communication.ComplateMovement(Convert.ToInt64(parameters[0]), Convert.ToBoolean(parameters[1]), out message);

            return new object[] { result, message };
            }

        private static object[] GetNewMovementId(object[] parameters)
            {
            long documentId;

            if (!communication.GetNewMovementId(Convert.ToInt64(parameters[0]), out documentId))
                {
                return new object[] { 0 };
                }

            return new object[] { documentId };
            }

        private static object[] ComplateInventory(object[] parameters)
            {
            string message;
            bool result = communication.ComplateInventory(Convert.ToInt64(parameters[0]), Convert.ToBoolean(parameters[1]), out message);

            return new object[] { result, message };
            }

        private static object[] GetNewInventoryId(object[] parameters)
            {
            long documentId;

            if (!communication.GetNewInventoryId(Convert.ToInt64(parameters[0]), out documentId))
                {
                return new object[] { 0 };
                }

            return new object[] { documentId };
            }

        private static object[] GetPalletBalance(object[] parameters)
            {
            string nomenclatureDescription;
            string trayDescription;
            long trayId;
            long linerId;
            byte linersAmount;
            int unitsPerBox;
            string cellDescription;
            long cellId;
            long previousPalletCode;
            DateTime productionDate;
            long partyId;
            long nomenclatureId;

            if (!communication.GetPalletBalance(Convert.ToInt64(parameters[0]),
                    out nomenclatureId,
                    out nomenclatureDescription, out trayId, out linerId, out linersAmount,
                    out unitsPerBox, out cellId, out cellDescription, out previousPalletCode, out  productionDate, out partyId))
                {
                return new object[] { false };
                }

            return new object[] { nomenclatureId, nomenclatureDescription, trayId, linerId, linersAmount, unitsPerBox, cellId, cellDescription, previousPalletCode, 
            productionDate.ConvertToStringDateOnly(), partyId };
            }

        private static object[] WriteStickerFact(object[] parameters)
            {
            if (!communication.WriteStickerFact(
                Convert.ToInt64(parameters[0]),
                Convert.ToInt64(parameters[1]),
                Convert.ToBoolean(parameters[2]),
                Convert.ToInt64(parameters[3]),
                Convert.ToInt64(parameters[4]),
                Convert.ToInt64(parameters[5]),
                Convert.ToInt32(parameters[6]),
                Convert.ToInt32(parameters[7]),
                Convert.ToInt32(parameters[8])))
                {
                return new object[] { };
                }

            return new object[] { true };
            }

        private static object[] GetAcceptanceId(object[] parameters)
            {
            long acceptanceId;

            if (!communication.GetAcceptanceId(Convert.ToInt64(parameters[0]), out acceptanceId))
                {
                return new object[] { };
                }

            return new object[] { acceptanceId };
            }

        #region Get
       
       



        /// <summary>
        /// К-сть документів, що чекають обробки
        /// </summary>
        /// <returns>Прийманя, Інветаризація, Відбір, Переміщення</returns>
        private static object[] GetCountOfDocuments()
            {
            string acceptanceDocCount;
            string inventoryDocCount;
            string selectionDocCount;
            string movementDocCount;
            communication.GetCountOfDocuments(out acceptanceDocCount, out inventoryDocCount,
                                              out selectionDocCount, out movementDocCount);
            return new object[]
                       {
                           acceptanceDocCount,
                           inventoryDocCount,
                           selectionDocCount,
                           movementDocCount
                       };
            }

        /// <summary>
        /// Отримати список конрагентів для відбіру продукції
        /// </summary>
        /// <returns>Таблиця контрагентів (Description, Id)</returns>
        private static object[] GetContractorsForSelection()
            {
            Query query = DB.NewQuery(@"DECLARE @Today DATETIME2=CAST(GETDATE() AS Date)
DECLARE @ShipmentPlan_Type uniqueidentifier='029B0572-E5B5-48CD-9805-1211319A5633'

SELECT RTRIM(RTRIM(c.Description)+' №'+CAST(s.Number AS VARCHAR)) Description, c.Id Id
FROM ShipmentPlan s 
JOIN Contractors c ON c.Id=s.Contractor
JOIN Movement m ON m.Source=s.Id AND m.SourceType=@ShipmentPlan_Type
WHERE s.State=0 AND s.MarkForDeleting=0 AND CAST(s.Date AS DATE)=@Today");
            DataTable table = query.SelectToTable();

            if (table != null)
                {
                return new object[] { true, table };
                }

            return new object[] { false };
            }

        #endregion

        #region Set
       



        #endregion

        #region Check
     




        private static object[] GetTareTable(object[] parameters)
            {
            DataTable table;
            if (!communication.GetTareTable(out table))
                {
                return new object[] { };
                }

            return new object[] { table };
            }


        private static object[] GetStickerData(object[] parameters)
            {
            long nomenclatureId;
            string nomenclatureDescription;
            long trayId;
            int totalUnitsQuantity;
            int unitsPerBox;
            string cellDescription;
            long cellId;
            bool currentAcceptance;

            if (!communication.GetStickerData(Convert.ToInt64(parameters[0]), Convert.ToInt64(parameters[1]),
                    out nomenclatureId, out nomenclatureDescription, out trayId, out totalUnitsQuantity, out unitsPerBox, out cellId, out cellDescription, out currentAcceptance))
                {
                return new object[] { };
                }

            return new object[] { nomenclatureId, nomenclatureDescription, trayId, totalUnitsQuantity, unitsPerBox, cellId, cellDescription, currentAcceptance };
            }

        private static object[] ComplateAcceptance(object[] parameters)
            {
            string message;
            bool result = communication.ComplateAcceptance(Convert.ToInt64(parameters[0]), Convert.ToBoolean(parameters[1]), out message);

            return new object[] { result, message };
            }

        #endregion
        }
    }