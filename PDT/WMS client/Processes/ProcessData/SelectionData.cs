using System.Collections.Generic;

namespace WMS_client.Processes
    {
    public class SelectionData : IProcessData
        {
        public int BoxSize { get; set; }
        public string GoodsDescription { get; set; }
        public string Date { get; set; }
        public string Cell { get; set; }
        public double BoxCount { get; set; }
        public double UnitCount { get; set; }
        public long PalletId { get; set; }
        public KeyValuePair<long, string> Contractor { get; set; }
        public long DocId { get; set; }
        /// <summary>Заголовок процесу</summary>
        public string Topic { get; set; }

        public SelectionData(){}

        public SelectionData(string topic)
            {
            Topic = topic;
            }
        }
    }