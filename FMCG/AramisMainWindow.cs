﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using Aramis.Core.WritingUtils;
using Aramis.DatabaseConnector;
using Aramis.DatabaseUpdating;
using Aramis.UI.WinFormsDevXpress.Forms;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.HelperClasses.PDT;
using AtosFMCG.HelperClasses.ViewOfServiceTables;
using AtosFMCG.PrintForms;
using AtosFMCG.TouchScreen;
using AtosFMCG.TouchScreen.PalletSticker;
using Catalogs;
using DevExpress.XtraBars;
using Aramis.Platform;
using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon;
using DevExpress.LookAndFeel;
using Aramis.SystemConfigurations;
using Aramis.UI;
using Aramis;
using Documents;
using FMCG.Utils;
using StorekeeperManagementServer;

namespace AtosFMCG
    {
    /// <summary>Головна форма системи</summary>
    public partial class AramisMainWindow : RibbonForm, IMainForm
        {
        #region Властивості
        public Action ShowConnectionTroublesForm { get; set; }
        public bool AutoStartMode { get; set; }
        public ImageCollection SmallImagesCollection
            {
            get { return smallImagesCollection; }
            }
        public ImageCollection LargeImagesCollection
            {
            get { return largeImagesCollection; }
            }
        public new UserLookAndFeel LookAndFeel
            {
            get { return defaultLookAndFeel.LookAndFeel; }
            }

        /// <summary>Форма відкрита Адміністратором</summary>
        private bool openByAdmnin;
        #endregion

        /// <summary>Головна форма системи</summary>
        public AramisMainWindow()
            {
            InitializeComponent();
            }

        /// <summary>Виконання дій при автозапуску</summary>
        public void OnAutoStart() { }

        #region Event handling
        /// <summary>Форму завантажено</summary>
        private void AramisMainWindow_Load(object sender, EventArgs e)
            {
            openByAdmnin = SystemAramis.CurrentUserAdmin;

            adminPropertiesPage.Visible = openByAdmnin;
            //Приховати системні (тестові та інш.) групи 
            // updGroup.Visible = openByAdmnin;
            favGroup.Visible = openByAdmnin;
            testGroup.Visible = openByAdmnin;
            serviceTablesGroup.Visible = openByAdmnin;

            //ТСД сервер
            bool isManagerOfDCT = SystemAramis.CurrentUser.Roles.Rows.Cast<DataRow>().Any(
                row => Convert.ToInt64(row["Role"]) == Users.ManagerOfPDT.Id);

            if (openByAdmnin || isManagerOfDCT)
                {
                ltlServerState.Visibility = BarItemVisibility.Always;
                runSMServer();
                dctServerGroup.Visible = true;
                }
            else
                {
                ltlServerState.Visibility = BarItemVisibility.Never;
                dctServerGroup.Visible = false;
                }
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
        /// <summary>StorekeeperManagementServer </summary>
        private InfoForm smServer;

        private TouchScreenMainForm touchScreenMainForm;

        private void serverState_ItemClick(object sender, ItemClickEventArgs e)
            {
            runSMServer();
            }

        private void runSMServer()
            {
            try
                {
                if (smServer == null || !smServer.IsRun)
                    {
                    smServer = new InfoForm(ReceiveMessages.ReceiveMessage, PDTSettings.AllowedIPs(), Consts.ServerIP,
                                            Consts.UpdateFolderName);

                    if (smServer.Server != null)
                        {
                        Func<List<KeyValuePair<Guid, int>>> method = smServer.Server.CatchingConnection.GetPdtSessions;

                        UIConsts.SessionUpdated += () =>
                            {
                                method().ForEach(kvp => UIConsts.UpdateSession(kvp.Key, kvp.Value));
                            };
                        }

                    if (smServer.IsRun)
                        {
                        showSuccessResultOfConnection();
                        }
                    else
                        {
                        showFailResultOfConnection();
                        }
                    }
                //Якщо сервер запущено і цю дію робить Адмін - відкрити вікно симулювання читання штрих-коду
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
                showFailResultOfConnection();
                }
            }

        void UIConsts_SessionUpdated()
            {
            // UIConsts.UpdateSession();
            }

        private void showSuccessResultOfConnection()
            {
            serverState.Caption = "Запущено!";
            serverState.LargeImageIndex = 24;
            serverState.SuperTip.Items.Clear();
            serverState.SuperTip.Items.Add("Сервер для роботи з ТЗД запущено!");

            //ltlServerState.ImageIndex = 20;
            //ltlServerState.Caption = "Сервер для роботи з ТЗД запущено!";
            }

        private void showFailResultOfConnection()
            {
            serverState.LargeImageIndex = 22;
            serverState.Caption = "Помилка!";
            serverState.SuperTip.Items.Clear();
            serverState.SuperTip.Items.Add("Сервер для роботи з ТЗД не зміг запуститись!");

            //ltlServerState.ImageIndex = 1;
            //ltlServerState.Caption = "Сервер для роботи з ТЗД не зміг запуститись!";
            }
        #endregion

        #region Для тестів
        private void loadScreen_ItemClick(object sender, ItemClickEventArgs e)
            {
            if (touchScreenMainForm == null || touchScreenMainForm.IsDisposed)
                {
                touchScreenMainForm = new TouchScreenMainForm();
                }
            touchScreenMainForm.Show();
            touchScreenMainForm.Focus();
            }

        private void printPalletLabel_ItemClick(object sender, ItemClickEventArgs e)
            {
            var tasks = new List<StickerInfo>()
                {
                    new StickerInfo() {Nomenclature = "Живчик 1л Апельсин sdf sdfs dfsdfds", Barcode = "ыва",  Driver = "Жорняк", 
                ReleaseDate = new DateTime(2013, 7,1),
                HalpExpiryDate = new DateTime(2013, 8,30),
                ExpiryDate = new DateTime(2013, 10,30),
                AcceptionDate = DateTime.Now,
                PacksCount = 150,
                    Id=99443},
                //    new StickerInfo() {Nomenclature = "Пиво светлое 0.5", Barcode = "ыва",  Driver = "Жорняк", 
                //ReleaseDate = new DateTime(2013, 7,1),
                //HalpExpiryDate = new DateTime(2013, 8,30),
                //ExpiryDate = new DateTime(2013, 10,30),
                //AcceptionDate = DateTime.Now,
                //PacksCount = 150},
                    new StickerInfo() {Nomenclature = "Пиво темное 0.5", Barcode = "ыва",  Driver = "Жорняк", 
                ReleaseDate = new DateTime(2013, 7,1),
                HalpExpiryDate = new DateTime(2013, 8,30),
                ExpiryDate = new DateTime(2013, 10,30),
                AcceptionDate = DateTime.Now,
                PacksCount = 150,Id=99443}
                };

            var stickersCreator = new StickersPrintingHelper(tasks, ThermoPrinters.GetCurrentPrinterName());
            stickersCreator.Print();

            //Window palletPrintForm = new Window();
            //PalletPrintForm label = new PalletPrintForm(
            //    "Живчик 1л Апельсин",
            //    15,
            //    15678923,
            //    new DateTime(2010, 1, 15),
            //    new DateTime(2013, 05, 09),
            //    new DateTime(2016, 10, 18),
            //    "Іванопополус",
            //    "55568408/965",
            //    DateTime.Now);
            //label.UpdateLayout();
            //palletPrintForm.Width = 700;
            //palletPrintForm.Height = 500;
            //palletPrintForm.Content = label;
            //palletPrintForm.Show();
            }

        private void tstInvoke_ItemClick(object sender, ItemClickEventArgs e)
            {
            string result = InvokeStringMethod<string>("Test", new object[] { 1.ToString(), "222" });
            Console.Write(result);
            }

        public static T InvokeStringMethod<T>(string methodName, object[] args)
            {
            Type calledType = typeof(ReceiveMessages);

            return (T)calledType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                null, null, args);
            }
        #endregion

        private void starterUploadBarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
            {
            PlatformMethods.UploadLoaderFiles(true);
            }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(AcceptancePlan));
            }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(AcceptanceOfGoods));
            }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
            {
            PlatformMethods.UploadLoaderFiles(true);
            }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(Stickers));
            }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
            {
            new RemaindersUpdater().Update();
            }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(PDTFuncsTests));
            }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(Inventory));
            }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(Moving));
            }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(Nomenclature));
            }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowReport("Залишки на складі");
            }

        private void _PDTUpdateButton_ItemClick(object sender, ItemClickEventArgs e)
            {
            PlatformMethods.UpdatePDTFiles();
            }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(ShipmentPlan));
            }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(MatrixAdapters));
            }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowReport("Послідовність палет");
            }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowList(typeof(Shipment));
            }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
            {
            ReceiveMessages.Сommunication.CreatePickingDocuments();
            }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
            {
            var locker = new DatabaseObjectLocker(typeof(Moving), 4);
            var result = locker.LockForCurrentPdtThread();
            Trace.WriteLine(result);
            }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowReport("Розбіжності при відборі");
            }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
            {
            UserInterface.Current.ShowReport("Штрих-код працівника");
            }
        }
    }