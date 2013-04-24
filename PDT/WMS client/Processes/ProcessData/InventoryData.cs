using System.Collections.Generic;
namespace WMS_client.Processes
    {
    public class InventoryData : IProcessData
        {
        private const int MAX_NUMBER_OF_ATTEMPTS = 2;

        public int AttemptsRemaining { get { return MAX_NUMBER_OF_ATTEMPTS - NumberOfAttempts; } }
        public int NumberOfAttempts { get; set; }
        public bool CanRepeatAttempt { get { return NumberOfAttempts < MAX_NUMBER_OF_ATTEMPTS; } }
        public PlaningData<double> Count { get; set; } 
        public string Goods { get; set; }
        public string Date { get; set; }
        public long PalletId { get; set; }
        public string Measure { get; set; }
        public KeyValuePair<long, long> DocInfo { get; set; }
        public KeyValuePair<long, string> Cell { get; set; }
        /// <summary>Заголовок процесу</summary>
        public string Topic { get; set; }

        public InventoryData(){}

        public InventoryData(string topic)
            {
            Topic = topic;
            }
        }
    }