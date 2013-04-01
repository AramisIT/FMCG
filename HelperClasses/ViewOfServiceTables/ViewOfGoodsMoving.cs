using System.Data;
using System.Windows.Forms;
using Aramis.DatabaseConnector;

namespace AtosFMCG.HelperClasses.ViewOfServiceTables
    {
    public partial class ViewOfGoodsMoving : Form
        {
        public ViewOfGoodsMoving()
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
	CONVERT(VARCHAR(10),m.WritingDate,104)WritingDate,
	m.DocTypeId,
	m.DocId,
	m.RowNumber,
	c.Description Cell,
	n.Description Nomenclature,
	mu.Description Measures,
	CONVERT(VARCHAR(10),m.ExpariedDate,104)ExpariedDate,
	m.UniqueCode,
	m.State,
	m.Quantity
FROM GoodsMoving m
LEFT JOIN Cells c ON c.Id=m.Cell
LEFT JOIN Nomenclature n ON n.Id=m.Nomenclature
LEFT JOIN Measures mu ON mu.Id=m.MeasureUnit");
            DataTable table = query.SelectToTable();
            goodsMoving.DataSource = table;
            }
        }
    }
