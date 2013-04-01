using System.Data;
using System.Windows.Forms;
using Aramis.DatabaseConnector;

namespace AtosFMCG.HelperClasses.ViewOfServiceTables
    {
    public partial class ViewOfFilledCell : Form
        {
        public ViewOfFilledCell()
            {
            InitializeComponent();
            Load += ViewOfGoodsMoving_Load;
            }

        void ViewOfGoodsMoving_Load(object sender, System.EventArgs e)
            {
            fillData();
            }

        private void fillData()
            {
            Query query = DB.NewQuery("SELECT PalletCode,PreviousCode FROM FilledCell");
            DataTable table = query.SelectToTable();
            goodsMoving.DataSource = table;
            }
        }
    }
