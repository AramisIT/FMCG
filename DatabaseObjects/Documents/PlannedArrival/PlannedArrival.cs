using System;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Platform;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.Enums;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    /// <summary>План приходу</summary>
    [Document(Description = "План приходу", GUID = "0455B8DB-F11B-4B3B-A727-D4E889A1EFCB", NumberType = NumberType.Int64, NumberIsReadonly = false)]
    public class PlannedArrival : DocumentTable
        {
        #region Properties
        /// <summary>Вхідний номер/Номер накладної</summary>
        [DataField(Description = "№ накладної", ShowInList = true, NotEmpty = true)]
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

        /// <summary>Прихід від</summary>
        [DataField(Description = "Прихід від", ShowInList = true)]
        public TypesOfArrival TypeOfArrival
            {
            get
                {
                return z_TypeOfArrival;
                }
            set
                {
                if (z_TypeOfArrival == value)
                    {
                    return;
                    }

                z_TypeOfArrival = value;
                NotifyPropertyChanged("TypeOfArrival");
                }
            }
        private TypesOfArrival z_TypeOfArrival;

        /// <summary>Контрагент</summary>
        [DataField(Description = "Контрагент", ShowInList = true, NotEmpty = true, AllowOpenItem = true)]
        public Contractors Contractor
            {
            get
                {
                return (Contractors)GetValueForObjectProperty("Contractor");
                }
            set
                {
                SetValueForObjectProperty("Contractor", value);
                }
            }

        /// <summary>Перевізник</summary>
        [DataField(Description = "Перевізник", ShowInList = true, AllowOpenItem = true)]
        public Contractors Carrier
            {
            get
                {
                return (Contractors)GetValueForObjectProperty("Carrier");
                }
            set
                {
                SetValueForObjectProperty("Carrier", value);
                }
            }

        /// <summary>Водій</summary>
        [DataField(Description = "Водій", ShowInList = true, AllowOpenItem = true)]
        public Drivers Driver
            {
            get
                {
                return (Drivers)GetValueForObjectProperty("Driver");
                }
            set
                {
                SetValueForObjectProperty("Driver", value);
                }
            }

        /// <summary>Машина</summary>
        [DataField(Description = "Машина", ShowInList = true, AllowOpenItem = true)]
        public Cars Car
            {
            get
                {
                return (Cars)GetValueForObjectProperty("Car");
                }
            set
                {
                SetValueForObjectProperty("Car", value);
                }
            }

        /// <summary>Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)</summary>
        [DataField(Description = "Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)", ShowInList = true, StorageType = StorageTypes.Local)]
        public string Info
            {
            get { return string.Concat(Responsible.Description, ' ', Date.ToString()); }
            }

        #region Table Nomeclature
        /// <summary>Номенлатура</summary>
        [Table(Columns = "Nomenclature, NomenclatureMeasure, NomenclatureDate, NomenclatureCount, NomenclaturePrice, NomenclatureSum, NomenclatureParty", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенлатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        /// <summary>Номенлатура</summary>
        [SubTableField(Description = "Номенлатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Nomenclature { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn NomenclatureMeasure { get; set; }

        /// <summary>Дата виробництва</summary>
        [SubTableField(Description = "Дата виробництва", PropertyType = typeof(DateTime))]
        public DataColumn NomenclatureDate { get; set; }

        /// <summary>К-сть</summary>
        [SubTableField(Description = "К-сть", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn NomenclatureCount { get; set; }

        /// <summary>Ціна</summary>
        [SubTableField(Description = "Ціна", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn NomenclaturePrice { get; set; }

        /// <summary>Сума</summary>
        [SubTableField(Description = "Сума", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2, StorageType = StorageTypes.Local)]
        public DataColumn NomenclatureSum { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Party))]
        public DataColumn NomenclatureParty { get; set; }
        #endregion

        #region Table Tare
        /// <summary>Номенлатура</summary>
        [Table(Columns = "Tare, TareMeasure, TareDate, TareCount, TarePrice, TareSum, TareParty", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенлатура")]
        public DataTable TareInfo
            {
            get { return GetSubtable("TareInfo"); }
            }

        /// <summary>Номенлатура</summary>
        [SubTableField(Description = "Номенлатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Tare { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn TareMeasure { get; set; }

        /// <summary>Дата виробництва</summary>
        [SubTableField(Description = "Дата виробництва", PropertyType = typeof(DateTime))]
        public DataColumn TareDate { get; set; }

        /// <summary>К-сть</summary>
        [SubTableField(Description = "К-сть", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn TareCount { get; set; }

        /// <summary>Ціна</summary>
        [SubTableField(Description = "Ціна", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn TarePrice { get; set; }

        /// <summary>Сума</summary>
        [SubTableField(Description = "Сума", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2, StorageType = StorageTypes.Local)]
        public DataColumn TareSum { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Party))]
        public DataColumn TareParty { get; set; }
        #endregion
        #endregion

        #region DocumentTable
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

            TableRowChanged += PlannedArrival_TableRowChanged;
            fillNomenclatureData();
            fillTareData();
            }
        #endregion

        #region Filling
        private void fillNomenclatureData()
            {
            foreach (DataRow row in NomenclatureInfo.Rows)
                {
                fillNomenclatureRowData(row);
                }
            }

        private void fillNomenclatureRowData(DataRow row)
            {
            row[NomenclatureSum] = (double)row[NomenclatureCount] * (double)row[NomenclaturePrice];
            }

        private void fillTareData()
            {
            foreach (DataRow row in TareInfo.Rows)
                {
                fillTareRowData(row);
                }
            }

        private void fillTareRowData(DataRow row)
            {
            row[TareSum] = (double)row[TareCount] * (double)row[TarePrice];
            }
        #endregion

        #region Changed
        void PlannedArrival_TableRowChanged(DataTable dataTable, DataColumn currentColumn, DataRow currentRow)
            {
            if (dataTable.Equals(NomenclatureInfo))
                {
                if (currentColumn.Equals(NomenclatureCount) || currentColumn.Equals(NomenclaturePrice))
                    {
                    fillNomenclatureRowData(currentRow);
                    }
                }
            else if (dataTable.Equals(TareInfo))
                {
                if (currentColumn.Equals(TareCount) || currentColumn.Equals(TarePrice))
                    {
                    fillTareRowData(currentRow);
                    }
                }
            } 
        #endregion
        }
    }