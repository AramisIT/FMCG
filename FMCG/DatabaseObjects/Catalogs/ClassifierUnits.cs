using Aramis;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using AramisInfostructure.Core;
using Catalogs;

namespace Catalogs
    {
    /// <summary>������ ������</summary>
    [Catalog(Description = "������������ ������� �����", GUID = "06906080-CA40-44F8-91A0-F2C700F44739", HierarchicType = HierarchicTypes.None, ShowCodeFieldInForm = false)]
    public class ClassifierUnits : CatalogTable
        {
        /// <summary>����������</summary>
        [DataField(Description = "����������", ShowInList = true)]
        public string CutName
            {
            get
                {
                return z_CutName;
                }
            set
                {
                if (z_CutName == value)
                    {
                    return;
                    }

                z_CutName = value;
                NotifyPropertyChanged("CutName");
                }
            }
        private string z_CutName = string.Empty;

        #region ���������������� ��������
        private const string CATALOG_NAME = "ClassifierUnits";

        /// <summary>����</summary>
        public static IDBObjectRef Box
            {
            get
                {
                return z_Box ?? (z_Box = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "����"));
                }
            }
        private static IDBObjectRef z_Box;

        /// <summary>
        /// �������
        /// </summary>
        public static IDBObjectRef Bottle
            {
            get
                {
                return z_Bottle ?? (z_Bottle = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "�������"));
                }
            }
        private static IDBObjectRef z_Bottle;

        /// <summary>
        ///  ������� 
        /// </summary>
        public static IDBObjectRef Pallet
            {
            get
                {
                return z_Pallet ?? (z_Pallet = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "�������"));
                }
            }
        private static IDBObjectRef z_Pallet;
        #endregion
        }
    }