using Aramis.Attributes;
using Aramis.Core;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>Міста</summary>
    [Catalog(Description = "Міста", GUID = "A8A04E8E-176A-424E-A2C7-9BC00DCE9FA7")]
    public class Cities : CatalogTable
        {
        /// <summary>Країна</summary>
        [DataField(Description = "Країна", ShowInList = true)]
        public Countries Country
            {
            get
                {
                return (Countries)GetValueForObjectProperty("Country");
                }
            set
                {
                SetValueForObjectProperty("Country", value);
                }
            }
        }
    }