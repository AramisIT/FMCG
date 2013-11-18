using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WMS_client
    {
    public static class Extentions
        {
        public static void AddColumn(this DataGrid dataGrid, string caption, string columnName, int width)
            {
            DataGridTextBoxColumn ColumnStyle = new DataGridTextBoxColumn();
            ColumnStyle.HeaderText = caption;
            ColumnStyle.MappingName = columnName;
            ColumnStyle.Width = width;
            dataGrid.TableStyles["Mobile"].GridColumnStyles.Add(ColumnStyle);
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
        }
    }
