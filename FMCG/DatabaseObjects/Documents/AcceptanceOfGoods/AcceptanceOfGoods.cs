using System;
using System.Collections.Generic;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Platform;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Interfaces;
using AtosFMCG.Enums;
using Catalogs;
using Documents;

namespace Documents
    {
    /// <summary>Приймання товару</summary>
    [Document(Description = "Приймання товару", GUID = "0ACBC4E6-5486-4F2E-B207-3E8D012A080B", NumberType = NumberType.Int64, NumberIsReadonly = false)]
    public class AcceptanceOfGoods : DocumentTable,IIncomeOwner
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
        public AcceptancePlan Source
            {
            get
                {
                return (AcceptancePlan)GetValueForObjectProperty("Source");
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
        [DataField(Description = "Контрагент", ShowInList = true, StorageType = StorageTypes.Local)]
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
        /// <summary>Номенклатура</summary>
        [Table(Columns = "NomenclatureCode, Nomenclature, NomenclatureParty, NomenclatureMeasure, NomenclatureDate, NomenclatureCount, NomenclatureCell, IsTare", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенклатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        /// <summary>Код груза</summary>
        [SubTableField(Description = "Код вантажу", PropertyType = typeof(long))]
        public DataColumn NomenclatureCode { get; set; }

        /// <summary>Номенклатура</summary>
        [SubTableField(Description = "Номенклатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Nomenclature { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn NomenclatureMeasure { get; set; }

        /// <summary>Дата виробництва</summary>
        [SubTableField(Description = "Дата виробництва", PropertyType = typeof(DateTime), StorageType = StorageTypes.Local, ReadOnly = true)]
        public DataColumn NomenclatureDate { get; set; }

        /// <summary>К-сть</summary>
        [SubTableField(Description = "К-сть", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn NomenclatureCount { get; set; }

        /// <summary>Комірка</summary>
        [SubTableField(Description = "Комірка", PropertyType = typeof(Cells))]
        public DataColumn NomenclatureCell { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Parties))]
        public DataColumn NomenclatureParty { get; set; }

        /// <summary>Тара</summary>
        [SubTableField(Description = "Тара", PropertyType = typeof(bool), StorageType =  StorageTypes.Local, ReadOnly = true)]
        public DataColumn IsTare { get; set; }
        #endregion
        #endregion

        #region DocumentTable
        readonly Dictionary<long, bool> tareDic = new Dictionary<long,bool>();
        readonly Dictionary<long, DateTime> partyDic = new Dictionary<long, DateTime>();

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
            TableRowAdded += AcceptanceOfGoods_TableRowAdded;
            fillSourceData();
            fillingTare();
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

        private void fillingTare()
            {
            foreach (DataRow row in NomenclatureInfo.Rows)
                {
                fillTareInRow(row);
                fillDateInRow(row);
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

        private void fillDateInRow(DataRow row)
            {
            long partyId = (long)row[NomenclatureParty];

            if (partyDic.ContainsKey(partyId))
                {
                row[NomenclatureDate] = partyDic[partyId];
                }
            else
                {
                Parties party = new Parties();
                party.Read(partyId);
                partyDic.Add(partyId, party.DateOfManufacture);
                row[NomenclatureDate] = party.DateOfManufacture;
                }
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

        void AcceptanceOfGoods_TableRowAdded(DataTable dataTable, DataRow currentRow)
            {
            if(dataTable.Equals(NomenclatureInfo))
                {
                currentRow[NomenclatureCode] = newCodeNumber;
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
                else if(currentColumn.Equals(NomenclatureParty))
                    {
                    fillDateInRow(currentRow);
                    }
                }
            }
        #endregion

        #region newCodeNumber
        private static long newCodeNumber
            {
            get
                {
                if(z_NewCodeNumber==0)
                    {
                    z_NewCodeNumber = GetNewCode();
                    }
                else
                    {
                    z_NewCodeNumber++;
                    }

                return z_NewCodeNumber;
                }
            }
        private static long z_NewCodeNumber;

        public static long GetNewCode()
            {
            Query query = DB.NewQuery(@"SELECT MAX(s.NomenclatureCode)+1 
FROM AcceptanceOfGoods a
JOIN SubAcceptanceOfGoodsNomenclatureInfo s ON s.IdDoc=a.Id");
            object code = query.SelectScalar();

            return code == null ? 1 : Convert.ToInt64(code);
            } 
        #endregion
        }
    }