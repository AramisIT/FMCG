using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>������ ������</summary>
    [Catalog(Description = "������ ������", GUID = "AAA2F111-74B3-4081-A37B-260DF3DD1BB3", HierarchicType = HierarchicTypes.None, ShowCodeFieldInForm = false)]
    public class FilledCell : CatalogTable
        {
        #region Properties
        /// <summary>�� ������</summary>
        [DataField(Description = "�� ������", ShowInList = true, Unique = true)]
        public long CellId
            {
            get
                {
                return z_CellId;
                }
            set
                {
                if (z_CellId == value)
                    {
                    return;
                    }

                z_CellId = value;
                NotifyPropertyChanged("CellId");
                }
            }
        private long z_CellId;

        /// <summary>�� ������</summary>
        [DataField(Description = "�� ������", ShowInList = true)]
        public long ParentCellId
            {
            get
                {
                return z_ParentCellId;
                }
            set
                {
                if (z_ParentCellId == value)
                    {
                    return;
                    }

                z_ParentCellId = value;
                NotifyPropertyChanged("ParentCellId");
                }
            }
        private long z_ParentCellId;
        #endregion
        }
    }