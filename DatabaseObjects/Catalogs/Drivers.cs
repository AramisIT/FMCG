using Aramis.Attributes;
using Aramis.Core;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>��䳿</summary>
    [Catalog(Description = "��䳿", GUID = "A28EF6BD-E24F-4215-9BBD-848ECF678129")]
    public class Drivers: CatalogTable
        {
        /// <summary>���������</summary>
        [DataField(Description = "���������", ShowInList = true)]
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