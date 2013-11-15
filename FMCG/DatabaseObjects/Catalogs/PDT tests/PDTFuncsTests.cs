using System;
using System.Data;
using Aramis.Attributes;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Core;
using Aramis.SystemConfigurations;
using Catalogs;

namespace Catalogs
    {
    public enum ParametersTypes
        {
        Bool,
        Long,
        Decimal,
        String
        }

    [Catalog(Description = "Тест функций терминала", GUID = "A5726B61-52EF-4415-940C-F7C055C7FC36", DescriptionSize = 35, HierarchicType = HierarchicTypes.None)]
    public class PDTFuncsTests : CatalogTable
        {

        [Table(Columns = "ParameterName, ParameterValue, ParameterType")]
        [DataField(Description = "Номенклатура")]
        public DataTable MethodParameters
            {
            get { return GetSubtable("MethodParameters"); }
            }

        [SubTableField(Description = "Имя", PropertyType = typeof(string))]
        public DataColumn ParameterName { get; set; }

        [SubTableField(Description = "Значение", PropertyType = typeof(string), Size = 100)]
        public DataColumn ParameterValue { get; set; }

        [SubTableField(Description = "Тип параметра", PropertyType = typeof(ParametersTypes))]
        public DataColumn ParameterType { get; set; }

        public PDTFuncsTests()
            : base()
            {
            }

        public object[] GetParametersValues()
            {
            var result = new object[MethodParameters.Rows.Count];

            for (int index = 0; index < MethodParameters.Rows.Count; index++)
                {
                var row = MethodParameters.Rows[index];
                var parameterType = (ParametersTypes)row[ParameterType];
                var stringValue = row[ParameterValue] as string;

                result[index] = convertValue(stringValue, parameterType);
                }

            return result;
            }

        private object convertValue(string stringValue, ParametersTypes parameterType)
            {
            switch (parameterType)
                {
                case ParametersTypes.Bool:
                    return Convert.ToBoolean(stringValue);

                case ParametersTypes.Long:
                    return Convert.ToInt64(stringValue);

                case ParametersTypes.Decimal:
                    return Convert.ToDecimal(stringValue);

                default:
                    return stringValue.ToString();
                }
            }
        }
    }