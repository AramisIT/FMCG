using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Platform;
using Aramis.SystemConfigurations;
using Aramis.UI.WinFormsDevXpress;
using AramisWpfComponents.Excel;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Documents;
using AtosFMCG.Enums;
using AtosFMCG.TouchScreen.PalletSticker;
using Catalogs;
using Documents;
using FMCG.DatabaseObjects.Enums;
using FMCG.HelperClasses.PDT;
using pdtExternalStorage;
using Documents.GoodsAcceptance;

namespace AtosFMCG.HelperClasses.PDT
    {
    public class PDTCommunication : IRemoteCommunications
        {
        #region Get
        /// <summary>Отримати список машин для "Приймання товару"</summary>
        /// <returns>Таблиця (Id, Description)</returns>
        public bool GetCarsForAcceptance(out DataTable table)
            {
            Query query = DB.NewQuery(@"DECLARE @Today DATETIME2=CAST(GETDATE() AS DATE);

SELECT DISTINCT c.Id, RTRIM(c.Description)Description
FROM AcceptanceOfGoods a
JOIN AcceptancePlan p ON p.Id=a.Source
JOIN Cars c ON c.Id=p.Car
WHERE a.MarkForDeleting=0 AND a.State=0 AND @Today=CAST(p.Date AS DATE)");
            table = query.SelectToTable();

            return table != null;
            }

        /// <summary>Отримати місце розміщення зі штрихкоду</summary>
        /// <param name="barcode">Штрихкод</param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns>Чи була операція успішна</returns>
        public bool GetPlaceDataFromCode(string barcode, out string type, out long id)
            {
            type = BarcodeWorker.GetTypeOfData(barcode).ToString();
            bool result = BarcodeWorker.GetIdFromBarcode(barcode, out id);

            return result;
            }

        /// <summary>Проверить разрешена ли установка паллет вручную</summary>
        /// <returns>Разрешена ли установка паллет вручную</returns>
        public bool GetPermitInstallPalletManually()
            {
            return Consts.PermitInstallPalletManually;
            }

        /// <summary>Необхідні дані про паллету, що переміщується</summary>
        /// <param name="palletId">ІД паллети</param>
        /// <param name="goods">ID вантажу</param>
        /// <param name="date">Дата вантажу</param>
        /// <param name="boxCount">К-сть ящиків</param>
        /// <param name="bottleCount">К-сть бутилок</param>
        /// <returns>Груз, Дата, К-сть ящиків, К-сть бутилок</returns>
        public bool GetDataAboutMovingPallet(int palletId, out string goods, out string date, out double boxCount,
                                             out double bottleCount)
            {
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

            if (result == null)
                {
                goods = string.Empty;
                date = string.Empty;
                boxCount = 0;
                bottleCount = 0;
                return false;
                }

            goods = result["Goods"].ToString();
            date = Convert.ToDateTime(result["Date"]).ToShortDateString();
            boxCount = Convert.ToDouble(result["BoxCount"]);
            bottleCount = Convert.ToDouble(result["BottleCount"]);
            return true;
            }

        /// <summary>Дані для інвентаризації</summary>
        /// <param name="count">К-сть</param>
        /// <param name="nomenclature">Назва грузу/номенклатури</param>
        /// <param name="date">Дата паллети</param>
        /// <param name="palletId">ІД паллети</param>
        /// <param name="measure">Од.виміру</param>
        /// <param name="docId">Номер документу</param>
        /// <param name="lineNumber">Номер строки</param>
        /// <param name="cellId">ІД комірки</param>
        /// <param name="cell">Найменування комірки</param>
        /// <returns>Чи була операція успішна</returns>
        public bool GetDataForInventory(out double count, out string nomenclature, out string date, out long palletId,
                                        out string measure, out long docId, out long lineNumber, out long cellId,
                                        out string cell)
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
LEFT JOIN Parties p ON p.Id=n.Party
LEFT JOIN Measures m ON m.Id=n.Measure
LEFT JOIN ClassifierUnits u ON u.Id=m.Classifier
LEFT JOIN Cells c ON c.Id=n.Cell
WHERE i.State=0 AND i.MarkForDeleting=0 AND n.FactValue=0 AND CAST(i.Date AS DATE)=CAST(GetDate() AS DATE)");
            QueryResult result = query.SelectRow();

            if (result != null)
                {
                count = Convert.ToDouble(result["Count"]);
                nomenclature = result["Nomenclature"].ToString();
                date = result["DateOfManufacture"].ToString();
                palletId = Convert.ToInt64(result["PalletCode"]);
                measure = result["Measure"].ToString();
                docId = Convert.ToInt64(result["DocId"]);
                lineNumber = Convert.ToInt64(result["LineNumber"]);
                cellId = Convert.ToInt64(result["CellId"]);
                cell = result["Cell"].ToString();

                return true;
                }

            count = 0;
            nomenclature = string.Empty;
            date = string.Empty;
            palletId = 0;
            measure = string.Empty;
            docId = 0;
            lineNumber = 0;
            cellId = 0;
            cell = string.Empty;

            return false;
            }

        /// <summary>К-сть документів, що чекають обробки</summary>
        /// <param name="acceptanceDocCount">К-сть документів "Прийманя"</param>
        /// <param name="inventoryDocCount">К-сть документів "Інветаризація"</param>
        /// <param name="selectionDocCount">К-сть документів "Відбір"</param>
        /// <param name="movementDocCount">К-сть документів "Переміщення"</param>
        public bool GetCountOfDocuments(out string acceptanceDocCount, out string inventoryDocCount,
                                        out string selectionDocCount, out string movementDocCount)
            {
            //Умова відбору: Стан = "Заплановано" + Не помічений на видалення + На сьогодні
            Query query = DB.NewQuery(@"DECLARE @Today DATETIME2=CAST(GETDATE() AS DATE);

WITH
Data AS (
	SELECT 'Acceptance' Type, COUNT(1) Count
	FROM AcceptanceOfGoods a
	WHERE a.State<=1 AND a.MarkForDeleting=0 
	
	UNION ALL
	
	SELECT 'Inventory' Type,COUNT(1) Count
	FROM Inventory
	WHERE State=0 AND MarkForDeleting=0 AND @Today=CAST(Date AS DATE)
	
	UNION ALL
	
	SELECT 'Selection' Type,COUNT(1) Count
	FROM ShipmentPlan
	WHERE State=0 AND MarkForDeleting=0 AND @Today=CAST(Date AS DATE)
	
	UNION ALL
	
	SELECT 'Picking' Type,COUNT(1) Count
	FROM Moving
	WHERE (State = @PlanState or State = @ProcessingState) AND MarkForDeleting=0 AND PickingPlan<>0)

SELECT * 
FROM Data d
PIVOT (MAX(Count) for Type in([Acceptance],[Inventory],[Selection],[Picking])) as pivoted");
            query.AddInputParameter("PlanState", (int)StatesOfDocument.Planned);
            query.AddInputParameter("ProcessingState", (int)StatesOfDocument.Processing);

            QueryResult result = query.SelectRow();

            if (result == null)
                {
                string emptyValue = 0.ToString();
                acceptanceDocCount = emptyValue;
                inventoryDocCount = emptyValue;
                selectionDocCount = emptyValue;
                movementDocCount = emptyValue;
                }
            else
                {
                acceptanceDocCount = result[TypesOfProcess.Acceptance.ToString()].ToString();
                inventoryDocCount = result[TypesOfProcess.Inventory.ToString()].ToString();
                selectionDocCount = result[TypesOfProcess.Selection.ToString()].ToString();
                movementDocCount = result[TypesOfProcess.Picking.ToString()].ToString();
                }

            return true;
            }

        /// <summary>Інформація про ПЕРШУ паллету (тут строка) для відбору</summary>
        /// <param name="contractor">Контрагент</param>
        /// <param name="id">ID документа</param>
        /// <param name="palletId">ІД паллети</param>
        /// <param name="goods">Найменування товару</param>
        /// <param name="date">Дата паллети</param>
        /// <param name="boxCount">К-сть ящиків</param>
        /// <param name="unitCount">К-сть одиниць (не ящики)</param>
        /// <param name="baseCount">К-сть одиниць в одному ящику</param>
        /// <param name="cell">Найменування комірки</param>
        /// <returns>Чи була операція успішна</returns>
        public bool GetSelectionRowInfo(long contractor, out long id, out long palletId, out string goods,
                                        out string date, out double boxCount, out double unitCount, out int baseCount,
                                        out string cell)
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
	LEFT JOIN Parties p ON p.Id=d.Party
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
                id = Convert.ToInt64(result["Id"]);
                palletId = Convert.ToInt64(result["Code"]);
                goods = result["Goods"].ToString();
                date = result["Date"].ToString();
                boxCount = Convert.ToDouble(result["BoxCount"]);
                unitCount = Convert.ToDouble(result["UnitCount"]);
                baseCount = Convert.ToInt32(result["BaseCount"]);
                cell = result["Cell"].ToString();

                return true;
                }

            id = 0;
            palletId = 0;
            goods = string.Empty;
            date = string.Empty;
            boxCount = 0;
            unitCount = 0;
            baseCount = 0;
            cell = string.Empty;

            return false;
            }

        /// <summary>Отримати додаткову інформацію по прийманню товару</summary>
        /// <param name="count">К-сть</param>
        /// <param name="goods">Id товару</param>
        /// <param name="car">Id машинини</param>
        /// <param name="party">Id партії</param>
        /// <param name="incomeDoc">Id документу приймання</param>
        /// <param name="date">Дата паллети</param>
        /// <param name="cellId">Id комірки</param>
        /// <param name="cell">Найменування комірки</param>
        /// <param name="palett">Id паллети</param>
        /// <returns>Чи була операція успішна</returns>
        public bool GetAdditionalInfoAboutAccepnedGoods(double count, long goods, long car, long party,
                                                        out long incomeDoc, out string date, out long cellId,
                                                        out string cell, out long palett)
            {
            DateTime dateTime = Parties.GetDateOfManufactureById(party);
            date = dateTime.ToShortDateString();
            KeyValuePair<long, string> cellData;
            //Cells.GetNewCellForGoods(goods, dateTime, out cellData, out palett);

            incomeDoc = getIncomeDoc(count, goods, car);
            palett = 0;
            cellId = 0;//cellData.Key;
            cell = "";//cellData.Value;
            return true;
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
	JOIN AcceptancePlan p ON p.Id=a.Source
	JOIN SubAcceptancePlanNomenclatureInfo n ON n.IdDoc=p.Id
	WHERE 
		a.MarkForDeleting=0 AND 
		a.State=0 AND 
		p.Car=@Car AND
		n.Nomenclature=@Goods
	GROUP BY a.Id)
,FactData AS(
	SELECT a.Id,SUM(n.NomenclatureCount) Count
	FROM AcceptanceOfGoods a
	JOIN AcceptancePlan p ON p.Id=a.Source
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
JOIN AcceptancePlan pa ON pa.Id=a.Source
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

        #region Set
        /// <summary>Збереження даних інвентаризації</summary>
        /// <param name="docId">Id документу</param>
        /// <param name="lineNumber">Номер строки</param>
        /// <param name="count">К-сть</param>
        public void SetInventory(long docId, long lineNumber, long count)
            {
            Query updRowFactData =
                DB.NewQuery(
                    "UPDATE SubInventoryNomenclatureInfo SET FactValue=@Count WHERE IdDoc=@DocId AND LineNumber=@LineNumber");
            updRowFactData.AddInputParameter("DocId", docId);
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

        /// <summary>Збереження даних по переміщенню паллети</summary>
        /// <param name="palletId">Id паллети</param>
        /// <param name="newPos">Нове розміщення паллети</param>
        /// <param name="isCell">Чи являється нова позиція коміркою</param>
        public void SetMoving(long palletId, long newPos, bool isCell)
            {

            }

        /// <summary>Зберегти дані по прийманню товару</summary>
        /// <param name="nomenclature">Номенклатура</param>
        /// <param name="party">Партія</param>
        /// <param name="boxCount">К-сть ящиків</param>
        /// <param name="bottleCount">К-сть бутилок</param>
        /// <param name="planId">ІД документу</param>
        /// <param name="previousPallet">ІД попередньої паллети</param>
        /// <param name="cellId">ІД комірки</param>
        /// <param name="isCell">Чи являється нове місце коміркою</param>
        public void SetAcceptanceData(long nomenclature, long party, double boxCount, double bottleCount, long planId,
                                      long previousPallet, long cellId, bool isCell)
            {
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

        /// <summary>Зберегти дані про переміщення</summary>
        /// <param name="docId">Id документу</param>
        /// <param name="palletId">Id паллети</param>
        /// <param name="cellId">Id комірки</param>
        public void SetSelectionData(long docId, long palletId, long cellId)
            {

            }

        /// <summary></summary>
        /// <param name="palletId"></param>
        /// <param name="planId"></param>
        /// <param name="nomenclature"></param>
        /// <param name="boxCount"></param>
        /// <param name="measure"></param>
        /// <param name="party"></param>
        /// <param name="cell"></param>
        private static void createNewSubAccepnatceRow(long palletId, long planId, long nomenclature, double boxCount,
                                                      long measure, long party, long cell)
            {
            //поки нема сенсу перероблювати..
            Query query =
                DB.NewQuery("EXEC AddRowIntoAcceptanceOfGoods @Code,@PlanId,@Nomenclature,@Cell,@Count,@Measure,@Party");
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
        /// <param name="barcode">Штрихкод</param>
        public bool CheckBarcodeForExistUser(string barcode)
            {
            return BarcodeWorker.CheckMatchingBarcodeAndType<Users>(barcode);
            }

        /// <summary>Перевірити чи існує товар з таким штрих-кодом</summary>
        /// <param name="barcode">Штрихкод</param>
        /// <param name="id">Id товару</param>
        /// <param name="description">Найменування товару</param>
        /// <returns>Чи існує товар</returns>
        public bool CheckBarcodeForExistGoods(string barcode, out long id, out string description)
            {
            return BarcodeWorker.GetCutDataByBarcode<Nomenclature>(barcode, out id, out description);
            }

        /// <summary>Перевірка штрих-коду паллети (комірки) для переміщення</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="cellIsAccepted"></param>
        /// <param name="palletId">Id паллети</param>
        /// <param name="isCell">Чи являється нове місце коміркою</param>
        /// <returns>Чи пройшло перевірку нове розміщення</returns>
        public bool CheckPalletBarcodeForMoving(string barcode, bool cellIsAccepted, out long palletId, out bool isCell)
            {
            return BarcodeWorker.CheckPalletBarcodeForMoving(barcode, cellIsAccepted, out palletId, out isCell);
            }

        /// <summary>Перевірити комірку інвентаризації</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="cellId">ID комірки</param>
        /// <param name="result">Чи співпадають паллети</param>
        /// <returns>Чи пройшла перевірка</returns>
        public bool CheckInventoryPallet(string barcode, long cellId, out bool result)
            {
            if (BarcodeWorker.GetTypeOfData(barcode) == typeof(long))
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

                result = resultObj != null && Convert.ToBoolean(resultObj);
                return true;
                }

            result = false;
            return false;
            }

        /// <summary>Перевірка комірки для відвантаження</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="id">ІД комірки</param>
        public bool CheckCellFormShipment(string barcode, out long id)
            {
            if (BarcodeWorker.GetTypeOfData(barcode) == typeof(Cells))
                {
                return BarcodeWorker.GetIdFromBarcode(barcode, out id);
                }

            id = 0;
            return false;
            }
        #endregion


        public bool GetDataAboutMovingPallet(int palletId, out string goods, out DateTime date, out double boxCount, out double bottleCount)
            {
            throw new NotImplementedException();
            }

        public bool GetTareTable(out DataTable tareTable)
            {
            tareTable = new DataTable();
            tareTable.Columns.AddRange(new DataColumn[]{new DataColumn("Description", typeof(string)),
                new DataColumn("Id", typeof(long)),
                new DataColumn("TareType", typeof(int)) });

            tareTable.Rows.Add(Consts.StandartTray.Description, Consts.StandartTray.Id, 1);
            tareTable.Rows.Add(Consts.NonStandartTray.Description, Consts.NonStandartTray.Id, 1);

            tareTable.Rows.Add(Consts.StandartLiner.Description, Consts.StandartLiner.Id, 2);
            tareTable.Rows.Add(Consts.NonStandartLiner.Description, Consts.NonStandartLiner.Id, 2);

            return true;
            }


        public bool GetStickerData(long acceptanceId, long stickerId,
                out string nomenclatureDescription, out long trayId,
                out int unitsPerBox, out long cellId, out string cellDescription, out bool currentAcceptance)
            {
            long stickerAcceptanceId;
            RowsStates rowState;
            long rowCellId;
            GetAcceptanceId(stickerId, false, out stickerAcceptanceId, out rowState, out rowCellId);
            currentAcceptance = acceptanceId == stickerAcceptanceId;

            if (currentAcceptance)
                {
                var sticker = new Stickers();
                sticker.Read(stickerId);

                nomenclatureDescription = sticker.Nomenclature.Description;
                trayId = sticker.Tray.Id;
                unitsPerBox = sticker.Nomenclature.UnitsQuantityPerPack;

                cellId = rowCellId;
                cellDescription = FastInput.GetCashedData(typeof(Cells).Name).GetDescription(rowCellId);
                }
            else
                {
                Cells palletCell = findPalletCell(stickerId);
                cellId = palletCell.Id;
                cellDescription = palletCell.Description;

                nomenclatureDescription = string.Empty;
                trayId = 0;
                unitsPerBox = 0;
                }
            return true;
            }

        private Cells findPalletCell(long stickerId)
            {
            var q = DB.NewQuery("SELECT Top 1 Cell FROM [FMCG].[dbo].[GetStockBalance] ('0001-01-01', 0,0,2,0, @StickerId)");
            q.AddInputParameter("StickerId", stickerId);

            long cellId = q.SelectToList<long>().FirstOrDefault();
            var cell = new Cells();
            cell.Read(cellId);

            return cell;
            }

        public bool GetAcceptanceId(long stickerId, out long acceptanceId)
            {
            RowsStates rowState;
            long cellId;
            return GetAcceptanceId(stickerId, true, out acceptanceId, out rowState, out cellId);
            }

        public bool GetAcceptanceId(long stickerId, bool setProcessingStatus, out long acceptanceId, out RowsStates rowState, out long cellId)
            {
            var q = DB.NewQuery(@"select top 1 info.IdDoc, info.NomenclatureState [State], info.NomenclatureCell

from SubAcceptanceOfGoodsNomenclatureInfo info
join AcceptanceOfGoods a on a.Id = info.IdDoc

where a.MarkForDeleting = 0 and NomenclatureCode = @StickerCode");

            q.AddInputParameter("StickerCode", stickerId);
            var resultObj = q.SelectRow();

            acceptanceId = 0;
            rowState = RowsStates.PlannedAcceptance;
            cellId = 0;

            if (resultObj != null)
                {
                acceptanceId = Convert.ToInt64(resultObj[0]);
                rowState = (RowsStates)Convert.ToInt32(resultObj[1]);
                cellId = Convert.ToInt64(resultObj[2]);
                }

            bool documentFound = acceptanceId > 0;

            if (documentFound && setProcessingStatus)
                {
                var doc = new AcceptanceOfGoods().Read(acceptanceId) as AcceptanceOfGoods;
                doc.State = StatesOfDocument.Processing;
                doc.Write();
                }

            return documentFound;
            }

        public bool WriteStickerFact(long acceptanceId, long stickerId, bool palletChanged, long cellId, long trayId, long linerId,
            int linersQuantity, int packsCount, int unitsCount)
            {
            if (palletChanged)
                {
                var sticker = new Stickers();
                sticker.Read(stickerId);
                sticker.Tray = new Nomenclature().Read(trayId) as Nomenclature;
                sticker.Liner = new Nomenclature().Read(linerId) as Nomenclature;
                sticker.LinersQuantity = linersQuantity;
                sticker.Quantity = packsCount;
                sticker.UnitsQuantity = unitsCount;

                if (sticker.Write() != WritingResult.Success)
                    {
                    return false;
                    }
                }

            var acceptance = new AcceptanceOfGoods();
            acceptance.Read(acceptanceId);

            var result = acceptance.WriteStickerFact(stickerId, cellId, trayId, linerId, linersQuantity, packsCount, unitsCount);
            return result;
            }

        public bool ComplateAcceptance(long acceptanceId, bool forceCompletion, out string errorMessage)
            {
            var acceptance = new AcceptanceOfGoods();
            acceptance.Read(acceptanceId);
            errorMessage = string.Empty;

            acceptance.State = StatesOfDocument.Completed;
            return acceptance.Write() == WritingResult.Success;
            }

        public bool GetPalletBalance(long palletId, out string nomenclatureDescription, out long trayId, out long linerId, out byte linersAmount,
            out int unitsPerBox, out long cellId, out string cellDescription, out long previousPalletCode, out DateTime productionDate, out long partyId)
            {
            partyId = trayId = linerId = cellId = previousPalletCode = unitsPerBox = linersAmount = 0;
            productionDate = DateTime.MinValue;
            cellDescription = string.Empty;

            GoodsRows goodsRows = getPalletBalance(palletId);

            var sticker = new Stickers();
            sticker.Read(palletId);
            nomenclatureDescription = sticker.Nomenclature.Description;
            unitsPerBox = sticker.Nomenclature.UnitsQuantityPerPack;

            if (goodsRows.TrayRow != null)
                {
                trayId = Convert.ToInt64(goodsRows.TrayRow[GoodsRows.NOMENCLATURE]);
                }

            if (goodsRows.LinerRow != null)
                {
                linerId = Convert.ToInt64(goodsRows.LinerRow[GoodsRows.NOMENCLATURE]);
                linersAmount = Convert.ToByte(goodsRows.LinerRow[GoodsRows.QUANTITY]);
                }

            if (goodsRows.WareRow != null)
                {
                cellId = Convert.ToInt64(goodsRows.WareRow[GoodsRows.CELL]);
                cellDescription = goodsRows.WareRow["CellDescription"].ToString();
                var productionDateObj = goodsRows.WareRow["ProductionDate"];
                productionDate = productionDateObj.IsNull() ? DateTime.MinValue : (DateTime)productionDateObj;
                partyId = Convert.ToInt64(goodsRows.WareRow["Party"]);
                }

            var q = DB.NewQuery("select top 1 relation.PreviousPallet from dbo.GetPalletsRelations ('0001-01-01', @palletId, 0) relation");
            q.AddInputParameter("palletId", palletId);
            previousPalletCode = q.SelectInt64();

            return true;
            }

        private GoodsRows getPalletBalance(long palletId)
            {
            const string sql = @"
with tareTypes as (
select top 2 cast(Value as bigint) [Id], 2 wareType 
from [SystemConsts] where [description] = 'StandartTray' or [description] = 'NonStandartTray' 

union all 

select top 2 cast(Value as bigint) [Id], 3 wareType 
from [SystemConsts] where [description] = 'StandartLiner' or [description] = 'NonStandartLiner'
)

select Stock.Nomenclature, Stock.Cell NomenclatureCell, Stock.Party, 
par.DateOfManufacture [ProductionDate], Stock.Quantity NomenclatureFact,
    isnull(RTRIM(c.[Description]),'') [CellDescription],
ISNULL(tareTypes.wareType, case when Stock.Party = 0 then 1 else 0 end) NomenclatureType

 FROM dbo.GetStockBalance (
  '0001-01-01',
  0,
  0,
  @ComplateType,
  0,
  @PalletId) Stock
  
    left join tareTypes on tareTypes.Id = Stock.Nomenclature and Stock.Party = 0
    left join Cells c on c.Id = Stock.Cell
    left join Parties par on par.Id = Stock.Party
    
";
            var q = DB.NewQuery(sql);
            q.AddInputParameter("ComplateType", (int)RowsStates.Completed);
            q.AddInputParameter("PalletId", palletId);
            var table = q.SelectToTable();

            var result = new GoodsRows();
            foreach (DataRow row in table.Rows)
                {
                var tareType = (GoodsRows.NomenclatureTypes)row["NomenclatureType"];
                result.SetRow(row, tareType);
                }

            return result;
            }

        public bool GetNewInventoryId(long userId, out long documentId)
            {
            var doc = new Inventory();
            doc.SetRef("Responsible", userId);
            doc.Date = SystemConfiguration.ServerDateTime;
            var writeResult = doc.Write();

            documentId = doc.Id;
            return writeResult == WritingResult.Success;
            }

        public bool ComplateInventory(long documentId, bool forceCompletion, out string errorMessage)
            {
            var inventory = new Inventory();
            inventory.Read(documentId);
            errorMessage = string.Empty;

            inventory.State = StatesOfDocument.Completed;
            return inventory.Write() == WritingResult.Success;
            }

        public bool ComplateMovement(long documentId, bool forceCompletion, out string errorMessage)
            {
            var document = new Moving();
            document.Read(documentId);
            errorMessage = string.Empty;

            document.State = StatesOfDocument.Completed;
            return document.Write() == WritingResult.Success;
            }

        public bool GetNewMovementId(long userId, out long documentId)
            {
            var doc = new Moving();
            doc.SetRef("Responsible", userId);
            doc.Date = SystemConfiguration.ServerDateTime;
            var writeResult = doc.Write();

            documentId = doc.Id;
            return writeResult == WritingResult.Success;
            }

        public bool WriteInventoryResult(long documentId, DataTable resultTable)
            {
            var q = DB.NewQuery("select max(LineNumber) lastRowNumber from subInventoryNomenclatureInfo where IdDoc = @IdDoc");
            q.AddInputParameter("IdDoc", documentId);
            var lastLineNumber = q.SelectInt64();

            var inventory = new Inventory();
            var currentTime = SystemConfiguration.ServerDateTime;

            // Эта строка должна быть в этом месте, т.к. после получения таблицы можно обращаться к столбцам, они уже не null
            var docTable = inventory.NomenclatureInfo;

            var palletCode = Convert.ToInt64(resultTable.Rows[0][inventory.PalletCode.ColumnName]);

            new BoxesFinder(resultTable, palletCode);

            for (int rowIndex = 0; rowIndex < resultTable.Rows.Count; rowIndex++)
                {
                var sourceRow = resultTable.Rows[rowIndex];
                var newRow = docTable.GetNewRow(inventory);
                newRow[inventory.RowState] = (int)RowsStates.Completed;
                newRow[inventory.RowDate] = currentTime;

                var isTare = rowIndex != 0;
                if (!isTare)
                    {
                    var sticker = (Stickers)new Stickers().Read(palletCode);
                    newRow[inventory.Party] = sticker.GetRef("Party");
                    }

                newRow[inventory.Nomenclature] = Convert.ToInt64(sourceRow[inventory.Nomenclature.ColumnName]);
                newRow[inventory.PlanValue] = Convert.ToDecimal(sourceRow[inventory.PlanValue.ColumnName]);
                newRow[inventory.FactValue] = Convert.ToDecimal(sourceRow[inventory.FactValue.ColumnName]);

                newRow[inventory.PalletCode] = palletCode;

                newRow[inventory.StartCodeOfPreviousPallet] = Convert.ToInt64(sourceRow[inventory.StartCodeOfPreviousPallet.ColumnName]);
                newRow[inventory.FinalCodeOfPreviousPallet] = Convert.ToInt64(sourceRow[inventory.FinalCodeOfPreviousPallet.ColumnName]);

                newRow[inventory.StartCell] = Convert.ToInt64(sourceRow[inventory.StartCell.ColumnName]);
                newRow[inventory.FinalCell] = Convert.ToInt64(sourceRow[inventory.FinalCell.ColumnName]);

                newRow[Subtable.LINE_NUMBER_COLUMN_NAME] = lastLineNumber + rowIndex + 1;
                newRow.AddRowToTable(inventory);
                }

            var rowsToInsert = inventory.NomenclatureInfo;

            q = DB.NewQuery(@"insert into subInventoryNomenclatureInfo([IdDoc],[LineNumber],[PalletCode],[Nomenclature],
[PlanValue],[FactValue],[RowState],[RowDate],
[StartCodeOfPreviousPallet] ,[FinalCodeOfPreviousPallet],
[StartCell],[FinalCell], [Party])
 
select @IdDoc, [LineNumber],[PalletCode],[Nomenclature],
[PlanValue],[FactValue],[RowState],[RowDate],
[StartCodeOfPreviousPallet] ,[FinalCodeOfPreviousPallet],
[StartCell],[FinalCell], [Party]
from @table");

            q.AddInputTVPParameter("table", rowsToInsert, "dbo.tvp_Inventory_NomenclatureInfo");
            q.AddInputParameter("IdDoc", documentId);

            q.Execute();

            return q.ThrowedException == null;
            }

        public bool WriteMovementResult(long documentId, DataTable resultTable)
            {
            var q = DB.NewQuery("select max(LineNumber) lastRowNumber from SubMovingNomenclatureInfo where IdDoc = @IdDoc");
            q.AddInputParameter("IdDoc", documentId);
            var lastLineNumber = q.SelectInt64();

            var document = new Moving();
            var currentTime = SystemConfiguration.ServerDateTime;

            // Эта строка должна быть в этом месте, т.к. после получения таблицы можно обращаться к столбцам, они уже не null
            var docTable = document.NomenclatureInfo;

            var palletCode = Convert.ToInt64(resultTable.Rows[0][document.PalletCode.ColumnName]);

            new BoxesFinder(resultTable, palletCode);


            for (int rowIndex = 0; rowIndex < resultTable.Rows.Count; rowIndex++)
                {
                var sourceRow = resultTable.Rows[rowIndex];
                var newRow = docTable.GetNewRow(document);
                newRow[document.RowState] = (int)RowsStates.Completed;
                newRow[document.RowDate] = currentTime;

                var isTare = rowIndex != 0;
                if (!isTare)
                    {
                    var sticker = (Stickers)new Stickers().Read(palletCode);
                    newRow[document.Party] = sticker.GetRef("Party");
                    }

                newRow[document.Nomenclature] = Convert.ToInt64(sourceRow[document.Nomenclature.ColumnName]);
                newRow[document.PlanValue] = Convert.ToDecimal(sourceRow[document.PlanValue.ColumnName]);
                newRow[document.FactValue] = Convert.ToDecimal(sourceRow[document.FactValue.ColumnName]);

                newRow[document.PalletCode] = palletCode;

                newRow[document.StartCodeOfPreviousPallet] = Convert.ToInt64(sourceRow[document.StartCodeOfPreviousPallet.ColumnName]);
                newRow[document.FinalCodeOfPreviousPallet] = Convert.ToInt64(sourceRow[document.FinalCodeOfPreviousPallet.ColumnName]);

                newRow[document.StartCell] = Convert.ToInt64(sourceRow[document.StartCell.ColumnName]);
                newRow[document.FinalCell] = Convert.ToInt64(sourceRow[document.FinalCell.ColumnName]);

                newRow[Subtable.LINE_NUMBER_COLUMN_NAME] = lastLineNumber + rowIndex + 1;
                newRow.AddRowToTable(document);
                }

            var rowsToInsert = document.NomenclatureInfo;

            q = DB.NewQuery(@"insert into subMovingNomenclatureInfo([IdDoc],[LineNumber],[PalletCode],[Nomenclature],
[PlanValue],[FactValue],[RowState],[RowDate],
[StartCodeOfPreviousPallet] ,[FinalCodeOfPreviousPallet],
[StartCell],[FinalCell], [Party])
 
select @IdDoc, [LineNumber],[PalletCode],[Nomenclature],
[PlanValue],[FactValue],[RowState],[RowDate],
[StartCodeOfPreviousPallet] ,[FinalCodeOfPreviousPallet],
[StartCell],[FinalCell], [Party]
from @table");

            q.AddInputTVPParameter("table", rowsToInsert, "dbo.tvp_Moving_NomenclatureInfo");
            q.AddInputParameter("IdDoc", documentId);

            q.Execute();

            return q.ThrowedException == null;
            }

        public bool WritePickingResult(long documentId, int currentLineNumber, DataTable resultTable, long partyId)
            {
            var document = new Moving();
            document.Read(documentId);

            var currentTime = SystemConfiguration.ServerDateTime;

            var resultWareRow = resultTable.Rows[0];
            var wareRow = document.NomenclatureInfo.Rows[currentLineNumber - 1];
            wareRow[document.Party] = partyId;
            wareRow[document.FactValue] = resultWareRow[document.FactValue.ColumnName];
            wareRow[document.StartCell] = resultWareRow[document.StartCell.ColumnName];
            wareRow[document.FinalCell] = Consts.RedemptionCell.Id;
            wareRow[document.StartCodeOfPreviousPallet] = resultWareRow[document.StartCodeOfPreviousPallet.ColumnName];
            wareRow[document.FinalCodeOfPreviousPallet] = 0L;
            wareRow[document.RowState] = (int)RowsStates.Completed;
            wareRow[document.RowDate] = currentTime;


            var palletCode = Convert.ToInt64(resultTable.Rows[0][document.PalletCode.ColumnName]);

            wareRow[document.PalletCode] = palletCode;
            var boxesFinder = new BoxesFinder(resultTable, palletCode);

            if (boxesFinder.BoxesRowAdded)
                {
                var boxesRow = document.NomenclatureInfo.GetNewRow(document);

                boxesRow[document.Nomenclature] = boxesFinder.BoxesRow[document.Nomenclature.ColumnName];

                boxesRow[document.StartCodeOfPreviousPallet] = 0L;
                boxesRow[document.FinalCodeOfPreviousPallet] = 0L;
                boxesRow[document.RowState] = (int)RowsStates.Completed;
                boxesRow[document.RowDate] = currentTime;

                boxesRow[document.FactValue] = boxesFinder.BoxesRow[document.FactValue.ColumnName];

                boxesRow[document.FinalCell] = wareRow[document.FinalCell];
                boxesRow[document.StartCell] = wareRow[document.StartCell];

                boxesRow[document.PalletCode] = palletCode;

                boxesRow.AddRowToTable(document);
                }


            var result = document.Write();
            return result == WritingResult.Success;
            }

        public DataTable GetPickingDocuments()
            {
            var q = DB.NewQuery(@"
Select m.Id, (rtrim(c.[Description]) + ' № ' 
+ CAST(p.number as nvarchar(max))
) [Description] 

from Moving m
join ShipmentPlan p on p.Id = m.PickingPlan
left join Contractors c on c.Id = p.Contractor
where (m.State = @PlanState or m.State = @ProcessingState) AND m.MarkForDeleting = 0 
");
            q.AddInputParameter("PlanState", (int)StatesOfDocument.Planned);
            q.AddInputParameter("ProcessingState", (int)StatesOfDocument.Processing);

            var result = q.SelectToTable();
            return result;
            }

        public bool GetPickingTask(long documentId, out long stickerId, out long wareId, out string wareDescription,
            out long cellId, out string cellDescription,
            out long partyId, out DateTime productionDate,
            out int unitsPerBox, out int unitsToPick,
            out int lineNumber)
            {
            const string sql =
            @"
select top 1 cap.[State],
task.PalletCode stickerId, task.LineNumber,
task.Nomenclature wareId, rtrim(n.[description]) wareDescription,
task.StartCell cellId, ISNULL(rtrim(c.[Description]), '') cellDescription,
task.Party partyId, p.DateOfManufacture productionDate,
n.UnitsQuantityPerPack unitsPerBox, task.PlanValue unitsToPick 

from SubMovingNomenclatureInfo task
join Nomenclature n on n.Id = task.Nomenclature
left join Cells c on c.Id = task.StartCell
join Parties p on p.Id = task.Party
join Moving cap on cap.Id = task.IdDoc

where IdDoc = @IdDoc and RowState = @RowState
order by [LineNumber]";
            var q = DB.NewQuery(sql);
            q.AddInputParameter("IdDoc", documentId);
            q.AddInputParameter("RowState", RowsStates.PlannedPicking);
            var qResult = q.SelectRow();

            if (qResult == null)
                {
                stickerId = wareId = cellId = partyId = unitsToPick = unitsPerBox = lineNumber = 0;
                wareDescription = cellDescription = string.Empty;
                productionDate = DateTime.MinValue;
                return true;
                }

            var documentState = (StatesOfDocument)Convert.ToInt32(qResult["State"]);
            if (documentState == StatesOfDocument.Planned)
                {
                var moving = (Moving)new Moving().Read(documentId);
                moving.State = StatesOfDocument.Processing;
                moving.Write();
                }

            stickerId = Convert.ToInt64(qResult["stickerId"]);
            wareId = Convert.ToInt64(qResult["wareId"]);
            wareDescription = qResult["wareDescription"].ToString();
            cellId = Convert.ToInt64(qResult["cellId"]);
            cellDescription = qResult["cellDescription"].ToString();
            partyId = Convert.ToInt64(qResult["partyId"]);
            productionDate = (DateTime)qResult["productionDate"];
            unitsPerBox = Convert.ToInt32(qResult["unitsPerBox"]);
            unitsToPick = Convert.ToInt32(qResult["unitsToPick"]);
            lineNumber = Convert.ToInt32(qResult["LineNumber"]);
            return true;
            }

        public bool PrintStickers(DataTable result)
            {
            List<Stickers> stickers = new List<Stickers>();
            foreach (DataRow row in result.Rows)
                {
                var sticker = new Stickers();
                sticker.Read(Convert.ToInt64(row[0]));
                stickers.Add(sticker);
                }

            var stickersCreator = new StickersPrintingHelper(stickers, ThermoPrinters.GetCurrentPrinterName());

            (UIConsts.MainWindow as Form).Invoke(new Action(() =>
                {
                    stickersCreator.Print();
                }));

            return true;
            }
        }
    }