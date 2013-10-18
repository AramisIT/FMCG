using System.Windows.Forms;
using Aramis.UI.WinFormsDevXpress;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Attributes;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using Documents;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    [View(DBObjectGuid = "0ACBC4E6-5486-4F2E-B207-3E8D012A080B", ViewType = ViewFormType.DocItem)]
    public partial class AcceptanceOfGoodsItemForm : DevExpress.XtraBars.Ribbon.RibbonForm, IItemForm
        {
        #region Fields
        private DocumentTable item;
        public DatabaseObject Item
            {
            get { return item; }
            set { item = (DocumentTable)value; }
            }
        public AcceptanceOfGoods Document
            {
            get { return (AcceptanceOfGoods)item; }
            }
        #endregion

        public AcceptanceOfGoodsItemForm()
            {
            InitializeComponent();
            Load += AcceptanceOfGoodsItemForm_Load;
            }

        void AcceptanceOfGoodsItemForm_Load(object sender, System.EventArgs e)
            {
            Document.TableRowAdded += Document_TableRowAdded;
            }

        void Document_TableRowAdded(System.Data.DataTable dataTable, System.Data.DataRow currentRow)
            {
            if (showNomenclatureBarButtonItem.Checked || showTareBarButtonItem.Checked)
                {
                skip = true;
                showTareBarButtonItem.Checked = false;
                showNomenclatureBarButtonItem.Checked = false;
                showTareRows(ShownModes.All);
                skip = false;
                }
            }

        #region Result
        private void TryCancel()
            {
            Close();
            }

        private void Itemform_KeyDown(object sender, KeyEventArgs e)
            {
            if (e.KeyCode == Keys.Escape)
                {
                TryCancel();
                }
            else if (e.KeyCode == Keys.Enter && e.Control)
                {
                OK_ItemClick(null, null);
                }
            else if (e.KeyCode == Keys.S && e.Control)
                {
                Write_ItemClick(null, null);
                }
            }

        private bool Write()
            {
            return Item.Write() == WritingResult.Success;
            }

        private void OK_ItemClick(object sender, ItemClickEventArgs e)
            {
            if (Write())
                {
                Close();
                }
            }

        private void Write_ItemClick(object sender, ItemClickEventArgs e)
            {
            Write();
            }

        private void cancel_ItemClick(object sender, ItemClickEventArgs e)
            {
            TryCancel();
            }
        #endregion

        #region Change show modes
        private enum ShownModes { All, Tare, Nomenclature }
        private bool skip;

        private void showNomenclature_CheckedChanged(object sender, System.EventArgs e)
            {
            setWaresVisibility(showNomenclatureBarButtonItem.Checked);
            }

        private void setWaresVisibility(bool waresVisible)
            {
            if (!skip)
                {
                skip = true;
                showTareBarButtonItem.Checked = !waresVisible;
                showNomenclatureBarButtonItem.Checked = waresVisible;
                showTareRows(waresVisible ? ShownModes.Nomenclature : ShownModes.Tare);
                skip = false;
                }
            }



        private void showTareRows(ShownModes mode)
            {
            if (mode == ShownModes.All)
                {
                nomenclatureView.Columns[Document.IsTare.ColumnName].FilterInfo = new ColumnFilterInfo();
                }
            else
                {
                string filterExpression = string.Format("[{0}]={1}", Document.IsTare.ColumnName, mode == ShownModes.Tare);
                nomenclatureView.Columns[Document.IsTare.ColumnName].FilterInfo = new ColumnFilterInfo(filterExpression);
                }
            }
        #endregion

        private void barCheckItem1_CheckedChanged(object sender, ItemClickEventArgs e)
            {

            }

        private void showNomenclatureBarButtonItem_CheckedChanged(object sender, ItemClickEventArgs e)
            {
            setWaresVisibility(showNomenclatureBarButtonItem.Checked);
            }

        private void showTareBarButtonItem_CheckedChanged(object sender, ItemClickEventArgs e)
            {
            setWaresVisibility(!showTareBarButtonItem.Checked);
            }
        }
    }