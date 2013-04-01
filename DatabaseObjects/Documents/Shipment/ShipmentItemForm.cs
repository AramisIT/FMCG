using System.Windows.Forms;
using Aramis.UI.WinFormsDevXpress;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Attributes;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    [View(DBObjectGuid = "003741EB-2D7A-49FD-B711-92A039FB4832", ViewType = ViewFormType.DocItem)]
    public partial class ShipmentItemForm : DevExpress.XtraBars.Ribbon.RibbonForm, IItemForm
        {
        #region Fields
        private DocumentTable item;
        public DatabaseObject Item
            {
            get { return item; }
            set { item = (DocumentTable)value; }
            }
        public Shipment Document
            {
            get { return (Shipment)item; }
            }
        #endregion

        public ShipmentItemForm()
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
            if(showNomenclature.Checked || showTare.Checked)
                {
                skip = true;
                showTare.Checked = false;
                showNomenclature.Checked = false;
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
        private enum ShownModes{All, Tare, Nomenclature}
        private bool skip;

        private void showNomenclature_CheckedChanged(object sender, System.EventArgs e)
            {
            if (!skip)
                {
                skip = true;
                showTare.Checked = false;
                showTareRows(showNomenclature.Checked ? ShownModes.Nomenclature : ShownModes.All);
                skip = false;
                }
            }

        private void showTare_CheckedChanged(object sender, System.EventArgs e)
            {
            if(!skip)
                {
                skip = true;
                showNomenclature.Checked = false;
                showTareRows(showTare.Checked ? ShownModes.Tare : ShownModes.All);
                skip = false;
                }
            }

        private void showTareRows(ShownModes mode)
            {
            if(mode == ShownModes.All)
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
        }
    }