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
        }
    }
