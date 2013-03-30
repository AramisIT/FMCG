using Aramis.Attributes;
using Aramis.Core;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>Одиниці виміру</summary>
    [Catalog(Description = "Одиниці виміру", GUID = "AAA9608E-2F23-4BB5-98C6-78BC782AE4DD", ShowCodeFieldInForm = false)]
    public class Measures : CatalogTable
        {
        /// <summary>Власник</summary>
        [DataField(Description = "Власник", ShowInList = true)]
        public Nomenclature Nomenclature
            {
            get
                {
                return (Nomenclature)GetValueForObjectProperty("Nomenclature");
                }
            set
                {
                SetValueForObjectProperty("Nomenclature", value);
                }
            }

        /// <summary>Номенклатура (тари)</summary>
        [DataField(Description = "Номенклатура (тари)", ShowInList = true)]
        public Nomenclature Tare
            {
            get
                {
                return (Nomenclature)GetValueForObjectProperty("Tare");
                }
            set
                {
                SetValueForObjectProperty("Tare", value);
                }
            }

        /// <summary>Кількість базових одиниць</summary>
        [DataField(Description = "Кількість базових одиниць", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public double BaseCount
            {
            get
                {
                return z_BaseCount;
                }
            set
                {
                if (z_BaseCount == value)
                    {
                    return;
                    }

                z_BaseCount = value;
                NotifyPropertyChanged("BaseCount");
                }
            }
        private double z_BaseCount;
        }
    }