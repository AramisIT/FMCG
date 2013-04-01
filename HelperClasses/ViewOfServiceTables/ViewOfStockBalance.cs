using System.Data;
using System.Windows.Forms;
using Aramis.DatabaseConnector;

namespace AtosFMCG.HelperClasses.ViewOfServiceTables
    {
    public partial class ViewOfStockBalance : Form
        {
        public ViewOfStockBalance()
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
            Query query = DB.NewQuery(@"
SELECT TOP 250
	c.Description Cell,
	n.Description Nomenclature,
	m.Description Measures,
	CONVERT(VARCHAR(10),b.ExpariedDate,104)ExpariedDate,
	b.UniqueCode,
	b.State,
	b.Quantity
FROM StockBalance b
LEFT JOIN Cells c ON c.Id=b.Cell
LEFT JOIN Nomenclature n ON n.Id=b.Nomenclature
LEFT JOIN Measures m ON m.Id=b.MeasureUnit");
            DataTable table = query.SelectToTable();
            goodsMoving.DataSource = table;
            }
        }
    }
