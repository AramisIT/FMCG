using System;
using System.Collections.Generic;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Platform;
using Aramis.UI.WinFormsDevXpress;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Interfaces;
using AtosFMCG.Enums;
using Catalogs;
using Documents;

namespace Documents
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

        #region StorageType = StorageTypes.Local
        /// <summary>Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)</summary>
        [DataField(Description = "Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)", ShowInList = true, StorageType = StorageTypes.Local)]
        public string Info
            {
            get { return string.Concat(Responsible.Description, ' ', Date.ToString()); }
            }

        [DataField(Description = "Контрагент", ShowInList = true, AllowOpenItem = true)]
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

        #endregion

        #region Table Nomeclature
        /// <summary>Номенклатура</summary>
        [Table(Columns = "NomenclatureCode, Nomenclature, NomenclatureParty, NomenclatureMeasure, NomenclatureDate, NomenclaturePlan, NomenclatureFact, NomenclatureCell, IsTare", ShowLineNumberColumn = true)]
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
        [SubTableField(Description = "План", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn NomenclaturePlan { get; set; }

        /// <summary>К-сть</summary>
        [SubTableField(Description = "Факт", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn NomenclatureFact { get; set; }

        /// <summary>Комірка</summary>
        [SubTableField(Description = "Комірка", PropertyType = typeof(Cells))]
        public DataColumn NomenclatureCell { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Parties))]
        public DataColumn NomenclatureParty { get; set; }

        /// <summary>Тара</summary>
        [SubTableField(Description = "Тара", PropertyType = typeof(bool), StorageType = StorageTypes.Local, ReadOnly = true)]
        public DataColumn IsTare { get; set; }
        #endregion

        [Table(Columns = "AcceptancePlan")]
        [DataField(Description = "Плани приймання")]
        public DataTable Plans
            {
            get { return GetSubtable("Plans"); }
            }

        [SubTableField(Description = "План приймання", PropertyType = typeof(AcceptancePlan))]
        public DataColumn AcceptancePlan { get; set; }

        #endregion


        readonly Dictionary<long, bool> tareDic = new Dictionary<long, bool>();
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

            TableRowChanged += AcceptanceOfGoods_TableRowChanged;
            TableRowAdded += AcceptanceOfGoods_TableRowAdded;
            fillingTare();
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


        #region Changed

        void AcceptanceOfGoods_TableRowAdded(DataTable dataTable, DataRow currentRow)
            {
            if (dataTable.Equals(NomenclatureInfo))
                {
                currentRow[NomenclatureCode] = newCodeNumber;
                }
            }

        void AcceptanceOfGoods_TableRowChanged(DataTable dataTable, DataColumn currentColumn, DataRow currentRow)
            {
            if (dataTable.Equals(NomenclatureInfo))
                {
                if (currentColumn.Equals(Nomenclature))
                    {
                    fillTareInRow(currentRow);
                    }
                else if (currentColumn.Equals(NomenclatureParty))
                    {
                    fillDateInRow(currentRow);
                    }
                }
            }
        #endregion

        private static long newCodeNumber
            {
            get
                {
                if (z_NewCodeNumber == 0)
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

        private void fillPlan(DateTime planDocsDate)
            {
            var q = DB.NewQuery(@"select Id from AcceptancePlan

where CAST([Date] as date) = @Date
and Driver = @Driver
and Car = @Car
and MarkForDeleting = 0");
            q.AddInputParameter("Date", planDocsDate.StartOfDay());
            q.AddInputParameter("Driver", Driver.Id);
            q.AddInputParameter("Car", Car.Id);
            Plans.Rows.Clear();
            q.Foreach(qResult => addPlanDocument(Convert.ToInt64(qResult[0])));
            }

        private void addPlanDocument(long acceptancePlanId)
            {
            var row = Plans.GetNewRow(this);
            row[AcceptancePlan] = acceptancePlanId;
            row.AddRowToTable(this);

            var acceptancePlan = new AcceptancePlan();
            acceptancePlan.Read(acceptancePlanId);
            foreach (DataRow stickerRow in acceptancePlan.Stickers.Rows)
                {
                var sticker = new Stickers();
                sticker.Read(stickerRow[acceptancePlan.Sticker]);
                addWaresFromSticker(sticker);
                }
            }

        private void addWaresFromSticker(Stickers sticker)
            {
            Cells cell = sticker.Nomenclature.IsKeg() ? Consts.RedemptionCell : new Cells();

            addWareRow(sticker, cell, sticker.Nomenclature, sticker.UnitsQuantity);
            addWareRow(sticker, cell, sticker.Nomenclature.BoxType, sticker.Quantity);
            addWareRow(sticker, cell, sticker.Tray, 1);
            }

        private void addWareRow(Stickers sticker, Cells cell, Nomenclature nomenclature, int quantity)
            {
            if (quantity > 0 && !nomenclature.Empty)
                {
                var row = NomenclatureInfo.GetNewRow(this);

                row[Nomenclature] = nomenclature.Id;
                row[NomenclaturePlan] = quantity;
                row[NomenclatureCode] = sticker.Id;
                row[NomenclatureCell] = cell.Id;

                row.AddRowToTable(this);
                }
            }

        internal static void CreateNewAcceptance(AcceptancePlan acceptancePlan)
            {
            if (!@"Створити ""Приймання товару""".Ask()) return;

            var acceptance = new AcceptanceOfGoods();
            acceptance.Date = DateTime.Now;
            acceptance.Driver = acceptancePlan.Driver;
            acceptance.Car = acceptancePlan.Car;
            acceptance.Contractor = acceptancePlan.Contractor;
            acceptance.fillPlan(acceptancePlan.Date);
            if (acceptance.Write() != WritingResult.Success)
                {
                @"Невдала спроба запису документу ""Приймання товару""!".WarningBox();
                }
            }
        }
    }