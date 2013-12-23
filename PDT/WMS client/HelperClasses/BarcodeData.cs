using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client.HelperClasses
    {
    public class BarcodeData
        {

        public int UnitsRemainder
            {
            get
                {
                return UnitsPerBox > 0 ? TotalUnitsQuantity % UnitsPerBox : TotalUnitsQuantity;
                }
            }

        public int FullPacksCount
            {
            get
                {
                return UnitsPerBox > 0 ? TotalUnitsQuantity / UnitsPerBox : 0;
                }
            }

        public int ExpectualPacksCount
            {
            get
                {
                return TotalUnitsQuantity / UnitsPerBox
                    + (((TotalUnitsQuantity % UnitsPerBox) > 0) ? 1 : 0);
                }
            }

        public long StickerId { get; set; }

        public CatalogItem Nomenclature { get; set; }

        public CatalogItem Tray { get; set; }

        /// <summary>
        /// Total amount of bottles, boxes, etc. 
        /// </summary>
        public int TotalUnitsQuantity { get; set; }

        public int UnitsPerBox { get; set; }

        public CatalogItem Liner { get; set; }

        public byte LinersAmount { get; set; }

        public bool HasLiners
            {
            get
                {
                return LinersAmount > 0
                       && Liner != null
                       && Liner.Id != 0;
                }
            }

        public bool HasTray
            {
            get
                {
                return Tray != null
                       && Tray.Id != 0;
                }
            }

        public CatalogItem Cell { get; set; }

        public CatalogItem Party { get; set; }

        public long PreviousStickerCode { get; set; }

        public bool Empty
            {
            get
                {
                return Nomenclature == null || Nomenclature.Id == 0;
                }
            }

        public BarcodeData GetCopy()
            {
            return new BarcodeData()
                {
                    StickerId = StickerId,
                    Nomenclature = Nomenclature.GetCopy(),
                    Tray = Tray.GetCopy(),
                    TotalUnitsQuantity = TotalUnitsQuantity,
                    UnitsPerBox = UnitsPerBox,
                    Liner = Liner.GetCopy(),
                    LinersAmount = LinersAmount,
                    Cell = Cell.GetCopy(),
                    Party = Party.GetCopy(),
                    PreviousStickerCode = PreviousStickerCode
                };
            }

        public DateTime ProductionDate
            {
            get
                {
                return Party == null ? DateTime.MinValue : Party.Description.ToDateTime();
                }
            }

        public bool SameWare(BarcodeData barcodeData)
            {
            return SameWare(barcodeData, true);
            }

        public bool SameWare(BarcodeData barcodeData, bool checkParty)
            {
            if (Nomenclature == null || barcodeData.Nomenclature == null) return false;

            if (barcodeData.Nomenclature.Id != Nomenclature.Id) return false;

            if (checkParty)
                {
                if (!barcodeData.ProductionDate.Equals(ProductionDate)) return false;
                }

            return true;
            }

        public bool ReadStickerInfo()
            {
            string nomenclatureDescription;
            string trayDescription;
            long trayId;
            long linerId;
            byte linersAmount;
            int unitsPerBox;
            string cellDescription;
            long cellId;
            long previousPalletCode;
            long partyId;
            DateTime productionDate;
            if (
                !new ServerInteraction().GetPalletBalance(StickerId,
                    out nomenclatureDescription, out trayId, out linerId, out linersAmount,
                    out unitsPerBox, out cellId, out cellDescription, out previousPalletCode, out productionDate, out partyId))
                {
                Cell = new CatalogItem();
                StickerId = 0;
                return false;
                }

            PreviousStickerCode = previousPalletCode;
            if (Nomenclature == null)
                {
                Nomenclature = new CatalogItem();
                }
            Nomenclature.Description = nomenclatureDescription;
            Tray = new CatalogItem()
            {
                Id = trayId,
                Description = new Repository().GetTrayDescription(trayId)
            };

            Liner = new CatalogItem()
            {
                Id = linerId,
                Description = new Repository().GetLinerDescription(linerId)
            };

            Party = new CatalogItem() { Id = partyId, Description = productionDate.ToStandartString() };
            LinersAmount = linersAmount;

            UnitsPerBox = Convert.ToInt32(unitsPerBox);
            Cell = new CatalogItem() { Description = cellDescription, Id = cellId };

            return true;
            }
        }
    }
