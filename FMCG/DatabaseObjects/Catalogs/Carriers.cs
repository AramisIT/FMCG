using System;
using Aramis.Attributes;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Core;
using Aramis.SystemConfigurations;
using AtosFMCG.DatabaseObjects.Interfaces;
using Catalogs;

namespace Catalogs
    {
    [Catalog(Description = "Перевізники", GUID = "ACA2C7F1-692B-4669-BEB6-C29D7A1789C6", DescriptionSize = 35, HierarchicType = HierarchicTypes.GroupsAndElements)]
    public class Carriers : CatalogTable, ISyncWith1C
        {
        /// <summary>Посилання 1С</summary>
        [DataField(Description = "Посилання 1С", ShowInList = false)]
        public Guid Ref1C
            {
            get
                {
                return z_Ref1C;
                }
            set
                {
                if (z_Ref1C == value)
                    {
                    return;
                    }
                z_Ref1C = value;
                NotifyPropertyChanged("Ref1C");
                }
            }
        private Guid z_Ref1C;

        public Carriers()
            : base()
            {

            }
        }
    }