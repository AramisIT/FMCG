using Aramis;

namespace AtosFMCG.DatabaseObjects.Documents
{
    partial class InventoryItemForm
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
            this.createTask = new DevExpress.XtraBars.BarButtonItem();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.NomenclatureInfoButtonsBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.State = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.TypeOfInventory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Date = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.IncomeNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.NomenclatureInfo = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.Info = new DevExpress.XtraEditors.LabelControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.StartPeriod = new DevExpress.XtraEditors.DateEdit();
            this.FinishPeriod = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.periodPanel = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.State.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfInventory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IncomeNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StartPeriod.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishPeriod.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodPanel)).BeginInit();
            this.periodPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.OK);
            this.ribbonStatusBar.ItemLinks.Add(this.barButtonItem2);
            this.ribbonStatusBar.ItemLinks.Add(this.cancel);
            this.ribbonStatusBar.ItemLinks.Add(this.createTask);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 477);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(650, 31);
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
            // createTask
            // 
            this.createTask.Caption = "Сформувати завдання";
            this.createTask.Enabled = false;
            this.createTask.Glyph = global::FMCG.Properties.Resources.add;
            this.createTask.Id = 18;
            this.createTask.Name = "createTask";
            this.createTask.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.createTask_ItemClick);
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
            this.createTask});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 19;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(650, 49);
            this.ribbon.StatusBar = this.NomenclatureInfoButtonsBar;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // NomenclatureInfoButtonsBar
            // 
            this.NomenclatureInfoButtonsBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.NomenclatureInfoButtonsBar.Location = new System.Drawing.Point(2, 21);
            this.NomenclatureInfoButtonsBar.Name = "NomenclatureInfoButtonsBar";
            this.NomenclatureInfoButtonsBar.Ribbon = this.ribbon;
            this.NomenclatureInfoButtonsBar.Size = new System.Drawing.Size(646, 27);
            // 
            // panelControl
            // 
            this.panelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl.Controls.Add(this.periodPanel);
            this.panelControl.Controls.Add(this.State);
            this.panelControl.Controls.Add(this.labelControl4);
            this.panelControl.Controls.Add(this.TypeOfInventory);
            this.panelControl.Controls.Add(this.labelControl3);
            this.panelControl.Controls.Add(this.Date);
            this.panelControl.Controls.Add(this.labelControl2);
            this.panelControl.Controls.Add(this.IncomeNumber);
            this.panelControl.Controls.Add(this.labelControl1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 49);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(650, 57);
            this.panelControl.TabIndex = 0;
            // 
            // State
            // 
            this.State.Location = new System.Drawing.Point(85, 31);
            this.State.MenuManager = this.ribbon;
            this.State.Name = "State";
            this.State.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.State.Size = new System.Drawing.Size(132, 20);
            this.State.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 34);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(25, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Стан";
            // 
            // TypeOfInventory
            // 
            this.TypeOfInventory.Location = new System.Drawing.Point(258, 31);
            this.TypeOfInventory.MenuManager = this.ribbon;
            this.TypeOfInventory.Name = "TypeOfInventory";
            this.TypeOfInventory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TypeOfInventory.Size = new System.Drawing.Size(132, 20);
            this.TypeOfInventory.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(223, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(18, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Тип";
            // 
            // Date
            // 
            this.Date.EditValue = null;
            this.Date.Location = new System.Drawing.Point(258, 5);
            this.Date.MenuManager = this.ribbon;
            this.Date.Name = "Date";
            this.Date.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Date.Size = new System.Drawing.Size(132, 20);
            this.Date.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(223, 8);
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
            this.IncomeNumber.Size = new System.Drawing.Size(132, 20);
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
            // NomenclatureInfo
            // 
            this.NomenclatureInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NomenclatureInfo.Location = new System.Drawing.Point(2, 48);
            this.NomenclatureInfo.MainView = this.gridView1;
            this.NomenclatureInfo.MenuManager = this.ribbon;
            this.NomenclatureInfo.Name = "NomenclatureInfo";
            this.NomenclatureInfo.Size = new System.Drawing.Size(646, 301);
            this.NomenclatureInfo.TabIndex = 1;
            this.NomenclatureInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.NomenclatureInfo;
            this.gridView1.Name = "gridView1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.Info);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 457);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(650, 20);
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
            this.groupControl1.Controls.Add(this.NomenclatureInfoButtonsBar);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 106);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(650, 351);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Номенклатура";
            // 
            // StartPeriod
            // 
            this.StartPeriod.EditValue = null;
            this.StartPeriod.Location = new System.Drawing.Point(14, 0);
            this.StartPeriod.MenuManager = this.ribbon;
            this.StartPeriod.Name = "StartPeriod";
            this.StartPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StartPeriod.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.StartPeriod.Size = new System.Drawing.Size(100, 20);
            this.StartPeriod.TabIndex = 7;
            // 
            // FinishPeriod
            // 
            this.FinishPeriod.EditValue = null;
            this.FinishPeriod.Location = new System.Drawing.Point(138, 0);
            this.FinishPeriod.MenuManager = this.ribbon;
            this.FinishPeriod.Name = "FinishPeriod";
            this.FinishPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.FinishPeriod.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.FinishPeriod.Size = new System.Drawing.Size(100, 20);
            this.FinishPeriod.TabIndex = 8;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(3, 3);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(5, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "з";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(120, 3);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(12, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "по";
            // 
            // periodPanel
            // 
            this.periodPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.periodPanel.Controls.Add(this.labelControl6);
            this.periodPanel.Controls.Add(this.StartPeriod);
            this.periodPanel.Controls.Add(this.labelControl5);
            this.periodPanel.Controls.Add(this.FinishPeriod);
            this.periodPanel.Location = new System.Drawing.Point(396, 31);
            this.periodPanel.Name = "periodPanel";
            this.periodPanel.Size = new System.Drawing.Size(239, 20);
            this.periodPanel.TabIndex = 3;
            this.periodPanel.Visible = false;
            // 
            // InventoryItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 508);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "InventoryItemForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Item form";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Itemform_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.State.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfInventory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IncomeNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StartPeriod.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishPeriod.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodPanel)).EndInit();
            this.periodPanel.ResumeLayout(false);
            this.periodPanel.PerformLayout();
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
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar NomenclatureInfoButtonsBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraEditors.ComboBoxEdit TypeOfInventory;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit Date;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit IncomeNumber;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit State;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.BarButtonItem createTask;
        private DevExpress.XtraEditors.PanelControl periodPanel;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.DateEdit StartPeriod;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DateEdit FinishPeriod;
    }
}