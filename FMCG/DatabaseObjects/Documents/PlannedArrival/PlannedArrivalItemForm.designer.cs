using Aramis;

namespace AtosFMCG.DatabaseObjects.Documents
{
    partial class PlannedArrivalItemForm
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
            if (disposing && (components != null))
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
            this.OK = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.cancel = new DevExpress.XtraBars.BarButtonItem();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.TareInfoButtonsBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.NomenclatureInfoButtonsBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.Car = new Aramis.AramisSearchLookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.Driver = new Aramis.AramisSearchLookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.Carrier = new Aramis.AramisSearchLookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.Contractor = new Aramis.AramisSearchLookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.TypeOfArrival = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Date = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.IncomeNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.NomenclatureInfo = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.TareInfo = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.Info = new DevExpress.XtraEditors.LabelControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Car.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Driver.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Contractor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfArrival.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IncomeNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TareInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.OK);
            this.ribbonStatusBar.ItemLinks.Add(this.barButtonItem2);
            this.ribbonStatusBar.ItemLinks.Add(this.cancel);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 485);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(657, 23);
            // 
            // OK
            // 
            this.OK.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.OK.Caption = "OK";
            this.OK.Id = 0;
            this.OK.ImageIndex = 0;
            this.OK.Name = "OK";
            this.OK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OK_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem2.Caption = "Записати";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Write_ItemClick);
            // 
            // cancel
            // 
            this.cancel.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.cancel.Caption = "Відміна";
            this.cancel.Id = 2;
            this.cancel.ImageIndex = 1;
            this.cancel.Name = "cancel";
            this.cancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.cancel_ItemClick);
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.OK,
            this.barButtonItem2,
            this.cancel});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 18;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(657, 54);
            this.ribbon.StatusBar = this.NomenclatureInfoButtonsBar;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // TareInfoButtonsBar
            // 
            this.TareInfoButtonsBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.TareInfoButtonsBar.Location = new System.Drawing.Point(0, 0);
            this.TareInfoButtonsBar.Name = "TareInfoButtonsBar";
            this.TareInfoButtonsBar.Ribbon = this.ribbon;
            this.TareInfoButtonsBar.Size = new System.Drawing.Size(652, 23);
            // 
            // NomenclatureInfoButtonsBar
            // 
            this.NomenclatureInfoButtonsBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.NomenclatureInfoButtonsBar.Location = new System.Drawing.Point(0, 0);
            this.NomenclatureInfoButtonsBar.Name = "NomenclatureInfoButtonsBar";
            this.NomenclatureInfoButtonsBar.Ribbon = this.ribbon;
            this.NomenclatureInfoButtonsBar.Size = new System.Drawing.Size(652, 23);
            // 
            // panelControl
            // 
            this.panelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl.Controls.Add(this.Car);
            this.panelControl.Controls.Add(this.labelControl7);
            this.panelControl.Controls.Add(this.Driver);
            this.panelControl.Controls.Add(this.labelControl6);
            this.panelControl.Controls.Add(this.Carrier);
            this.panelControl.Controls.Add(this.labelControl5);
            this.panelControl.Controls.Add(this.Contractor);
            this.panelControl.Controls.Add(this.labelControl4);
            this.panelControl.Controls.Add(this.TypeOfArrival);
            this.panelControl.Controls.Add(this.labelControl3);
            this.panelControl.Controls.Add(this.Date);
            this.panelControl.Controls.Add(this.labelControl2);
            this.panelControl.Controls.Add(this.IncomeNumber);
            this.panelControl.Controls.Add(this.labelControl1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 54);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(657, 84);
            this.panelControl.TabIndex = 2;
            // 
            // Car
            // 
            this.Car.BaseFilter = null;
            this.Car.Location = new System.Drawing.Point(407, 57);
            this.Car.MenuManager = this.ribbon;
            this.Car.Name = "Car";
            this.Car.Properties.BaseFilter = null;
            this.Car.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Car.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Car.Properties.NullText = "";
            this.Car.Size = new System.Drawing.Size(241, 20);
            this.Car.TabIndex = 6;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(335, 60);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(40, 13);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "Машина";
            // 
            // Driver
            // 
            this.Driver.BaseFilter = null;
            this.Driver.Location = new System.Drawing.Point(85, 57);
            this.Driver.MenuManager = this.ribbon;
            this.Driver.Name = "Driver";
            this.Driver.Properties.BaseFilter = null;
            this.Driver.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Driver.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Driver.Properties.NullText = "";
            this.Driver.Size = new System.Drawing.Size(246, 20);
            this.Driver.TabIndex = 3;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(10, 60);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(27, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "Водій";
            // 
            // Carrier
            // 
            this.Carrier.BaseFilter = null;
            this.Carrier.Location = new System.Drawing.Point(407, 31);
            this.Carrier.MenuManager = this.ribbon;
            this.Carrier.Name = "Carrier";
            this.Carrier.Properties.BaseFilter = null;
            this.Carrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Carrier.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Carrier.Properties.NullText = "";
            this.Carrier.Size = new System.Drawing.Size(241, 20);
            this.Carrier.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(335, 34);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Перевізник";
            // 
            // Contractor
            // 
            this.Contractor.BaseFilter = null;
            this.Contractor.Location = new System.Drawing.Point(85, 31);
            this.Contractor.MenuManager = this.ribbon;
            this.Contractor.Name = "Contractor";
            this.Contractor.Properties.BaseFilter = null;
            this.Contractor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Contractor.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Contractor.Properties.NullText = "";
            this.Contractor.Size = new System.Drawing.Size(246, 20);
            this.Contractor.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(10, 34);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Контрагент";
            // 
            // TypeOfArrival
            // 
            this.TypeOfArrival.Location = new System.Drawing.Point(407, 5);
            this.TypeOfArrival.MenuManager = this.ribbon;
            this.TypeOfArrival.Name = "TypeOfArrival";
            this.TypeOfArrival.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TypeOfArrival.Size = new System.Drawing.Size(132, 20);
            this.TypeOfArrival.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(335, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(64, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Тип приходу";
            // 
            // Date
            // 
            this.Date.EditValue = null;
            this.Date.Location = new System.Drawing.Point(231, 5);
            this.Date.MenuManager = this.ribbon;
            this.Date.Name = "Date";
            this.Date.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Date.Size = new System.Drawing.Size(100, 20);
            this.Date.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(196, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Дата ";
            // 
            // IncomeNumber
            // 
            this.IncomeNumber.Location = new System.Drawing.Point(85, 5);
            this.IncomeNumber.MenuManager = this.ribbon;
            this.IncomeNumber.Name = "IncomeNumber";
            this.IncomeNumber.Size = new System.Drawing.Size(105, 20);
            this.IncomeNumber.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "№ накладної";
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 138);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabPage = this.xtraTabPage1;
            this.tabControl1.Size = new System.Drawing.Size(657, 327);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.NomenclatureInfo);
            this.xtraTabPage1.Controls.Add(this.NomenclatureInfoButtonsBar);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(652, 301);
            this.xtraTabPage1.Text = "Номенклатура";
            // 
            // NomenclatureInfo
            // 
            this.NomenclatureInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NomenclatureInfo.Location = new System.Drawing.Point(0, 23);
            this.NomenclatureInfo.MainView = this.gridView1;
            this.NomenclatureInfo.MenuManager = this.ribbon;
            this.NomenclatureInfo.Name = "NomenclatureInfo";
            this.NomenclatureInfo.Size = new System.Drawing.Size(652, 278);
            this.NomenclatureInfo.TabIndex = 1;
            this.NomenclatureInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.NomenclatureInfo;
            this.gridView1.Name = "gridView1";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.TareInfo);
            this.xtraTabPage2.Controls.Add(this.TareInfoButtonsBar);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(652, 298);
            this.xtraTabPage2.Text = "Тара";
            // 
            // TareInfo
            // 
            this.TareInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TareInfo.Location = new System.Drawing.Point(0, 23);
            this.TareInfo.MainView = this.gridView2;
            this.TareInfo.MenuManager = this.ribbon;
            this.TareInfo.Name = "TareInfo";
            this.TareInfo.Size = new System.Drawing.Size(652, 275);
            this.TareInfo.TabIndex = 3;
            this.TareInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.TareInfo;
            this.gridView2.Name = "gridView2";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.Info);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 465);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(657, 20);
            this.panelControl1.TabIndex = 0;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit1.EditValue = global::FMCG.Properties.Resources._1317825614_information_balloon;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit1.MenuManager = this.ribbon;
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Size = new System.Drawing.Size(20, 20);
            this.pictureEdit1.TabIndex = 3;
            this.pictureEdit1.ToolTip = "Хто останнім редагував документ та дата останнього редагування";
            this.pictureEdit1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.pictureEdit1.ToolTipTitle = "Довідка:";
            // 
            // Info
            // 
            this.Info.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.Info.Location = new System.Drawing.Point(24, 3);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(12, 13);
            this.Info.TabIndex = 1;
            this.Info.Text = "...";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem1.Caption = "OK";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ImageIndex = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem3.Caption = "Записати";
            this.barButtonItem3.Id = 1;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem4.Caption = "Відміна";
            this.barButtonItem4.Id = 2;
            this.barButtonItem4.ImageIndex = 1;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // PlannedArrivalItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 508);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "PlannedArrivalItemForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Item form";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Itemform_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Car.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Driver.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Contractor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfArrival.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IncomeNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TareInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.BarButtonItem OK;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem cancel;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraTab.XtraTabControl tabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl Info;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar TareInfoButtonsBar;
        private DevExpress.XtraGrid.GridControl NomenclatureInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar NomenclatureInfoButtonsBar;
        private DevExpress.XtraGrid.GridControl TareInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private Aramis.AramisSearchLookUpEdit Contractor;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit TypeOfArrival;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit Date;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit IncomeNumber;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private AramisSearchLookUpEdit Driver;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private AramisSearchLookUpEdit Carrier;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private AramisSearchLookUpEdit Car;
        private DevExpress.XtraEditors.LabelControl labelControl7;
    }
}