using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;

namespace AtosFMCG.DatabaseObjects.Catalogs
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
        }
    }