using System.Collections.Generic;

namespace WMS_client.Processes
    {
    public class AcceptanceData : IProcessData
        {
        /// <summary>Товар</summary>
        public KeyValuePair<long, string> Goods { get; set; }
        /// <summary>Розміщення паллети</summary>
        /// <summary>Встановлюється в комірку?</summary>
        public bool IsCell { get; set; }
        /// <summary>Id попередньої палети (комірки)</summary>
        public PlaningData<long> Place { get; set; }
        /// <summary>Комірка</summary>
        public KeyValuePair<long, string> Cell { get; set; }
        /// <summary>Дата виготовлення</summary>
        public string Date { get; set; }
        /// <summary>Заголовок процесу</summary>
        public string Topic { get; set; }
        /// <summary>Штрихкод користувача</summary>
        public string UserBarcode { get; set; }
        /// <summary>Id машини</summary>
        public long Car { get; set; }
        /// <summary>Id партії</summary>
        public long Party { get; set; }
        /// <summary>Id документу прийомки</summary>
        public long ConsignmentNote { get; set; }
        /// <summary>К-сть ящиків</summary>
        public double BoxCount { get; set; }
        /// <summary>К-сть бутилок</summary>
        public double BottleCount { get; set; }
        /// <summary>Дозволити встановлювати паллети вручну</summary>
        public bool PermitInstallPalletManually { get; set; }

        public AcceptanceData()
            {
            Topic = "Приймання";
            }
        }
    }