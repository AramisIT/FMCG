using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
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
using DevExpress.XtraEditors;
using Documents;
using FMCG.DatabaseObjects.Enums;
using FMCG.HelperClasses.PDT;
using FMCG.Utils;
using FMCG.Utils.Printing;
using pdtExternalStorage;
using Documents.GoodsAcceptance;

namespace AtosFMCG.HelperClasses.PDT
    {
    public class PDTCommunication : IRemoteCommunications
        {
        private ConcurrentDictionary<string, long> userIdsDictionary = new ConcurrentDictionary<string, long>();

        public void SetUserId(int userId)
            {
            var threadName = System.Threading.Thread.CurrentThread.Name;
            userIdsDictionary.AddOrUpdate(threadName, userId, (key, value) => userId);
            }

        private long getUserId()
            {
            var threadName = System.Threading.Thread.CurrentThread.Name;
            long userId;
            if (userIdsDictionary.TryGetValue(threadName, out userId))
                {
                return userId;
                }

            return 0;
            }

        /// <summary>Получить кол-во документов доступных для обработки</summary>
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
                out long cellId, out string cellDescription, out bool currentAcceptance,
                out int wareBarcodesCount)
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

                var q = DB.NewQuery("select count(*) from Barcodes where MarkForDeleting = 0 and Nomenclature = @Ware");
                q.AddInputParameter("Ware", nomenclatureId);

                wareBarcodesCount = q.SelectInt32();
                if (wareBarcodesCount == 0)
                    {
                    var isKeg = !sticker.Nomenclature.BoxType.Empty
                                && sticker.Nomenclature.UnitsQuantityPerPallet > 0
                                && sticker.Nomenclature.UnitsQuantityPerPack == 1
                                && sticker.Nomenclature.ShelfLife > 0;

                    if (isKeg)
                        {
                        // у кеги не может быть штрих-кода
                        wareBarcodesCount = -1;
                        }
                    }
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
                wareBarcodesCount = 0;
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

        public bool WriteStickerFact(long acceptanceId, long stickerId, bool palletChanged, long cellId, long previousStickerId, long trayId, long linerId,
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

            var result = acceptance.WriteStickerFact(stickerId, cellId, previousStickerId, trayId, linerId, linersQuantity, packsCount, unitsCount);
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
            out int unitsPerBox, out long cellId, out string cellDescription, out long previousPalletCode, out DateTime productionDate, out long partyId,
            out int totalUnitsQuantity)
            {
            partyId = trayId = linerId = cellId = previousPalletCode = totalUnitsQuantity = unitsPerBox = linersAmount = 0;
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
                totalUnitsQuantity = Convert.ToInt32(goodsRows.WareRow[GoodsRows.QUANTITY]);
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

        public bool GetNewInventoryId(out long documentId)
            {
            var doc = new Inventory();
            doc.SetRef("Responsible", getUserId());
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

        public bool GetNewMovementId(out long documentId)
            {
            var doc = new Moving();
            doc.SetRef("Responsible", getUserId());
            doc.Date = SystemConfiguration.ServerDateTime;
            doc.SetRef("Responsible", getUserId());
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

            var userId = getUserId();
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
                newRow[document.Employee] = userId;
                newRow[Subtable.LINE_NUMBER_COLUMN_NAME] = lastLineNumber + rowIndex + 1;
                newRow.AddRowToTable(document);
                }

            var rowsToInsert = document.NomenclatureInfo;

            q = DB.NewQuery(@"insert into subMovingNomenclatureInfo([IdDoc],[LineNumber],[PalletCode],[Nomenclature],
[PlanValue],[FactValue],[RowState],[RowDate],
[StartCodeOfPreviousPallet] ,[FinalCodeOfPreviousPallet],
[StartCell],[FinalCell], [Party], [Employee])
 
select @IdDoc, [LineNumber],[PalletCode],[Nomenclature],
[PlanValue],[FactValue],[RowState],[RowDate],
[StartCodeOfPreviousPallet] ,[FinalCodeOfPreviousPallet],
[StartCell],[FinalCell], [Party], [Employee]
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
Select m.Id, convert(nvarchar(max), day(p.Date), 4)+'.'+convert(nvarchar(max), month(p.Date), 4) + ' ' + convert(nvarchar(max),datepart(hour, p.Date)) + ':' + 
convert(nvarchar(max),datepart(MINUTE, p.Date)) [Description] 

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

        public bool GetPickingTask(long documentId, long palletId, int predefinedTaskLineNumber, int currentLineNumber,
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
            q.AddInputParameter("userId", getUserId());
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
                documentId.SetRowState(typeof(Moving), lineNumber, RowsStates.Processing, employee: getUserId());
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

            printStickers(stickers);

            return true;
            }

        private static void printStickers(List<Stickers> stickers)
            {
            var stickersCreator = new StickersPrintingHelper(stickers);

            (UIConsts.MainWindow as Form).Invoke(new Action(() => { stickersCreator.Print(); }));
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

        public DataTable GetParties(long wareId, SelectionFilters selectionFilter)
            {
            switch (selectionFilter)
                {
                case SelectionFilters.RecentlyShipped:
                    var q = DB.NewQuery(string.Format(@"
declare @boundDate date = DATEADD (day , -{0}, cast(GETDATE() as date));

select distinct p.Id, p.TheDeadlineSuitability ExpirationDate from Moving

join dbo.SubMovingNomenclatureInfo n on n.IdDoc = Moving.Id
join Parties p on p.Id = n.Party

where Moving.PickingPlan > 0 and Moving.Date >= @boundDate 
and n.Nomenclature = @Nomenclature and Moving.MarkForDeleting = 0 

order by p.TheDeadlineSuitability", RECENTLY_SHIPPED_DAYS_AMOUNT));
                    q.AddInputParameter("Nomenclature", wareId);

                    var result = q.SelectToTable();
                    var expirationColumn = new DataColumn("Expiration", typeof(string));
                    result.Columns.Add(expirationColumn);

                    var dataTimeColumn = result.Columns["ExpirationDate"];
                    foreach (DataRow row in result.Rows)
                        {
                        var expirationDate = (DateTime)row[dataTimeColumn];
                        row[expirationColumn] = expirationDate.ConvertToStringDateOnly();
                        }
                    result.Columns.Remove(dataTimeColumn);

                    return result;

                default:
                    return new DataTable();
                }
            }

        public DataTable GetWaresInKegs(SelectionFilters selectionFilter)
            {
            var q = DB.NewQuery(@"
select n.Id, rtrim(n.Description) [Description]
from Nomenclature n 
where BoxType > 0 
and UnitsQuantityPerPallet > 0
and UnitsQuantityPerPack = 1 
and IsTare = 0 
and ShelfLife > 0 
and MarkForDeleting = 0 
");

            var result = q.SelectToTable();

            switch (selectionFilter)
                {
                case SelectionFilters.All:
                    return result;

                case SelectionFilters.RecentlyShipped:
                    return filterRecentlyShippedWares(result);
                }
            return result;
            }

        public long CreateNewSticker(long wareId, DateTime expirationDate, int unitsQuantity, int boxesCount, long linerId, int linersCount)
            {
            var party = Parties.FindByExpirationDate(wareId, expirationDate);

            if (party.IsNull() || party.Empty)
                {
                return 0;
                }

            var nomenclature = new Nomenclature() { ReadingId = wareId };
            var sticker = new Stickers()
                {
                    Nomenclature = nomenclature,
                    Party = party,
                    UnitsQuantity = unitsQuantity,
                    Quantity = boxesCount,
                    ReleaseDate = party.DateOfManufacture,
                    ExpiryDate = party.TheDeadlineSuitability,
                    AcceptionDate = DateTime.Now,
                    Liner = new Nomenclature() { ReadingId = linerId },
                    LinersQuantity = linersCount
                };

            // подразумевается, что паллета начатая
            sticker.StartUnitsQuantity = nomenclature.UnitsQuantityPerPallet > unitsQuantity
                ? nomenclature.UnitsQuantityPerPallet
                : unitsQuantity;

            var stickerExists = sticker.Write() == WritingResult.Success;

            if (stickerExists)
                {
                printStickers(new List<Stickers>() { sticker });
                }

            return stickerExists ? sticker.Id : 0;
            }

        public long CreateNewAcceptance()
            {
            var acceptance = new AcceptanceOfGoods()
                {
                    State = StatesOfDocument.Processing,
                    Date = SystemConfiguration.ServerDateTime
                };
            acceptance.SetRef("Responsible", getUserId());

            var writeResult = acceptance.Write();
            return writeResult == WritingResult.Success ? acceptance.Id : 0;
            }

        public DataTable GetWares(string barcode, SelectionFilters selectionFilter)
            {
            var q = DB.NewQuery(@"select n.Id, rtrim(n.Description) [Description]
from Barcodes b
join Nomenclature n on n.Id = b.Nomenclature
where b.Description = @barcode");
            q.AddInputParameter("barcode", barcode);

            var result = q.SelectToTable();

            switch (selectionFilter)
                {
                case SelectionFilters.All:
                    return result;

                case SelectionFilters.RecentlyShipped:
                    return filterRecentlyShippedWares(result);
                }
            return result;
            }

        private readonly int RECENTLY_SHIPPED_DAYS_AMOUNT = getRecentlyShippedDaysAmount();

        private static int getRecentlyShippedDaysAmount()
            {
#if DEBUG
            return 1000;
#endif
            return 1;
            }

        private DataTable filterRecentlyShippedWares(DataTable waresIdsTable)
            {
            while (waresIdsTable.Columns.Count > 1)
                {
                waresIdsTable.Columns.RemoveAt(1);
                }

            waresIdsTable.Columns[0].ColumnName = "Value";

            var q = DB.NewQuery(string.Format(@"
declare @boundDate date = DATEADD (day , -{0}, cast(GETDATE() as date));

select distinct nom.Id, rtrim(nom.Description) [Description] from  Moving

join dbo.SubMovingNomenclatureInfo n on n.IdDoc = Moving.Id
join @ids as wares on n.Nomenclature = wares.Value
join Nomenclature nom on nom.Id = n.Nomenclature

where Moving.PickingPlan > 0 and Moving.MarkForDeleting = 0 and Moving.Date >= @boundDate

order by rtrim(nom.Description)
", RECENTLY_SHIPPED_DAYS_AMOUNT));
            q.AddInputTVPParameter("ids", waresIdsTable, "ListOfInt64");
            var result = q.SelectToTable();

            return result;
            }

        public bool SetBarcode(string barcode, long stickerId, out bool recordWasAdded)
            {
            var sticker = new Stickers() { ReadingId = stickerId };
            var nomenclatureId = sticker.GetRef("Nomenclature");

            foreach (DataRow row in GetWares(barcode, SelectionFilters.All).Rows)
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