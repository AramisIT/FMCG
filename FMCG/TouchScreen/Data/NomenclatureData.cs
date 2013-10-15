using System;

namespace TouchScreen.Models.Data
    {
    public class NomenclatureData
        {
        public long LineNumber { get; set; }
        public ObjectValue Description { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
        public int ShelfLifeDays { get; set; }

        public int StandartPalletsCount { get; set; }
        public int NonStandartPalletsCount { get; set; }
        public int StandartPalletCountPer1 { get; set; }
        public int NonStandartPalletCountPer1 { get; set; }
        public int UnitsOnNotFullPallet { get; set; }
        public int UnitsOnNotFullNonStandartPallet { get; set; }


        public void UpdateQuantity()
            {
            Quantity = StandartPalletsCount*StandartPalletCountPer1 + UnitsOnNotFullPallet
                       + NonStandartPalletsCount*NonStandartPalletCountPer1 + UnitsOnNotFullNonStandartPallet;
            }
        }
    }