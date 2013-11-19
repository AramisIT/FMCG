using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client.HelperClasses
    {
    public class BarcodeData
        {
        public long StickerId { get; set; }

        public CatalogItem Nomenclature { get; set; }

        public CatalogItem Tray { get; set; }

        public int UnitsQuantity { get; set; }

        public int UnitsPerBox { get; set; }

        public CatalogItem Liner { get; set; }

        public byte LinersAmount { get; set; }

        public CatalogItem Cell { get; set; }

        public long PreviousStickerCode { get; set; }

        public BarcodeData GetCopy()
            {
            return new BarcodeData()
                {
                    StickerId = StickerId,
                    Nomenclature = Nomenclature.GetCopy(),
                    Tray = Tray.GetCopy(),
                    UnitsQuantity = UnitsQuantity,
                    UnitsPerBox = UnitsPerBox,
                    Liner = Liner.GetCopy(),
                    LinersAmount = LinersAmount,
                    Cell = Cell.GetCopy(),
                    PreviousStickerCode = PreviousStickerCode
                };
            }
        }
    }
