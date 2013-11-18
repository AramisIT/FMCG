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
