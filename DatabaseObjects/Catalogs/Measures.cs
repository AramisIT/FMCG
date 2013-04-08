using Aramis;
using Aramis.Attributes;
using Aramis.Core;
using Catalogs;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>������� �����</summary>
    [Catalog(Description = "������� �����", GUID = "AAA9608E-2F23-4BB5-98C6-78BC782AE4DD", ShowCodeFieldInForm = false)]
    public class Measures : CatalogTable
        {
        #region Properties
        /// <summary>�������</summary>
        [DataField(Description = "�������", ShowInList = true)]
        public Nomenclature Nomenclature
            {
            get
                {
                return (Nomenclature)GetValueForObjectProperty("Nomenclature");
                }
            set
                {
                SetValueForObjectProperty("Nomenclature", value);
                }
            }

        /// <summary>������������ (����)</summary>
        [DataField(Description = "������������ (����)", ShowInList = true)]
        public Nomenclature Tare
            {
            get
                {
                return (Nomenclature)GetValueForObjectProperty("Tare");
                }
            set
                {
                SetValueForObjectProperty("Tare", value);
                }
            }

        /// <summary>ʳ������ ������� �������</summary>
        [DataField(Description = "ʳ������ ������� �������", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public double BaseCount
            {
            get
                {
                return z_BaseCount;
                }
            set
                {
                if (z_BaseCount == value)
                    {
                    return;
                    }

                z_BaseCount = value;
                NotifyPropertyChanged("BaseCount");
                }
            }
        private double z_BaseCount; 
        #endregion

        #region ���������������� ��������
        private const string CATALOG_NAME = "Measures";

        /// <summary>����</summary>
        public static DBObjectRef Box
            {
            get
                {
                return z_Box ?? (z_Box = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "����"));
                }
            }
        private static DBObjectRef z_Box;

        //�������
        public static DBObjectRef Bottle
            {
            get
                {
                return z_Bottle ?? (z_Bottle = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "���"));
                }
            }
        private static DBObjectRef z_Bottle;
        #endregion
        }
    }