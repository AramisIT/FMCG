using System;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using AramisInfostructure.Queries;
using AtosFMCG.DatabaseObjects.Interfaces;

namespace Catalogs
    {
    /// <summary>Одиниці виміру</summary>
    [Catalog(Description = "Одиниці виміру", GUID = "AAA9608E-2F23-4BB5-98C6-78BC782AE4DD", ShowCodeFieldInForm = false)]
    public class Measures : CatalogTable, ISyncWith1C
        {
        #region Properties
        /// <summary>Посилання 1С</summary>
        [DataField(Description = "Посилання 1С", ShowInList = false)]
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

        /// <summary>Власник</summary>
        [DataField(Description = "Власник", ShowInList = true)]
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

        /// <summary>Класифікатор тари</summary>
        [DataField(Description = "Класифікатор тари", ShowInList = true)]
        public ClassifierUnits Classifier
            {
            get
                {
                return (ClassifierUnits)GetValueForObjectProperty("Classifier");
                }
            set
                {
                SetValueForObjectProperty("Classifier", value);
                }
            }

        /// <summary>Кількість базових одиниць</summary>
        [DataField(Description = "Кількість базових одиниць", ShowInList = true, DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
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

        #region Implemention of CatalogTable
        protected override WritingResult CheckingBeforeWriting()
            {
            Description = string.Format("{0} ({1})", Classifier.Description, Nomenclature.Description);
            return base.CheckingBeforeWriting();
            } 
        #endregion

        #region Get Box
        public static long GetBoxForPallet(long palletId)
            {
            IQuery query = DB.NewQuery(@"WITH
NomenclatureTable AS (SELECT Nomenclature,MeasureUnit FROM StockBalance b WHERE b.UniqueCode=@Pallet)
, BoxTable AS (SELECT * FROM Measures WHERE Classifier=@Box)
	
SELECT DISTINCT TOP 1 b.Id
FROM NomenclatureTable n
JOIN BoxTable b ON b.Id=n.MeasureUnit");
            query.AddInputParameter("Pallet", palletId);
            query.AddInputParameter("Box", ClassifierUnits.Box.Id);
            object idObj = query.SelectScalar();

            return idObj == null ? 0 : Convert.ToInt64(idObj);
            }

        public static long GetBoxForNomenclature(Nomenclature nomenclature)
            {
            return GetBoxForNomenclature(nomenclature.Id);
            }

        public static long GetBoxForNomenclature(long nomenclatureId)
            {
            return GetMeasureForNomenclature(nomenclatureId, ClassifierUnits.Box.Id);
            }

        public static long GetMeasureForNomenclature(long nomenclatureId, long classifier)
            {
            IQuery query = DB.NewQuery(@"SELECT Id FROM Measures m WHERE m.Nomenclature=@Nomenclature AND m.Classifier=@Classifier");
            query.AddInputParameter("Nomenclature", nomenclatureId);
            query.AddInputParameter("Classifier", classifier);
            object idObj = query.SelectScalar();

            return idObj == null ? 0 : Convert.ToInt64(idObj);
            }
        #endregion
        }
    }