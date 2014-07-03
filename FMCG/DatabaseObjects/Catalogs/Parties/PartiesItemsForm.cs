using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using Aramis.UI.WinFormsDevXpress;
using Catalogs;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    [View(DBObjectGuid = "AEDDF72B-5CD8-4702-A464-A8439D345D11", ViewType = ViewFormType.CatalogItem)]
    public partial class PartiesItemsForm : RibbonForm, IItemForm
        {
        #region Firlds
        private CatalogTable item;
        public IDatabaseObject Item
            {
            get { return item; }
            set { item = (CatalogTable)value; }
            }
        public Parties Catalog { get { return (Parties)Item; } }
        #endregion

        public PartiesItemsForm()
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