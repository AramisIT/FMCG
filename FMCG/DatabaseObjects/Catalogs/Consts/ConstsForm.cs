using System.Windows.Forms;
using DevExpress.XtraBars;
using System;
using Catalogs;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTab;
using Aramis.Platform;

namespace Aramis.UI.WinFormsDevXpress.Forms
{
    public partial class ConstsForm : RibbonForm
    {
        public enum ConstsPages
        {
            processPage,
            systemPage,
            pdtSettings
        }

        private readonly ConstsPages FirstPage;
        
        #region Constructor
        public ConstsForm()
        {
            InitializeComponent();

            lock (SystemConsts.locker)
            {
                // Если мы сюда попали, значит обновление не начнется пока мы не выйдем
                SystemConsts.СonstsAutoUpdating = false;
            }
        }

        public ConstsForm(ConstsPages firstPage):this()
        {
            FirstPage = firstPage;
        }
        #endregion

        #region Open/Close
        private void Itemform_Load(object sender, EventArgs e)
        {
            SetVisibleTabs();
            xtraTabControl.SelectedTabPageIndex = (int) FirstPage;
        }

        private void ConstsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemConsts.СonstsAutoUpdating = true;
        }
        #endregion

        #region Navigation
        private void Itemform_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        private void OKButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }
        #endregion

        #region Options
        private void SetVisibleTabs()
        {
            if (SystemAramis.CurrentUser.Ref == CatalogUsers.Admin)
            {
                foreach (XtraTabPage tab in xtraTabControl.TabPages)
                {
                    tab.PageVisible = true;
                }
            }
        }
        #endregion
    }
}