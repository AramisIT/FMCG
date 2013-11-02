namespace Catalogs
    {
    /// <summary>Константи</summary>
    public class Consts : SystemConsts
        {
        #region Процеси
        /// <summary>Дозволити карщику встановлювати паллети самостійно</summary>
        public static bool PermitInstallPalletManually
            {
            get
                {
                lock (locker)
                    {
                    return z_PermitInstallPalletManually;
                    }
                }
            set
                {
                lock (locker)
                    {
                    if (z_PermitInstallPalletManually != value)
                        {
                        z_PermitInstallPalletManually = value;
                        NotifyPropertyChanged("PermitInstallPalletManually");
                        }
                    }
                }
            }
        private static bool z_PermitInstallPalletManually;
        #endregion

        #region Налаштування ТСД
        /// <summary>IP серверу для роботи ТСД</summary>
        public static string ServerIP
            {
            get
                {
                lock (locker)
                    {
                    return z_ServerIP;
                    }
                }
            set
                {
                lock (locker)
                    {
                    if (z_ServerIP != value)
                        {
                        z_ServerIP = value;
                        NotifyPropertyChanged("ServerIP");
                        }
                    }
                }
            }
        private static string z_ServerIP;

        /// <summary>Папка обміну</summary>
        public static string UpdateFolderName
            {
            get
                {
                lock (locker)
                    {
                    return z_UpdateFolderName;
                    }
                }
            set
                {
                lock (locker)
                    {
                    if (z_UpdateFolderName != value)
                        {
                        z_UpdateFolderName = value;
                        NotifyPropertyChanged("UpdateFolderName");
                        }
                    }
                }
            }
        private static string z_UpdateFolderName;
        #endregion

        /// <summary>
        /// Ячейка "Выкуп - буфер"
        /// </summary>
        public static Cells RedemptionCell
            {
            get
                {
                lock (locker)
                    {
                    return (Cells)GetValueForObjectProperty("RedemptionCell");
                    }
                }
            set
                {
                lock (locker)
                    {
                    SetValueForObjectProperty("RedemptionCell", value);
                    }
                }
            }


        #region Tare
        
        /// <summary>
        /// Стандартный поддон
        /// </summary>
        public static Nomenclature StandartTray
            {
            get
                {
                lock (locker)
                    {
                    return (Nomenclature)GetValueForObjectProperty("StandartTray");
                    }
                }
            set
                {
                lock (locker)
                    {
                    SetValueForObjectProperty("StandartTray", value);
                    }
                }
            }

        /// <summary>
        /// Нестандартный поддон (обычно евро)
        /// </summary>
        public static Nomenclature NonStandartTray
            {
            get
                {
                lock (locker)
                    {
                    return (Nomenclature)GetValueForObjectProperty("NonStandartTray");
                    }
                }
            set
                {
                lock (locker)
                    {
                    SetValueForObjectProperty("NonStandartTray", value);
                    }
                }
            }

        /// <summary>
        /// Стандартная прокладка
        /// </summary>
        public static Nomenclature StandartLiner
            {
            get
                {
                lock (locker)
                    {
                    return (Nomenclature)GetValueForObjectProperty("StandartLiner");
                    }
                }
            set
                {
                lock (locker)
                    {
                    SetValueForObjectProperty("StandartLiner", value);
                    }
                }
            }

        /// <summary>
        /// Нестандартная прокладка
        /// </summary>
        public static Nomenclature NonStandartLiner
            {
            get
                {
                lock (locker)
                    {
                    return (Nomenclature)GetValueForObjectProperty("NonStandartLiner");
                    }
                }
            set
                {
                lock (locker)
                    {
                    SetValueForObjectProperty("NonStandartLiner", value);
                    }
                }
            } 

        #endregion
        }
    }
