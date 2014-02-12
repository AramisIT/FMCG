using System;
using System.Collections.Generic;
using System.Linq;
using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Catalogs;
using Catalogs;

namespace AtosFMCG.HelperClasses
    {
    /// <summary>Роботник зі штрихкодами</summary>
    public static class BarcodeWorker
        {
        #region Additional fields,consts ...
        public const string BARCODE_STR = "Barcode";
        /// <summary>Мінімальна довжина штрихкоду</summary>
        private const int MIN_BARCODE_LENGTH = 2;
        /// <summary>Роздільник типу і даних</summary>
        private const string SEPARATOR_TYPE_AND_DATA = "_";
        /// <summary>Префікси</summary>
        private static readonly Dictionary<string, Type> prefixes = new Dictionary<string, Type>
                                                                        {
                                                                            {"us", typeof (Users)},
                                                                            {"nm", typeof (Nomenclature)},
                                                                            {"cl", typeof (Cells)},
                                                                            {String.Empty, typeof (long)}
                                                                        }; 
        #endregion

        /// <summary>Визначення чи являється штрихкод двомірним</summary>
        /// <param name="barcode">Штрихкод</param>
        /// <returns>Чи являється штрихкод двомірним</returns>
        public static bool Is2DCode(byte[] barcode)
            {
            return true;
            }

        #region Code128
        /// <summary>Отримати ID зі штрих-коду</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="id">ID</param>
        /// <returns>Чи було отримано ID</returns>
        public static bool GetIdFromBarcode(string barcode, out long id)
            {
            string prefix;
            string idStr;
            bool result = GetPartsFromBarcode(barcode, out prefix, out idStr);

            if (result)
                {
                return Int64.TryParse(idStr, out id);
                }

            id = 0;
            return false;
            }

        /// <summary>Отримати частини зі штрих-коду</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="prefix">Префікс (відповідає за тип даних)</param>
        /// <param name="id">ID</param>
        /// <returns>Чи було отримано частинки</returns>
        public static bool GetPartsFromBarcode(string barcode, out string prefix, out string id)
            {
            if (!String.IsNullOrWhiteSpace(barcode) && barcode.Length >= MIN_BARCODE_LENGTH)
                {
                int index = barcode.IndexOf(SEPARATOR_TYPE_AND_DATA);

                if (index >= 0)
                    {
                    prefix = barcode.Substring(0, index);
                    id = barcode.Substring(index + 1, barcode.Length - prefix.Length - 1);
                    return true;
                    }

                }

            prefix = String.Empty;
            id = String.Empty;
            return false;
            }

        /// <summary>Отримати тип даних зі штрихкоду</summary>
        /// <param name="barcode">Штрих-код</param>
        /// <returns>Тип даних</returns>
        public static Type GetTypeOfData(string barcode)
            {
            string prefix;
            string id;

            if (GetPartsFromBarcode(barcode, out prefix, out id))
                {
                foreach (KeyValuePair<string, Type> p in prefixes.Where(p => prefix == p.Key))
                    {
                    return p.Value;
                    }
                }

            return typeof(Nullable);
            }

        /// <summary>Перевірити штрих-код на відповідність типу даних</summary>
        /// <typeparam name="T">Тип даних</typeparam>
        /// <param name="barcodeObj">Штрих-код</param>
        /// <returns>Штрих-код відповідає типу даних</returns>
        public static bool CheckMatchingBarcodeAndType<T>(object barcodeObj)
            {
            return barcodeObj != null && CheckMatchingBarcodeAndType(barcodeObj.ToString(), typeof(T));
            }

        /// <summary>Перевірити штрих-код на відповідність типу даних</summary>
        /// <typeparam name="T">Тип даних</typeparam>
        /// <param name="barcodeObj">Штрих-код</param>
        /// <returns>Штрих-код відповідає типу даних</returns>
        public static bool CheckMatchingBarcodeAndType<T>(string barcodeObj)
            {
            return barcodeObj != null && CheckMatchingBarcodeAndType(barcodeObj, typeof(T));
            }

        /// <summary>Перевірити штрих-код на відповідність типу даних</summary>
        /// <param name="barcodeObj">Штрих-код</param>
        /// <param name="expectedType">Тип даних</param>
        /// <returns>Штрих-код відповідає типу даних</returns>
        public static bool CheckMatchingBarcodeAndType(object barcodeObj, Type expectedType)
            {
            return barcodeObj != null && CheckMatchingBarcodeAndType(barcodeObj.ToString(), expectedType);
            }

        /// <summary>Перевірити штрих-код на відповідність типу даних</summary>
        /// <param name="expectedType">Тип даних</param>
        /// <param name="barcode">Штрих-код</param>
        /// <returns>Штрих-код відповідає типу даних</returns>
        public static bool CheckMatchingBarcodeAndType(string barcode, Type expectedType)
            {
            string prefix;
            string id;

            if (GetPartsFromBarcode(barcode, out prefix, out id))
                {
                return prefixes[prefix] == expectedType;
                }

            return false;
            }

        /// <summary>Перевірити штрих-код на відповідність типу даних</summary>
        /// <param name="expectedType">Тип даних</param>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="id">ID</param>
        /// <returns>Штрих-код відповідає типу даних</returns>
        public static bool CheckMatchingBarcodeAndType(string barcode, Type expectedType, out long id)
            {
            string prefix;
            string idStr;

            if (GetPartsFromBarcode(barcode, out prefix, out idStr))
                {
                Int64.TryParse(idStr, out id);
                return prefixes[prefix] == expectedType;
                }

            id = 0;
            return false;
            }

        /// <summary>Отримати коротку інформацію зі штрихкоду</summary>
        /// <typeparam name="T">Тип даних</typeparam>
        /// <param name="barcode">Штрих-код</param>
        /// <param name="id">ID</param>
        /// <param name="description">Опис</param>
        /// <returns>Чи було отримано коротку інформацію зі штрих-коду</returns>
        public static bool GetCutDataByBarcode<T>(string barcode, out long id, out string description)
            {
            Type type = typeof(T);

            if (CheckMatchingBarcodeAndType(barcode, type, out id))
                {
                string command = String.Format("SELECT RTRIM(Description) FROM {0} WHERE Id=@Id", type.Name);
                Query query = DB.NewQuery(command);
                query.AddInputParameter("Id", id);
                object descriptionObj = query.SelectScalar();

                if (descriptionObj != null)
                    {
                    description = descriptionObj.ToString();
                    return true;
                    }
                }

            id = 0;
            description = String.Empty;
            return false;
            }

        #endregion
        }
    }
