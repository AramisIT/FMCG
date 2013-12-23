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

        public static object[] ReceiveMessage(string procedure, object[] parameters)
            {
            switch (procedure)
                {
                case "GetCarsForAcceptance":
                    return GetCarsForAcceptance();
                case "GetPlaceDataFromCode":
                    return GetPlaceDataFromCode(parameters);
                case "GetPermitInstallPalletManually":
                    return GetPermitInstallPalletManually();
                case "GetDataAboutMovingPallet":
                    return GetDataAboutMovingPallet(parameters);
                case "GetAdditionalInfoAboutAccepnedGoods":
                    return GetAdditionalInfoAboutAccepnedGoods(parameters);
                case "GetDataForInventory":
                    return GetDataForInventory();
                case "GetCountOfDocuments":
                    return GetCountOfDocuments();
                case "GetContractorsForSelection":
                    return GetContractorsForSelection();
                case "GetSelectionRowInfo":
                    return GetSelectionRowInfo();
                case "SetAcceptanceData":
                    SetAcceptanceData(parameters);
                    break;
                case "SetMoving":
                    SetMoving(parameters);
                    break;
                case "SetInventory":
                    SetInventory(parameters);
                    break;
                case "SetSelectionData":
                    SetSelectionData(parameters);
                    break;
                case "CheckBarcodeForExistUser":
                    return CheckBarcodeForExistUser(parameters);
                case "CheckBarcodeForExistGoods":
                    return CheckBarcodeForExistGoods(parameters);
                case "CheckPalletBarcodeForMoving":
                    return CheckPalletBarcodeForMoving(parameters);
                case "CheckInventoryPallet":
                    return CheckInventoryPallet(parameters);
                case "CheckCellFormShipment":
                    return CheckCellFormShipment(parameters);
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

            if (!communication.GetPickingTask(Convert.ToInt64(parameters[0]), Convert.ToInt64(parameters[1]), Convert.ToInt32(parameters[2]), Convert.ToInt32(parameters[3]),
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

            if (!communication.GetPalletBalance(Convert.ToInt64(parameters[0]),
                    out nomenclatureDescription, out trayId, out linerId, out linersAmount,
                    out unitsPerBox, out cellId, out cellDescription, out previousPalletCode, out  productionDate, out partyId))
                {
                return new object[] { false };
                }

            return new object[] { nomenclatureDescription, trayId, linerId, linersAmount, unitsPerBox, cellId, cellDescription, previousPalletCode, 
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
        /// <summary>Отримати список машин для "Приймання товару"</summary>
        /// <returns>Таблиця (Id, Description)</returns>
        private static object[] GetCarsForAcceptance()
            {
            DataTable table;
            return new object[] { communication.GetCarsForAcceptance(out table), table };
            }

        /// <summary>Отримати місце розміщення зі штрихкоду</summary>
        /// <param name="parameters">Штрихкод</param>
        /// <returns>Відсканованне розміщення</returns>
        private static object[] GetPlaceDataFromCode(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            string type;
            long id;
            return new object[] { communication.GetPlaceDataFromCode(barcode, out type, out id), type, id };
            }

        /// <summary>Проверить разрешена ли установка паллет вручную</summary>
        /// <returns>Разрешена ли установка паллет вручную</returns>
        private static object[] GetPermitInstallPalletManually()
            {
            return new object[] { communication.GetPermitInstallPalletManually() };
            }

        /// <summary>Необхідні дані про паллету, що переміщується</summary>
        /// <param name="parameters">ІД паллети</param>
        /// <returns>Груз, Дата, К-сть ящиків, К-сть бутилок</returns>
        private static object[] GetDataAboutMovingPallet(IList<object> parameters)
            {
            int palletId = Convert.ToInt32(parameters[0]);
            string goods;
            string date;
            double boxCount;
            double bottleCount;

            return new object[]
                       {
                           communication.GetDataAboutMovingPallet(
                               palletId, out goods, out date, out boxCount, out bottleCount),
                           goods, date, boxCount, bottleCount
                       };
            }

        /// <summary>
        /// Дані для інвентаризації
        /// </summary>
        private static object[] GetDataForInventory()
            {
            double count;
            string nomenclature;
            string date;
            long palletId;
            string measure;
            long docId;
            long lineNumber;
            long cellId;
            string cell;

            return new object[]
                       {
                           communication.GetDataForInventory(out count, out nomenclature, out date, out palletId,
                                                             out measure, out docId, out lineNumber, out cellId,
                                                             out cell),
                           count, nomenclature, date, palletId, measure, docId, lineNumber, cellId, cell
                       };
            }

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

        /// <summary>Інформація про ПЕРШУ паллету (тут строка) для відбору</summary>
        private static object[] GetSelectionRowInfo()
            {
            long id;
            long palletId;
            string goods;
            string date;
            double boxCount;
            double unitCount;
            int baseCount;
            string cell;

            return new object[]
                       {
                           communication.GetSelectionRowInfo(
                               0, out id, out palletId, out goods, out date, out boxCount, out unitCount, out baseCount,
                               out cell),
                           id, palletId, goods, date, boxCount, unitCount, baseCount, cell
                       };
            }

        /// <summary>Отримати додаткову інформацію по прийманню товару</summary>
        private static object[] GetAdditionalInfoAboutAccepnedGoods(IList<object> parameters)
            {
            double count = Convert.ToDouble(parameters[0]);
            long goods = Convert.ToInt64(parameters[1]);
            long car = Convert.ToInt64(parameters[2]);
            long party = Convert.ToInt64(parameters[3]);

            long incomeDoc;
            string date;
            long cellId;
            string cell;
            long palett;

            return new object[]
                       {
                           communication.GetAdditionalInfoAboutAccepnedGoods(
                               count, goods, car, party, out incomeDoc, out date, out cellId, out cell, out palett),
                           incomeDoc, date, cellId, cell, palett
                       };
            }
        #endregion

        #region Set
        /// <summary>
        /// Збереження даних інвентаризації
        /// </summary>
        /// <param name="parameters">ІД документу, Номер рядка, К-сть</param>
        private static void SetInventory(IList<object> parameters)
            {
            long docId = Convert.ToInt64(parameters[0]);
            long lineNumber = Convert.ToInt64(parameters[1]);
            long count = Convert.ToInt64(parameters[2]);

            communication.SetInventory(docId, lineNumber, count);
            }

        /// <summary>Збереження даних по переміщенню паллети</summary>
        /// <param name="parameters">ІД паллети, Нова позиція, Чи являється нова позиція порожньою коміркою</param>
        private static void SetMoving(IList<object> parameters)
            {
            long palletId = Convert.ToInt64(parameters[0]);
            long newPos = Convert.ToInt64(parameters[1]);
            bool isCell = Convert.ToBoolean(parameters[2]);

            communication.SetMoving(palletId, newPos, isCell);
            }

        /// <summary>Зберегти дані по прийманню товару</summary>
        private static void SetAcceptanceData(IList<object> parameters)
            {
            long nomenclature = Convert.ToInt64(parameters[0]);
            long party = Convert.ToInt64(parameters[1]);
            double boxCount = Convert.ToDouble(parameters[2]);
            double bottleCount = Convert.ToDouble(parameters[3]);
            long planId = Convert.ToInt64(parameters[4]);
            long previousPallet = Convert.ToInt64(parameters[5]);
            long cellId = Convert.ToInt64(parameters[6]);
            bool isCell = Convert.ToBoolean(parameters[7]);

            communication.SetAcceptanceData(nomenclature, party, boxCount, bottleCount, planId, previousPallet, cellId,
                                            isCell);
            }

        /// <summary>
        /// Зберегти дані про переміщення
        /// </summary>
        /// <param name="parameters">ІД документу, ІД паллети, ІД комірки</param>
        private static void SetSelectionData(IList<object> parameters)
            {
            long docId = Convert.ToInt64(parameters[0]);
            long palletId = Convert.ToInt64(parameters[1]);
            long cellId = Convert.ToInt64(parameters[2]);

            communication.SetSelectionData(docId, palletId, cellId);
            }
        #endregion

        #region Check
        /// <summary>Перевірити чи існує користувач з таким штрих-кодом</summary>
        private static object[] CheckBarcodeForExistUser(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            return new object[] { communication.CheckBarcodeForExistUser(barcode) };
            }

        /// <summary>Перевірити чи існує товар з таким штрих-кодом</summary>
        private static object[] CheckBarcodeForExistGoods(IList<object> parameters)
            {
            long id;
            string description;
            string barcode = parameters[0].ToString();

            return new object[]
                       {
                           communication.CheckBarcodeForExistGoods(barcode, out id, out description),
                           id,
                           description
                       };
            }

        /// <summary>Перевірка штрих-коду паллети (комірки) для переміщення</summary>
        /// <param name="parameters">Штрих-код</param>
        /// <returns>result, palletId</returns>
        private static object[] CheckPalletBarcodeForMoving(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            bool cellIsAccepted = parameters.Count > 1 && Convert.ToBoolean(parameters[1]);
            long palletId;
            bool isCell;
            return new object[]
                       {
                           communication.CheckPalletBarcodeForMoving(barcode, cellIsAccepted, out palletId, out isCell),
                           palletId,
                           isCell
                       };
            }

        /// <summary>Перевірити комірку інвентаризації</summary>
        /// <param name="parameters">Штрихкод, ІД комірки</param>
        /// <returns></returns>
        private static object[] CheckInventoryPallet(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            long cellId = Convert.ToInt64(parameters[1]);
            bool result;

            return new object[] { communication.CheckInventoryPallet(barcode, cellId, out result), result };
            }

        /// <summary>Перевірка комірки для відвантаження</summary>
        /// <param name="parameters">Штрихкод</param>
        private static object[] CheckCellFormShipment(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            long id;

            return new object[] { communication.CheckCellFormShipment(barcode, out id), id };
            }

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
            string nomenclatureDescription;
            long trayId;
            int unitsPerBox;
            string cellDescription;
            long cellId;
            bool currentAcceptance;

            if (!communication.GetStickerData(Convert.ToInt64(parameters[0]), Convert.ToInt64(parameters[1]),
                    out nomenclatureDescription, out trayId, out unitsPerBox, out cellId, out cellDescription, out currentAcceptance))
                {
                return new object[] { };
                }

            return new object[] { nomenclatureDescription, trayId, unitsPerBox, cellId, cellDescription, currentAcceptance };
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