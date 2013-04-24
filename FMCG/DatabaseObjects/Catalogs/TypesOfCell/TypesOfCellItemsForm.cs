using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using Aramis.UI.WinFormsDevXpress;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    [View(DBObjectGuid = "A54A0C77-9677-4740-B3EC-5B9DB6EB3749", ViewType = ViewFormType.CatalogItem)]
    public partial class TypesOfCellItemsForm : RibbonForm, IItemForm
        {
        #region Firlds
        private CatalogTable item;
        public DatabaseObject Item
            {
            get { return item; }
            set { item = (CatalogTable)value; }
            }
        public TypesOfCell Catalog { get { return (TypesOfCell)Item; } }
        #endregion

        public TypesOfCellItemsForm()
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