using Aramis.Attributes;
using Aramis.Core;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>Типи комірок</summary>
    [Catalog(Description = "Типи комірок", GUID = "A54A0C77-9677-4740-B3EC-5B9DB6EB3749")]
    public class TypesOfCell : CatalogTable
        {
        #region Properties
        /// <summary>Ширина, см</summary>
        [DataField(Description = "Ширина, см", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
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

        /// <summary>Висота, см</summary>
        [DataField(Description = "Висота, см", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
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

        /// <summary>Глубина, см</summary>
        [DataField(Description = "Глубина, см", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
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

        /// <summary>Допустима вага</summary>
        [DataField(Description = "Допустима вага", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
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

        /// <summary>Кількість паллетомісць</summary>
        [DataField(Description = "Кількість паллетомісць", ShowInList = true)]
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

        /// <summary>Віртуальна комірка</summary>
        [DataField(Description = "Віртуальна комірка", ShowInList = true)]
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