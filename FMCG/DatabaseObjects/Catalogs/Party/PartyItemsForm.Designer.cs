namespace AtosFMCG.DatabaseObjects.Catalogs
{
    partial class PartyItemsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartyItemsForm));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.Ok = new DevExpress.XtraBars.BarButtonItem();
            this.Write = new DevExpress.XtraBars.BarButtonItem();
            this.Cancel = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.RefreshMapsInfoButton = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.FillProductivity = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.TheDeadlineSuitability = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.DateOfManufacture = new DevExpress.XtraEditors.DateEdit();
            this.ShelfLife50P = new DevExpress.XtraEditors.DateEdit();
            this.Description = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Nomenclature = new Aramis.AramisSearchLookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TheDeadlineSuitability.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TheDeadlineSuitability.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateOfManufacture.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateOfManufacture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShelfLife50P.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShelfLife50P.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nomenclature.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.Ok,
            this.Write,
            this.Cancel});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 10;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(398, 30);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // Ok
            // 
            this.Ok.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.Ok.Caption = "Ок";
            this.Ok.Id = 7;
            this.Ok.ImageIndex = 0;
            this.Ok.Name = "Ok";
            this.Ok.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OK_ItemClick);
            // 
            // Write
            // 
            this.Write.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.Write.Caption = "Записати";
            this.Write.Id = 8;
            this.Write.Name = "Write";
            this.Write.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Write_ItemClick);
            // 
            // Cancel
            // 
            this.Cancel.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.Cancel.Caption = "Відміна";
            this.Cancel.Id = 9;
            this.Cancel.ImageIndex = 1;
            this.Cancel.Name = "Cancel";
            this.Cancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Cancel_ItemClick);
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.Ok);
            this.ribbonStatusBar.ItemLinks.Add(this.Write);
            this.ribbonStatusBar.ItemLinks.Add(this.Cancel);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 181);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(398, 23);
            // 
            // RefreshMapsInfoButton
            // 
            this.RefreshMapsInfoButton.Caption = "constructorButton";
            this.RefreshMapsInfoButton.Glyph = ((System.Drawing.Image)(resources.GetObject("RefreshMapsInfoButton.Glyph")));
            this.RefreshMapsInfoButton.Id = 1;
            this.RefreshMapsInfoButton.Name = "RefreshMapsInfoButton";
            toolTipTitleItem1.Text = "Обновить информацию по картам";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Инициировать обновление информации по картам";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.RefreshMapsInfoButton.SuperTip = superToolTip1;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Плановый доход";
            this.barButtonItem1.Id = 5;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // FillProductivity
            // 
            this.FillProductivity.Caption = "Заполнить урожайность";
            this.FillProductivity.Id = 6;
            this.FillProductivity.Name = "FillProductivity";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 3;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem3.Caption = "Ок";
            this.barButtonItem3.Id = 7;
            this.barButtonItem3.ImageIndex = 0;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem4.Caption = "Записать";
            this.barButtonItem4.Id = 8;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem5.Caption = "Отменить";
            this.barButtonItem5.Id = 9;
            this.barButtonItem5.ImageIndex = 1;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem6.Caption = "Ок";
            this.barButtonItem6.Id = 7;
            this.barButtonItem6.ImageIndex = 0;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem7.Caption = "Записать";
            this.barButtonItem7.Id = 8;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem8.Caption = "Отменить";
            this.barButtonItem8.Id = 9;
            this.barButtonItem8.ImageIndex = 1;
            this.barButtonItem8.Name = "barButtonItem8";
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Caption = "barButtonItem2";
            this.barButtonItem9.Id = 3;
            this.barButtonItem9.Name = "barButtonItem9";
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem10.Caption = "OK";
            this.barButtonItem10.Id = 4;
            this.barButtonItem10.ImageIndex = 0;
            this.barButtonItem10.Name = "barButtonItem10";
            // 
            // barButtonItem11
            // 
            this.barButtonItem11.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem11.Caption = "Отмена";
            this.barButtonItem11.Id = 6;
            this.barButtonItem11.ImageIndex = 1;
            this.barButtonItem11.Name = "barButtonItem11";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.TheDeadlineSuitability);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.DateOfManufacture);
            this.panelControl1.Controls.Add(this.ShelfLife50P);
            this.panelControl1.Controls.Add(this.Description);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.Nomenclature);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 30);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(398, 151);
            this.panelControl1.TabIndex = 35;
            // 
            // TheDeadlineSuitability
            // 
            this.TheDeadlineSuitability.EditValue = null;
            this.TheDeadlineSuitability.Location = new System.Drawing.Point(162, 112);
            this.TheDeadlineSuitability.MenuManager = this.ribbon;
            this.TheDeadlineSuitability.Name = "TheDeadlineSuitability";
            this.TheDeadlineSuitability.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TheDeadlineSuitability.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.TheDeadlineSuitability.Size = new System.Drawing.Size(224, 20);
            this.TheDeadlineSuitability.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 63);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(100, 13);
            this.labelControl3.TabIndex = 40;
            this.labelControl3.Text = "Дата виготовлення";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 89);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(117, 13);
            this.labelControl4.TabIndex = 41;
            this.labelControl4.Text = "Термін приданості 50%";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(11, 115);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(145, 13);
            this.labelControl5.TabIndex = 43;
            this.labelControl5.Text = "Кінцевий термін придатності";
            // 
            // DateOfManufacture
            // 
            this.DateOfManufacture.EditValue = null;
            this.DateOfManufacture.Location = new System.Drawing.Point(162, 60);
            this.DateOfManufacture.MenuManager = this.ribbon;
            this.DateOfManufacture.Name = "DateOfManufacture";
            this.DateOfManufacture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateOfManufacture.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateOfManufacture.Size = new System.Drawing.Size(224, 20);
            this.DateOfManufacture.TabIndex = 1;
            // 
            // ShelfLife50P
            // 
            this.ShelfLife50P.EditValue = null;
            this.ShelfLife50P.Location = new System.Drawing.Point(162, 86);
            this.ShelfLife50P.MenuManager = this.ribbon;
            this.ShelfLife50P.Name = "ShelfLife50P";
            this.ShelfLife50P.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ShelfLife50P.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ShelfLife50P.Size = new System.Drawing.Size(224, 20);
            this.ShelfLife50P.TabIndex = 2;
            // 
            // Description
            // 
            this.Description.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Description.Location = new System.Drawing.Point(118, 11);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(21, 13);
            this.Description.TabIndex = 45;
            this.Description.Text = "{0}";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 37);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(73, 13);
            this.labelControl2.TabIndex = 38;
            this.labelControl2.Text = "Номенклатура";
            // 
            // Nomenclature
            // 
            this.Nomenclature.BaseFilter = null;
            this.Nomenclature.Location = new System.Drawing.Point(118, 34);
            this.Nomenclature.Name = "Nomenclature";
            this.Nomenclature.Properties.BaseFilter = null;
            this.Nomenclature.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Nomenclature.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Nomenclature.Properties.NullText = "";
            this.Nomenclature.Size = new System.Drawing.Size(268, 20);
            this.Nomenclature.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(73, 13);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "Найменування";
            // 
            // PartyItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 204);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PartyItemsForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "ItemsForm";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TheDeadlineSuitability.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TheDeadlineSuitability.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateOfManufacture.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateOfManufacture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShelfLife50P.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShelfLife50P.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nomenclature.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem RefreshMapsInfoButton;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem FillProductivity;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem Ok;
        private DevExpress.XtraBars.BarButtonItem Write;
        private DevExpress.XtraBars.BarButtonItem Cancel;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;
        private DevExpress.XtraBars.BarButtonItem barButtonItem10;
        private DevExpress.XtraBars.BarButtonItem barButtonItem11;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Aramis.AramisSearchLookUpEdit Nomenclature;
        private DevExpress.XtraEditors.DateEdit TheDeadlineSuitability;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DateEdit DateOfManufacture;
        private DevExpress.XtraEditors.DateEdit ShelfLife50P;
        private DevExpress.XtraEditors.LabelControl Description;
    }
}