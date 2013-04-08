using Aramis.Attributes;
using Aramis.Core;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>���� ������</summary>
    [Catalog(Description = "���� ������", GUID = "A54A0C77-9677-4740-B3EC-5B9DB6EB3749")]
    public class TypesOfCell : CatalogTable
        {
        #region Properties
        /// <summary>������, ��</summary>
        [DataField(Description = "������, ��", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public double Width
            {
            get
                {
                return z_Width;
                }
            set
                {
                if (z_Width == value)
                    {
                    return;
                    }

                z_Width = value;
                NotifyPropertyChanged("Width");
                }
            }
        private double z_Width;

        /// <summary>������, ��</summary>
        [DataField(Description = "������, ��", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public double Height
            {
            get
                {
                return z_Height;
                }
            set
                {
                if (z_Height == value)
                    {
                    return;
                    }

                z_Height = value;
                NotifyPropertyChanged("Height");
                }
            }
        private double z_Height;

        /// <summary>�������, ��</summary>
        [DataField(Description = "�������, ��", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public double Depth
            {
            get
                {
                return z_Depth;
                }
            set
                {
                if (z_Depth == value)
                    {
                    return;
                    }

                z_Depth = value;
                NotifyPropertyChanged("Depth");
                }
            }
        private double z_Depth;

        /// <summary>��������� ����</summary>
        [DataField(Description = "��������� ����", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public double AllowableWeight
            {
            get
                {
                return z_AllowableWeight;
                }
            set
                {
                if (z_AllowableWeight == value)
                    {
                    return;
                    }

                z_AllowableWeight = value;
                NotifyPropertyChanged("AllowableWeight");
                }
            }
        private double z_AllowableWeight;

        /// <summary>ʳ������ �����������</summary>
        [DataField(Description = "ʳ������ �����������", ShowInList = true)]
        public int NumberOfPallets
            {
            get
                {
                return z_NumberOfPallets;
                }
            set
                {
                if (z_NumberOfPallets == value)
                    {
                    return;
                    }

                z_NumberOfPallets = value;
                NotifyPropertyChanged("NumberOfPallets");
                }
            }
        private int z_NumberOfPallets;

        /// <summary>³�������� ������</summary>
        [DataField(Description = "³�������� ������", ShowInList = true)]
        public bool IsVirtual
            {
            get
                {
                return z_IsVirtual;
                }
            set
                {
                if (z_IsVirtual == value)
                    {
                    return;
                    }

                z_IsVirtual = value;
                NotifyPropertyChanged("IsVirtual");
                }
            }
        private bool z_IsVirtual; 
        #endregion
        }
    }