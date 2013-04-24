using System;
using System.Data;
using System.Windows.Forms;
using Aramis.DatabaseConnector;

namespace AtosFMCG.HelperClasses.ViewOfServiceTables
    {
    public partial class ViewBaseForm : Form
        {
        #region Properties
        private const int DEFAULT_COUNT = 250;

        protected int countOfRows
            {
            get
                {
                int coutn = Convert.ToInt32(rowsCount.Text);
                return coutn > 0 ? coutn : DEFAULT_COUNT;
                }
            }

        protected string Command
            {
            get { return z_Command; }
            set
                {
                z_Command = value;
                fillData();
                }
            }
        private string z_Command;
        #endregion

        public ViewBaseForm()
            {
            InitializeComponent();
            rowsCount.Text = DEFAULT_COUNT.ToString();
            }

        #region Filling
        private void refresh_Click(object sender, EventArgs e)
            {
            fillData();
            }

        protected void fillData()
            {
            string queryCommand = string.Format(Command, countOfRows);
            Query query = DB.NewQuery(queryCommand);
            DataTable table = query.SelectToTable();
            dataGrid.DataSource = table;
            } 
        #endregion

        #region Close
        private void rowsCount_KeyDown(object sender, KeyEventArgs e)
            {
            checkForClose(e.KeyCode);
            }

        private void dataGrid_KeyDown(object sender, KeyEventArgs e)
            {
            checkForClose(e.KeyCode);
            }

        private void checkForClose(Keys key)
            {
            if (key == Keys.Escape)
                {
                Close();
                }
            }

        private void refresh_KeyDown(object sender, KeyEventArgs e)
            {
            checkForClose(e.KeyCode);
            } 
        #endregion
        }
    }
