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
        }
    }
