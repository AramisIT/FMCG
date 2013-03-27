using System;
using Catalogs;
using DevExpress.XtraBars;
using Aramis.Platform;
using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon;
using DevExpress.LookAndFeel;
using Aramis.SystemConfigurations;
using Aramis.UI;
using Aramis;

namespace AtosFMCG
    {
    /// <summary>Головна форма системи</summary>
    public partial class AramisMainWindow : RibbonForm, IMainForm
        {
        #region Властивості
        public Action ShowConnectionTroublesForm { get; set; }
        public bool AutoStartMode{get;set;}
        public ImageCollection SmallImagesCollection { get { return smallImagesCollection; } }
        public ImageCollection LargeImagesCollection { get { return largeImagesCollection; } }
        new public UserLookAndFeel LookAndFeel { get { return defaultLookAndFeel.LookAndFeel; } }

        /// <summary>Форма відкрита Адміністратором</summary>
        private bool openByAdmnin;
        #endregion

        /// <summary>Головна форма системи</summary>
        public AramisMainWindow()
            {
            InitializeComponent();
            }

        /// <summary>Виконання дій при автозапуску</summary>
        public void OnAutoStart()
            {
            }

        #region Event handling
        /// <summary>Форму завантажено</summary>
        private void AramisMainWindow_Load(object sender, EventArgs e)
            {
            openByAdmnin = SystemAramis.CurrentUser.Ref == CatalogUsers.Admin;
            UpdateGroup.Visible = openByAdmnin;
            }
        #endregion

        /// <summary>Відкрити довідник "Користувачі"</summary>
        private void openUsers_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(Users));
            }

        private void openRoles_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(Users));
            }

        #region Головна панель
        #region Объекты системы
        /// <summary>Відкрити список довідників</summary>
        private void openCatalogs_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowObjectSelectingView(AramisObjectType.Catalog);
            }

        /// <summary>Відкрити список документів</summary>
        private void openDocuments_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowObjectSelectingView(AramisObjectType.Document);
            }
        #endregion

        #region Оновлення
        /// <summary>Оновлення БД з формою</summary>
        private void UpdateDBStructureButton_ItemClick(object sender, ItemClickEventArgs e)
            {
            PlatformMethods.UpdateDB(true);
            }

        /// <summary>Онивлення БД без форми</summary>
        private void FastDBUpdateButton_ItemClick(object sender, ItemClickEventArgs e)
            {
            PlatformMethods.UpdateDB(false);
            }

        /// <summary>Видалення помічених</summary>
        private void openDeleteMarked_ItemClick(object sender, ItemClickEventArgs e)
            {
            new DeleteMarkedObjectsForm().Show();
            }

        /// <summary>Повне оновлення</summary>
        private void openFullUpdate_ItemClick(object sender, ItemClickEventArgs e)
            {
            PlatformMethods.UpdateSolution();
            }

        /// <summary>Права</summary>
        private void openRights_ItemClick(object sender, ItemClickEventArgs e)
            {
            PlatformMethods.ObjectsPermissions();
            }
        #endregion 
        #endregion
        }
    }