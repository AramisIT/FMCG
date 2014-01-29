using System;

namespace TouchScreen.Models.Data
    {
    public class NomenclatureData
        {
        private int quantity;

        public long LineNumber { get; set; }
        public ObjectValue Description { get; set; }

        public int Quantity
            {
            get
                {
                return quantity;
                }
            set
                {
                quantity = value;
                }
            }
        public DateTime Date { get; set; }
        public int ShelfLifeDays { get; set; }

        public int StandartPalletsCount { get; set; }
        public int NonStandartPalletsCount { get; set; }
        public int UnitsAmountInOneStandartPallet { get; set; }
        public int UnitsAmountInOneNonStandartPallet { get; set; }
        public int UnitsOnNotFullPallet { get; set; }
        public int UnitsOnNotFullNonStandartPallet { get; set; }

        public static NomenclatureData ZeroValue
            {
            get
                {
                return new NomenclatureData() { Description = new ObjectValue(string.Empty, 0) };
                }
            }

        public void UpdateQuantity()
            {
            Quantity = StandartPalletsCount * UnitsAmountInOneStandartPallet + UnitsOnNotFullPallet
                       + NonStandartPalletsCount * UnitsAmountInOneNonStandartPallet + UnitsOnNotFullNonStandartPallet;
            }

        public void UpdatePalletQuantity()
            {
            NonStandartPalletsCount = 0;
            UnitsOnNotFullNonStandartPallet = 0;
            StandartPalletsCount = UnitsAmountInOneStandartPallet == 0 ? 0 : Quantity / UnitsAmountInOneStandartPallet;
            UnitsOnNotFullPallet = Quantity - StandartPalletsCount * UnitsAmountInOneStandartPallet;
            }
        }
    }