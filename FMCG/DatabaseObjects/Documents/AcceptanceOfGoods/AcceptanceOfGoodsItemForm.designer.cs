using Aramis;

namespace AtosFMCG.DatabaseObjects.Documents
{
    partial class AcceptanceOfGoodsItemForm
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
            this.showTareBarButtonItem = new DevExpress.XtraBars.BarCheckItem();
            this.showNomenclatureBarButtonItem = new DevExpress.XtraBars.BarCheckItem();
            this.openPalletButton = new DevExpress.XtraBars.BarButtonItem();
            this.NomenclatureInfoButtonsBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.PlansBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.date = new DevExpress.XtraEditors.DateEdit();
            this.Car = new DevExpress.XtraEditors.LabelControl();
            this.Carrier = new DevExpress.XtraEditors.LabelControl();
            this.Driver = new DevExpress.XtraEditors.LabelControl();
            this.Contractor = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.State = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.label = new DevExpress.XtraEditors.LabelControl();
            this.NomenclatureInfo = new DevExpress.XtraGrid.GridControl();
            this.nomenclatureView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.Info = new DevExpress.XtraEditors.LabelControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.waresTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.plansTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.Plans = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItem2 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.createAcceptanceButtonItem = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.date.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.State.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomenclatureView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.waresTabPage.SuspendLayout();
            this.plansTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Plans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.OK);
            this.ribbonStatusBar.ItemLinks.Add(this.barButtonItem2);
            this.ribbonStatusBar.ItemLinks.Add(this.cancel);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 572);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(884, 31);
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
            this.barButtonItem2.Caption = "��������";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Write_ItemClick);
            // 
            // cancel
            // 
            this.cancel.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.cancel.Caption = "³����";
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
            this.cancel,
            this.showTareBarButtonItem,
            this.showNomenclatureBarButtonItem,
            this.openPalletButton});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 22;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(884, 49);
            this.ribbon.StatusBar = this.PlansBar;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // showTareBarButtonItem
            // 
            this.showTareBarButtonItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.showTareBarButtonItem.Caption = "����";
            this.showTareBarButtonItem.Id = 18;
            this.showTareBarButtonItem.Name = "showTareBarButtonItem";
            this.showTareBarButtonItem.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.showTareBarButtonItem_CheckedChanged);
            // 
            // showNomenclatureBarButtonItem
            // 
            this.showNomenclatureBarButtonItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.showNomenclatureBarButtonItem.Caption = "������������";
            this.showNomenclatureBarButtonItem.Id = 20;
            this.showNomenclatureBarButtonItem.Name = "showNomenclatureBarButtonItem";
            this.showNomenclatureBarButtonItem.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.showNomenclatureBarButtonItem_CheckedChanged);
            // 
            // openPalletButton
            // 
            this.openPalletButton.Caption = "�������� ������";
            this.openPalletButton.Id = 21;
            this.openPalletButton.Name = "openPalletButton";
            this.openPalletButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.openPalletButton_ItemClick);
            // 
            // NomenclatureInfoButtonsBar
            // 
            this.NomenclatureInfoButtonsBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.NomenclatureInfoButtonsBar.ItemLinks.Add(this.showNomenclatureBarButtonItem);
            this.NomenclatureInfoButtonsBar.ItemLinks.Add(this.showTareBarButtonItem);
            this.NomenclatureInfoButtonsBar.ItemLinks.Add(this.openPalletButton);
            this.NomenclatureInfoButtonsBar.Location = new System.Drawing.Point(0, 0);
            this.NomenclatureInfoButtonsBar.Name = "NomenclatureInfoButtonsBar";
            this.NomenclatureInfoButtonsBar.Ribbon = this.ribbon;
            this.NomenclatureInfoButtonsBar.Size = new System.Drawing.Size(878, 27);
            // 
            // PlansBar
            // 
            this.PlansBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PlansBar.Location = new System.Drawing.Point(0, 0);
            this.PlansBar.Name = "PlansBar";
            this.PlansBar.Ribbon = this.ribbon;
            this.PlansBar.Size = new System.Drawing.Size(878, 27);
            // 
            // panelControl
            // 
            this.panelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl.Controls.Add(this.date);
            this.panelControl.Controls.Add(this.Car);
            this.panelControl.Controls.Add(this.Carrier);
            this.panelControl.Controls.Add(this.Driver);
            this.panelControl.Controls.Add(this.Contractor);
            this.panelControl.Controls.Add(this.labelControl7);
            this.panelControl.Controls.Add(this.labelControl6);
            this.panelControl.Controls.Add(this.labelControl5);
            this.panelControl.Controls.Add(this.labelControl4);
            this.panelControl.Controls.Add(this.State);
            this.panelControl.Controls.Add(this.labelControl3);
            this.panelControl.Controls.Add(this.label);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 49);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(884, 94);
            this.panelControl.TabIndex = 2;
            // 
            // date
            // 
            this.date.EditValue = null;
            this.date.Location = new System.Drawing.Point(366, 8);
            this.date.MenuManager = this.ribbon;
            this.date.Name = "date";
            this.date.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.date.Size = new System.Drawing.Size(100, 20);
            this.date.TabIndex = 17;
            // 
            // Car
            // 
            this.Car.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Car.Location = new System.Drawing.Point(366, 72);
            this.Car.Name = "Car";
            this.Car.Size = new System.Drawing.Size(21, 13);
            this.Car.TabIndex = 16;
            this.Car.Text = "{0}";
            // 
            // Carrier
            // 
            this.Carrier.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Carrier.Location = new System.Drawing.Point(366, 53);
            this.Carrier.Name = "Carrier";
            this.Carrier.Size = new System.Drawing.Size(21, 13);
            this.Carrier.TabIndex = 15;
            this.Carrier.Text = "{0}";
            // 
            // Driver
            // 
            this.Driver.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Driver.Location = new System.Drawing.Point(99, 72);
            this.Driver.Name = "Driver";
            this.Driver.Size = new System.Drawing.Size(21, 13);
            this.Driver.TabIndex = 14;
            this.Driver.Text = "{0}";
            // 
            // Contractor
            // 
            this.Contractor.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Contractor.Location = new System.Drawing.Point(99, 53);
            this.Contractor.Name = "Contractor";
            this.Contractor.Size = new System.Drawing.Size(21, 13);
            this.Contractor.TabIndex = 13;
            this.Contractor.Text = "{0}";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(261, 72);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(40, 13);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "������";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(10, 72);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "����";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(261, 53);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(57, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "���������";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(10, 53);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "����������";
            // 
            // State
            // 
            this.State.Location = new System.Drawing.Point(99, 8);
            this.State.MenuManager = this.ribbon;
            this.State.Name = "State";
            this.State.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.State.Size = new System.Drawing.Size(156, 20);
            this.State.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(82, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "���� ���������";
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(272, 11);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(83, 13);
            this.label.TabIndex = 2;
            this.label.Text = "���� ���������";
            // 
            // NomenclatureInfo
            // 
            this.NomenclatureInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NomenclatureInfo.Location = new System.Drawing.Point(0, 27);
            this.NomenclatureInfo.MainView = this.nomenclatureView;
            this.NomenclatureInfo.MenuManager = this.ribbon;
            this.NomenclatureInfo.Name = "NomenclatureInfo";
            this.NomenclatureInfo.Size = new System.Drawing.Size(878, 354);
            this.NomenclatureInfo.TabIndex = 1;
            this.NomenclatureInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.nomenclatureView});
            // 
            // nomenclatureView
            // 
            this.nomenclatureView.GridControl = this.NomenclatureInfo;
            this.nomenclatureView.Name = "nomenclatureView";
            this.nomenclatureView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.nomenclatureView_RowStyle);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.Info);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 552);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(884, 20);
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
            this.pictureEdit1.ToolTip = "��� �������� ��������� �������� �� ���� ���������� �����������";
            this.pictureEdit1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.pictureEdit1.ToolTipTitle = "������:";
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
            this.barButtonItem3.Caption = "��������";
            this.barButtonItem3.Id = 1;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem4.Caption = "³����";
            this.barButtonItem4.Id = 2;
            this.barButtonItem4.ImageIndex = 1;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 143);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.waresTabPage;
            this.tabControl.Size = new System.Drawing.Size(884, 409);
            this.tabControl.TabIndex = 9;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.waresTabPage,
            this.plansTabPage});
            // 
            // waresTabPage
            // 
            this.waresTabPage.Controls.Add(this.NomenclatureInfo);
            this.waresTabPage.Controls.Add(this.NomenclatureInfoButtonsBar);
            this.waresTabPage.Name = "waresTabPage";
            this.waresTabPage.Size = new System.Drawing.Size(878, 381);
            this.waresTabPage.Text = "������������";
            // 
            // plansTabPage
            // 
            this.plansTabPage.Controls.Add(this.Plans);
            this.plansTabPage.Controls.Add(this.PlansBar);
            this.plansTabPage.Name = "plansTabPage";
            this.plansTabPage.Size = new System.Drawing.Size(878, 381);
            this.plansTabPage.Text = "������ ���������";
            // 
            // Plans
            // 
            this.Plans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Plans.Location = new System.Drawing.Point(0, 27);
            this.Plans.MainView = this.gridView3;
            this.Plans.MenuManager = this.ribbon;
            this.Plans.Name = "Plans";
            this.Plans.Size = new System.Drawing.Size(878, 354);
            this.Plans.TabIndex = 7;
            this.Plans.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.Plans;
            this.gridView3.Name = "gridView3";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barCheckItem1.Caption = "������������";
            this.barCheckItem1.Id = 20;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // barCheckItem2
            // 
            this.barCheckItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barCheckItem2.Caption = "����";
            this.barCheckItem2.Id = 18;
            this.barCheckItem2.Name = "barCheckItem2";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem5.Caption = "OK";
            this.barButtonItem5.Id = 0;
            this.barButtonItem5.ImageIndex = 0;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem6.Caption = "��������";
            this.barButtonItem6.Id = 1;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem7.Caption = "³����";
            this.barButtonItem7.Id = 2;
            this.barButtonItem7.ImageIndex = 1;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem8.Caption = "OK";
            this.barButtonItem8.Id = 0;
            this.barButtonItem8.ImageIndex = 0;
            this.barButtonItem8.Name = "barButtonItem8";
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem9.Caption = "��������";
            this.barButtonItem9.Id = 1;
            this.barButtonItem9.Name = "barButtonItem9";
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem10.Caption = "³����";
            this.barButtonItem10.Id = 2;
            this.barButtonItem10.ImageIndex = 1;
            this.barButtonItem10.Name = "barButtonItem10";
            // 
            // createAcceptanceButtonItem
            // 
            this.createAcceptanceButtonItem.Caption = "�������� \"��������� ������\"";
            this.createAcceptanceButtonItem.Id = 18;
            this.createAcceptanceButtonItem.Name = "createAcceptanceButtonItem";
            // 
            // AcceptanceOfGoodsItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 603);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "AcceptanceOfGoodsItemForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Item form";
            
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Itemform_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.date.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.State.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomenclatureView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.waresTabPage.ResumeLayout(false);
            this.plansTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Plans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.BarButtonItem OK;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem cancel;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl Info;
        private DevExpress.XtraGrid.GridControl NomenclatureInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView nomenclatureView;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar NomenclatureInfoButtonsBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit State;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl label;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl Car;
        private DevExpress.XtraEditors.LabelControl Carrier;
        private DevExpress.XtraEditors.LabelControl Driver;
        private DevExpress.XtraEditors.LabelControl Contractor;
        private DevExpress.XtraBars.BarCheckItem showTareBarButtonItem;
        private DevExpress.XtraBars.BarCheckItem showNomenclatureBarButtonItem;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage waresTabPage;
        private DevExpress.XtraTab.XtraTabPage plansTabPage;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar PlansBar;
        private DevExpress.XtraGrid.GridControl Plans;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;
        private DevExpress.XtraBars.BarButtonItem barButtonItem10;
        private DevExpress.XtraBars.BarButtonItem createAcceptanceButtonItem;
        private DevExpress.XtraEditors.DateEdit date;
        private DevExpress.XtraBars.BarButtonItem openPalletButton;
    }
}