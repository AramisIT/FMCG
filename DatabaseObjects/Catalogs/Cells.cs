using System.Collections.Generic;
using Aramis.Attributes;
using Aramis.Core;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>������ ���������</summary>
    [Catalog(Description = "������ ���������", GUID = "A4BDEF67-D02D-469E-928E-F5BE8FFCA0A1")]
    public class Cells : CatalogTable
        {
        #region Properties
        /// <summary>��� ������</summary>
        [DataField(Description = "��� ������", ShowInList = true)]
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