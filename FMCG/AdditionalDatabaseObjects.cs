using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aramis.Platform.SystemSetup;

namespace FMCG
    {
    public class AdditionalDatabaseObjects : CheckSolutionDBStruct
        {
        private readonly List<string> afterTableCreatedSP = new List<string>()
            {
                 #region GetArrivalResult

		 @"CREATE PROCEDURE GetArrivalResult
	@DocRefStr nchar(36)
AS
BEGIN

declare @docRef as uniqueidentifier = cast(@DocRefStr as uniqueidentifier);

declare @IdDoc bigint;

select top 1 @IdDoc = Id from PlannedArrival where Ref1C = @docRef

select goods.LineNumber, nom.Ref1C NomenclatureRef, goods.NomenclatureCount, p.DateOfManufacture PartyDate 
from dbo.SubPlannedArrivalNomenclatureInfo goods
join Nomenclature nom on nom.Id = goods.Nomenclature
join Parties p on p.Id = goods.NomenclatureParty
where goods.IdDoc = @IdDoc

order by goods.LineNumber
END --GetArrivalResult" 

        	#endregion

                 #region GetArrivalTareResult

		 , @"CREATE PROCEDURE GetArrivalTareResult
	@DocRefStr nchar(36)
AS
BEGIN
declare @docRef as uniqueidentifier = cast(@DocRefStr as uniqueidentifier);

declare @IdDoc bigint;

select top 1 @IdDoc = Id from PlannedArrival where Ref1C = @docRef

select tare.LineNumber, nom.Ref1C NomenclatureRef, tare.TareCount 
from dbo.SubPlannedArrivalTareInfo tare
join Nomenclature nom on nom.Id = tare.Tare

where tare.IdDoc = @IdDoc

order by tare.LineNumber
END --GetArrivalTareResult" 

        	#endregion
            };

        public AdditionalDatabaseObjects()
            {
            AfterTableCreateSP.AddRange(afterTableCreatedSP);
            }
        }
    }
