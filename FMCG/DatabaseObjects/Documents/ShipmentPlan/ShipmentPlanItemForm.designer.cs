using Aramis;

namespace AtosFMCG.DatabaseObjects.Documents
{
    partial class ShipmentPlanItemForm
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
            this.NomenclatureInfoButtonsBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.State = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.Car = new Aramis.AramisSearchLookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.Driver = new Aramis.AramisSearchLookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.Carrier = new Aramis.AramisSearchLookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.Contractor = new Aramis.AramisSearchLookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.TypeOfShipment = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Date = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.IncomeNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.Info = new DevExpress.XtraEditors.LabelControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.NomenclatureInfo = new DevExpress.XtraGrid.GridControl();
            this.nomenclatureView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.showNomenclature = new DevExpress.XtraEditors.CheckButton();
            this.showTare = new DevExpress.XtraEditors.CheckButton();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.createMovement = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.State.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Car.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Driver.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Contractor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfShipment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IncomeNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomenclatureView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.OK);
            this.ribbonStatusBar.ItemLinks.Add(this.barButtonItem2);
            this.ribbonStatusBar.ItemLinks.Add(this.cancel);
            this.ribbonStatusBar.ItemLinks.Add(this.createMovement);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 552);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(720, 23);
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
            this.cancel,
            this.createMovement});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 19;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(720, 54);
            this.ribbon.StatusBar = this.NomenclatureInfoButtonsBar;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // NomenclatureInfoButtonsBar
            // 
            this.NomenclatureInfoButtonsBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NomenclatureInfoButtonsBar.Location = new System.Drawing.Point(0, -4);
            this.NomenclatureInfoButtonsBar.Name = "NomenclatureInfoButtonsBar";
            this.NomenclatureInfoButtonsBar.Ribbon = this.ribbon;
            this.NomenclatureInfoButtonsBar.Size = new System.Drawing.Size(716, 27);
            // 
            // panelControl
            // 
            this.panelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl.Controls.Add(this.State);
            this.panelControl.Controls.Add(this.labelControl8);
            this.panelControl.Controls.Add(this.Car);
            this.panelControl.Controls.Add(this.labelControl7);
            this.panelControl.Controls.Add(this.Driver);
            this.panelControl.Controls.Add(this.labelControl6);
            this.panelControl.Controls.Add(this.Carrier);
            this.panelControl.Controls.Add(this.labelControl5);
            this.panelControl.Controls.Add(this.Contractor);
            this.panelControl.Controls.Add(this.labelControl4);
            this.panelControl.Controls.Add(this.TypeOfShipment);
            this.panelControl.Controls.Add(this.labelControl3);
            this.panelControl.Controls.Add(this.Date);
            this.panelControl.Controls.Add(this.labelControl2);
            this.panelControl.Controls.Add(this.IncomeNumber);
            this.panelControl.Controls.Add(this.labelControl1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 54);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(720, 110);
            this.panelControl.TabIndex = 0;
            // 
            // State
            // 
            this.State.Location = new System.Drawing.Point(455, 31);
            this.State.MenuManager = this.ribbon;
            this.State.Name = "State";
            this.State.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.State.Size = new System.Drawing.Size(209, 20);
            this.State.TabIndex = 3;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(366, 34);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(83, 13);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "Стан документу";
            // 
            // Car
            // 
            this.Car.BaseFilter = null;
            this.Car.Location = new System.Drawing.Point(455, 83);
            this.Car.MenuManager = this.ribbon;
            this.Car.Name = "Car";
            this.Car.Properties.BaseFilter = null;
            this.Car.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Car.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Car.Properties.NullText = "";
            this.Car.Size = new System.Drawing.Size(209, 20);
            this.Car.TabIndex = 7;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(366, 86);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(40, 13);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "Машина";
            // 
            // Driver
            // 
            this.Driver.BaseFilter = null;
            this.Driver.Location = new System.Drawing.Point(114, 83);
            this.Driver.MenuManager = this.ribbon;
            this.Driver.Name = "Driver";
            this.Driver.Properties.BaseFilter = null;
            this.Driver.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Driver.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Driver.Properties.NullText = "";
            this.Driver.Size = new System.Drawing.Size(246, 20);
            this.Driver.TabIndex = 6;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(10, 86);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(27, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "Водій";
            // 
            // Carrier
            // 
            this.Carrier.BaseFilter = null;
            this.Carrier.Location = new System.Drawing.Point(455, 57);
            this.Carrier.MenuManager = this.ribbon;
            this.Carrier.Name = "Carrier";
            this.Carrier.Properties.BaseFilter = null;
            this.Carrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Carrier.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Carrier.Properties.NullText = "";
            this.Carrier.Size = new System.Drawing.Size(209, 20);
            this.Carrier.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(366, 60);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Перевізник";
            // 
            // Contractor
            // 
            this.Contractor.BaseFilter = null;
            this.Contractor.Location = new System.Drawing.Point(114, 57);
            this.Contractor.MenuManager = this.ribbon;
            this.Contractor.Name = "Contractor";
            this.Contractor.Properties.BaseFilter = null;
            this.Contractor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Contractor.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Contractor.Properties.NullText = "";
            this.Contractor.Size = new System.Drawing.Size(246, 20);
            this.Contractor.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(10, 60);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Контрагент";
            // 
            // TypeOfShipment
            // 
            this.TypeOfShipment.Location = new System.Drawing.Point(114, 31);
            this.TypeOfShipment.MenuManager = this.ribbon;
            this.TypeOfShipment.Name = "TypeOfShipment";
            this.TypeOfShipment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TypeOfShipment.Size = new System.Drawing.Size(246, 20);
            this.TypeOfShipment.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(98, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Тип відвантаження";
            // 
            // Date
            // 
            this.Date.EditValue = null;
            this.Date.Location = new System.Drawing.Point(260, 6);
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
            this.labelControl2.Location = new System.Drawing.Point(225, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Дата ";
            // 
            // IncomeNumber
            // 
            this.IncomeNumber.Location = new System.Drawing.Point(114, 5);
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
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.Info);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 532);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(720, 20);
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
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.NomenclatureInfo);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 164);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(720, 368);
            this.groupControl1.TabIndex = 22;
            this.groupControl1.Text = "Номенклатура";
            // 
            // NomenclatureInfo
            // 
            this.NomenclatureInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NomenclatureInfo.Location = new System.Drawing.Point(2, 44);
            this.NomenclatureInfo.MainView = this.nomenclatureView;
            this.NomenclatureInfo.MenuManager = this.ribbon;
            this.NomenclatureInfo.Name = "NomenclatureInfo";
            this.NomenclatureInfo.Size = new System.Drawing.Size(716, 322);
            this.NomenclatureInfo.TabIndex = 1;
            this.NomenclatureInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.nomenclatureView});
            // 
            // nomenclatureView
            // 
            this.nomenclatureView.GridControl = this.NomenclatureInfo;
            this.nomenclatureView.Name = "nomenclatureView";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.showNomenclature);
            this.panelControl2.Controls.Add(this.showTare);
            this.panelControl2.Controls.Add(this.NomenclatureInfoButtonsBar);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 21);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(716, 23);
            this.panelControl2.TabIndex = 6;
            // 
            // showNomenclature
            // 
            this.showNomenclature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showNomenclature.Location = new System.Drawing.Point(540, 0);
            this.showNomenclature.Name = "showNomenclature";
            this.showNomenclature.Size = new System.Drawing.Size(88, 23);
            this.showNomenclature.TabIndex = 4;
            this.showNomenclature.Text = "Номенклатура";
            // 
            // showTare
            // 
            this.showTare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showTare.Location = new System.Drawing.Point(636, 0);
            this.showTare.Name = "showTare";
            this.showTare.Size = new System.Drawing.Size(75, 23);
            this.showTare.TabIndex = 3;
            this.showTare.Text = "Тара";
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
            this.barButtonItem6.Caption = "Записати";
            this.barButtonItem6.Id = 1;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem7.Caption = "Відміна";
            this.barButtonItem7.Id = 2;
            this.barButtonItem7.ImageIndex = 1;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // createMovement
            // 
            this.createMovement.Caption = "Сформувати переміщення";
            this.createMovement.Id = 18;
            this.createMovement.ImageIndex = 2;
            this.createMovement.Name = "createMovement";
            this.createMovement.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.createMovement_ItemClick);
            // 
            // ShipmentPlanItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 575);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "ShipmentPlanItemForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Item form";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Itemform_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.State.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Car.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Driver.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Carrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Contractor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfShipment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IncomeNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomenclatureView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
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
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private Aramis.AramisSearchLookUpEdit Contractor;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit TypeOfShipment;
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
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar NomenclatureInfoButtonsBar;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl NomenclatureInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView nomenclatureView;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckButton showNomenclature;
        private DevExpress.XtraEditors.CheckButton showTare;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraEditors.ComboBoxEdit State;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraBars.BarButtonItem createMovement;
    }
}