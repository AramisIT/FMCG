using System;
using System.ComponentModel;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using AtosFMCG.DatabaseObjects.Interfaces;

namespace Catalogs
    {
    /// <summary>����</summary>
    [Catalog(Description = "����", GUID = "AEDDF72B-5CD8-4702-A464-A8439D345D11", DescriptionSize = 70)]
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
            get { return (Nomenclature)GetValueForObjectProperty("Nomenclature"); }
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
                NotifyPropertyChanged("ShelfLife50P");
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
                NotifyPropertyChanged("ShelfLife50P");
                }
            }
        private DateTime z_TheDeadlineSuitability;

        /// <summary>����� ��������� 50%</summary>
        [DataField(Description = "����� ��������� 50%", ShowInList = true, ReadOnly = true, StorageType = StorageTypes.Local)]
        public DateTime ShelfLife50P
            {
            get
                {
                var shelfLifeDays = (int)((TimeSpan)(TheDeadlineSuitability.Date - DateOfManufacture.Date)).TotalDays;
                return DateOfManufacture.AddDays(shelfLifeDays * 0.5);
                }
            set
                {

                }
            }

        #endregion

        public int ShelfLifeDays
            {
            get
                {
                var days = (TheDeadlineSuitability - DateOfManufacture).TotalDays;
                return (int)days;
                }
            }

        #region Fill
        public void FillAddData(int shelfLifeDays)
            {
            TheDeadlineSuitability = DateOfManufacture.AddDays(shelfLifeDays);
            fillDescription();
            }

        protected override void InitItemBeforeShowing()
            {
            fillDescription();
            }

        /// <summary>�������� ������������ ���������</summary>
        private void fillDescription()
            {
            Description = string.Format("{1}: {0}", Nomenclature.Description, DateOfManufacture.ToShortDateString());
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

        internal static Parties Find(long nomenclatureId, DateTime productionDate, int shelfLifeDays)
            {
            Query query = DB.NewQuery(@"SELECT Top 1 Id 
FROM Parties 
WHERE markForDeleting = 0 
    and Nomenclature=@Nomenclature 
    AND CAST(DateOfManufacture AS DATE)=@Date
    and CAST(TheDeadlineSuitability AS DATE)=@ExpirationDate");

            query.AddInputParameter("Date", productionDate.Date);
            query.AddInputParameter("ExpirationDate", productionDate.AddDays(shelfLifeDays).Date);
            query.AddInputParameter("Nomenclature", nomenclatureId);
            var partyId = query.SelectInt64();

            return new Parties() { ReadingId = partyId };
            }

        internal static Parties FindByExpirationDate(long wareId, DateTime expirationDate)
            {
            var q = DB.NewQuery(@"
select top 1 Id from Parties p 

where p.Nomenclature = @Nomenclature and 
p.TheDeadlineSuitability = @ExpirationDate
and p.MarkForDeleting = 0 
");
            q.AddInputParameter("Nomenclature", wareId);
            q.AddInputParameter("ExpirationDate", expirationDate);
            var partyId = q.SelectInt64();

            return new Parties() { ReadingId = partyId };
            }
        }
    }