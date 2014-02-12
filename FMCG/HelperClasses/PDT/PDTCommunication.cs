using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Aramis.Core;
using Aramis.Core.WritingUtils;
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
using DevExpress.XtraBars;
using Documents;
using FMCG.DatabaseObjects.Enums;
using FMCG.HelperClasses.PDT;
using FMCG.Utils;
using pdtExternalStorage;
using Documents.GoodsAcceptance;

namespace AtosFMCG.HelperClasses.PDT
    {
    public class PDTCommunication : IRemoteCommunications
        {
        #region Get
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
                out long nomenclatureId, out string nomenclatureDescription, out long trayId,
                out int totalUnitsQuantity, out int unitsPerBox,
                out long cellId, out string cellDescription, out bool currentAcceptance)
            {
            long stickerAcceptanceId;
            RowsStates rowState;
            long rowCellId;
            GetAcceptanceId(stickerId, false, out stickerAcceptanceId, out rowState, out rowCellId);
            currentAcceptance = acceptanceId == stickerAcceptanceId;

            if (currentAcceptance)
                {
                var sticker = new Stickers() { ReadingId = stickerId };

                nomenclatureId = sticker.Nomenclature.Id;
                nomenclatureDescription = sticker.Nomenclature.Description;
                trayId = sticker.Tray.Id;
                unitsPerBox = sticker.Nomenclature.UnitsQuantityPerPack;
                totalUnitsQuantity = sticker.UnitsQuantity;

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
                nomenclatureId = 0;
                totalUnitsQuantity = 0;
                }
            return true;
            }

        private Cells findPalletCell(long stickerId)
            {
            var q = DB.NewQuery("SELECT Top 1 Cell FROM [FMCG].[dbo].[GetStockBalance] ('0001-01-01', 0,0,2,0, @StickerId)");
            q.AddInputParameter("StickerId", stickerId);

            long cellId = q.SelectToList<long>().FirstOrDefault();
            var cell = new Cells() { ReadingId = cellId };

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
                var doc = new AcceptanceOfGoods() { ReadingId = acceptanceId };
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
                var sticker = new Stickers() { ReadingId = stickerId };
                sticker.Tray = new Nomenclature() { ReadingId = trayId };
                sticker.Liner = new Nomenclature() { ReadingId = linerId };
                sticker.LinersQuantity = linersQuantity;
                sticker.Quantity = packsCount;
                sticker.UnitsQuantity = unitsCount;

                if (sticker.Write() != WritingResult.Success)
                    {
                    return false;
                    }
                }

            var acceptance = new AcceptanceOfGoods() { ReadingId = acceptanceId };

            var result = acceptance.WriteStickerFact(stickerId, cellId, trayId, linerId, linersQuantity, packsCount, unitsCount);
            return result;
            }

        public bool ComplateAcceptance(long acceptanceId, bool forceCompletion, out string errorMessage)
            {
            var acceptance = new AcceptanceOfGoods() { ReadingId = acceptanceId };
            errorMessage = string.Empty;

            acceptance.State = StatesOfDocument.Completed;
            return acceptance.Write() == WritingResult.Success;
            }

        public bool GetPalletBalance(long palletId,
            out long nomenclatureId,
            out string nomenclatureDescription,
            out long trayId, out long linerId, out byte linersAmount,
            out int unitsPerBox, out long cellId, out string cellDescription, out long previousPalletCode, out DateTime productionDate, out long partyId)
            {
            partyId = trayId = linerId = cellId = previousPalletCode = unitsPerBox = linersAmount = 0;
            productionDate = DateTime.MinValue;
            cellDescription = string.Empty;

            GoodsRows goodsRows = getPalletBalance(palletId);

            var sticker = new Stickers() { ReadingId = palletId };
            nomenclatureId = sticker.Nomenclature.Id;
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
            var inventory = new Inventory() { ReadingId = documentId };
            errorMessage = string.Empty;

            inventory.State = StatesOfDocument.Completed;
            return inventory.Write() == WritingResult.Success;
            }

        public bool ComplateMovement(long documentId, bool forceCompletion, out string errorMessage)
            {
            errorMessage = string.Empty;
            var document = new Moving() { ReadingId = documentId };
            using (var locker = new DatabaseObjectLocker(document))
                {
                if (!locker.LockForCurrentPdtThread()) return false;

                document.State = StatesOfDocument.Completed;
                return document.Write() == WritingResult.Success;
                }
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
                    var sticker = new Stickers() { ReadingId = palletCode };
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
                    var sticker = new Stickers() { ReadingId = palletCode };
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

        public bool WritePickingResult(long documentId, int currentLineNumber, DataTable resultTable, long partyId, out int sameWareNextTaskLineNumber)
            {
            sameWareNextTaskLineNumber = 0;
            var currentTime = SystemConfiguration.ServerDateTime;

            var document = new Moving() { ReadingId = documentId };

            using (var locker = new DatabaseObjectLocker(document))
                {
                if (!locker.LockForCurrentPdtThread()) return false;

                var docTable = document.NomenclatureInfo;
                var palletCode = Convert.ToInt64(resultTable.Rows[0][document.PalletCode.ColumnName]);

                var wareRow = docTable.Rows[currentLineNumber - 1];
                var newPickingTaskPlan = 0M;

                for (int rowIndex = 0; rowIndex < resultTable.Rows.Count; rowIndex++)
                    {
                    var resultWareRow = resultTable.Rows[rowIndex];

                    var plan = Convert.ToDecimal(resultWareRow[document.PlanValue.ColumnName]);
                    var fact = Convert.ToDecimal(resultWareRow[document.FactValue.ColumnName]);

                    DataRow docRow = null;
                    var isTare = rowIndex != 0;
                    if (isTare)
                        {
                        docRow = docTable.GetNewRow(document);
                        docRow[document.Nomenclature] = Convert.ToInt64(resultWareRow[document.Nomenclature.ColumnName]);
                        }
                    else
                        {
                        docRow = wareRow;
                        docRow[document.Party] = partyId;

                        newPickingTaskPlan = plan - fact;
                        }

                    docRow[document.PlanValue] = plan;
                    docRow[document.FactValue] = fact;
                    docRow[document.StartCell] = resultWareRow[document.StartCell.ColumnName];
                    docRow[document.FinalCell] = Consts.RedemptionCell.Id;
                    docRow[document.StartCodeOfPreviousPallet] =
                        resultWareRow[document.StartCodeOfPreviousPallet.ColumnName];
                    docRow[document.FinalCodeOfPreviousPallet] = 0L;
                    docRow[document.RowState] = (int)RowsStates.Completed;
                    docRow[document.RowDate] = currentTime;
                    docRow[document.PalletCode] = palletCode;

                    if (isTare)
                        {
                        docRow.AddRowToTable(document);
                        }
                    }

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

                if (newPickingTaskPlan > 0)
                    {
                    var newTaskRow = document.NomenclatureInfo.GetNewRow(document);

                    newTaskRow[document.PlanValue] = newPickingTaskPlan;
                    newTaskRow[document.Nomenclature] = wareRow[document.Nomenclature.ColumnName];
                    newTaskRow[document.RowState] = (int)RowsStates.PlannedPicking;

                    newTaskRow.AddRowToTable(document);

                    sameWareNextTaskLineNumber = Convert.ToInt32(newTaskRow[Subtable.LINE_NUMBER_COLUMN_NAME]);


                    wareRow[document.PlanValue] = wareRow[document.FactValue];
                    }

                var result = document.Write();
                return result == WritingResult.Success;
                }
            }

        public DataTable GetPickingDocuments()
            {
            var q = DB.NewQuery(@"
Select m.Id, convert(nvarchar(max), day(p.Date), 4)+'.'+convert(nvarchar(max), month(p.Date), 4) [Description] 

from Moving m
join ShipmentPlan p on p.Id = m.PickingPlan
where (m.State = @PlanState or m.State = @ProcessingState) AND m.MarkForDeleting = 0 
order by p.Date desc
");
            q.AddInputParameter("PlanState", (int)StatesOfDocument.Planned);
            q.AddInputParameter("ProcessingState", (int)StatesOfDocument.Processing);

            var result = q.SelectToTable();
            return result;
            }

        public bool GetPickingTask(int userId, long documentId, long palletId, int predefinedTaskLineNumber, int currentLineNumber,
            out long stickerId, out long wareId, out string wareDescription,
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
task.Party partyId, isnull(p.DateOfManufacture, '0001-01-01') productionDate,
n.UnitsQuantityPerPack unitsPerBox, task.PlanValue unitsToPick 

from SubMovingNomenclatureInfo task
join Nomenclature n on n.Id = task.Nomenclature
left join Cells c on c.Id = task.StartCell
left join Parties p on p.Id = task.Party
join Moving cap on cap.Id = task.IdDoc

where IdDoc = @IdDoc and (RowState = @PlannedPickingState or (RowState = @ProcessingState and Employee = @userId))
and (@currentLineNumber = 0 or task.LineNumber = @currentLineNumber)
and (@nomenclature = 0 or task.Nomenclature = @nomenclature)
order by [LineNumber]";
            var q = DB.NewQuery(sql);
            q.AddInputParameter("IdDoc", documentId);
            q.AddInputParameter("PlannedPickingState", RowsStates.PlannedPicking);
            q.AddInputParameter("ProcessingState", RowsStates.Processing);
            q.AddInputParameter("userId", userId);
            q.AddInputParameter("currentLineNumber", predefinedTaskLineNumber);

            var nomenclatureId = 0L;
            if (palletId != 0)
                {
                var sticker = new Stickers() { ReadingId = palletId };
                nomenclatureId = sticker.GetRef("Nomenclature");
                }
            q.AddInputParameter("nomenclature", nomenclatureId);

            stickerId = wareId = cellId = partyId = unitsToPick = unitsPerBox = lineNumber = 0;
            wareDescription = cellDescription = string.Empty;
            productionDate = DateTime.MinValue;

            // В дальнейшем может потребоваться выбирать строку с переданной паллетой palletId, а может и не потребоваться
            using (var locker = new DatabaseObjectLocker(typeof(Moving), documentId))
                {
                if (!locker.LockForCurrentPdtThread()) return false;

                var qResult = q.SelectRow();

                if (qResult == null)
                    {
                    return true;
                    }

                var documentState = (StatesOfDocument)Convert.ToInt32(qResult["State"]);
                if (documentState == StatesOfDocument.Planned)
                    {
                    var moving = new Moving() { ReadingId = documentId };
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

                if (currentLineNumber > 0)
                    {
                    documentId.SetRowState(typeof(Moving), currentLineNumber, RowsStates.PlannedPicking);
                    }
                documentId.SetRowState(typeof(Moving), lineNumber, RowsStates.Processing, employee: userId);
                }
            return true;
            }

        public bool PrintStickers(DataTable result)
            {
            List<Stickers> stickers = new List<Stickers>();
            foreach (DataRow row in result.Rows)
                {
                var sticker = new Stickers() { ReadingId = row[0] };
                stickers.Add(sticker);
                }

            var stickersCreator = new StickersPrintingHelper(stickers, ThermoPrinters.GetCurrentPrinterName());

            (UIConsts.MainWindow as Form).Invoke(new Action(() =>
                {
                    stickersCreator.Print();
                }));

            return true;
            }

        public bool ReadConsts(out DataTable constsTable)
            {
            constsTable = new DataTable();
            constsTable.Columns.Add("ConstName", typeof(string));
            constsTable.Columns.Add("ConstValue", typeof(string));

            constsTable.Rows.Add("WoodLinerId", Consts.NonStandartLiner.Id);

            return true;
            }

        public bool CreatePickingDocuments()
            {
            var sql = @"select s.Id from ShipmentPlan s 
left join Moving m on m.PickingPlan = s.Id
where s.MarkForDeleting = 0
group by s.Id
having Max(case when m.Id is null then 1 else m.MarkForDeleting end) = 1 
and Min(case when m.Id is null then 1 else m.MarkForDeleting end) = 1";
            var docsIds = DB.NewQuery(sql).SelectToList<Int64>();
            docsIds.ForEach(id => createMoving(id));
            return true;
            }

        private bool createMoving(long shipmentId)
            {
            var document = new Moving();
            document.SetRef("PickingPlan", shipmentId);
            document.Date = DateTime.Now;

            var q = DB.NewQuery(@"select Nomenclature, sum(Quantity) [Quantity], min(LineNumber) LineNumber
from SubShipmentPlanNomenclatureInfo
where IdDoc = @IdDoc 
group by Nomenclature
order by LineNumber");
            q.AddInputParameter("IdDoc", shipmentId);
            var docTable = document.NomenclatureInfo;
            q.Foreach(qResult =>
                {
                    var nomenclarute = Convert.ToInt64(qResult["Nomenclature"]);
                    var quantity = Convert.ToDecimal(qResult["Quantity"]);

                    var docRow = docTable.GetNewRow(document);
                    docRow[document.Nomenclature] = nomenclarute;
                    docRow[document.PlanValue] = quantity;
                    docRow[document.RowState] = RowsStates.PlannedPicking;
                    docRow.AddRowToTable(document);
                });



            var result = document.Write();

            return result == WritingResult.Success;
            }

        public string GetUserName(int userId)
            {
            var user = new Users() { ReadingId = userId };

            SystemMessage.InstanceMessage.Message = "descr = " + user.Description;

            return user.Description;
            }

        public DataTable GetWares(string barcode)
            {
            var q = DB.NewQuery(@"select n.Id, rtrim(n.Description) [Description]
from Barcodes b
join Nomenclature n on n.Id = b.Nomenclature
where b.Description = @barcode");
            q.AddInputParameter("barcode", barcode);

            var result = q.SelectToTable();
            return result;
            }

        public bool SetBarcode(string barcode, long stickerId, out bool recordWasAdded)
            {
            var sticker = new Stickers() { ReadingId = stickerId };
            var nomenclatureId = sticker.GetRef("Nomenclature");

            foreach (DataRow row in GetWares(barcode).Rows)
                {
                var barcodeAlreadyAttached = (long)row["Id"] == nomenclatureId;
                if (barcodeAlreadyAttached)
                    {
                    recordWasAdded = false;
                    return true;
                    }
                }

            var barcodeRecord = new Barcodes() { Description = barcode };
            barcodeRecord.SetRef("Nomenclature", nomenclatureId);
            recordWasAdded = barcodeRecord.Write() == WritingResult.Success;
            return recordWasAdded;
            }


        public void SetPalletStatus(long stickerId, bool fullPallet)
            {
            var sticker = new Stickers() { ReadingId = stickerId };

            if (sticker.StartUnitsQuantity > 0)
                {
                return;
                }

            if (fullPallet)
                {
                sticker.StartUnitsQuantity = sticker.UnitsQuantity;
                }
            else
                {
                if (sticker.Nomenclature.UnitsQuantityPerPallet > sticker.UnitsQuantity)
                    {
                    sticker.StartUnitsQuantity = sticker.Nomenclature.UnitsQuantityPerPallet;
                    }
                else
                    {
                    // на практике такой ситуации не должно быть. Если же есть, главное, чтобы нач. количество было больше текущего
                    sticker.StartUnitsQuantity = sticker.UnitsQuantity + 1;
                    }
                }

            sticker.Write();
            }
        }
    }