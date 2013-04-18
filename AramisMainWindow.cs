using System;
using System.Windows;
using Aramis.UI.WinFormsDevXpress.Forms;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.HelperClasses.DCT;
using AtosFMCG.HelperClasses.Deleted;
using AtosFMCG.HelperClasses.ViewOfServiceTables;
using AtosFMCG.PrintForms;
using AtosFMCG.TouchScreen;
using Catalogs;
using DevExpress.XtraBars;
using Aramis.Platform;
using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon;
using DevExpress.LookAndFeel;
using Aramis.SystemConfigurations;
using Aramis.UI;
using Aramis;
using StorekeeperManagementServer;

namespace AtosFMCG
    {
    /// <summary>Головна форма системи</summary>
    public partial class AramisMainWindow : RibbonForm, IMainForm
        {
        #region Властивості
        public Action ShowConnectionTroublesForm { get; set; }
        public bool AutoStartMode { get; set; }
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

            //Приховати системні (тестові та інш.) групи 
            updGroup.Visible = openByAdmnin;
            favGroup.Visible = openByAdmnin;
            testGroup.Visible = openByAdmnin;
            dctServerGroup.Visible = openByAdmnin;
            serviceTablesGroup.Visible = openByAdmnin;

            //todo: Додати роль "Менеджер ТСД"
            if (openByAdmnin)
                {
                ltlServerState.Visibility = BarItemVisibility.Always;
                runSMServer();
                }
            }

        private void AramisMainWindow_Shown(object sender, EventArgs e)
            {
            //loadScreen_ItemClick(null, null);
            }
        #endregion

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

        #region Обране
        private void openUsers_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(Users));
            }

        private void openReportsSetting_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(MatrixReports));
            }

        private void openConsts_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowSystemObject(SystemConsts.GetEntity(), new ConstsForm());
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

        #region Службові таблиці
        private void openStockBalance_ItemClick(object sender, ItemClickEventArgs e)
            {
            ViewOfStockBalance view = new ViewOfStockBalance();
            view.Show();
            }

        private void openGoodsMoving_ItemClick(object sender, ItemClickEventArgs e)
            {
            ViewOfGoodsMoving view = new ViewOfGoodsMoving();
            view.Show();
            }

        private void openFilledCell_ItemClick(object sender, ItemClickEventArgs e)
            {
            ViewOfFilledCell view = new ViewOfFilledCell();
            view.Show();
            }
        #endregion
        #endregion

        #region Робота з Терміналом Збіру Даних
        private InfoForm smServer;

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
            {
            runSMServer();
            }

        private void runSMServer()
            {
            try
                {
                if (smServer == null || !smServer.IsRun)
                    {
                    smServer = new InfoForm(ReceiveMessages.ReceiveMessage);

                    if (smServer.IsRun)
                        {
                        serverState.Caption = "Запущено!";
                        serverState.LargeImageIndex = 24;
                        serverState.SuperTip.Items.Clear();
                        serverState.SuperTip.Items.Add("Сервер для роботи з ТСД запущено!");
                        
                        ltlServerState.ImageIndex = 20;
                        ltlServerState.Caption= "Сервер для роботи з ТСД запущено!";
                        }
                    else
                        {
                        serverState.LargeImageIndex = 22;
                        serverState.Caption = "Помилка!";
                        serverState.SuperTip.Items.Clear();
                        serverState.SuperTip.Items.Add("Сервер для роботи з ТСД не зміг запуститись!");

                        ltlServerState.ImageIndex = 1;
                        ltlServerState.Caption = "Сервер для роботи з ТСД не зміг запуститись!";
                        }
                    }
                else if (SystemAramis.CurrentUser.Id == CatalogUsers.Admin.Id)
                    {
                    SendToTCD sendForm = new SendToTCD(smServer);
                    sendForm.Show();
                    }
                }
            catch (Exception exc)
                {
                smServer = null;
                exc.Message.WarningBox();
                serverState.Caption = "Помилка!";
                serverState.LargeImageIndex = 22;
                serverState.SuperTip.Items.Clear();
                serverState.SuperTip.Items.Add(exc.Message);
                }
            }
        #endregion

        #region Для тестів
        private void tstDeleted_ItemClick(object sender, ItemClickEventArgs e)
            {
            AlterDeletedColumn.Run();
            }

        private void tstDeleteMarked_ItemClick(object sender, ItemClickEventArgs e)
            {
            DeleteMarked.GetBlockedInfo(typeof(Cells).Name, 1);
            DeleteMarked.GetBlockedInfo(typeof(Cities).Name, 1);
            }

        private void loadScreen_ItemClick(object sender, ItemClickEventArgs e)
            {
            MainScreen mainScreen = new MainScreen();
            mainScreen.Show();
            } 

        private void printPalletLabel_ItemClick(object sender, ItemClickEventArgs e)
            {
            Window palletPrintForm = new Window();
            PalletPrintForm label = new PalletPrintForm(
                "Живчик 1л Апельсин",
                15,
                15678923,
                new DateTime(2010, 1, 15),
                new DateTime(2013, 05, 09),
                new DateTime(2016, 10, 18),
                "Іванополус",
                "55568408/965",
                DateTime.Now);
            label.UpdateLayout();
            palletPrintForm.Width = 700;
            palletPrintForm.Height = 500;
            palletPrintForm.Content = label;
            palletPrintForm.Show();
            }
        #endregion
        }
    }