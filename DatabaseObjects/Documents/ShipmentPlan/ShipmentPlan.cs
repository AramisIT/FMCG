using System.Collections.Generic;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Platform;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.Enums;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    /// <summary>План відвантаження</summary>
    [Document(Description = "План відвантаження", GUID = "029B0572-E5B5-48CD-9805-1211319A5633", NumberType = NumberType.Int64, NumberIsReadonly = false)]
    public class ShipmentPlan : DocumentTable
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

        /// <summary>Тип відвантаження</summary>
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

        /// <summary>Місто</summary>
        [DataField(Description = "Місто", ShowInList = true)]
        public Cities City
            {
            get
                {
                return (Cities)GetValueForObjectProperty("City");
                }
            set
                {
                SetValueForObjectProperty("City", value);
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
        [Table(Columns = "Code, Nomenclature, Measure, Quantity, Party, Cell, IsTare", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенлатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        /// <summary>Код груза</summary>
        [SubTableField(Description = "Код вантажу", PropertyType = typeof(long))]
        public DataColumn Code { get; set; }

        /// <summary>Номенлатура</summary>
        [SubTableField(Description = "Номенлатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Nomenclature { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn Measure { get; set; }

        /// <summary>К-сть</summary>
        [SubTableField(Description = "К-сть", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn Quantity { get; set; }

        /// <summary>Комірка</summary>
        [SubTableField(Description = "Комірка", PropertyType = typeof(Cells))]
        public DataColumn Cell { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Party))]
        public DataColumn Party { get; set; }

        /// <summary>Тара</summary>
        [SubTableField(Description = "Тара", PropertyType = typeof(bool), StorageType = StorageTypes.Local, ReadOnly = true)]
        public DataColumn IsTare { get; set; }
        #endregion
        #endregion

        #region DocumentTable
        readonly Dictionary<long, bool> tareDic = new Dictionary<long, bool>();

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
            fillingTare();
            }
        #endregion

        #region Filling
        private void fillingTare()
            {
            foreach (DataRow row in NomenclatureInfo.Rows)
                {
                fillTareInRow(row);
                }
            }

        private void fillTareInRow(DataRow row)
            {
            long nomenclatureId = (long)row[Nomenclature];

            if (tareDic.ContainsKey(nomenclatureId))
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
        }
    }