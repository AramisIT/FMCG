namespace Aramis.UI.WinFormsDevXpress.Forms
    {
    partial class ConstsForm
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
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.OKButton = new DevExpress.XtraBars.BarButtonItem();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.systemPage = new DevExpress.XtraTab.XtraTabPage();
            this.PreviewDatabaseName = new DevExpress.XtraEditors.TextEdit();
            this.OriginalDatabaseName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl33 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl32 = new DevExpress.XtraEditors.LabelControl();
            this.processPage = new DevExpress.XtraTab.XtraTabPage();
            this.PermitInstallPalletManually = new DevExpress.XtraEditors.CheckEdit();
            this.dctSettings = new DevExpress.XtraTab.XtraTabPage();
            this.ServerIP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.UpdateFolderName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.systemPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalDatabaseName.Properties)).BeginInit();
            this.processPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PermitInstallPalletManually.Properties)).BeginInit();
            this.dctSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateFolderName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.OKButton);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 402);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(486, 31);
            // 
            // OKButton
            // 
            this.OKButton.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.OKButton.Caption = "OK";
            this.OKButton.Id = 6;
            this.OKButton.ImageIndex = 0;
            this.OKButton.Name = "OKButton";
            this.OKButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OKButton_ItemClick);
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.barButtonGroup1,
            this.OKButton});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 9;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(486, 49);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // barButtonGroup1
            // 
            this.barButtonGroup1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonGroup1.Caption = "barButtonGroup1";
            this.barButtonGroup1.Id = 4;
            this.barButtonGroup1.ItemLinks.Add(this.OKButton);
            this.barButtonGroup1.MenuAppearance.AppearanceMenu.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.barButtonGroup1.MenuAppearance.AppearanceMenu.Normal.Options.UseFont = true;
            this.barButtonGroup1.MenuAppearance.MenuBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.barButtonGroup1.MenuAppearance.MenuBar.Options.UseFont = true;
            this.barButtonGroup1.MenuAppearance.MenuCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.barButtonGroup1.MenuAppearance.MenuCaption.Options.UseFont = true;
            this.barButtonGroup1.MenuAppearance.SideStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.barButtonGroup1.MenuAppearance.SideStrip.Options.UseFont = true;
            this.barButtonGroup1.Name = "barButtonGroup1";
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl.Location = new System.Drawing.Point(0, 49);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.systemPage;
            this.xtraTabControl.Size = new System.Drawing.Size(486, 353);
            this.xtraTabControl.TabIndex = 2;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.processPage,
            this.systemPage,
            this.dctSettings});
            this.xtraTabControl.Visible = false;
            // 
            // systemPage
            // 
            this.systemPage.Controls.Add(this.PreviewDatabaseName);
            this.systemPage.Controls.Add(this.OriginalDatabaseName);
            this.systemPage.Controls.Add(this.labelControl33);
            this.systemPage.Controls.Add(this.labelControl32);
            this.systemPage.Name = "systemPage";
            this.systemPage.Size = new System.Drawing.Size(480, 325);
            this.systemPage.Text = "Система";
            // 
            // PreviewDatabaseName
            // 
            this.PreviewDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewDatabaseName.Location = new System.Drawing.Point(111, 38);
            this.PreviewDatabaseName.MenuManager = this.ribbon;
            this.PreviewDatabaseName.Name = "PreviewDatabaseName";
            this.PreviewDatabaseName.Size = new System.Drawing.Size(362, 20);
            this.PreviewDatabaseName.TabIndex = 8;
            // 
            // OriginalDatabaseName
            // 
            this.OriginalDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OriginalDatabaseName.Location = new System.Drawing.Point(111, 12);
            this.OriginalDatabaseName.MenuManager = this.ribbon;
            this.OriginalDatabaseName.Name = "OriginalDatabaseName";
            this.OriginalDatabaseName.Size = new System.Drawing.Size(362, 20);
            this.OriginalDatabaseName.TabIndex = 7;
            // 
            // labelControl33
            // 
            this.labelControl33.Location = new System.Drawing.Point(11, 41);
            this.labelControl33.Name = "labelControl33";
            this.labelControl33.Size = new System.Drawing.Size(27, 13);
            this.labelControl33.TabIndex = 6;
            this.labelControl33.Text = "Копія";
            // 
            // labelControl32
            // 
            this.labelControl32.Location = new System.Drawing.Point(9, 15);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(86, 13);
            this.labelControl32.TabIndex = 4;
            this.labelControl32.Text = "Основне рішення";
            // 
            // processPage
            // 
            this.processPage.Controls.Add(this.PermitInstallPalletManually);
            this.processPage.Name = "processPage";
            this.processPage.Size = new System.Drawing.Size(480, 325);
            this.processPage.Text = "Процеси";
            // 
            // PermitInstallPalletManually
            // 
            this.PermitInstallPalletManually.Location = new System.Drawing.Point(8, 12);
            this.PermitInstallPalletManually.MenuManager = this.ribbon;
            this.PermitInstallPalletManually.Name = "PermitInstallPalletManually";
            this.PermitInstallPalletManually.Properties.Caption = "Дозволити карщику встановлювати паллети самостійно";
            this.PermitInstallPalletManually.Size = new System.Drawing.Size(449, 19);
            this.PermitInstallPalletManually.TabIndex = 0;
            // 
            // dctSettings
            // 
            this.dctSettings.Controls.Add(this.label2);
            this.dctSettings.Controls.Add(this.label1);
            this.dctSettings.Controls.Add(this.UpdateFolderName);
            this.dctSettings.Controls.Add(this.labelControl2);
            this.dctSettings.Controls.Add(this.ServerIP);
            this.dctSettings.Controls.Add(this.labelControl1);
            this.dctSettings.Name = "dctSettings";
            this.dctSettings.Size = new System.Drawing.Size(480, 325);
            this.dctSettings.Text = "Налаштування ТСД";
            // 
            // ServerIP
            // 
            this.ServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerIP.Location = new System.Drawing.Point(111, 12);
            this.ServerIP.MenuManager = this.ribbon;
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Properties.Mask.EditMask = "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(" +
    "25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
            this.ServerIP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.ServerIP.Size = new System.Drawing.Size(140, 20);
            this.ServerIP.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "IP серверу";
            // 
            // UpdateFolderName
            // 
            this.UpdateFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateFolderName.Location = new System.Drawing.Point(111, 38);
            this.UpdateFolderName.MenuManager = this.ribbon;
            this.UpdateFolderName.Name = "UpdateFolderName";
            this.UpdateFolderName.Size = new System.Drawing.Size(362, 20);
            this.UpdateFolderName.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Папка обміну";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(108, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "C:\\Program Files\\Logiston SM Server\\Update";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(6, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(467, 30);
            this.label2.TabIndex = 13;
            this.label2.Text = "Будь ласка, не забудьте, що у довіднику \'Налаштування ТСД\' необхідно вказати IP а" +
    "дресси ТСД яким дозволено підлючатись до серверу";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConstsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 433);
            this.Controls.Add(this.xtraTabControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(299, 434);
            this.Name = "ConstsForm";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Константы системы";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConstsForm_FormClosed);
            this.Load += new System.EventHandler(this.Itemform_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Itemform_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.systemPage.ResumeLayout(false);
            this.systemPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalDatabaseName.Properties)).EndInit();
            this.processPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PermitInstallPalletManually.Properties)).EndInit();
            this.dctSettings.ResumeLayout(false);
            this.dctSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateFolderName.Properties)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup1;
        private DevExpress.XtraBars.BarButtonItem OKButton;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage systemPage;
        private DevExpress.XtraEditors.LabelControl labelControl33;
        private DevExpress.XtraEditors.LabelControl labelControl32;
        private DevExpress.XtraEditors.TextEdit PreviewDatabaseName;
        private DevExpress.XtraEditors.TextEdit OriginalDatabaseName;
        private DevExpress.XtraTab.XtraTabPage processPage;
        private DevExpress.XtraEditors.CheckEdit PermitInstallPalletManually;
        private DevExpress.XtraTab.XtraTabPage dctSettings;
        private DevExpress.XtraEditors.TextEdit UpdateFolderName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit ServerIP;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        }
    }