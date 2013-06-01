using System;
using System.Collections.Generic;
using System.Data;
using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Documents;
using Catalogs;
using pdtExternalStorage;

namespace AtosFMCG.HelperClasses.PDT
    {
    public class PDTCommunication : IRemoteCommunications
        {
        #region Get
        /// <summary>�������� ������ ����� ��� "��������� ������"</summary>
        /// <returns>������� (Id, Description)</returns>
        public bool GetCarsForAcceptance(out DataTable table)
            {
            Query query = DB.NewQuery(@"DECLARE @Today DATETIME2=CAST(GETDATE() AS DATE);

SELECT DISTINCT c.Id, RTRIM(c.Description)Description
FROM AcceptanceOfGoods a
JOIN PlannedArrival p ON p.Id=a.Source
JOIN Cars c ON c.Id=p.Car
WHERE a.MarkForDeleting=0 AND a.State=0 AND @Today=CAST(p.Date AS DATE)");
            table = query.SelectToTable();

            return table != null;
            }

        /// <summary>�������� ���� ��������� � ���������</summary>
        /// <param name="barcode">��������</param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns>�� ���� �������� ������</returns>
        public bool GetPlaceDataFromCode(string barcode, out string type, out long id)
            {
            type = BarcodeWorker.GetTypeOfData(barcode).ToString();
            bool result = BarcodeWorker.GetIdFromBarcode(barcode, out id);

            return result;
            }

        /// <summary>��������� ��������� �� ��������� ������ �������</summary>
        /// <returns>��������� �� ��������� ������ �������</returns>
        public bool GetPermitInstallPalletManually()
            {
            return Consts.PermitInstallPalletManually;
            }

        /// <summary>�������� ��� ��� �������, �� �����������</summary>
        /// <param name="palletId">�� �������</param>
        /// <param name="goods">ID �������</param>
        /// <param name="date">���� �������</param>
        /// <param name="boxCount">�-��� �����</param>
        /// <param name="bottleCount">�-��� �������</param>
        /// <returns>����, ����, �-��� �����, �-��� �������</returns>
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

        /// <summary>��� ��� ��������������</summary>
        /// <param name="count">�-���</param>
        /// <param name="nomenclature">����� �����/������������</param>
        /// <param name="date">���� �������</param>
        /// <param name="palletId">�� �������</param>
        /// <param name="measure">��.�����</param>
        /// <param name="docId">����� ���������</param>
        /// <param name="lineNumber">����� ������</param>
        /// <param name="cellId">�� ������</param>
        /// <param name="cell">������������ ������</param>
        /// <returns>�� ���� �������� ������</returns>
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
LEFT JOIN Party p ON p.Id=n.Party
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

        /// <summary>�-��� ���������, �� ������� �������</summary>
        /// <param name="acceptanceDocCount">�-��� ��������� "��������"</param>
        /// <param name="inventoryDocCount">�-��� ��������� "�������������"</param>
        /// <param name="selectionDocCount">�-��� ��������� "³���"</param>
        /// <param name="movementDocCount">�-��� ��������� "����������"</param>
        public void GetCountOfDocuments(out string acceptanceDocCount, out string inventoryDocCount,
                                        out string selectionDocCount, out string movementDocCount)
            {
            //����� ������: ���� = "�����������" + �� �������� �� ��������� + �� �������
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
                movementDocCount = result[TypesOfProcess.Movement.ToString()].ToString();
                }
            }

        /// <summary>���������� ��� ����� ������� (��� ������) ��� ������</summary>
        /// <param name="contractor">����������</param>
        /// <param name="id">ID ���������</param>
        /// <param name="palletId">�� �������</param>
        /// <param name="goods">������������ ������</param>
        /// <param name="date">���� �������</param>
        /// <param name="boxCount">�-��� �����</param>
        /// <param name="unitCount">�-��� ������� (�� �����)</param>
        /// <param name="baseCount">�-��� ������� � ������ �����</param>
        /// <param name="cell">������������ ������</param>
        /// <returns>�� ���� �������� ������</returns>
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

        /// <summary>�������� ��������� ���������� �� ��������� ������</summary>
        /// <param name="count">�-���</param>
        /// <param name="goods">Id ������</param>
        /// <param name="car">Id ��������</param>
        /// <param name="party">Id ����</param>
        /// <param name="incomeDoc">Id ��������� ���������</param>
        /// <param name="date">���� �������</param>
        /// <param name="cellId">Id ������</param>
        /// <param name="cell">������������ ������</param>
        /// <param name="palett">Id �������</param>
        /// <returns>�� ���� �������� ������</returns>
        public bool GetAdditionalInfoAboutAccepnedGoods(double count, long goods, long car, long party,
                                                        out long incomeDoc, out string date, out long cellId,
                                                        out string cell, out long palett)
            {
            DateTime dateTime = Party.GetDateOfManufactureById(party);
            date = dateTime.ToShortDateString();
            KeyValuePair<long, string> cellData;
            Cells.GetNewCellForGoods(goods, dateTime, out cellData, out palett);

            incomeDoc = getIncomeDoc(count, goods, car);
            cellId = cellData.Key;
            cell = cellData.Value;
            return true;
            }

        /// <summary>�� ��������� ����� ���������</summary>
        /// <param name="count">ʳ������ ������</param>
        /// <param name="goods">�� ������</param>
        /// <param name="car">�� ������</param>
        /// <returns>�� ��������� ����� ���������</returns>
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
	AND a.State<>4 -- 4='���������'
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

                //���� ���� ���������, �� ���������� ����� - ����� ������
                return Convert.ToInt64(table.Rows[0]["Id"]);
                }

            return 0L;
            }
        #endregion

        #region Set
        /// <summary>���������� ����� ��������������</summary>
        /// <param name="docId">Id ���������</param>
        /// <param name="lineNumber">����� ������</param>
        /// <param name="count">�-���</param>
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
--��� ���������� ���������� ��������������, ���� �� '���������'/'���������' ���������� ������ '���������'
--���� � ��� ��� ���� ����� �� ��������� ��������
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

        /// <summary>���������� ����� �� ���������� �������</summary>
        /// <param name="palletId">Id �������</param>
        /// <param name="newPos">���� ��������� �������</param>
        /// <param name="isCell">�� ��������� ���� ������� �������</param>
        public void SetMoving(long palletId, long newPos, bool isCell)
            {
            Movement.MovePallet(palletId, newPos, isCell);
            }

        /// <summary>�������� ��� �� ��������� ������</summary>
        /// <param name="nomenclature">������������</param>
        /// <param name="party">�����</param>
        /// <param name="boxCount">�-��� �����</param>
        /// <param name="bottleCount">�-��� �������</param>
        /// <param name="planId">�� ���������</param>
        /// <param name="previousPallet">�� ���������� �������</param>
        /// <param name="cellId">�� ������</param>
        /// <param name="isCell">�� ��������� ���� ���� �������</param>
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

        /// <summary>�������� ��� ��� ����������</summary>
        /// <param name="docId">Id ���������</param>
        /// <param name="palletId">Id �������</param>
        /// <param name="cellId">Id ������</param>
        public void SetSelectionData(long docId, long palletId, long cellId)
            {
            Movement.MovePallet(palletId, cellId, true);

            Query query = DB.NewQuery(
                "UPDATE SubShipmentPlanNomenclatureInfo SET IsMove=1 WHERE IdDoc=@DocId AND Code=@PalletId");
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
        private static void createNewSubAccepnatceRow(long palletId, long planId, long nomenclature, double boxCount,
                                                      long measure, long party, long cell)
            {
            //���� ���� ����� �������������..
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
        /// <summary>��������� �� ���� ���������� � ����� �����-�����</summary>
        /// <param name="barcode">��������</param>
        public bool CheckBarcodeForExistUser(string barcode)
            {
            return BarcodeWorker.CheckMatchingBarcodeAndType<Users>(barcode);
            }

        /// <summary>��������� �� ���� ����� � ����� �����-�����</summary>
        /// <param name="barcode">��������</param>
        /// <param name="id">Id ������</param>
        /// <param name="description">������������ ������</param>
        /// <returns>�� ���� �����</returns>
        public bool CheckBarcodeForExistGoods(string barcode, out long id, out string description)
            {
            return BarcodeWorker.GetCutDataByBarcode<Nomenclature>(barcode, out id, out description);
            }

        /// <summary>�������� �����-���� ������� (������) ��� ����������</summary>
        /// <param name="barcode">�����-���</param>
        /// <param name="cellIsAccepted"></param>
        /// <param name="palletId">Id �������</param>
        /// <param name="isCell">�� ��������� ���� ���� �������</param>
        /// <returns>�� ������� �������� ���� ���������</returns>
        public bool CheckPalletBarcodeForMoving(string barcode, bool cellIsAccepted, out long palletId, out bool isCell)
            {
            return BarcodeWorker.CheckPalletBarcodeForMoving(barcode, cellIsAccepted, out palletId, out isCell);
            }        
        
        /// <summary>��������� ������ ��������������</summary>
        /// <param name="barcode">�����-���</param>
        /// <param name="cellId">ID ������</param>
        /// <param name="result">�� ���������� �������</param>
        /// <returns>�� ������� ��������</returns>
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

        /// <summary>�������� ������ ��� ������������</summary>
        /// <param name="barcode">�����-���</param>
        /// <param name="id">�� ������</param>
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
        }
    }