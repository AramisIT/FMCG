using System;
using System.Collections.Generic;

namespace TouchScreen.Models.Data
{
    /// <summary>План приходу</summary>
    public class PlannedArrivalData : BaseData
        {
        /// <summary>Машина</summary>
        public KeyValuePair<long, string> Car { get; set; }
        /// <summary>Накладна</summary>
        public KeyValuePair<long, string> Invoice { get; set; }
        }

    public class NomenclatureData
        {
        public string LineNumber { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
        }
}