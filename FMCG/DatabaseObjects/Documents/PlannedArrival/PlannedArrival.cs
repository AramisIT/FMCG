using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Documents;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Platform;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Interfaces;
using AtosFMCG.Enums;
using AtosFMCG.TouchScreen.PalletSticker;
using Catalogs;
using TouchScreen.Models.Data;

namespace Documents
    {
    /// <summary>План приходу</summary>
    [Document(Description = "План приймання", GUID = "0455B8DB-F11B-4B3B-A727-D4E889A1EFCB", NumberType = NumberType.Int64, NumberIsReadonly = false, ShowLastModifiedDateInList = true)]
    public class PlannedArrival : DocumentTable, ISyncWith1C
        {
        #region Properties
        /// <summary>Посилання 1С</summary>
        [DataField(Description = "Посилання 1С", ShowInList = false)]
        public Guid Ref1C
            {
            get
                {
                return z_Ref1C;
                }
            set
                {
                if (z_Ref1C == value)
                    {
                    return;
                    }
                z_Ref1C = value;
                NotifyPropertyChanged("Ref1C");
                }
            }
        private Guid z_Ref1C;

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
        [DataField(Description = "Приймання від", ShowInList = true)]
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
        public Carriers Carrier
            {
            get
                {
                return (Carriers)GetValueForObjectProperty("Carrier");
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
        /// <summary>Номенклатура</summary>
        [Table(Columns = "Nomenclature, NomenclatureMeasure, NomenclatureCount, NomenclaturePrice, NomenclatureSum, NomenclatureParty", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенклатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        /// <summary>Номенклатура</summary>
        [SubTableField(Description = "Номенклатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Nomenclature { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn NomenclatureMeasure { get; set; }

        /// <summary>К-сть</summary>
        [SubTableField(Description = "К-сть", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn NomenclatureCount { get; set; }

        /// <summary>Ціна</summary>
        [SubTableField(Description = "Ціна", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn NomenclaturePrice { get; set; }

        /// <summary>Сума</summary>
        [SubTableField(Description = "Сума", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2, StorageType = StorageTypes.Local)]
        public DataColumn NomenclatureSum { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Parties))]
        public DataColumn NomenclatureParty { get; set; }
        #endregion

        #region Table Tare
        /// <summary>Номенклатура</summary>
        [Table(Columns = "Tare, TareMeasure, TareCount, TarePrice, TareSum, TareParty", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенклатура")]
        public DataTable TareInfo
            {
            get { return GetSubtable("TareInfo"); }
            }

        /// <summary>Номенклатура</summary>
        [SubTableField(Description = "Номенклатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Tare { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn TareMeasure { get; set; }

        /// <summary>К-сть</summary>
        [SubTableField(Description = "К-сть", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn TareCount { get; set; }

        /// <summary>Ціна</summary>
        [SubTableField(Description = "Ціна", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn TarePrice { get; set; }

        /// <summary>Сума</summary>
        [SubTableField(Description = "Сума", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2, StorageType = StorageTypes.Local)]
        public DataColumn TareSum { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Parties))]
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
            row[NomenclatureSum] = (decimal)row[NomenclatureCount] * (decimal)row[NomenclaturePrice];
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
            row[TareSum] = (decimal)row[TareCount] * (decimal)row[TarePrice];
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

        internal void PrintStickers(List<NomenclatureData> wareList)
            {
            var printTasks = createPrintTasks(wareList);
            var stickersCreator = new StickersPrintingHelper(printTasks, ThermoPrinters.GetCurrentPrinterName());
            stickersCreator.Print();
            }

        private List<StickerInfo> createPrintTasks(List<NomenclatureData> wareList)
            {
            var stickers = createStickers(wareList);
            var result = new List<StickerInfo>();
            stickers.ForEach(sticker =>
                {
                    var stickerInfo = new StickerInfo()
                    {
                        AcceptionDate = sticker.AcceptionDate,
                        Barcode = sticker.Barcode,
                        Driver = sticker.Driver.Description,
                        ReleaseDate = sticker.ReleaseDate,
                        ExpiryDate = sticker.ExpiryDate,
                        HalpExpiryDate = sticker.HalpExpiryDate,
                        Nomenclature = sticker.Nomenclature.Description,
                        PacksCount = sticker.Quantity,
                        Id = sticker.Id
                    };
                    result.Add(stickerInfo);
                });

            return result;
            }

        private List<Stickers> createStickers(List<NomenclatureData> wareList)
            {
            var result = new List<Stickers>();

            for (int rowIndex = 0; rowIndex < NomenclatureInfo.Rows.Count; rowIndex++)
                {
                DataRow row = NomenclatureInfo.Rows[rowIndex];

                var nomenclatureCount = Convert.ToInt32(row[NomenclatureCount]);
                var nomenclature = new Nomenclature();
                nomenclature.Read((long)row[Nomenclature]);
                var countInOnePalet = nomenclature.UnitsQuantityPerPallet;
                var countInOnePack = nomenclature.UnitsQuantityPerPack;
                if (countInOnePack == 0)
                    {
                    countInOnePack = 1;// to avoid division by zero
                    }

                var party = new Parties();
                party.Read((long)row[NomenclatureParty]);


                var nomenclatureData = wareList[rowIndex];
                var createSpecificStikers = nomenclatureData.StandartPalletsCount > 0 ||
                                            nomenclatureData.NonStandartPalletsCount > 0;

                var pallets = createSpecificStikers ? buildNonStandartQuantitiesList(nomenclatureData, countInOnePack) : buildStandartQuantitiesList(nomenclatureCount, countInOnePalet, countInOnePack);

                foreach (var unitsQuantity in pallets)
                    {
                    var newSticker = createSticker(nomenclature, party, unitsQuantity);
                    if (newSticker == null)
                        {
                        return new List<Stickers>();
                        }
                    result.Add(newSticker);
                    }
                }

            return result;
            }

        private List<int> buildNonStandartQuantitiesList(NomenclatureData nomenclatureData, int countInOnePack)
            {
            var pallets = new List<int>();

            for (int stickerIndex = 0; stickerIndex < nomenclatureData.StandartPalletsCount; stickerIndex++)
                {
                pallets.Add(nomenclatureData.StandartPalletCountPer1 / countInOnePack);
                }

            if (nomenclatureData.UnitsOnNotFullPallet > 0)
                {
                pallets.Add(nomenclatureData.UnitsOnNotFullPallet / countInOnePack);
                }

            for (int stickerIndex = 0; stickerIndex < nomenclatureData.NonStandartPalletsCount; stickerIndex++)
                {
                pallets.Add(nomenclatureData.NonStandartPalletCountPer1 / countInOnePack);
                }

            if (nomenclatureData.UnitsOnNotFullNonStandartPallet > 0)
                {
                pallets.Add(nomenclatureData.UnitsOnNotFullNonStandartPallet / countInOnePack);
                }

            return pallets;
            }

        private List<int> buildStandartQuantitiesList(int nomenclatureCount, int countInOnePalet, int countInOnePack)
            {
            var pallets = new List<int>();
            while (nomenclatureCount > 0)
                {
                if (nomenclatureCount >= countInOnePalet)
                    {
                    nomenclatureCount -= countInOnePalet;
                    var packsCount = countInOnePack == 0 ? 0 : (int)(countInOnePalet / countInOnePack);
                    pallets.Add(packsCount);
                    }
                else
                    {
                    var packsCount = countInOnePack == 0 ? 0 : (int)(nomenclatureCount / countInOnePack);
                    if (countInOnePack != 0 && nomenclatureCount % countInOnePack > 0)
                        {
                        packsCount++;
                        }
                    pallets.Add(packsCount);
                    nomenclatureCount = 0;
                    }
                }

            return pallets;
            }

        private Stickers createSticker(Nomenclature nomenclature, Parties party, int unitsQuantity)
            {
            var sticker = new Stickers();
            sticker.Nomenclature = nomenclature;
            sticker.Quantity = unitsQuantity;
            sticker.Driver = Driver;
            sticker.AcceptionDate = Date;
            sticker.ReleaseDate = party.DateOfManufacture;
            sticker.ExpiryDate = party.TheDeadlineSuitability;
            if (sticker.Write() != WritingResult.Success)
                {
                return null;
                }
            return sticker;
            }


        }
    }