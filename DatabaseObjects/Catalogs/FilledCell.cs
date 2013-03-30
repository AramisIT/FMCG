using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>Зайняті комірки</summary>
    [Catalog(Description = "Зайняті комірки", GUID = "AAA2F111-74B3-4081-A37B-260DF3DD1BB3", HierarchicType = HierarchicTypes.None, ShowCodeFieldInForm = false)]
    public class FilledCell : CatalogTable
        {
        #region Properties
        /// <summary>ID коду товару</summary>
        [DataField(Description = "ID коду товару", ShowInList = true, Unique = true)]
        public long GoodsCode
            {
            get
                {
                return z_GoodsCode;
                }
            set
                {
                if (z_GoodsCode == value)
                    {
                    return;
                    }

                z_GoodsCode = value;
                NotifyPropertyChanged("GoodsCode");
                }
            }
        private long z_GoodsCode;

        /// <summary>ІД батьківського коду товару</summary>
        [DataField(Description = "ІД батьківського коду товару", ShowInList = true)]
        public long ParentCode
            {
            get
                {
                return z_ParentCode;
                }
            set
                {
                if (z_ParentCode == value)
                    {
                    return;
                    }

                z_ParentCode = value;
                NotifyPropertyChanged("ParentCode");
                }
            }
        private long z_ParentCode;
        #endregion
        }
    }