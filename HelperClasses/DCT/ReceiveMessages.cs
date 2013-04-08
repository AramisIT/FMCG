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

        private static object[] GetPlaceDataFromCode(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            Type type = BarcodeWorker.GetTypeOfData(barcode);
            long id;
            bool result = BarcodeWorker.GetIdFromBarcode(barcode, out id);

            return new object[] {result, type, id};
            }

        private static object[] GetPermitInstallPalletManually()
            {
            return new object[] {Consts.PermitInstallPalletManually};
            }

        private static object[] GetDataAboutMovingPallet(IList<object> parameters)
            {
            long palletId = Convert.ToInt64(parameters[0]);

            Query query = DB.NewQuery(@"--DECLARE @PalletId BIGINT=1
--DECLARE @BoxId BIGINT=1
--DECLARE @BottleId BIGINT=3;

WITH 
Data AS (
	SELECT 
        RTRIM(n.Description) Goods,
        CONVERT(VARCHAR(10),b.ExpariedDate,104)ExpariedDate,
        b.Quantity,
        b.MeasureUnit
	FROM StockBalance b
	JOIN Nomenclature n ON n.Id=b.Nomenclature
	WHERE b.UniqueCode=@PalletId)
	
SELECT *
FROM (
	SELECT DISTINCT
		ISNULL(box.Goods, bot.Goods)Goods,
		ISNULL(box.ExpariedDate,bot.ExpariedDate)Date,
		CASE WHEN box.MeasureUnit=@BoxId	THEN box.Quantity ELSE 0 END BoxCount,
		CASE WHEN bot.MeasureUnit=@BottleId THEN bot.Quantity ELSE 0 END BottleCount
	FROM Data box
	JOIN Data bot ON 1=1)t
WHERE t.BottleCount<>0 OR t.BoxCount<>0");
            query.AddInputParameter("PalletId", palletId);
            query.AddInputParameter("BoxId", Measures.Box.Id);
            query.AddInputParameter("BottleId", Measures.Bottle.Id);
            QueryResult result = query.SelectRow();

            return new[] { result["Goods"], result["Date"], result["BoxCount"], result["BottleCount"] };
            }

        private static object[] GetDataForInventory()
            {
            Query query = DB.NewQuery(@"SELECT TOP 1 
    i.Id DocId,
    n.LineNumber,
	n.PlanValue Count,
	RTRIM(nd.Description) Nomenclature,
	CONVERT(VARCHAR(10),p.DateOfManufacture,104) DateOfManufacture,
	n.PalletCode,
	RTRIM(m.Description) Measure,
	n.Cell CellId,
	RTRIM(c.Description) Cell
FROM Inventory i
JOIN SubInventoryNomenclatureInfo n ON n.IdDoc=i.Id
LEFT JOIN Nomenclature nd ON nd.Id=n.Nomenclature
LEFT JOIN Party p ON p.Id=n.Party
LEFT JOIN Measures m ON m.Id=n.Measure
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

        private static object[] GetContractorsForSelection()
            {
            Query query = DB.NewQuery(@"DECLARE @Today DATETIME2=CAST(GETDATE() AS Date)

SELECT RTRIM(c.Description)+' №'+CAST(s.Number AS VARCHAR) Description, c.Id Id
FROM ShipmentPlan s 
JOIN Contractors c ON c.Id=s.Contractor
JOIN SubShipmentPlanNomenclatureInfo p ON p.IdDoc=s.Id AND p.IsMove=0
WHERE s.State=0 AND s.MarkForDeleting=0 AND CAST(Date AS DATE)=@Today");
            DataTable table = query.SelectToTable();

            if(table!=null)
                {
                return new object[]{true, table};
                }

            return new object[]{false};
            }

        private static object[] GetSelectionRowInfo()
            {
            Query query = DB.NewQuery(@"--DECLARE @BoxMeasure BIGINT=1;
DECLARE @Today DATETIME2=CAST(GETDATE() AS Date);

WITH
PreparedData AS (
	SELECT 
		s.Id
		,i.Code
		,i.Nomenclature
		,i.Party
		,SUM(CASE WHEN i.Measure=@BoxMeasure THEN i.Quantity ELSE 0 END) BoxCount
		,SUM(CASE WHEN i.Measure<>@BoxMeasure THEN i.Quantity ELSE 0 END) UnitCount
		,i.Cell
	FROM ShipmentPlan s 
	JOIN SubShipmentPlanNomenclatureInfo i ON i.IdDoc=s.Id
	WHERE s.State=0 AND s.MarkForDeleting=0 AND CAST(Date AS DATE)=@Today AND i.IsMove=0
	GROUP BY s.Id,i.Code,i.Nomenclature,i.Party,i.Cell)

SELECT TOP 1 d.Id,d.Code,RTRIM(n.Description) Goods,CONVERT(VARCHAR(10),p.DateOfManufacture,104)Date,BoxCount,UnitCount,m.BaseCount,RTRIM(c.Description)Cell
FROM PreparedData d
LEFT JOIN Nomenclature n ON n.Id=d.Nomenclature
LEFT JOIN Party p ON p.Id=d.Party
LEFT JOIN Measures m ON m.Id=@BoxMeasure
LEFT JOIN Cells c ON c.Id=d.Cell");
            query.AddInputParameter("BoxMeasure", Measures.Box.Id);
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
--DECLARE @Car	BIGINT=2;

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
--WHERE p.Count>f.Count
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
                createNewSubAccepnatceRow(palletId,planId, nomenclature, boxCount, Measures.Box.Id, party, cellId);
                isCreated = true;
                }

            if (bottleCount != 0)
                {
                createNewSubAccepnatceRow(palletId, planId, nomenclature, bottleCount, Measures.Bottle.Id, party, cellId);
                isCreated = true;
                }

            if (isCreated)
                {
                PalletMover.EstablishPalletToCell(palletId, isCell ? 0 : previousPallet);
                }
            }

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

        private static void createNewSubAccepnatceRow(long palletId, long planId, long nomenclature, double boxCount, long measure, long party, long cell)
            {
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

    public enum Processes : long
        {
        Acceptance,
        Inventory,
        Selection,
        Movement
        }
    }