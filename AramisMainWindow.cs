﻿using System;
using AtosFMCG.HelperClasses.DCT;
using AtosFMCG.HelperClasses.ViewOfServiceTables;
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

            if (openByAdmnin)
                {
                runSMServer();
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
                        serverState.SuperTip.Items.Add("Сервер запущено!");
                        }
                    else
                        {
                        serverState.LargeImageIndex = 22;
                        serverState.Caption = "Помилка!";
                        serverState.SuperTip.Items.Clear();
                        serverState.SuperTip.Items.Add("Сервер не зміг запуститись!");
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

        #region Термінал Збіру Даних
        private InfoForm smServer;

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
            {
            runSMServer();
            }
        #endregion
        #endregion
        }
    }