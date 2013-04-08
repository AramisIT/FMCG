namespace AtosFMCG.HelperClasses.ViewOfServiceTables
    {
    public partial class ViewOfFilledCell : ViewBaseForm
        {
        public ViewOfFilledCell()
            {
            InitializeComponent();
            Command = @"
SELECT TOP {0}
	ROW_NUMBER() OVER (ORDER BY t.LineNumber) [#],
	t.Number [Номер документу приймання товару],
	PalletCode [Паллета],
	PreviousCode [Попередня паллета],
	Nomenclature [Номенклатура],
	Party [Партія],
	Cell [Початкова комірка]
FROM(
	SELECT
		a.IdDoc*100000000+a.LineNumber LineNumber,
		d.Number,
		f.PalletCode,
		f.PreviousCode,
		n.Description Nomenclature,
		p.Description Party,
		c.Description Cell,
		ROW_NUMBER() OVER (PARTITION BY f.PalletCode ORDER BY f.PalletCode) RowNumber
	FROM FilledCell f 
	LEFT JOIN SubAcceptanceOfGoodsNomenclatureInfo a ON a.NomenclatureCode=f.PalletCode
	LEFT JOIN AcceptanceOfGoods d ON d.Id=a.IdDoc
	LEFT JOIN Nomenclature n ON n.Id=a.Nomenclature
	LEFT JOIN Party p ON p.Id=a.NomenclatureParty
	LEFT JOIN Cells c ON c.Id=a.NomenclatureCell)t
WHERE t.RowNumber=1
ORDER BY t.LineNumber";
            }
        }
    }
