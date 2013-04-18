using System;
using System.Collections.Generic;
using System.Data;
using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Documents;
using Catalogs;

namespace AtosFMCG.HelperClasses.DCT
    {
    public static class ReceiveMessages
        {
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
                }

            return new object[0];
            }

        #region Get
        /// <summary>Отримати список машин для "Приймання товару"</summary>
        /// <returns>Таблиця (Id, Description)</returns>
        private static object[] GetCarsForAcceptance()
            {
            Query query = DB.NewQuery(@"DECLARE @Today DATETIME2=CAST(GETDATE() AS DATE);

SELECT DISTINCT c.Id, RTRIM(c.Description)Description
FROM AcceptanceOfGoods a
JOIN PlannedArrival p ON p.Id=a.Source
JOIN Cars c ON c.Id=p.Car
WHERE a.MarkForDeleting=0 AND a.State=0 AND @Today=CAST(p.Date AS DATE)");
            DataTable table = query.SelectToTable();

            return new object[] { table };
            }

        /// <summary>Отримати місце розміщення зі штрихкоду</summary>
        /// <param name="parameters">Штрихкод</param>
        /// <returns>Відсканованне розміщення</returns>
        private static object[] GetPlaceDataFromCode(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            Type type = BarcodeWorker.GetTypeOfData(barcode);
            long id;
            bool result = BarcodeWorker.GetIdFromBarcode(barcode, out id);

            return new object[] {result, type, id};
            }

        /// <summary>Проверить разрешена ли установка паллет вручную</summary>
        /// <returns>Разрешена ли установка паллет вручную</returns>
        private static object[] GetPermitInstallPalletManually()
            {
            return new object[] {Consts.PermitInstallPalletManually};
            }

        /// <summary>Необхідні дані про паллету, що переміщується</summary>
        /// <param name="parameters">ІД паллети</param>
        /// <returns>Груз, Дата, К-сть ящиків, К-сть бутилок</returns>
        private static object[] GetDataAboutMovingPallet(IList<object> parameters)
            {
            long palletId = Convert.ToInt64(parameters[0]);

            Query query = DB.NewQuery(@"
--DECLARE @PalletId BIGINT=1
--DECLARE @BoxId BIGINT=1;

WITH 
Data AS (
	SELECT 
		b.Nomenclature,
        b.ExpariedDate,
        SUM(CASE WHEN b.MeasureUnit=@BoxId THEN b.Quantity ELSE 0 END) BoxCount,
        SUM(CASE WHEN b.MeasureUnit<>@BoxId THEN b.Quantity ELSE 0 END) BottleCount
	FROM StockBalance b
	WHERE b.UniqueCode=@PalletId
	GROUP BY b.Nomenclature,b.ExpariedDate)

SELECT RTRIM(n.Description) Goods,CONVERT(VARCHAR(10),d.ExpariedDate,104)Date,BoxCount,BottleCount
FROM Data d
JOIN Nomenclature n ON n.Id=d.Nomenclature
WHERE d.BottleCount<>0 OR d.BoxCount<>0");
            query.AddInputParameter("PalletId", palletId);
            query.AddInputParameter("BoxId", Measures.GetBoxForPallet(palletId));
            QueryResult result = query.SelectRow();

            return new[] { result["Goods"], result["Date"], result["BoxCount"], result["BottleCount"] };
            }

        /// <summary>
        /// Дані для інвентаризації
        /// </summary>
        private static object[] GetDataForInventory()
            {
            Query query = DB.NewQuery(@"SELECT TOP 1 
    i.Id DocId,
    n.LineNumber,
	n.PlanValue Count,
	RTRIM(nd.Description) Nomenclature,
	CONVERT(VARCHAR(10),p.DateOfManufacture,104) DateOfManufacture,
	n.PalletCode,
	RTRIM(u.CutName) Measure,
	n.Cell CellId,
	RTRIM(c.Description) Cell
FROM Inventory i
JOIN SubInventoryNomenclatureInfo n ON n.IdDoc=i.Id
LEFT JOIN Nomenclature nd ON nd.Id=n.Nomenclature
LEFT JOIN Party p ON p.Id=n.Party
LEFT JOIN Measures m ON m.Id=n.Measure
LEFT JOIN ClassifierUnits u ON u.Id=m.Classifier
LEFT JOIN Cells c ON c.Id=n.Cell
WHERE i.State=0 AND i.MarkForDeleting=0 AND n.FactValue=0 AND CAST(i.Date AS DATE)=CAST(GetDate() AS DATE)");
            QueryResult result = query.SelectRow();

            if(result!=null)
                {
                return new[]
                           {
                               true,
                               result["Count"], 
                               result["Nomenclature"],
                               result["DateOfManufacture"],
                               result["PalletCode"], 
                               result["Measure"], 
                               result["DocId"], 
                               result["LineNumber"],
                               result["CellId"], 
                               result["Cell"]
                           };
                }

            return new object[]{false};
            }

        /// <summary>
        /// К-сть документів, що чекають обробки
        /// </summary>
        /// <returns>Прийманя, Інветаризація, Відбір, Переміщення</returns>
        private static object[] GetCountOfDocuments()
            {
            //Умова відбору: Стан = "Заплановано" + Не помічений на видалення + На сьогодні
            Query query = DB.NewQuery(@"DECLARE @Today DATETIME2=CAST(GETDATE() AS DATE);

WITH
Data AS (
	SELECT 'Acceptance' Type,COUNT(1) Count
	FROM PlannedArrival p
	LEFT JOIN AcceptanceOfGoods a ON a.Source=p.Id
	WHERE a.State<3 AND p.MarkForDeleting=0 AND @Today=CAST(p.Date AS DATE)
	
	UNION ALL
	
	SELECT 'Inventory' Type,COUNT(1) Count
	FROM Inventory
	WHERE State=0 AND MarkForDeleting=0 AND @Today=CAST(Date AS DATE)
	
	UNION ALL
	
	SELECT 'Selection' Type,COUNT(1) Count
	FROM ShipmentPlan
	WHERE State=0 AND MarkForDeleting=0 AND @Today=CAST(Date AS DATE)
	
	UNION ALL
	
	SELECT 'Movement' Type,COUNT(1) Count
	FROM Movement
	WHERE State=0 AND MarkForDeleting=0 AND @Today=CAST(Date AS DATE))


SELECT * 
FROM Data d
PIVOT (MAX(Count) for Type in([Acceptance],[Inventory],[Selection],[Movement])) as pivoted");
            QueryResult result = query.SelectRow();

            return new[]
                       {
                           result[Processes.Acceptance.ToString()],
                           result[Processes.Inventory.ToString()],
                           result[Processes.Selection.ToString()],
                           result[Processes.Movement.ToString()]
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

            if(table!=null)
                {
                return new object[]{true, table};
                }

            return new object[]{false};
            }

        /// <summary>Інформація про ПЕРШУ паллету (тут строка) для відбору</summary>
        /// <returns>true,Code,Goods,Date,BoxCount,UnitCount,BaseCount,Cell </returns>
        private static object[] GetSelectionRowInfo()
            {
            Query query = DB.NewQuery(@"--DECLARE @Box BIGINT=1;

DECLARE @Today DATETIME2=CAST(GETDATE() AS Date)
DECLARE @ShipmentPlan_Type uniqueidentifier='029B0572-E5B5-48CD-9805-1211319A5633';

WITH
PreparedData AS (
	SELECT 
		s.Id
		,n.NomenclatureCode Code
		,n.Nomenclature
		,n.NomenclatureParty Party
		,SUM(CASE WHEN Measures.Classifier=@Box THEN n.NomenclatureCount ELSE 0 END) BoxCount
		,SUM(CASE WHEN Measures.Classifier<>@Box THEN n.NomenclatureCount ELSE 0 END) UnitCount
		,n.SourceCell Cell
	FROM ShipmentPlan s 
	JOIN Movement m ON m.Source=s.Id AND m.SourceType=@ShipmentPlan_Type
	JOIN SubMovementNomenclatureInfo n ON n.IdDoc=m.Id
	JOIN Measures ON Measures.Id=n.NomenclatureMeasure
	WHERE s.State=0 AND s.MarkForDeleting=0 AND CAST(s.Date AS DATE)=@Today AND n.RowState=0
	GROUP BY s.Id,n.NomenclatureCode,n.Nomenclature,n.NomenclatureParty,n.SourceCell)
,MainData AS(
	SELECT d.Id,d.Code,RTRIM(n.Description) Goods,CONVERT(VARCHAR(10),p.DateOfManufacture,104)Date,BoxCount,UnitCount,m.BaseCount,RTRIM(c.Description)Cell
	FROM PreparedData d
	LEFT JOIN Nomenclature n ON n.Id=d.Nomenclature
	LEFT JOIN Party p ON p.Id=d.Party
	LEFT JOIN Measures m ON m.Nomenclature=n.Id AND m.Classifier=@Box
	LEFT JOIN Cells c ON c.Id=d.Cell)
,OrderDate AS (
	SELECT Code,ROW_NUMBER() OVER (ORDER BY Date DESC)RowNumber
	FROM(
		SELECT d.Code,MAX(m.WritingDate)Date
		FROM MainData d
		JOIN GoodsMoving m ON m.UniqueCode=d.Code
		GROUP BY d.Code)t)

SELECT TOP 1 m.*
FROM MainData m
JOIN OrderDate o ON o.Code=m.Code
ORDER BY o.RowNumber");
            query.AddInputParameter("Box", ClassifierUnits.Box.Id);
            QueryResult result = query.SelectRow();

            if (result != null)
                {
                return new[]
                           {
                               true,
                               result["Id"],
                               result["Code"],
                               result["Goods"],
                               result["Date"],
                               result["BoxCount"],
                               result["UnitCount"],
                               result["BaseCount"],
                               result["Cell"]
                           };
                }

            return new object[] {false};
            }

        #region GetAdditionalInfoAboutAccepnedGoods
        /// <summary>Отримати додаткову інформацію по прийманню товару</summary>
        private static object[] GetAdditionalInfoAboutAccepnedGoods(IList<object> parameters)
            {
            double count = Convert.ToDouble(parameters[0]);
            long goods = Convert.ToInt64(parameters[1]);
            long car = Convert.ToInt64(parameters[2]);
            long party = Convert.ToInt64(parameters[3]);
            long incomeDoc = getIncomeDoc(count, goods, car);
            long palett;
            DateTime date = Party.GetDateOfManufactureById(party);
            KeyValuePair<long, string> cell;
            Cells.GetNewCellForGoods(goods, date, out cell, out palett);

            return new object[] {incomeDoc, date, cell.Key, cell.Value, palett};
            }

        /// <summary>ІД документу плану приймання</summary>
        /// <param name="count">Кількість товару</param>
        /// <param name="goods">ІД товару</param>
        /// <param name="car">ІД машини</param>
        /// <returns>ІД документу плану приймання</returns>
        private static long getIncomeDoc(double count, long goods, long car)
            {
            Query query = DB.NewQuery(@"--DECLARE @Goods	BIGINT=1
--DECLARE @Car	BIGINT=2
DECLARE @Date	DATETIME2=CAST(GETDATE() AS DATE);

WITH
PlanData AS (
	SELECT a.Id,SUM(n.NomenclatureCount) Count
	FROM AcceptanceOfGoods a
	JOIN PlannedArrival p ON p.Id=a.Source
	JOIN SubPlannedArrivalNomenclatureInfo n ON n.IdDoc=p.Id
	WHERE 
		a.MarkForDeleting=0 AND 
		a.State=0 AND 
		p.Car=@Car AND
		n.Nomenclature=@Goods
	GROUP BY a.Id)
,FactData AS(
	SELECT a.Id,SUM(n.NomenclatureCount) Count
	FROM AcceptanceOfGoods a
	JOIN PlannedArrival p ON p.Id=a.Source
	JOIN SubAcceptanceOfGoodsNomenclatureInfo n ON n.IdDoc=a.Id
	WHERE
		a.MarkForDeleting=0 AND
		a.State=0 AND
		p.Car=@Car AND
		n.Nomenclature=@Goods
	GROUP BY a.Id)

SELECT 
	a.Id,
	ISNULL(p.Count,0) PlanCount,
	ISNULL(f.Count,0) FactCount
FROM AcceptanceOfGoods a 
LEFT JOIN PlanData p ON p.Id=a.Id
LEFT JOIN FactData f ON f.Id=a.Id
JOIN PlannedArrival pa ON pa.Id=a.Source
WHERE 
	a.MarkForDeleting=0
	AND a.State<>4 -- 4='Завершено'
	AND CAST(pa.Date AS DATE)=@Date
");
            query.AddInputParameter("Goods", goods);
            query.AddInputParameter("Car", car);
            DataTable table = query.SelectToTable();

            if (table != null && table.Rows.Count > 0)
                {
                foreach (DataRow row in table.Rows)
                    {
                    double plan = Convert.ToDouble(row["PlanCount"]);
                    double fact = Convert.ToDouble(row["FactCount"]);

                    if (plan >= fact + count)
                        {
                        return Convert.ToInt64(row["Id"]);
                        }
                    }

                //Якщо нема документу, що задовільняє умови - взяти перший
                return Convert.ToInt64(table.Rows[0]["Id"]);
                }

            return 0L;
            } 
        #endregion
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

            Query updRowFactData = DB.NewQuery("UPDATE SubInventoryNomenclatureInfo SET FactValue=@Count WHERE IdDoc=@DocId AND LineNumber=@LineNumber");
            updRowFactData.AddInputParameter("DocId",docId );
            updRowFactData.AddInputParameter("LineNumber", lineNumber);
            updRowFactData.AddInputParameter("Count", count);
            updRowFactData.Execute();

            Query updFinishedDocs = DB.NewQuery(@"
--Всім сьогоднішнім документам Інвентаризації, котрі не 'Завершено'/'Скасовано' встановити статус 'Завершено'
--якщо в них вже нема строк які необхідно обробити
DECLARE @Today DATETIME2=CAST(GETDATE() AS DATE);

WITH
Documents AS (
	SELECT i.Id, COUNT(s.IdDoc) Total,SUM(CASE WHEN s.FactValue=0 THEN 0 ELSE 1 END) Finished
	FROM Inventory i
	LEFT JOIN SubInventoryNomenclatureInfo s ON S.IdDoc=i.Id
	WHERE i.State<3 AND CAST(i.Date AS DATE)=@Today
	GROUP BY i.Id)

UPDATE Inventory
SET State=4
FROM Inventory i
JOIN (SELECT Id FROM Documents WHERE Total=Finished)d ON i.Id=d.Id");
            updFinishedDocs.Execute();
            }

        /// <summary>
        /// Збереження даних по переміщенню паллети
        /// </summary>
        /// <param name="parameters">ІД паллети, Нова позиція, Чи являється нова позиція порожньою коміркою</param>
        private static void SetMoving(IList<object> parameters)
            {
            long palletId = Convert.ToInt64(parameters[0]);
            long newPos = Convert.ToInt64(parameters[1]);
            bool isCell = Convert.ToBoolean(parameters[2]);

            Movement.MovePallet(palletId, newPos, isCell);
            }

        /// <summary>Зберегти дані по прийманню товару</summary>
        private static void SetAcceptanceData(IList<object> parameters)
            {
            long nomenclature = Convert.ToInt64(parameters[0]);
            long party = Convert.ToInt64(parameters[1]);
            double boxCount = Convert.ToDouble(parameters[4]);
            double bottleCount = Convert.ToDouble(parameters[5]);
            long planId = Convert.ToInt64(parameters[6]);
            long previousPallet = Convert.ToInt64(parameters[7]);
            long cellId = Convert.ToInt64(parameters[8]);
            bool isCell = Convert.ToBoolean(parameters[9]);
            bool isCreated = false;
            long palletId = AcceptanceOfGoods.GetNewCode();

            if (boxCount != 0)
                {
                long box = Measures.GetBoxForNomenclature(nomenclature);
                createNewSubAccepnatceRow(palletId, planId, nomenclature, boxCount, box, party, cellId);
                isCreated = true;
                }

            if (bottleCount != 0)
                {
                long bottle = Measures.GetMeasureForNomenclature(nomenclature, ClassifierUnits.Bottle.Id);
                createNewSubAccepnatceRow(palletId, planId, nomenclature, bottleCount, bottle, party, cellId);
                isCreated = true;
                }

            if (isCreated)
                {
                PalletMover.EstablishPalletToCell(palletId, isCell ? 0 : previousPallet);
                }
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

            Movement.MovePallet(palletId, cellId, true);

            Query query =
                DB.NewQuery("UPDATE SubShipmentPlanNomenclatureInfo SET IsMove=1 WHERE IdDoc=@DocId AND Code=@PalletId");
            query.AddInputParameter("DocId", docId);
            query.AddInputParameter("PalletId", palletId);
            query.Execute();
            }

        /// <summary></summary>
        /// <param name="palletId"></param>
        /// <param name="planId"></param>
        /// <param name="nomenclature"></param>
        /// <param name="boxCount"></param>
        /// <param name="measure"></param>
        /// <param name="party"></param>
        /// <param name="cell"></param>
        private static void createNewSubAccepnatceRow(long palletId, long planId, long nomenclature, double boxCount, long measure, long party, long cell)
            {
            //поки нема сенсу перероблювати..
            Query query = DB.NewQuery("EXEC AddRowIntoAcceptanceOfGoods @Code,@PlanId,@Nomenclature,@Cell,@Count,@Measure,@Party");
            query.AddInputParameter("Code", palletId);
            query.AddInputParameter("PlanId", planId);
            query.AddInputParameter("Nomenclature", nomenclature);
            query.AddInputParameter("Cell", cell);
            query.AddInputParameter("Count", boxCount);
            query.AddInputParameter("Measure", measure);
            query.AddInputParameter("Party", party);
            query.Execute();
            }
        #endregion

        #region Check
        /// <summary>Перевірити чи існує користувач з таким штрих-кодом</summary>
        private static object[] CheckBarcodeForExistUser(IList<object> parameters)
            {
            return new object[] { BarcodeWorker.CheckMatchingBarcodeAndType<Users>(parameters[0]) };
            }

        /// <summary>Перевірити чи існує товар з таким штрих-кодом</summary>
        private static object[] CheckBarcodeForExistGoods(IList<object> parameters)
            {
            long id;
            string description;
            string barcode = parameters[0].ToString();

            return BarcodeWorker.GetCutDataByBarcode<Nomenclature>(barcode, out id, out description)
                       ? new object[] {true, id, description}
                       : new object[] {false};
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
            bool result = BarcodeWorker.CheckPalletBarcodeForMoving(barcode, cellIsAccepted, out palletId, out isCell);

            return new object[] {result, palletId, isCell};
            }

        /// <summary>Перевірити комірку інвентаризації</summary>
        /// <param name="parameters">Штрихкод, ІД комірки</param>
        /// <returns></returns>
        private static object[] CheckInventoryPallet(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            long cellId = Convert.ToInt64(parameters[1]);

            if(BarcodeWorker.GetTypeOfData(barcode) == typeof(long))
                {
                long palletId;
                BarcodeWorker.GetIdFromBarcode(barcode, out palletId);
                Query query = DB.NewQuery(@"
--DECLARE @PalletId BIGINT=5
--DECLARE @Cell BIGINT=1;

WITH 
PalletInCell AS (
	SELECT f.PalletCode,f.PreviousCode
	FROM FilledCell f
	JOIN StockBalance b ON b.UniqueCode=f.PalletCode
	WHERE b.Cell=@Cell)
,LastPalletInCell AS (
	SELECT DISTINCT p2.PalletCode
	FROM PalletInCell p1
	FULL JOIN PalletInCell p2 ON p1.PreviousCode=p2.PalletCode
	WHERE p1.PalletCode IS NULL)

SELECT CASE WHEN PalletCode=@PalletId THEN 1 ELSE 0 END Valid
FROM LastPalletInCell ");
                query.AddInputParameter("Cell", cellId);
                query.AddInputParameter("PalletId", palletId);
                object resultObj = query.SelectScalar();

                return new object[] {true, resultObj != null && Convert.ToBoolean(resultObj)};
                }

            return new object[]{false};
            }

        /// <summary>Перевірка комірки для відвантаження</summary>
        /// <param name="parameters">Штрихкод</param>
        private static object[] CheckCellFormShipment(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();

            if(BarcodeWorker.GetTypeOfData(barcode) == typeof(Cells))
                {
                long id;
                return new object[] {BarcodeWorker.GetIdFromBarcode(barcode, out id), id};
                } 
            return new object[] {  false};
            }
        #endregion
        }
    }