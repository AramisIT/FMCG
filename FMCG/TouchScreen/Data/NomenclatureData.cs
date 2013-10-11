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
        }
    }