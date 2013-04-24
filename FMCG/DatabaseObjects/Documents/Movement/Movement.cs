using System.Collections.Generic;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Platform;
using Aramis.SystemConfigurations;
using Aramis.UI.WinFormsDevXpress;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Interfaces;
using AtosFMCG.Enums;
using AtosFMCG.HelperClasses;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    /// <summary>Переміщення</summary>
    [Document(Description = "Переміщення", GUID = "015CC1EA-D666-431E-9D08-510395C78E4C", NumberType = NumberType.Int64, NumberIsReadonly = false)]
    public class Movement : DocumentTable
        {
        #region Properties
        /// <summary>Стан документу</summary>
        [DataField(Description = "Стан документу", ShowInList = true)]
        public StatesOfDocument State
            {
            get
                {
                return z_State;
                }
            set
                {
                if (z_State == value)
                    {
                    return;
                    }

                z_State = value;
                NotifyPropertyChanged("State");
                }
            }
        private StatesOfDocument z_State;

        /// <summary>Джерело</summary>
        [DataField(Description = "Джерело", ShowInList = true, AllowOpenItem = true, AllowedTypes = new[]{typeof(ShipmentPlan), typeof(AcceptanceOfGoods)})]
        public DocumentTable Source
            {
            get
                {
                return (DocumentTable)GetValueForObjectProperty("Source");
                }
            set
                {
                SetValueForObjectProperty("Source", value);
                }
            }

        #region StorageType = StorageTypes.Local
        /// <summary>Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)</summary>
        [DataField(Description = "Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)", ShowInList = true, StorageType = StorageTypes.Local)]
        public string Info
            {
            get { return string.Concat(Responsible.Description, ' ', Date.ToString()); }
            }

        /// <summary>Номер накладної</summary>
        [DataField(Description = "Номер накладної", ShowInList = true, StorageType = StorageTypes.Local)]
        public string IncomeNumber
            {
            get
                {
                return z_IncomeNumber;
                }
            set
                {
                if (z_IncomeNumber == value)
                    {
                    return;
                    }

                z_IncomeNumber = value;
                NotifyPropertyChanged("IncomeNumber");
                }
            }
        private string z_IncomeNumber = string.Empty;

        /// <summary>Дата накладної</summary>
        [DataField(Description = "Дата накладної", ShowInList = true, StorageType = StorageTypes.Local)]
        public string IncomeDate
            {
            get
                {
                return z_IncomeDate;
                }
            set
                {
                if (z_IncomeDate == value)
                    {
                    return;
                    }

                z_IncomeDate = value;
                NotifyPropertyChanged("IncomeDate");
                }
            }
        private string z_IncomeDate = string.Empty;
        #endregion

        #region Table Nomeclature
        /// <summary>Номенлатура</summary>
        [Table(Columns = "NomenclatureCode, Nomenclature, NomenclatureParty, NomenclatureMeasure, NomenclatureCount, SourceCell, DestinationCell, RowState, IsTare", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенлатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        /// <summary>Код груза</summary>
        [SubTableField(Description = "Код вантажу", PropertyType = typeof(long))]
        public DataColumn NomenclatureCode { get; set; }

        /// <summary>Номенлатура</summary>
        [SubTableField(Description = "Номенлатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Nomenclature { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn NomenclatureMeasure { get; set; }

        /// <summary>К-сть</summary>
        [SubTableField(Description = "К-сть", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn NomenclatureCount { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Party))]
        public DataColumn NomenclatureParty { get; set; }

        /// <summary>Комірка-джерело</summary>
        [SubTableField(Description = "Комірка-джерело", PropertyType = typeof(Cells))]
        public DataColumn SourceCell { get; set; }

        /// <summary>Комірка-призначення</summary>
        [SubTableField(Description = "Комірка-призначення", PropertyType = typeof(Cells))]
        public DataColumn DestinationCell { get; set; }

        /// <summary>Переміщено</summary>
        [SubTableField(Description = "Переміщено", PropertyType = typeof(StatesOfDocument), ReadOnly = true)]
        public DataColumn RowState { get; set; }

        /// <summary>Тара</summary>
        [SubTableField(Description = "Тара", PropertyType = typeof(bool), StorageType =  StorageTypes.Local, ReadOnly = true)]
        public DataColumn IsTare { get; set; }
        #endregion
        #endregion

        #region DocumentTable
        readonly Dictionary<long, bool> tareDic = new Dictionary<long,bool>();

        protected override WritingResult CheckingBeforeWriting()
            {
            if (Responsible.Id != SystemAramis.CurrentUser.Id)
                {
                Responsible = SystemAramis.CurrentUser;
                }

            return base.CheckingBeforeWriting();
            }

        protected override void InitItemBeforeShowing()
            {
            base.InitItemBeforeShowing();

            ValueOfObjectPropertyChanged += AcceptanceOfGoods_ValueOfObjectPropertyChanged;
            TableRowChanged += AcceptanceOfGoods_TableRowChanged;
            TableRowAdded += Movement_TableRowAdded;
            fillSourceData();
            fillingTare();
            }
        #endregion

        #region Filling
        private void fillSourceData()
            {
            IIncomeOwner owner = Source as IIncomeOwner;

            if(owner!=null)
                {
                IncomeNumber = owner.IncomeNumber;
                }

            IncomeDate = Source == null || Source.Id == 0 ? string.Empty : Source.Date.ToShortDateString();
            }

        private void fillingTare()
            {
            foreach (DataRow row in NomenclatureInfo.Rows)
                {
                fillTareInRow(row);
                }
            }

        private void fillTareInRow(DataRow row)
            {
            long nomenclatureId = (long) row[Nomenclature];

            if(tareDic.ContainsKey(nomenclatureId))
                {
                row[IsTare] = tareDic[nomenclatureId];
                }
            else
                {
                Nomenclature nomenclature = new Nomenclature();
                nomenclature.Read(nomenclatureId);
                tareDic.Add(nomenclatureId, nomenclature.IsTare);
                row[IsTare] = nomenclature.IsTare;
                }
            }
        #endregion

        #region Changed
        void Movement_TableRowAdded(DataTable dataTable, DataRow currentRow)
            {
            if(dataTable.Equals(NomenclatureInfo))
                {
                currentRow[RowState] = StatesOfDocument.Planned;
                }
            }

        void AcceptanceOfGoods_ValueOfObjectPropertyChanged(string propertyName)
            {
            switch (propertyName)
                {
                    case "Source":
                        fillSourceData();
                        break;
                }
            }

        void AcceptanceOfGoods_TableRowChanged(DataTable dataTable, DataColumn currentColumn, DataRow currentRow)
            {
            if(dataTable.Equals(NomenclatureInfo))
                {
                if (currentColumn.Equals(Nomenclature))
                    {
                    fillTareInRow(currentRow);
                    }
                }
            }
        #endregion

        #region Static
        public static void MovePallet(long palletId, long newPositionId, bool isCell)
            {
            Query query = DB.NewQuery(@"
DECLARE @Today DATETIME=CAST(GETDATE() AS DATE)

SELECT TOP 1 m.Id,n.LineNumber
FROM Movement m 
JOIN SubMovementNomenclatureInfo n ON n.IdDoc=m.Id 
WHERE
    m.State=0 
    AND CAST(m.Date AS DATE)=@Today
    AND m.MarkForDeleting=0 
    AND n.NomenclatureCode=@PalletId
    AND RowState=0");
            query.AddInputParameter("PalletId", palletId);
            QueryResult result = query.SelectRow();

            if(result!=null)
                {
                //Якщо переміщення виконується за завданням, то просто оновити IsMoved для виконаного завдання
                object docId = result["Id"];
                object lineNumber = result["LineNumber"];

                Query updateCommand = DB.NewQuery(@"
UPDATE SubMovementNomenclatureInfo SET RowState=@State WHERE IdDoc=@DocId AND LineNumber=@LineNumber");
                updateCommand.AddInputParameter("DocId", docId);
                updateCommand.AddInputParameter("State", StatesOfDocument.Achieved);
                updateCommand.AddInputParameter("LineNumber", lineNumber);
                updateCommand.Execute();
                }
            else
                {
                //Інакше перевірити і при потребі створити новий документ документу
                Query getDocCommand = DB.NewQuery(@"
DECLARE @Today DATETIME=CAST(GETDATE() AS DATE)

SELECT TOP 1 @DocId=Id 
FROM Movement
WHERE MarkForDeleting=0 AND State=@State AND Source=0 AND CAST(Date AS DATE)=@Today
ORDER BY Date DESC");
                getDocCommand.AddInputParameter("State", StatesOfDocument.Achieved);
                object idObj = getDocCommand.SelectScalar();
                Movement movementDoc = new Movement();
                
                if(idObj==null)
                    {
                    movementDoc.Date = SystemConfiguration.ServerDateTime.Date;
                    }
                else
                    {
                    //Якщо документ просто читаємо його, інакше в результаті буде створено новий док
                    movementDoc.Read(idObj);
                    }

                Query getNomenclatureCommand = DB.NewQuery(@"
SELECT DISTINCT b.Nomenclature,b.Quantity,b.MeasureUnit,b.Cell,n.NomenclatureParty
FROM StockBalance b 
LEFT JOIN SubAcceptanceOfGoodsNomenclatureInfo n ON n.NomenclatureCode=b.UniqueCode
WHERE b.UniqueCode=@PalletId AND Quantity>0");
                getNomenclatureCommand.AddInputParameter("PalletId", palletId);
                DataTable table = getNomenclatureCommand.SelectToTable();

                foreach (DataRow row in table.Rows)
                    {
                    DataRow newRow = movementDoc.NomenclatureInfo.GetNewRow(movementDoc);
                    newRow.SetRefValueToRowCell(movementDoc, movementDoc.Nomenclature, row["Nomenclature"], typeof(Nomenclature));
                    newRow[movementDoc.NomenclatureCode] = palletId;
                    newRow[movementDoc.NomenclatureCount] = row["Quantity"];
                    newRow.SetRefValueToRowCell(movementDoc, movementDoc.NomenclatureMeasure, row["MeasureUnit"], typeof(Measures));
                    newRow.SetRefValueToRowCell(movementDoc, movementDoc.NomenclatureParty, row["NomenclatureParty"], typeof(Party));
                    newRow.SetRefValueToRowCell(movementDoc, movementDoc.SourceCell, row["Cell"], typeof(Cells));
                    newRow.SetRefValueToRowCell(movementDoc, movementDoc.DestinationCell, newPositionId, typeof(Cells));
                    newRow[movementDoc.RowState] = StatesOfDocument.Achieved;
                    newRow.AddRowToTable(movementDoc);
                    }

                movementDoc.Write();
                }

            PalletMover.MovePalletToNewPlace(palletId, isCell ? 0 : newPositionId); 

            #region Старая реализация через процедуру..
            //Query query = DB.NewQuery(@"EXEC FinishMovement @Responsible,@DestinationCell,@PalletId");
            //query.AddInputParameter("Responsible", SystemAramis.CurrentUser.Id);
            //query.AddInputParameter("DestinationCell", newPositionId);
            //query.AddInputParameter("PalletId", palletId);
            //query.Execute();
            #endregion
            }
        #endregion
        }
    }