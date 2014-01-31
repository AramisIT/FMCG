using System;
using Aramis.Attributes;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Core;
using Aramis.SystemConfigurations;
using Catalogs;

namespace Catalogs
    {
    [Catalog(Description = "Штрих-кода продукции", GUID = "ADBFC943-1918-48EE-BCB1-8E34E39C6704", DescriptionSize = 20, HierarchicType = HierarchicTypes.None)]
    public class Barcodes : CatalogTable
        {
        [DataField(Description = "Номенклатура", ShowInList = true)]
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

        public Barcodes()
            : base()
            {

            }
        }
    }