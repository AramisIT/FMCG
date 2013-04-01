using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Platform;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.Enums;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    /// <summary>Інвентаризація</summary>
    [Document(Description = "Інвентаризація", GUID = "0E87222E-830D-466A-826F-8ABBC4B36FEE", NumberType = NumberType.Int64, NumberIsReadonly = false)]
    public class Inventory : DocumentTable
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

        /// <summary>Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)</summary>
        [DataField(Description = "Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)", ShowInList = true, StorageType = StorageTypes.Local)]
        public string Info
            {
            get { return string.Concat(Responsible.Description, ' ', Date.ToString()); }
            }

        #region Таблична частина
        /// <summary>Номенклатура</summary>
        [Table(Columns = "PalletCode,Nomenclature,Measure,PlanValue,FactValue,Party,Cell", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенклатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        /// <summary>Код вантажа</summary>
        [SubTableField(Description = "Код вантажу", PropertyType = typeof(long))]
        public DataColumn PalletCode { get; set; }

        /// <summary>Номенклатура</summary>
        [SubTableField(Description = "Номенклатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Nomenclature { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn Measure { get; set; }

        /// <summary>План</summary>
        [SubTableField(Description = "План", PropertyType = typeof(double))]
        public DataColumn PlanValue { get; set; }

        /// <summary>Факт</summary>
        [SubTableField(Description = "Факт", PropertyType = typeof(double))]
        public DataColumn FactValue { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Party))]
        public DataColumn Party { get; set; }

        /// <summary>Комірка</summary>
        [SubTableField(Description = "Комірка", PropertyType = typeof(Cells))]
        public DataColumn Cell { get; set; }
        #endregion
        #endregion

        protected override WritingResult CheckingBeforeWriting()
            {
            if (Responsible.Id != SystemAramis.CurrentUser.Id)
                {
                Responsible = SystemAramis.CurrentUser;
                }

            return base.CheckingBeforeWriting();
            }
        }
    }