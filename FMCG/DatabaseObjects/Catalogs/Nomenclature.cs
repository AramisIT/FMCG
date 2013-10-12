using System;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using AtosFMCG.DatabaseObjects.Interfaces;

namespace Catalogs
    {
    /// <summary>������������</summary>
    [Catalog(Description = "������������", GUID = "A29CEC12-B241-4574-82BE-F6EACE352E24", HierarchicType = HierarchicTypes.GroupsAndElements, ShowCodeFieldInForm = false, DescriptionSize = 100)]
    public class Nomenclature : CatalogTable, ISyncWith1C
        {
        #region Properties
        /// <summary>��������� 1�</summary>
        [DataField(Description = "��������� 1�", ShowInList = false, ShowInForm = false)]
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

        [DataField(Description = "ʳ��-�� � �����", ShowInList = true)]
        public int UnitsQuantityPerPallet
            {
            get
                {
                return z_UnitsQuantityPerPallet;
                }
            set
                {
                if (z_UnitsQuantityPerPallet == value)
                    {
                    return;
                    }

                z_UnitsQuantityPerPallet = value;
                NotifyPropertyChanged("UnitsQuantityPerPallet");
                }
            }
        private int z_UnitsQuantityPerPallet;

        [DataField(Description = "ʳ��-�� � ��������", ShowInList = true)]
        public int UnitsQuantityPerPack
            {
            get
                {
                return z_UnitsQuantityPerPack;
                }
            set
                {
                if (z_UnitsQuantityPerPack == value)
                    {
                    return;
                    }

                z_UnitsQuantityPerPack = value;
                NotifyPropertyChanged("UnitsQuantityPerPack");
                }
            }
        private int z_UnitsQuantityPerPack;

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