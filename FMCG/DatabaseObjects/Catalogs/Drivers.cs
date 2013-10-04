using System;
using Aramis.Attributes;
using Aramis.Core;
using AtosFMCG.DatabaseObjects.Interfaces;
using Catalogs;

namespace Catalogs
    {
    /// <summary>��䳿</summary>
    [Catalog(Description = "��䳿", GUID = "A28EF6BD-E24F-4215-9BBD-848ECF678129")]
    public class Drivers : CatalogTable, ISyncWith1C
        {
        /// <summary>��������� 1�</summary>
        [DataField(Description = "��������� 1�", ShowInList = false)]
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