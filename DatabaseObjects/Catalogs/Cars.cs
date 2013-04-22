using System;
using Aramis.Attributes;
using Aramis.Core;
using AtosFMCG.DatabaseObjects.Interfaces;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>Машини</summary>
    [Catalog(Description = "Машини", GUID = "A44B32E2-FF6F-4F2B-867F-83C99817609B")]
    public class Cars : CatalogTable, ISyncWith1C
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
        }
    }