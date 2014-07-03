using System;
using System.Text;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Platform;
using Aramis.SystemConfigurations;
using Aramis.Attributes;
using Aramis.UI.WinFormsDevXpress;
using AtosFMCG.HelperClasses.PDT;
using Catalogs;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Aramis.Core;
using System.Windows.Forms;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    [View(DBObjectGuid = "A5726B61-52EF-4415-940C-F7C055C7FC36", ViewType = ViewFormType.CatalogItem)]
    public partial class PDTFuncsTestsItemForm : RibbonForm, IItemForm
        {

        #region Поля и свойства

        private PDTFuncsTests calatog;

        public IDatabaseObject Item
            {
            get
                {
                return calatog;
                }
            set
                {
                calatog = (PDTFuncsTests)value;
                }
            }

        #endregion

        #region Event handling
        private void Itemform_Load(object sender, EventArgs e)
            {

            }

        public PDTFuncsTestsItemForm()
            {
            InitializeComponent();
            }

        private void TryCancel()
            {
            Close();
            }

        private void Itemform_KeyDown(object sender, KeyEventArgs e)
            {

            }

        #endregion

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

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
            {
            TryCancel();
            }

        private void executeButton_ItemClick(object sender, ItemClickEventArgs e)
            {
            ExecuteMethod();
            }

        private void ExecuteMethod()
            {
            object[] result = null;

            try
                {
                var parameters = calatog.GetParametersValues();
                result = ReceiveMessages.ReceiveMessage(calatog.Description, parameters, (int)SystemAramis.CurrentUserId);
                }
            catch (Exception exp)
                {
                string.Format("Исключение при выполнении метода: {0}", exp.Message);
                return;
                }

            var message = new StringBuilder();
            int index = 0;
            foreach (var returnValue in result)
                {
                message.AppendWithSeparatorFormat(string.Format("[{0}] = {1}\tType = {2}", index, returnValue.ToString().PadRight(25),
                    (returnValue ?? new object()).GetType()));
                index++;
                }

            message.ToString().AlertBox();
            }
        }
    }