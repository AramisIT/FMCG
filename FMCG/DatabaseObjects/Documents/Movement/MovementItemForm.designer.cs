using Aramis;

namespace AtosFMCG.DatabaseObjects.Documents
{
    partial class MovementItemForm
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
            this.IncomeDate = new DevExpress.XtraEditors.LabelControl();
            this.IncomeNumber = new DevExpress.XtraEditors.LabelControl();
            this.Source = new Aramis.AramisSearchLookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.State = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.label = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.NomenclatureInfo = new DevExpress.XtraGrid.GridControl();
            this.nomenclatureView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.Info = new DevExpress.XtraEditors.LabelControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.showNomenclature = new DevExpress.XtraEditors.CheckButton();
            this.showTare = new DevExpress.XtraEditors.CheckButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Source.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.State.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomenclatureView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
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
            this.ribbonStatusBar.Size = new System.Drawing.Size(742, 31);
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
            this.cancel});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 18;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(742, 49);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // NomenclatureInfoButtonsBar
            // 
            this.NomenclatureInfoButtonsBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NomenclatureInfoButtonsBar.Location = new System.Drawing.Point(0, -4);
            this.NomenclatureInfoButtonsBar.Name = "NomenclatureInfoButtonsBar";
            this.NomenclatureInfoButtonsBar.Ribbon = this.ribbon;
            this.NomenclatureInfoButtonsBar.Size = new System.Drawing.Size(738, 27);
            // 
            // panelControl
            // 
            this.panelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl.Controls.Add(this.IncomeDate);
            this.panelControl.Controls.Add(this.IncomeNumber);
            this.panelControl.Controls.Add(this.Source);
            this.panelControl.Controls.Add(this.labelControl8);
            this.panelControl.Controls.Add(this.State);
            this.panelControl.Controls.Add(this.labelControl3);
            this.panelControl.Controls.Add(this.label);
            this.panelControl.Controls.Add(this.labelControl1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 49);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(742, 53);
            this.panelControl.TabIndex = 2;
            // 
            // IncomeDate
            // 
            this.IncomeDate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.IncomeDate.Location = new System.Drawing.Point(366, 34);
            this.IncomeDate.Name = "IncomeDate";
            this.IncomeDate.Size = new System.Drawing.Size(21, 13);
            this.IncomeDate.TabIndex = 20;
            this.IncomeDate.Text = "{0}";
            // 
            // IncomeNumber
            // 
            this.IncomeNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.IncomeNumber.Location = new System.Drawing.Point(99, 34);
            this.IncomeNumber.Name = "IncomeNumber";
            this.IncomeNumber.Size = new System.Drawing.Size(21, 13);
            this.IncomeNumber.TabIndex = 19;
            this.IncomeNumber.Text = "{0}";
            // 
            // Source
            // 
            this.Source.BaseFilter = null;
            this.Source.Location = new System.Drawing.Point(366, 8);
            this.Source.MenuManager = this.ribbon;
            this.Source.Name = "Source";
            this.Source.Properties.BaseFilter = null;
            this.Source.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.Source.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.Source.Properties.NullText = "";
            this.Source.Size = new System.Drawing.Size(364, 20);
            this.Source.TabIndex = 1;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(261, 11);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(98, 13);
            this.labelControl8.TabIndex = 17;
            this.labelControl8.Text = "��������-�������";
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
            this.label.Location = new System.Drawing.Point(261, 34);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(83, 13);
            this.label.TabIndex = 2;
            this.label.Text = "���� ���������";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "� ��������";
            // 
            // NomenclatureInfo
            // 
            this.NomenclatureInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NomenclatureInfo.Location = new System.Drawing.Point(2, 44);
            this.NomenclatureInfo.MainView = this.nomenclatureView;
            this.NomenclatureInfo.MenuManager = this.ribbon;
            this.NomenclatureInfo.Name = "NomenclatureInfo";
            this.NomenclatureInfo.Size = new System.Drawing.Size(738, 404);
            this.NomenclatureInfo.TabIndex = 1;
            this.NomenclatureInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.nomenclatureView});
            // 
            // nomenclatureView
            // 
            this.nomenclatureView.GridControl = this.NomenclatureInfo;
            this.nomenclatureView.Name = "nomenclatureView";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.Info);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 552);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(742, 20);
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
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.NomenclatureInfo);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 102);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(742, 450);
            this.groupControl1.TabIndex = 21;
            this.groupControl1.Text = "������������";
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
            this.panelControl2.Size = new System.Drawing.Size(738, 23);
            this.panelControl2.TabIndex = 6;
            // 
            // showNomenclature
            // 
            this.showNomenclature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showNomenclature.Location = new System.Drawing.Point(562, 0);
            this.showNomenclature.Name = "showNomenclature";
            this.showNomenclature.Size = new System.Drawing.Size(88, 23);
            this.showNomenclature.TabIndex = 4;
            this.showNomenclature.Text = "������������";
            this.showNomenclature.CheckedChanged += new System.EventHandler(this.showNomenclature_CheckedChanged);
            // 
            // showTare
            // 
            this.showTare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showTare.Location = new System.Drawing.Point(658, 0);
            this.showTare.Name = "showTare";
            this.showTare.Size = new System.Drawing.Size(75, 23);
            this.showTare.TabIndex = 3;
            this.showTare.Text = "����";
            this.showTare.CheckedChanged += new System.EventHandler(this.showTare_CheckedChanged);
            // 
            // MovementItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 603);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.KeyPreview = true;
            this.Name = "MovementItemForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Item form";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Itemform_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Source.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.State.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomenclatureInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomenclatureView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
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
        private DevExpress.XtraGrid.GridControl NomenclatureInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView nomenclatureView;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar NomenclatureInfoButtonsBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraEditors.ComboBoxEdit State;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl label;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private AramisSearchLookUpEdit Source;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl IncomeNumber;
        private DevExpress.XtraEditors.LabelControl IncomeDate;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckButton showNomenclature;
        private DevExpress.XtraEditors.CheckButton showTare;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}