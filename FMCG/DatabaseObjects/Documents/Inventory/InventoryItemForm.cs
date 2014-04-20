using System.Windows.Forms;
using Aramis.DatabaseConnector;
using Aramis.UI.WinFormsDevXpress;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Attributes;
using AtosFMCG.Enums;
using DevExpress.XtraBars;
using Documents;
using FMCG.Utils;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    [View(DBObjectGuid = "0E87222E-830D-466A-826F-8ABBC4B36FEE", ViewType = ViewFormType.DocItem)]
    public partial class InventoryItemForm : DevExpress.XtraBars.Ribbon.RibbonForm, IItemForm
        {
        #region Fields
        private DocumentTable item;
        public DatabaseObject Item
            {
            get { return item; }
            set { item = (DocumentTable)value; }
            }
        public Inventory Document
            {
            get { return (Inventory)item; }
            }
        #endregion

        public InventoryItemForm()
            {
            InitializeComponent();
            Load += InventoryItemForm_Load;
            }

        private void InventoryItemForm_Load(object sender, System.EventArgs e)
            {

            Document.PropertyChanged += Document_PropertyChanged;
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

        private void Document_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
            switch (e.PropertyName)
                {
                case "State":

                    break;
                }
            }

        private void createTask_ItemClick(object sender, ItemClickEventArgs e)
            {

            }

        private void labelControl2_Click(object sender, System.EventArgs e)
            {

            }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
            {
            e.Appearance.BackColor = nomenclatureView.GetDataRow(e.RowHandle).GetRowColor();
            }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
            {
            Document.FixWrongRelations();
            }

        }
    }