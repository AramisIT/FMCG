using System.Collections.Generic;
using Aramis.Attributes;
using Aramis.Core;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>Комірки зберігання</summary>
    [Catalog(Description = "Комірки зберігання", GUID = "A4BDEF67-D02D-469E-928E-F5BE8FFCA0A1")]
    public class Cells : CatalogTable
        {
        #region Properties
        /// <summary>Тип комірки</summary>
        [DataField(Description = "Тип комірки", ShowInList = true)]
        public TypesOfCell TypeOfCell
            {
            get
                {
                return (TypesOfCell)GetValueForObjectProperty("TypeOfCell");
                }
            set
                {
                SetValueForObjectProperty("TypeOfCell", value);
                }
            } 
        #endregion

        public static KeyValuePair<long, string> GetNewCellForGoods(long goods)
            {
            return new KeyValuePair<long, string>(0, string.Empty);
            }
        }
    }