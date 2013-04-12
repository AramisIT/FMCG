namespace AtosFMCG
    {
    partial class AramisMainWindow
        {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
            {
            if ( disposing && ( components != null ) )
                {
                components.Dispose();
                }
            base.Dispose(disposing);
            }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
            {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AramisMainWindow));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.smallImagesCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.openCatalogs = new DevExpress.XtraBars.BarButtonItem();
            this.openDocuments = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.UpdateDBStructureButton = new DevExpress.XtraBars.BarButtonItem();
            this.delMarked = new DevExpress.XtraBars.BarButtonItem();
            this.updateSystem = new DevExpress.XtraBars.BarButtonItem();
            this.FastDBUpdateButton = new DevExpress.XtraBars.BarButtonItem();
            this.openRights = new DevExpress.XtraBars.BarButtonItem();
            this.openUsers = new DevExpress.XtraBars.BarButtonItem();
            this.openReportsSetting = new DevExpress.XtraBars.BarButtonItem();
            this.serverState = new DevExpress.XtraBars.BarButtonItem();
            this.openStockBalance = new DevExpress.XtraBars.BarButtonItem();
            this.openGoodsMoving = new DevExpress.XtraBars.BarButtonItem();
            this.openFilledCell = new DevExpress.XtraBars.BarButtonItem();
            this.ltlServerState = new DevExpress.XtraBars.BarButtonItem();
            this.openConsts = new DevExpress.XtraBars.BarButtonItem();
            this.tstDeleted = new DevExpress.XtraBars.BarButtonItem();
            this.tstDeleteMarked = new DevExpress.XtraBars.BarButtonItem();
            this.loadScreen = new DevExpress.XtraBars.BarButtonItem();
            this.largeImagesCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.mainPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.sysObjectsGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.favGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.updGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.serviceTablesGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.dctServerGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.testGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smallImagesCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImagesCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonDropDownControl = this.applicationMenu1;
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ApplicationIcon = global::AtosFMCG.Properties.Resources.TrayIcon;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Images = this.smallImagesCollection;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.openCatalogs,
            this.openDocuments,
            this.barButtonItem2,
            this.UpdateDBStructureButton,
            this.delMarked,
            this.updateSystem,
            this.FastDBUpdateButton,
            this.openRights,
            this.openUsers,
            this.openReportsSetting,
            this.serverState,
            this.openStockBalance,
            this.openGoodsMoving,
            this.openFilledCell,
            this.ltlServerState,
            this.openConsts,
            this.tstDeleted,
            this.tstDeleteMarked,
            this.loadScreen});
            this.ribbon.LargeImages = this.largeImagesCollection;
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 38;
            this.ribbon.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Never;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.mainPage});
            this.ribbon.Size = new System.Drawing.Size(1113, 144);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.Toolbar.ItemLinks.Add(this.ltlServerState);
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.ribbon;
            // 
            // smallImagesCollection
            // 
            this.smallImagesCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("smallImagesCollection.ImageStream")));
            this.smallImagesCollection.Images.SetKeyName(25, "DB.png");
            this.smallImagesCollection.Images.SetKeyName(26, "updDB.png");
            this.smallImagesCollection.Images.SetKeyName(27, "attach.png");
            this.smallImagesCollection.Images.SetKeyName(28, "battery.png");
            this.smallImagesCollection.Images.SetKeyName(29, "bug.png");
            this.smallImagesCollection.Images.SetKeyName(30, "flag_green.png");
            this.smallImagesCollection.Images.SetKeyName(31, "mail.png");
            this.smallImagesCollection.Images.SetKeyName(32, "on.png");
            this.smallImagesCollection.Images.SetKeyName(33, "star.png");
            this.smallImagesCollection.InsertImage(global::AtosFMCG.Properties.Resources.rights, "rights", typeof(global::AtosFMCG.Properties.Resources), 34);
            this.smallImagesCollection.Images.SetKeyName(34, "rights");
            // 
            // openCatalogs
            // 
            this.openCatalogs.Caption = "Довідники";
            this.openCatalogs.Id = 1;
            this.openCatalogs.LargeImageIndex = 0;
            this.openCatalogs.Name = "openCatalogs";
            this.openCatalogs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openCatalogs_ItemClick);
            // 
            // openDocuments
            // 
            this.openDocuments.Caption = "Документи";
            this.openDocuments.Id = 2;
            this.openDocuments.LargeImageIndex = 1;
            this.openDocuments.Name = "openDocuments";
            this.openDocuments.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openDocuments_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 3;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // UpdateDBStructureButton
            // 
            this.UpdateDBStructureButton.Caption = "Оновлення БД з формою";
            this.UpdateDBStructureButton.Id = 5;
            this.UpdateDBStructureButton.ImageIndex = 25;
            this.UpdateDBStructureButton.Name = "UpdateDBStructureButton";
            this.UpdateDBStructureButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.UpdateDBStructureButton_ItemClick);
            // 
            // delMarked
            // 
            this.delMarked.Caption = "Видалення помічених";
            this.delMarked.Id = 6;
            this.delMarked.ImageIndex = 4;
            this.delMarked.Name = "delMarked";
            this.delMarked.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openDeleteMarked_ItemClick);
            // 
            // updateSystem
            // 
            this.updateSystem.Caption = "Оновлення системи";
            this.updateSystem.Id = 10;
            this.updateSystem.ImageIndex = 13;
            this.updateSystem.Name = "updateSystem";
            this.updateSystem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openFullUpdate_ItemClick);
            // 
            // FastDBUpdateButton
            // 
            this.FastDBUpdateButton.Caption = "Оновлення БД без форми";
            this.FastDBUpdateButton.Id = 13;
            this.FastDBUpdateButton.ImageIndex = 25;
            this.FastDBUpdateButton.Name = "FastDBUpdateButton";
            this.FastDBUpdateButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FastDBUpdateButton_ItemClick);
            // 
            // openRights
            // 
            this.openRights.Caption = "Обмеження доступу";
            this.openRights.Id = 16;
            this.openRights.ImageIndex = 34;
            this.openRights.Name = "openRights";
            this.openRights.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openRights_ItemClick);
            // 
            // openUsers
            // 
            this.openUsers.Caption = "Користувачі системи";
            this.openUsers.Id = 25;
            this.openUsers.LargeImageIndex = 10;
            this.openUsers.Name = "openUsers";
            this.openUsers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openUsers_ItemClick);
            // 
            // openReportsSetting
            // 
            this.openReportsSetting.Caption = "Звіти (налаштування)";
            this.openReportsSetting.Id = 26;
            this.openReportsSetting.LargeImageIndex = 31;
            this.openReportsSetting.Name = "openReportsSetting";
            this.openReportsSetting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openReportsSetting_ItemClick);
            // 
            // serverState
            // 
            this.serverState.Caption = "Не запущено";
            this.serverState.Id = 28;
            this.serverState.LargeImageIndex = 4;
            this.serverState.Name = "serverState";
            toolTipTitleItem1.Text = "Сервер не запущено!";
            toolTipItem1.Appearance.Image = global::AtosFMCG.Properties.Resources._1317825614_information_balloon;
            toolTipItem1.Appearance.Options.UseImage = true;
            toolTipItem1.Image = global::AtosFMCG.Properties.Resources._1317825614_information_balloon;
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Сервер роботи з ТЗД не запущено...";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.serverState.SuperTip = superToolTip1;
            this.serverState.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // openStockBalance
            // 
            this.openStockBalance.Caption = "Залишки на складі";
            this.openStockBalance.Glyph = global::AtosFMCG.Properties.Resources.storage;
            this.openStockBalance.Id = 29;
            this.openStockBalance.Name = "openStockBalance";
            this.openStockBalance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openStockBalance_ItemClick);
            // 
            // openGoodsMoving
            // 
            this.openGoodsMoving.Caption = "Переміщення товару";
            this.openGoodsMoving.Glyph = global::AtosFMCG.Properties.Resources.move;
            this.openGoodsMoving.Id = 30;
            this.openGoodsMoving.Name = "openGoodsMoving";
            this.openGoodsMoving.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openGoodsMoving_ItemClick);
            // 
            // openFilledCell
            // 
            this.openFilledCell.Caption = "Розміщення паллет";
            this.openFilledCell.Glyph = global::AtosFMCG.Properties.Resources.order;
            this.openFilledCell.Id = 31;
            this.openFilledCell.Name = "openFilledCell";
            this.openFilledCell.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openFilledCell_ItemClick);
            // 
            // ltlServerState
            // 
            this.ltlServerState.Caption = "barButtonItem1";
            this.ltlServerState.Id = 33;
            this.ltlServerState.ImageIndex = 1;
            this.ltlServerState.Name = "ltlServerState";
            this.ltlServerState.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // openConsts
            // 
            this.openConsts.Caption = "Налаштування";
            this.openConsts.Id = 34;
            this.openConsts.LargeGlyph = global::AtosFMCG.Properties.Resources._1303219717_Settings;
            this.openConsts.Name = "openConsts";
            this.openConsts.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openConsts_ItemClick);
            // 
            // tstDeleted
            // 
            this.tstDeleted.Caption = "add Deleted col";
            this.tstDeleted.Id = 35;
            this.tstDeleted.Name = "tstDeleted";
            this.tstDeleted.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tstDeleted_ItemClick);
            // 
            // tstDeleteMarked
            // 
            this.tstDeleteMarked.Caption = "Del marked";
            this.tstDeleteMarked.Id = 36;
            this.tstDeleteMarked.Name = "tstDeleteMarked";
            this.tstDeleteMarked.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tstDeleteMarked_ItemClick);
            // 
            // loadScreen
            // 
            this.loadScreen.Caption = "load screen";
            this.loadScreen.Id = 37;
            this.loadScreen.Name = "loadScreen";
            this.loadScreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.loadScreen_ItemClick);
            // 
            // largeImagesCollection
            // 
            this.largeImagesCollection.ImageSize = new System.Drawing.Size(32, 32);
            this.largeImagesCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("largeImagesCollection.ImageStream")));
            this.largeImagesCollection.Images.SetKeyName(17, "fenology.png");
            this.largeImagesCollection.Images.SetKeyName(18, "ExternalData.png");
            this.largeImagesCollection.Images.SetKeyName(19, "Priva.png");
            this.largeImagesCollection.Images.SetKeyName(20, "61616175_10bddf4ddcd4.png");
            this.largeImagesCollection.Images.SetKeyName(21, "1306484724_gnome-help.png");
            this.largeImagesCollection.Images.SetKeyName(22, "iWarning.png");
            this.largeImagesCollection.Images.SetKeyName(23, "Tasks for me.png");
            this.largeImagesCollection.Images.SetKeyName(24, "Build_52.png");
            this.largeImagesCollection.Images.SetKeyName(25, "Print.png");
            this.largeImagesCollection.Images.SetKeyName(26, "database_save.png");
            this.largeImagesCollection.Images.SetKeyName(27, "1308134199_page_save.png");
            this.largeImagesCollection.Images.SetKeyName(28, "Tasks to control.png");
            this.largeImagesCollection.Images.SetKeyName(29, "My tasks.png");
            this.largeImagesCollection.Images.SetKeyName(30, "Actions-view-calendar-month-icon.png");
            this.largeImagesCollection.Images.SetKeyName(31, "stock_task.png");
            this.largeImagesCollection.Images.SetKeyName(32, "1311149276_3d bar chart.png");
            // 
            // mainPage
            // 
            this.mainPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.sysObjectsGroup,
            this.favGroup,
            this.updGroup,
            this.serviceTablesGroup,
            this.dctServerGroup,
            this.testGroup});
            this.mainPage.Name = "mainPage";
            this.mainPage.Text = "Головна панель";
            // 
            // sysObjectsGroup
            // 
            this.sysObjectsGroup.ItemLinks.Add(this.openCatalogs);
            this.sysObjectsGroup.ItemLinks.Add(this.openDocuments);
            this.sysObjectsGroup.Name = "sysObjectsGroup";
            this.sysObjectsGroup.Text = "Об\'єкти системи";
            // 
            // favGroup
            // 
            this.favGroup.ItemLinks.Add(this.openUsers);
            this.favGroup.ItemLinks.Add(this.openReportsSetting);
            this.favGroup.ItemLinks.Add(this.openConsts);
            this.favGroup.Name = "favGroup";
            this.favGroup.Text = "Обране";
            // 
            // updGroup
            // 
            this.updGroup.ItemLinks.Add(this.UpdateDBStructureButton);
            this.updGroup.ItemLinks.Add(this.FastDBUpdateButton);
            this.updGroup.ItemLinks.Add(this.updateSystem);
            this.updGroup.ItemLinks.Add(this.delMarked);
            this.updGroup.ItemLinks.Add(this.openRights);
            this.updGroup.Name = "updGroup";
            this.updGroup.ShowCaptionButton = false;
            this.updGroup.Text = "Оновлення";
            // 
            // serviceTablesGroup
            // 
            this.serviceTablesGroup.ItemLinks.Add(this.openStockBalance);
            this.serviceTablesGroup.ItemLinks.Add(this.openGoodsMoving);
            this.serviceTablesGroup.ItemLinks.Add(this.openFilledCell);
            this.serviceTablesGroup.Name = "serviceTablesGroup";
            this.serviceTablesGroup.Text = "Службові таблиці";
            // 
            // dctServerGroup
            // 
            this.dctServerGroup.ItemLinks.Add(this.serverState);
            this.dctServerGroup.Name = "dctServerGroup";
            this.dctServerGroup.Text = "ТЗД сервер";
            // 
            // testGroup
            // 
            this.testGroup.ItemLinks.Add(this.tstDeleted);
            this.testGroup.ItemLinks.Add(this.tstDeleteMarked);
            this.testGroup.ItemLinks.Add(this.loadScreen);
            this.testGroup.Name = "testGroup";
            this.testGroup.Text = "Для тестів";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 522);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1113, 31);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Blue";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Справочники";
            this.barButtonItem4.Id = 16;
            this.barButtonItem4.ImageIndex = 1;
            this.barButtonItem4.LargeImageIndex = 0;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // AramisMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 553);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "AramisMainWindow";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "ATOS FMCG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AramisMainWindow_Load);
            this.Shown += new System.EventHandler(this.AramisMainWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smallImagesCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImagesCollection)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage mainPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup sysObjectsGroup;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.Utils.ImageCollection largeImagesCollection;
        private DevExpress.Utils.ImageCollection smallImagesCollection;
        private DevExpress.XtraBars.BarButtonItem openCatalogs;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem openDocuments;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem UpdateDBStructureButton;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup updGroup;
        private DevExpress.XtraBars.BarButtonItem delMarked;
        private DevExpress.XtraBars.BarButtonItem updateSystem;
        private DevExpress.XtraBars.BarButtonItem FastDBUpdateButton;
        private DevExpress.XtraBars.BarButtonItem openRights;
        private DevExpress.XtraBars.BarButtonItem openUsers;
        private DevExpress.XtraBars.BarButtonItem openReportsSetting;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup favGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup testGroup;
        private DevExpress.XtraBars.BarButtonItem serverState;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup dctServerGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup serviceTablesGroup;
        private DevExpress.XtraBars.BarButtonItem openStockBalance;
        private DevExpress.XtraBars.BarButtonItem openGoodsMoving;
        private DevExpress.XtraBars.BarButtonItem openFilledCell;
        private DevExpress.XtraBars.BarButtonItem ltlServerState;
        private DevExpress.XtraBars.BarButtonItem openConsts;
        private DevExpress.XtraBars.BarButtonItem tstDeleted;
        private DevExpress.XtraBars.BarButtonItem tstDeleteMarked;
        private DevExpress.XtraBars.BarButtonItem loadScreen;
        }
    }