using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;

namespace Catalogs
    {
    /// <summary>������ ������</summary>
    [Catalog(Description = "���������� �������", GUID = "AAA2F111-74B3-4081-A37B-260DF3DD1BB3", HierarchicType = HierarchicTypes.None, ShowCodeFieldInForm = false)]
    public class PreviousPallets : CatalogTable
        {
        [DataField(Description = "������������� �������", ShowInList = true, Unique = true)]
        public long PalletCode
            {
            get
                {
                return z_PalletCode;
                }
            set
                {
                if (z_PalletCode == value)
                    {
                    return;
                    }

                z_PalletCode = value;
                NotifyPropertyChanged("PalletCode");
                }
            }
        private long z_PalletCode;

        [DataField(Description = "������������� ���������� �������", ShowInList = true)]
        public long PreviousCode
            {
            get
                {
                return z_PreviousCode;
                }
            set
                {
                if (z_PreviousCode == value)
                    {
                    return;
                    }

                z_PreviousCode = value;
                NotifyPropertyChanged("PreviousCode");
                }
            }
        private long z_PreviousCode;
        }
    }