namespace AtosFMCG.HelperClasses.ViewOfServiceTables
    {
    public partial class ViewOfStockBalance : ViewBaseForm
        {
        public ViewOfStockBalance()
            {
            InitializeComponent();
            Command = @"
SELECT TOP {0}
	ROW_NUMBER() OVER (ORDER BY b.ExpariedDate DESC) [#],
	c.Description [Комірка],
	n.Description [Номенклатура],
	m.Description [Од.вим.],
	CONVERT(VARCHAR(10),b.ExpariedDate,104) [Термін придатності],
	b.UniqueCode [Паллета],
	b.State [Статус],
	b.Quantity [К-сть]
FROM StockBalance b
LEFT JOIN Cells c ON c.Id=b.Cell
LEFT JOIN Nomenclature n ON n.Id=b.Nomenclature
LEFT JOIN Measures m ON m.Id=b.MeasureUnit";
            }
        }
    }
