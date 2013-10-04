using System.Collections.Generic;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Platform;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.Enums;
using Catalogs;

namespace Documents
    {
    /// <summary>Відвантаження</summary>
    [Document(Description = "Відвантаження", GUID = "003741EB-2D7A-49FD-B711-92A039FB4832", NumberType = NumberType.Int64, NumberIsReadonly = false)]
    public class Shipment : DocumentTable
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

        /// <summary>Тип відвантаження</summary>
        [DataField(Description = "Тип відвантаження", ShowInList = true)]
        public TypesOfShipment TypeOfShipment
            {
            get
                {
                return z_TypeOfShipment;
                }
            set
                {
                if (z_TypeOfShipment == value)
                    {
                    return;
                    }

                z_TypeOfShipment = value;
                NotifyPropertyChanged("TypeOfShipment");
                }
            }
        private TypesOfShipment z_TypeOfShipment;

        /// <summary>Контрагент</summary>
        [DataField(Description = "Контрагент", ShowInList = true)]
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
        [DataField(Description = "Перевізник", ShowInList = true)]
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
        [DataField(Description = "Водій", ShowInList = true)]
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
        [DataField(Description = "Машина", ShowInList = true)]
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
        }
    }