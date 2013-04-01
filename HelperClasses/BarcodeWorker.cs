using System;
using System.Collections.Generic;
using System.Linq;
using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Catalogs;
using Catalogs;

namespace AtosFMCG.HelperClasses
    {
    public static class BarcodeWorker
        {
        private const int MIN_BARCODE_LENGTH = 2;
        private const string SEPARATOR_TYPE_AND_DATA = "_";
        private static readonly Dictionary<string, Type> prefixes = new Dictionary<string, Type>
                                                                        {
                                                                            {"us", typeof (Users)},
                                                                            {"nm", typeof (Nomenclature)},
                                                                            {"cl", typeof (Cells)},
                                                                            {string.Empty, typeof (long)}
                                                                        };

        public static bool Is2DCode(byte[] barcode)
            {
            return true;
            }

        public static bool GetPartsFromBarcode(string barcode, out long id)
            {
            string prefix;
            string idStr;
            bool result = GetPartsFromBarcode(barcode, out prefix, out idStr);

            if (result)
                {
                return long.TryParse(idStr, out id);
                }

            id = 0;
            return false ;
            }

        public static bool GetPartsFromBarcode(string barcode, out string prefix, out string id)
            {
            if (!string.IsNullOrWhiteSpace(barcode) && barcode.Length >= MIN_BARCODE_LENGTH)
                {
                int index = barcode.IndexOf(SEPARATOR_TYPE_AND_DATA);

                if (index >= 0)
                    {
                    prefix = barcode.Substring(0, index);
                    id = barcode.Substring(index + 1, barcode.Length - prefix.Length - 1);
                    return true;
                    }

                }

            prefix = string.Empty;
            id = string.Empty;
            return false;
            }

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

            return typeof (Nullable);
            }

        public static bool CheckMatchingBarcodeAndType<T>(object barcodeObj)
            {
            return barcodeObj != null && CheckMatchingBarcodeAndType(barcodeObj.ToString(), typeof(T));
            }

        public static bool CheckMatchingBarcodeAndType(object barcodeObj, Type expectedType)
            {
            return barcodeObj != null && CheckMatchingBarcodeAndType(barcodeObj.ToString(), expectedType);
            }

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

        public static bool CheckMatchingBarcodeAndType(string barcode, Type expectedType, out long id)
            {
            string prefix;
            string idStr;

            if (GetPartsFromBarcode(barcode, out prefix, out idStr))
                {
                long.TryParse(idStr, out id);
                return prefixes[prefix] == expectedType;
                }

            id = 0;
            return false;
            }

        public static bool GetCutDataByBarcode<T>(string barcode, out long id, out string description)
            {
            Type type = typeof (T);

            if (CheckMatchingBarcodeAndType(barcode, type, out id))
                {
                string command = string.Format("SELECT RTRIM(Description) FROM {0} WHERE Id=@Id", type.Name);
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
            description = string.Empty;
            return false;
            }
        }

    /// <summary>Переміщувач паллети</summary>
    public static class PalletMover
        {
        /// <summary>Встановити паллету в комірку (підставити до іншої паллети)</summary>
        /// <param name="palletCode">Унікальний код палети</param>
        /// <param name="previous">Унікальний код палети до якої підставиться паллета, що встановлюється</param>
        public static void EstablishPalletToCell(long palletCode, long previous = 0)
            {
            FilledCell filledCell = new FilledCell
            {
                PalletCode = palletCode,
                PreviousCode = previous
            };
            filledCell.Write();
            //Query query = DB.NewQuery("");
            //query.AddInputParameter("PalletCode", palletCode);
            //query.AddInputParameter("Previous", previous);
            //query.Execute();
            }

        /// <summary>Перемістити паллету</summary>
        /// <param name="palletCode">Унікальний код паллети</param>
        /// <param name="newPreviousPallet">Унікальний код палети до якої підставиться паллета, що переміщується</param>
        public static void MovePalletToNewPlace(long palletCode, long newPreviousPallet = 0)
            {
            Query query = DB.NewQuery("UPDATE FilledCell SET PreviousCode=@PreviousCode WHERE PalletCode=@PalletCode");
            query.AddInputParameter("PreviousCode", newPreviousPallet);
            query.AddInputParameter("PalletCode", palletCode);
            query.Execute();
            }

        /// <summary>Прибрати паллету зі скалду</summary>
        /// <param name="palletCode">Унікальний код паллети</param>
        public static void RemovePallet(long palletCode)
            {
            Query query = DB.NewQuery("DELETE FROM FilledCell PalletCode=@PalletCode");
            query.AddInputParameter("PalletCode", palletCode);
            query.Execute();
            }
        }
    }
