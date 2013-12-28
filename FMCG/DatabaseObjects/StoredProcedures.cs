using System.Collections.Generic;
using System.Windows.Documents;

class ProceduresOfFMCG
    {
    private string procedures = @"

PROCEDURE [dbo].[Report_PalletsOrder]
	(@StartDate Datetime2,	
	@Cell bigint)
AS
BEGIN
with Remains as 
(
SELECT distinct rem.Code pallet, Cell

FROM [GetStockBalance] (
   @StartDate
  ,0
  ,@Cell
  ,2
  ,0
  ,0) rem  
), 

PalletsRelations as
(
SELECT Pallet, PreviousPallet FROM [GetPalletsRelations] (
   @StartDate, 0, 0)
), 

FirstPallets as
(
select rem.Cell, rem.pallet from Remains rem
left join PalletsRelations r on r.Pallet = rem.pallet
where r.PreviousPallet is null
), 

PalletsNumbers as
(
select cell, pallet, 1 OrdinalNumber from FirstPallets

union all

select n.Cell, r.Pallet, n.OrdinalNumber+1 from PalletsRelations r
join PalletsNumbers n on n.Pallet= r.PreviousPallet
)

select p.*, rtrim(Cells.Description) CellName from PalletsNumbers p
join Cells on Cells.Id = p.Cell

order by p.Cell, CellName 
END













PROCEDURE [dbo].[Report_StockBalance]
	(@StartDate Datetime2,
	@Nomenclature bigint,
	@Cell bigint)
AS
BEGIN 
	SELECT rem.Nomenclature, rem.Cell,rem.Party, rem.Code, rem.Quantity,
	case when (n.UnitsQuantityPerPack <> 0 and n.IsTare = 0) then (rem.Quantity / n.UnitsQuantityPerPack) else 0 end Packs,
	case when n.IsTare = 0 then 'Продукція' else 'Тара' end WareType,
rtrim(n.[Description]) NomenclatureDescription,
rtrim(c.[Description]) CellDescription,
p.DateOfManufacture ProductionDate,

n.IsTare 

--CONVERT ( nvarchar(max), p.DateOfManufacture, 4 )  ProductionDate
FROM [GetStockBalance] (
   @StartDate
  ,@Nomenclature 
  ,@Cell
  ,2
  ,0
  ,0) rem
  
  join Nomenclature n on n.Id = rem.Nomenclature
  join Cells c on c.Id = rem.Cell
  left join Parties p on p.Id = rem.Party
END








PROCEDURE [dbo].[Report_PickingDiscrepancies] 
	(@MovingId bigint)
AS
BEGIN

declare @planId bigint;
select @planId = isnull(PickingPlan,0) from Moving where Id = @movingId;

with pickingPlan as 
(
select p.Nomenclature, sum(p.Quantity) PlanValue 
	from SubShipmentPlanNomenclatureInfo p 
	where p.IdDoc = @planId
	group by Nomenclature
	having sum(p.Quantity) > 0
)
, pickingFact as 
(
select f.Nomenclature, sum(f.FactValue) FactValue
	from SubMovingNomenclatureInfo f
	where f.IdDoc = @movingId and f.RowState = 2 -- complated
	group by Nomenclature
	having sum(f.FactValue) > 0
)

select p.Nomenclature NomenclatureId, p.PlanValue, 
	ISNULL(f.FactValue, 0) FactValue,
	rtrim(n.Description) Nomenclature
	
	from pickingPlan p
	left join pickingFact f on p.Nomenclature = f.Nomenclature
	join Nomenclature n on n.Id = p.Nomenclature
	where PlanValue <> FactValue
	order by n.Description
END

";
    }