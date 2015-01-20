using Aramis;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using AramisInfostructure.Core;
using Catalogs;

namespace Catalogs
    {
    /// <summary>Зайняті комірки</summary>
    [Catalog(Description = "Класифікатор одиниць виміру", GUID = "06906080-CA40-44F8-91A0-F2C700F44739", HierarchicType = HierarchicTypes.None, ShowCodeFieldInForm = false)]
    public class ClassifierUnits : CatalogTable
        {
        /// <summary>Скорочення</summary>
        [DataField(Description = "Скорочення", ShowInList = true)]
        public string CutName
            {
            get
                {
                return z_CutName;
                }
            set
                {
                if (z_CutName == value)
                    {
                    return;
                    }

                z_CutName = value;
                NotifyPropertyChanged("CutName");
                }
            }
        private string z_CutName = string.Empty;

        #region Предопределенные элементы
        private const string CATALOG_NAME = "ClassifierUnits";

        /// <summary>Ящик</summary>
        public static IDBObjectRef Box
            {
            get
                {
                return z_Box ?? (z_Box = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "Ящик"));
                }
            }
        private static IDBObjectRef z_Box;

        /// <summary>
        /// Бутилка
        /// </summary>
        public static IDBObjectRef Bottle
            {
            get
                {
                return z_Bottle ?? (z_Bottle = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "Бутилка"));
                }
            }
        private static IDBObjectRef z_Bottle;

        /// <summary>
        ///  Паллета 
        /// </summary>
        public static IDBObjectRef Pallet
            {
            get
                {
                return z_Pallet ?? (z_Pallet = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "Паллета"));
                }
            }
        private static IDBObjectRef z_Pallet;
        #endregion
        }
    }