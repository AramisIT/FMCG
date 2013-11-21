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

        public static bool IsNumber(string str)
            {
            if (str == "") return false;

            for (int i = 0; i < str.Length; i++)
                {
                if (!Char.IsNumber(str, i)) return false;
                }

            return true;
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
