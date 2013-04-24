namespace WMS_client.Processes
    {
    public class MovementData : IProcessData
        {
        public string GoodsDescription { get; set; }
        public string Date { get; set; }
        public double BoxCount { get; set;}
        public double BottleCount { get; set; }
        public long PalletId { get; set; }
        public long PreviousPalletId { get; set; }
        public bool IsCell { get; set; }
        /// <summary>Заголовок процесу</summary>
        public string Topic { get; set; }
        }
    }