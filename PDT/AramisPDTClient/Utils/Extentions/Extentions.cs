using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace System
    {
    public static class Extentions
        {
        private const string DATE_TIME_FORMAT = "dd.MM.yyyy";

        public static bool IsNumber(this string stringValue)
            {
            if (string.IsNullOrEmpty(stringValue))
                {
                return false;
                }

            foreach (char @char in stringValue)
                {
                if (!char.IsNumber(@char))
                    {
                    return false;
                    }
                }
            return true;
            }

        public static bool IsItEAN13(this string barcode)
            {
            if (barcode.Length != 13) return false;
            if (!barcode.IsNumber()) return false;

            barcode = "0" + barcode;

            // calculate check digit
            int[] a = new int[13];
            a[0] = int.Parse(barcode[0].ToString()) * 3;
            a[1] = int.Parse(barcode[1].ToString());
            a[2] = int.Parse(barcode[2].ToString()) * 3;
            a[3] = int.Parse(barcode[3].ToString());
            a[4] = int.Parse(barcode[4].ToString()) * 3;
            a[5] = int.Parse(barcode[5].ToString());
            a[6] = int.Parse(barcode[6].ToString()) * 3;
            a[7] = int.Parse(barcode[7].ToString());
            a[8] = int.Parse(barcode[8].ToString()) * 3;
            a[9] = int.Parse(barcode[9].ToString());
            a[10] = int.Parse(barcode[10].ToString()) * 3;
            a[11] = int.Parse(barcode[11].ToString());
            a[12] = int.Parse(barcode[12].ToString()) * 3;
            int sum = a[0] + a[1] + a[2] + a[3] + a[4] + a[5] + a[6] + a[7] + a[8] + a[9] + a[10] + a[11] + a[12];
            int check = (10 - (sum % 10)) % 10;

            char lastChar = (char)('0' + check);
            var result = lastChar.Equals(barcode[13]);
            return result;
            }

        public static DateTime ToDateTime(this string dateTimeStr)
            {
            DateTime result;
            try
                {
                result = DateTime.ParseExact(dateTimeStr, DATE_TIME_FORMAT, null);
                }
            catch
                {
                result = DateTime.MinValue;
                }
            return result;
            }

        public static string ToStandartString(this DateTime dateTime)
            {
            return dateTime.ToString(DATE_TIME_FORMAT);
            }
       
        public static void Warning(this string message)
            {
            MessageBox.Show(message.ToUpper(), "aramis wms", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

        public static void ShowMessage(this string message)
            {
            MessageBox.Show(message.ToUpper(), "aramis wms", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }

        public static bool Ask(this string message)
            {
            return MessageBox.Show(message.ToUpper(), "aramis wms", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
            }

        public static void AddParameter(this SqlCeCommand command, string name, object value)
            {
            command.Parameters.Add(name, value);
            }
        }
    }
