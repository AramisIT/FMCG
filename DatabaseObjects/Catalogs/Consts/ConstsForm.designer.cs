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
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.systemPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalDatabaseName.Properties)).BeginInit();
            this.processPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PermitInstallPalletManually.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.OKButton);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 410);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(486, 23);
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
            this.ribbon.Size = new System.Drawing.Size(486, 54);
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
            this.xtraTabControl.Location = new System.Drawing.Point(0, 54);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.systemPage;
            this.xtraTabControl.Size = new System.Drawing.Size(486, 356);
            this.xtraTabControl.TabIndex = 2;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.processPage,
            this.systemPage});
            this.xtraTabControl.Visible = false;
            // 
            // systemPage
            // 
            this.systemPage.Controls.Add(this.PreviewDatabaseName);
            this.systemPage.Controls.Add(this.OriginalDatabaseName);
            this.systemPage.Controls.Add(this.labelControl33);
            this.systemPage.Controls.Add(this.labelControl32);
            this.systemPage.Name = "systemPage";
            this.systemPage.Size = new System.Drawing.Size(481, 330);
            this.systemPage.Text = "Система";
            // 
            // PreviewDatabaseName
            // 
            this.PreviewDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviewDatabaseName.Location = new System.Drawing.Point(111, 34);
            this.PreviewDatabaseName.MenuManager = this.ribbon;
            this.PreviewDatabaseName.Name = "PreviewDatabaseName";
            this.PreviewDatabaseName.Size = new System.Drawing.Size(362, 20);
            this.PreviewDatabaseName.TabIndex = 8;
            // 
            // OriginalDatabaseName
            // 
            this.OriginalDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OriginalDatabaseName.Location = new System.Drawing.Point(111, 8);
            this.OriginalDatabaseName.MenuManager = this.ribbon;
            this.OriginalDatabaseName.Name = "OriginalDatabaseName";
            this.OriginalDatabaseName.Size = new System.Drawing.Size(362, 20);
            this.OriginalDatabaseName.TabIndex = 7;
            // 
            // labelControl33
            // 
            this.labelControl33.Location = new System.Drawing.Point(11, 37);
            this.labelControl33.Name = "labelControl33";
            this.labelControl33.Size = new System.Drawing.Size(27, 13);
            this.labelControl33.TabIndex = 6;
            this.labelControl33.Text = "Копія";
            // 
            // labelControl32
            // 
            this.labelControl32.Location = new System.Drawing.Point(9, 11);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(86, 13);
            this.labelControl32.TabIndex = 4;
            this.labelControl32.Text = "Основне рішення";
            // 
            // processPage
            // 
            this.processPage.Controls.Add(this.PermitInstallPalletManually);
            this.processPage.Name = "processPage";
            this.processPage.Size = new System.Drawing.Size(481, 330);
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
        }
    }