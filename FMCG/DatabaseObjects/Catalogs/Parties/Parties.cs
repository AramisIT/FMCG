using System;
using System.ComponentModel;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Interfaces;

namespace Catalogs
    {
    /// <summary>����</summary>
    [Catalog(Description = "����", GUID = "AEDDF72B-5CD8-4702-A464-A8439D345D11")]
    public class Parties : CatalogTable, ISyncWith1C
        {
        #region Properties
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

        /// <summary>������������</summary>
        [DataField(Description = "������������", ShowInList = true, AllowOpenItem = true)]
        public Nomenclature Nomenclature
            {
            get { return (Nomenclature) GetValueForObjectProperty("Nomenclature"); }
            set { SetValueForObjectProperty("Nomenclature", value); }
            }

        /// <summary>���� ������������</summary>
        [DataField(Description = "���� ������������", ShowInList = true)]
        public DateTime DateOfManufacture
            {
            get { return z_DateOfManufacture; }
            set
                {
                if (z_DateOfManufacture == value)
                    {
                    return;
                    }

                z_DateOfManufacture = value;
                NotifyPropertyChanged("DateOfManufacture");
                }
            }
        private DateTime z_DateOfManufacture;

        /// <summary>ʳ������ ����� ����������</summary>
        [DataField(Description = "ʳ������ ����� ����������", ShowInList = true, ReadOnly = true)]
        public DateTime TheDeadlineSuitability
            {
            get { return z_TheDeadlineSuitability; }
            set
                {
                if (z_TheDeadlineSuitability == value)
                    {
                    return;
                    }

                z_TheDeadlineSuitability = value;
                NotifyPropertyChanged("TheDeadlineSuitability");
                }
            }
        private DateTime z_TheDeadlineSuitability;

        /// <summary>����� ��������� 50%</summary>
        [DataField(Description = "����� ��������� 50%", ShowInList = true, ReadOnly = true)]
        public DateTime ShelfLife50P
            {
            get { return z_ShelfLife50P; }
            set
                {
                if (z_ShelfLife50P == value)
                    {
                    return;
                    }

                z_ShelfLife50P = value;
                NotifyPropertyChanged("ShelfLife50P");
                }
            }
        private DateTime z_ShelfLife50P;
        #endregion

        #region CatalogTable
        protected override void InitItemBeforeShowing()
            {
            base.InitItemBeforeShowing();
            PropertyChanged += Party_PropertyChanged;
            ValueOfObjectPropertyChanged += Party_ValueOfObjectPropertyChanged;
            }
        #endregion

        #region Changes
        private void Party_ValueOfObjectPropertyChanged(string propertyName)
            {
            switch (propertyName)
                {
                    case "Nomenclature":
                        fillDeadline();
                        break;
                }
            }

        private void Party_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
            switch (e.PropertyName)
                {
                    case "DateOfManufacture":
                        fillDeadline();
                        break;
                }
            }
        #endregion

        #region Fill
        public void FillAddData()
            {
            fillDeadline();
            fillDescription();
            }

        /// <summary>���������� ������ ����������</summary>
        private void fillDeadline()
            {
            TheDeadlineSuitability = DateOfManufacture.AddDays(Nomenclature.ShelfLife);
            ShelfLife50P = DateOfManufacture.AddDays(Convert.ToInt32(Nomenclature.ShelfLife / 2));
            fillDescription();
            }

        /// <summary>�������� ������������ ���������</summary>
        private void fillDescription()
            {
            Description = string.Format("{0}: {1}", Nomenclature.Description, DateOfManufacture.ToShortDateString());
            }
        #endregion

        #region static
        public static DateTime GetDateOfManufactureById(long partyId)
            {
            Query query = DB.NewQuery("SELECT DateOfManufacture FROM Parties WHERE Id=@Id");
            query.AddInputParameter("Id", partyId);
            object date = query.SelectScalar();

            return date == null ? DateTime.MinValue : Convert.ToDateTime(date);
            }
        #endregion
        }
    }