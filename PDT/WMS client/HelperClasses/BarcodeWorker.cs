using System;

namespace WMS_client.HelperClasses
    {
    /// <summary>Робота зі штрихкодами</summary>
    public static class BarcodeWorker
        {


        private const char PALLET_PREFIX = '_';

        /// <summary>Чи являється строка валідним штрихкодом</summary>
        /// <param name="barcode">Строка</param>
        public static bool IsValidBarcode(this string barcode)
            {
            return ValidBarcode(barcode);
            }

        /// <summary>Чи являється строка валідним штрихкодом</summary>
        /// <param name="barcode">Строка</param>
        public static bool ValidBarcode(string barcode)
            {
            return true;
            }

        public static bool IsCell(this string barcode)
            {
            return barcode.Length >= 5 && barcode.StartsWith("C.");
            }

        public static bool IsSticker(this string barcode)
            {
            if (string.IsNullOrEmpty(barcode))
                {
                return false;
                }
            barcode = barcode.Replace("$$", "$");
            string[] values = barcode.Split('$');

            return values.Length > 1;
            }

        enum BarcodeDataIndexes
            {
            StickerId,
            NomenclatureId,
            DriverId,
            BoxesQuantity,
            ReleaseDate,
            ExpiryDate,
            AcceptionDate,
            UnitsQuantity,
            //UnitsPerBox,
            //NomenclatureDescription,
            //TrayId,
            //TrayDescription,

            IndexesCount
            }

        public static CatalogItem ToCell(this string barcode)
            {
            var cellInfo = barcode.Substring(2).Split(';');

            var cell = new CatalogItem() {Description = cellInfo[1], Id = Convert.ToInt64(cellInfo[0])};
            return cell;
            }

        public static BarcodeData ToBarcodeData(this string barcode)
            {
            var result = new BarcodeData();
            if (string.IsNullOrEmpty(barcode))
                {
                return result;
                }
            barcode = barcode.Replace("$$", "$");

            string[] values = barcode.Split('$');
            if (values.Length < 1)
                {
                return result;
                }

            result.StickerId = Convert.ToInt64(values[(int)BarcodeDataIndexes.StickerId]);

            if (values.Length < (int)BarcodeDataIndexes.IndexesCount)
                {
                return result;
                }

            result.TotalUnitsQuantity = Convert.ToInt32(values[(int)BarcodeDataIndexes.UnitsQuantity]);

            result.Nomenclature = new CatalogItem()
            {
                Id = Convert.ToInt64(values[(int)BarcodeDataIndexes.NomenclatureId])
            };

            return result;
            }

        public static bool GetPalletId(string barcode, out long id)
            {
            if (!string.IsNullOrEmpty(barcode) && barcode[0] == PALLET_PREFIX)
                {
                string idString = barcode.Substring(1, barcode.Length - 1);

                try
                    {
                    id = long.Parse(idString);
                    }
                catch
                    {
                    id = 0;
                    }

                return true;
                }

            id = 0;
            return false;
            }
        }
    }