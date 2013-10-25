using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtosFMCG.TouchScreen.PalletSticker
    {
    class StickerInfo
        {
        public string Nomenclature { get; set; }
        public int PacksCount { get; set; }
        public int UnitsQuantity { get; set; }
        public string Barcode { get; set; }


        public DateTime ReleaseDate { get; set; }
        public DateTime HalpExpiryDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public DateTime AcceptionDate { get; set; }
        public string Driver { get; set; }

        public long Id { get; set; }
        }
    }
