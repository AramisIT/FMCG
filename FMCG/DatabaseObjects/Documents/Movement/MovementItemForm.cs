using System.Windows.Forms;
using Aramis.UI.WinFormsDevXpress;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Attributes;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using Documents;
using FMCG.Utils;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    [View(DBObjectGuid = "015CC1EA-D666-431E-9D08-510395C78E4C", ViewType = ViewFormType.DocItem)]
    public partial class MovementItemForm : DevExpress.XtraBars.Ribbon.RibbonForm, IItemForm
        {
        #region Fields
        private DocumentTable item;
        public DatabaseObject Item
            {
            get { return item; }
            set { item = (DocumentTable)value; }
            }
        public Moving Document
            {
            get { return (Moving)item; }
            }
        #endregion

        public MovementItemForm()
            {
            InitializeComponent();
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

        #endregion

        private void nomenclatureView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
            {
            e.Appearance.BackColor = nomenclatureView.GetDataRow(e.RowHandle).GetRowColor();
            }
        }
    }