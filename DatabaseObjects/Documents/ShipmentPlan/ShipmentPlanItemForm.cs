using System.Windows.Forms;
using Aramis.UI.WinFormsDevXpress;
using Aramis.Core;
using Aramis.Enums;
using Aramis.Attributes;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    [View(DBObjectGuid = "029B0572-E5B5-48CD-9805-1211319A5633", ViewType = ViewFormType.DocItem)]
    public partial class ShipmentPlanItemForm : RibbonForm, IItemForm
        {
        #region Fields
        private DocumentTable item;
        public DatabaseObject Item
            {
            get { return item; }
            set { item = (DocumentTable)value; }
            }
        public ShipmentPlan Document
            {
            get { return (ShipmentPlan)item; }
            }
        #endregion

        public ShipmentPlanItemForm()
            {
            InitializeComponent();
            Load += ShipmentPlanItemForm_Load;
            }

        void ShipmentPlanItemForm_Load(object sender, System.EventArgs e)
            {
            setCaptionForCreationButton();
            Document.MovementDocIsAssigned += Document_MovementDocIsAssigned;
            }

        void Document_MovementDocIsAssigned(object sender, System.EventArgs e)
            {
            setCaptionForCreationButton();
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

        private void setCaptionForCreationButton()
            {
            createMovement.Caption = Document.HaveDocMovement ? "Відкрити \"Переміщення\"" : createMovement.Caption;
            }

        private void createMovement_ItemClick(object sender, ItemClickEventArgs e)
            {
            Document.CreateMovement();
            }
        }
    }