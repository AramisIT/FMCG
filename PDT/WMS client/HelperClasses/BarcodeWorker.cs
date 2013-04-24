namespace WMS_client.HelperClasses
{
    /// <summary>������ � �����������</summary>
    public static class BarcodeWorker
        {
        private const char PALLET_PREFIX = '_';

        /// <summary>�� ��������� ������ ������� ����������</summary>
        /// <param name="barcode">������</param>
        public static bool IsValidBarcode(this string barcode)
        {
            return ValidBarcode(barcode);
        }

        /// <summary>�� ��������� ������ ������� ����������</summary>
        /// <param name="barcode">������</param>
        public static bool ValidBarcode(string barcode)
        {
            return true;
        }

        public static bool GetPalletId(string barcode, out long id)
            {
            if(!string.IsNullOrEmpty(barcode) && barcode[0] == PALLET_PREFIX)
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