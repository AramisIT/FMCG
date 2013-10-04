using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using Aramis.UI.WinFormsDevXpress;
using Catalogs;
using DevExpress.XtraBars;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    [View(DBObjectGuid = "AC76395E-4648-41F2-879D-E37F1CEF2500", ViewType = ViewFormType.CatalogItem)]
    public partial class ContractorsItemsForm : DevExpress.XtraBars.Ribbon.RibbonForm, IItemForm
        {
        #region Firlds
        private CatalogTable item;
        public DatabaseObject Item
            {
            get { return item; }
            set { item = (CatalogTable)value; }
            }
        public Contractors Catalog { get { return (Contractors)Item; } }
        #endregion

        public ContractorsItemsForm()
            {
            InitializeComponent();
            }

        #region Result
        private void OK_ItemClick(object sender, ItemClickEventArgs e)
            {
            if (WriteItem())
                {
                Close();
                }
            }

        private void Write_ItemClick(object sender, ItemClickEventArgs e)
            {
            WriteItem();
            }

        private void Cancel_ItemClick(object sender, ItemClickEventArgs e)
            {
            TryCancel();
            }

        private bool WriteItem()
            {
            return Item.Write() == WritingResult.Success;
            }

        private void TryCancel()
            {
            Close();
            }
        #endregion
        }
    }