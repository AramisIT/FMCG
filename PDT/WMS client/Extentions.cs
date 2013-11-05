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
        }
    }
