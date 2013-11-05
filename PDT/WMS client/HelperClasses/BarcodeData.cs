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
        }
    }
