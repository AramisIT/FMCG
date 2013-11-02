﻿using System;
using System.Collections.Generic;
using Aramis.Attributes;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Core;
using Aramis.SystemConfigurations;
using AtosFMCG.TouchScreen.PalletSticker;
using Catalogs;

namespace Catalogs
    {
    [Catalog(Description = "Этикетки на паллеты", GUID = "A1204B4B-57D3-4771-B78A-E0B868D896D6", DescriptionSize = 75, HierarchicType = HierarchicTypes.None, ShowCreationDate = true, ShowCodeFieldInForm = false, ShowCodeFieldInList = false)]
    public class Stickers : CatalogTable
        {
        [DataField(Description = "Номенклатура", ShowInList = true)]
        public Nomenclature Nomenclature
            {
            get
                {
                return (Nomenclature)GetValueForObjectProperty("Nomenclature");
                }
            set
                {
                SetValueForObjectProperty("Nomenclature", value);
                }
            }

        [DataField(Description = "Піддон", ShowInList = true)]
        public Nomenclature Tray
            {
            get
                {
                return (Nomenclature)GetValueForObjectProperty("Tray");
                }
            set
                {
                SetValueForObjectProperty("Tray", value);
                }
            }

        [DataField(Description = "Водитель", ShowInList = true)]
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

        /// <summary>
        /// Количество упаковок
        /// </summary>
        [DataField(Description = "Количество упаковок", ShowInList = true)]
        public int Quantity
            {
            get
                {
                return z_Quantity;
                }
            set
                {
                if (z_Quantity == value)
                    {
                    return;
                    }

                z_Quantity = value;
                NotifyPropertyChanged("Quantity");
                }
            }
        private int z_Quantity;

        /// <summary>
        /// Количество единиц
        /// </summary>
        [DataField(Description = "Количество единиц", ShowInList = true)]
        public int UnitsQuantity
            {
            get
                {
                return z_UnitsQuantity;
                }
            set
                {
                if (z_UnitsQuantity == value)
                    {
                    return;
                    }

                z_UnitsQuantity = value;
                NotifyPropertyChanged("UnitsQuantity");
                }
            }
        private int z_UnitsQuantity;

        [DataField(Description = "Дата производства", ShowInList = false)]
        public DateTime ReleaseDate
            {
            get
                {
                return z_ReleaseDate;
                }
            set
                {
                if (z_ReleaseDate == value)
                    {
                    return;
                    }
                z_ReleaseDate = value;
                NotifyPropertyChanged("ReleaseDate");
                }
            }
        private DateTime z_ReleaseDate;

        [DataField(Description = "50% истчения срока годности", ShowInList = false, StorageType = StorageTypes.Local)]
        public DateTime HalpExpiryDate
            {
            get
                {
                var bestBeforeDay = (int)((TimeSpan)(ExpiryDate.Date - ReleaseDate.Date)).TotalDays;
                return ReleaseDate.AddDays(bestBeforeDay / 2);
                }
            }

        [DataField(Description = "Дата окончания срока годности", ShowInList = false)]
        public DateTime ExpiryDate
            {
            get
                {
                return z_ExpiryDate;
                }
            set
                {
                if (z_ExpiryDate == value)
                    {
                    return;
                    }
                z_ExpiryDate = value;
                NotifyPropertyChanged("ExpiryDate");
                }
            }
        private DateTime z_ExpiryDate;

        [DataField(Description = "Код этикетки", ReadOnly = true, StorageType = StorageTypes.Local)]
        public long StickerCode
            {
            get
                {
                return z_StickerCode;
                }
            set
                {
                if (z_StickerCode == value)
                    {
                    return;
                    }
                z_StickerCode = value;
                NotifyPropertyChanged("StickerCode");
                }
            }
        private long z_StickerCode;

        [DataField(Description = "Дата приемки", ShowInList = false)]
        public DateTime AcceptionDate
            {
            get
                {
                return z_AcceptionDate;
                }
            set
                {
                if (z_AcceptionDate == value)
                    {
                    return;
                    }
                z_AcceptionDate = value;
                NotifyPropertyChanged("AcceptionDate");
                }
            }
        private DateTime z_AcceptionDate;

        private const string DATE_FORMAT = "dd.MM.yy";
        public string Barcode
            {
            get
                {
                return string.Format("{0}$${1}$${2}$${3}$${4}$${5}$${6}$${7}", Id, Nomenclature.Id, Driver.Id, Quantity,
                    ReleaseDate.ToString(DATE_FORMAT),
                    ExpiryDate.ToString(DATE_FORMAT),
                    AcceptionDate.ToString(DATE_FORMAT),
                    UnitsQuantity
                    );
                }
            }

        protected override void InitItemBeforeShowing()
            {
            base.InitItemBeforeShowing();
            StickerCode = Id;
            }

        public Stickers()
            : base()
            {
            BeforeWriting += Stickers_BeforeWriting;
            }

        void Stickers_BeforeWriting(DatabaseObject item, ref bool cancel)
            {
            if (IsModified)
                {
                Description = string.Format("{0} - {2}; кол-во - {1}", Nomenclature.Description, Quantity, ExpiryDate.ToString(DATE_FORMAT));
                }
            }

        public void Print(DatabaseObject item)
            {
            printSticker(item as Stickers);
            }

        private void printSticker(Stickers sticker)
            {
            var stickersCreator = new StickersPrintingHelper(new List<Stickers>() { sticker }, ThermoPrinters.GetCurrentPrinterName());
            stickersCreator.Print();
            }

        public override Dictionary<string, Action<DatabaseObject>> GetActions()
            {
            var actions = new Dictionary<string, Action<DatabaseObject>>() { { "Друк", Print } };
            return actions;
            }
        }
    }