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
    /// <summary>Приймання товару</summary>
    [Document(Description = "Приймання товару", GUID = "0ACBC4E6-5486-4F2E-B207-3E8D012A080B", NumberType = NumberType.Int64, NumberIsReadonly = false)]
    public class AcceptanceOfGoods : DocumentTable
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

        /// <summary>Джерело </summary>
        [DataField(Description = "Джерело ", ShowInList = true, AllowOpenItem = true)]
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

        /// <summary>Контрагент</summary>
        [DataField(Description = "Контрагент", ShowInList = true)]
        public string Contractor
            {
            get
                {
                return z_Contractor;
                }
            set
                {
                if (z_Contractor == value)
                    {
                    return;
                    }

                z_Contractor = value;
                NotifyPropertyChanged("Contractor");
                }
            }
        private string z_Contractor = string.Empty;

        /// <summary>Перевізник</summary>
        [DataField(Description = "Перевізник", ShowInList = true, StorageType = StorageTypes.Local)]
        public string Carrier
            {
            get
                {
                return z_Carrier;
                }
            set
                {
                if (z_Carrier == value)
                    {
                    return;
                    }

                z_Carrier = value;
                NotifyPropertyChanged("Carrier");
                }
            }
        private string z_Carrier = string.Empty;

        /// <summary>Водій</summary>
        [DataField(Description = "Водій", ShowInList = true, StorageType = StorageTypes.Local)]
        public string Driver
            {
            get
                {
                return z_Driver;
                }
            set
                {
                if (z_Driver == value)
                    {
                    return;
                    }

                z_Driver = value;
                NotifyPropertyChanged("Driver");
                }
            }
        private string z_Driver = string.Empty;

        /// <summary>Машина</summary>
        [DataField(Description = "Машина", ShowInList = true, StorageType = StorageTypes.Local)]
        public string Car
            {
            get
                {
                return z_Car;
                }
            set
                {
                if (z_Car == value)
                    {
                    return;
                    }

                z_Car = value;
                NotifyPropertyChanged("Car");
                }
            }
        private string z_Car = string.Empty; 
        #endregion

        #region Table Nomeclature
        /// <summary>Номенлатура</summary>
        [Table(Columns = "NomenclatureCode, Nomenclature, NomenclatureMeasure, NomenclatureDate, NomenclatureCount, NomenclatureCell", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенлатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        /// <summary>Код груза</summary>
        [SubTableField(Description = "Код груза", PropertyType = typeof(long), ReadOnly = true)]
        public DataColumn NomenclatureCode { get; set; }

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

        /// <summary>Комірка</summary>
        [SubTableField(Description = "Комірка", PropertyType = typeof(Cells))]
        public DataColumn NomenclatureCell { get; set; }
        #endregion

        #region Table Tare
        /// <summary>Номенлатура</summary>
        [Table(Columns = "TareCode, Tare, TareMeasure, TareDate, TareCount, TareCell", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенлатура")]
        public DataTable TareInfo
            {
            get { return GetSubtable("TareInfo"); }
            }

        /// <summary>Код тари</summary>
        [SubTableField(Description = "Код тари", PropertyType = typeof(long), ReadOnly = true)]
        public DataColumn TareCode { get; set; }

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

        /// <summary>Комірка</summary>
        [SubTableField(Description = "Комірка", PropertyType = typeof(Cells))]
        public DataColumn TareCell { get; set; }
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

            ValueOfObjectPropertyChanged += AcceptanceOfGoods_ValueOfObjectPropertyChanged;

            fillSourceData();
            }
        #endregion

        #region Filling
        private void fillSourceData()
            {
            IncomeNumber = Source.IncomeNumber;
            IncomeDate = Source.Id==0 ? string.Empty : Source.Date.ToShortDateString();
            Contractor = Source.Contractor.Description;
            Carrier = Source.Carrier.Description;
            Driver = Source.Driver.Description;
            Car = Source.Car.Description;
            }
        #endregion

        #region Changed
        void AcceptanceOfGoods_ValueOfObjectPropertyChanged(string propertyName)
            {
            switch (propertyName)
                {
                    case "Source":
                        fillSourceData();
                        break;
                }
            }
        #endregion
        }
    }