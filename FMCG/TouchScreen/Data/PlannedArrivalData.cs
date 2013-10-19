using System.Collections.Generic;

namespace TouchScreen.Models.Data
{
    /// <summary>План приходу (основні дані для вибору документу)</summary>
    public class AcceptancePlanData : BaseData
        {
        /// <summary>Машина</summary>
        public KeyValuePair<long, string> Car { get; set; }
        /// <summary>Накладна</summary>
        public KeyValuePair<long, string> Invoice { get; set; }
        }
}