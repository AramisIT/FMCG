using System.Collections.Generic;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Platform;
using AtosFMCG.DatabaseObjects.Catalogs;
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
        [DataField(Description = "Джерело", ShowInList = true, AllowOpenItem = true)]
        public PlannedArrival Source
            {
            get
                {
                return (PlannedArrival)GetValueForObjectProperty("Source");
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
        [Table(Columns = "NomenclatureCode, Nomenclature, NomenclatureParty, NomenclatureMeasure, NomenclatureCount, SourceCell, DestinationCell, IsMoved, IsTare", ShowLineNumberColumn = true)]
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
        [SubTableField(Description = "Переміщено", PropertyType = typeof(bool), ReadOnly = true)]
        public DataColumn IsMoved { get; set; }

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
            IncomeNumber = Source.IncomeNumber;
            IncomeDate = Source.Id==0 ? string.Empty : Source.Date.ToShortDateString();
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
                currentRow[IsMoved] = false;
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
            Query query = DB.NewQuery(@"EXEC FinishMovement @Responsible,@DestinationCell,@PalletId");
            query.AddInputParameter("Responsible", SystemAramis.CurrentUser.Id);
            query.AddInputParameter("DestinationCell", newPositionId);
            query.AddInputParameter("PalletId", palletId);
            query.Execute();

            PalletMover.MovePalletToNewPlace(palletId, isCell ? 0 : newPositionId);
            }
        #endregion
        }
    }