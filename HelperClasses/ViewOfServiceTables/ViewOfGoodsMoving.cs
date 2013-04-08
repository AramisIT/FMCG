namespace AtosFMCG.HelperClasses.ViewOfServiceTables
    {
    public partial class ViewOfGoodsMoving : ViewBaseForm
        {
        public ViewOfGoodsMoving()
            {
            InitializeComponent();
            Command = @"
SELECT TOP {0}
	ROW_NUMBER() OVER (ORDER BY m.WritingDate DESC) [#],
	CONVERT(VARCHAR(10),m.WritingDate,104) [Дата запису],
	m.DocTypeId [GUID документу],
	m.DocId [ID документу],
	m.RowNumber [№ строки],
	c.Description [Комірка],
	n.Description [Номенклатура],
	mu.Description [Од.вим.],
	CONVERT(VARCHAR(10),m.ExpariedDate,104) [Термін придатності],
	m.UniqueCode [Паллета],
	m.State [Статус],
	m.Quantity [К-сть]
FROM GoodsMoving m
LEFT JOIN Cells c ON c.Id=m.Cell
LEFT JOIN Nomenclature n ON n.Id=m.Nomenclature
LEFT JOIN Measures mu ON mu.Id=m.MeasureUnit";
            }
        }
    }
