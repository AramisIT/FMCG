using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client.HelperClasses
    {
    public class BarcodeData
        {
        public BarcodeData()
            {
            Nomenclature = new CatalogItem();
            Party = new CatalogItem();
            Cell = new CatalogItem();
            Tray = new CatalogItem();
            Liner = new CatalogItem();
            }

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

        public CatalogItem Nomenclature { get; private set; }

        public CatalogItem Tray { get; private set; }

        public CatalogItem Liner { get; private set; }

        /// <summary>
        /// Total amount of bottles, boxes, etc. 
        /// </summary>
        public int TotalUnitsQuantity { get; set; }

        public int UnitsPerBox { get; set; }

        public byte LinersAmount { get; set; }

        public bool HasLiners
            {
            get
                {
                return LinersAmount > 0 && Liner.Id > 0;
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

        public CatalogItem Cell { get; private set; }

        public CatalogItem Party { get; private set; }

        public long PreviousStickerCode { get; set; }

        public bool Empty
            {
            get
                {
                return Nomenclature.Id == 0;
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
            long nomenclatureId;
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
            int totalUnitsQuantity;
            DateTime productionDate;
            if (
                !new ServerInteraction().GetPalletBalance(StickerId,
                    out nomenclatureId, out nomenclatureDescription, out trayId, out linerId, out linersAmount,
                    out unitsPerBox, out cellId, out cellDescription, out previousPalletCode, out productionDate, out partyId,
                    out totalUnitsQuantity))
                {
                StickerId = 0;
                return false;
                }

            PreviousStickerCode = previousPalletCode;

            TotalUnitsQuantity = totalUnitsQuantity;
            Nomenclature.Id = nomenclatureId;
            Nomenclature.Description = nomenclatureDescription;

            Tray.Id = trayId;
            Tray.Description = new Repository().GetTrayDescription(trayId);

            Liner.Id = linerId;
            Liner.Description = new Repository().GetLinerDescription(linerId);

            Party.Id = partyId;
            Party.Description = productionDate.ToStandartString();

            LinersAmount = linersAmount;

            UnitsPerBox = Convert.ToInt32(unitsPerBox);

            Cell.Id = cellId;
            Cell.Description = cellDescription;

            return true;
            }

        public bool LocatedIdCell
            {
            get { return Cell != null && !Cell.Empty; }
            }
        }
    }
