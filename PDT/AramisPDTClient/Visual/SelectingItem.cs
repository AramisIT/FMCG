using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WMS_client.Base.Visual
    {
    public partial class SelectingItem : Form
        {
        public SelectingItem()
            {
            InitializeComponent();

            dataGrid.TableStyles.Add(new DataGridTableStyle());
            dataGrid.TableStyles[0].MappingName = "Mobile";
            }

        public int SelectedIndex { get; set; }
        public List<CatalogItem> DataSource { get; set; }

        private void SelectingItem_Load(object sender, EventArgs e)
            {
            WindowState = FormWindowState.Maximized;
            fillDataGrid();
            }

        private void fillDataGrid()
            {
            var table = new DataTable("Mobile");
            table.Columns.AddRange(new DataColumn[] {new DataColumn("Description", typeof(string)), 
                new DataColumn("Id", typeof(long))});

            DataSource.ForEach(item => table.Rows.Add(item.Description, item.Id));

            dataGrid.DataSource = table;

            // Убираем автоматически сформировавшиеся столбики
            dataGrid.TableStyles["Mobile"].GridColumnStyles.Clear();
            dataGrid.RowHeadersVisible = false;

            dataGrid.AddColumn("Вибір піддона", "Description", 210);

            dataGrid.CurrentRowIndex = SelectedIndex < 0 ? 0 : SelectedIndex;
            dataGrid.Focus();
            }

        private void button2_Click(object sender, EventArgs e)
            {
            CancelSelecting();
            }

        public void CancelSelecting()
            {
            if (InvokeRequired)
                {
                Invoke(new Action(CancelSelecting));
                return;
                }
            DialogResult = DialogResult.Cancel;
            Close();
            }

        private void button1_Click(object sender, EventArgs e)
            {
            selectItem();
            }

        private void selectItem()
            {
            SelectedIndex = dataGrid.CurrentRowIndex;
            DialogResult = DialogResult.OK;
            Close();
            }

        internal void SetRowHeight(int rowHeight)
            {
            dataGrid.PreferredRowHeight = rowHeight;
            }

        private void SelectingItem_KeyPress(object sender, KeyPressEventArgs e)
            {
            var pressedKey = (Keys)e.KeyChar;

            switch (pressedKey)
                {
                case Keys.Enter:
                    selectItem();
                    break;
                }
            }
        }
    }