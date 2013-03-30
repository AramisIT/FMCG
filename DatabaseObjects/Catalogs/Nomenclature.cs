using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>������������</summary>
    [Catalog(Description = "������������", GUID = "A29CEC12-B241-4574-82BE-F6EACE352E24", HierarchicType = HierarchicTypes.None, ShowCodeFieldInForm = false)]
    public class Nomenclature : CatalogTable
        {
        #region Properties
        /// <summary>����</summary>
        [DataField(Description = "����", ShowInList = true)]
        public bool IsTare
            {
            get
                {
                return z_IsTare;
                }
            set
                {
                if (z_IsTare == value)
                    {
                    return;
                    }

                z_IsTare = value;
                NotifyPropertyChanged("IsTare");
                }
            }
        private bool z_IsTare;

        /// <summary>����� ���������� (���)</summary>
        [DataField(Description = "����� ���������� (���)", ShowInList = true)]
        public int ShelfLife
            {
            get
                {
                return z_ShelfLife;
                }
            set
                {
                if (z_ShelfLife == value)
                    {
                    return;
                    }

                z_ShelfLife = value;
                NotifyPropertyChanged("ShelfLife");
                }
            }
        private int z_ShelfLife;
        #endregion
        }
    }