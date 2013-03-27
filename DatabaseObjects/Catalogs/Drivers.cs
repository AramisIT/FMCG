using Aramis.Attributes;
using Aramis.Core;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>Водії</summary>
    [Catalog(Description = "Водії", GUID = "A28EF6BD-E24F-4215-9BBD-848ECF678129")]
    public class Drivers: CatalogTable
        {
        /// <summary>Перевізник</summary>
        [DataField(Description = "Перевізник", ShowInList = true)]
        public Contractors Currier
            {
                get
                    {
                        return (Contractors)GetValueForObjectProperty("Currier");
                    }
                set
                    {
                        SetValueForObjectProperty("Currier", value);
                    }
            }
        }
    }