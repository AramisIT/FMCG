namespace AtosFMCG.DatabaseObjects.Catalogs
{
    partial class TypesOfCellItemsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TypesOfCellItemsForm));
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.IsVirtual = new DevExpress.XtraEditors.CheckEdit();
            this.AllowableWeight = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Description = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.Width = new DevExpress.XtraEditors.CalcEdit();
            this.Height = new DevExpress.XtraEditors.CalcEdit();
            this.Depth = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsVirtual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllowableWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Description.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Width.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Height.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depth.Properties)).BeginInit();
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
            this.ribbon.Size = new System.Drawing.Size(265, 30);
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
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 234);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(265, 23);
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
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(8, 27);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Ширина";
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
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.IsVirtual);
            this.panelControl1.Controls.Add(this.AllowableWeight);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.Description);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 30);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(265, 204);
            this.panelControl1.TabIndex = 35;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl3.Location = new System.Drawing.Point(236, 37);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(11, 13);
            this.labelControl3.TabIndex = 39;
            this.labelControl3.Text = "гр";
            // 
            // IsVirtual
            // 
            this.IsVirtual.Location = new System.Drawing.Point(10, 169);
            this.IsVirtual.MenuManager = this.ribbon;
            this.IsVirtual.Name = "IsVirtual";
            this.IsVirtual.Properties.Caption = "Віртуальна комірка";
            this.IsVirtual.Size = new System.Drawing.Size(241, 19);
            this.IsVirtual.TabIndex = 2;
            // 
            // AllowableWeight
            // 
            this.AllowableWeight.Location = new System.Drawing.Point(99, 34);
            this.AllowableWeight.MenuManager = this.ribbon;
            this.AllowableWeight.Name = "AllowableWeight";
            this.AllowableWeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AllowableWeight.Size = new System.Drawing.Size(131, 20);
            this.AllowableWeight.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 37);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(81, 13);
            this.labelControl2.TabIndex = 39;
            this.labelControl2.Text = "Допустима вага";
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(99, 8);
            this.Description.MenuManager = this.ribbon;
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(153, 20);
            this.Description.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(73, 13);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "Найменування";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.Width);
            this.groupControl1.Controls.Add(this.Height);
            this.groupControl1.Controls.Add(this.Depth);
            this.groupControl1.Controls.Add(this.labelControl17);
            this.groupControl1.Controls.Add(this.labelControl16);
            this.groupControl1.Controls.Add(this.labelControl15);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Location = new System.Drawing.Point(12, 60);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(240, 103);
            this.groupControl1.TabIndex = 34;
            this.groupControl1.Text = "Фізичні розміри";
            // 
            // Width
            // 
            this.Width.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Width.Location = new System.Drawing.Point(56, 24);
            this.Width.MenuManager = this.ribbon;
            this.Width.Name = "Width";
            this.Width.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Width.Size = new System.Drawing.Size(162, 20);
            this.Width.TabIndex = 0;
            // 
            // Height
            // 
            this.Height.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Height.Location = new System.Drawing.Point(56, 50);
            this.Height.MenuManager = this.ribbon;
            this.Height.Name = "Height";
            this.Height.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Height.Size = new System.Drawing.Size(162, 20);
            this.Height.TabIndex = 1;
            // 
            // Depth
            // 
            this.Depth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Depth.Location = new System.Drawing.Point(56, 76);
            this.Depth.MenuManager = this.ribbon;
            this.Depth.Name = "Depth";
            this.Depth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Depth.Size = new System.Drawing.Size(162, 20);
            this.Depth.TabIndex = 2;
            // 
            // labelControl17
            // 
            this.labelControl17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl17.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl17.Location = new System.Drawing.Point(224, 79);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(11, 13);
            this.labelControl17.TabIndex = 35;
            this.labelControl17.Text = "см";
            // 
            // labelControl16
            // 
            this.labelControl16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl16.Location = new System.Drawing.Point(224, 53);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(11, 13);
            this.labelControl16.TabIndex = 34;
            this.labelControl16.Text = "см";
            // 
            // labelControl15
            // 
            this.labelControl15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl15.Location = new System.Drawing.Point(224, 27);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(11, 13);
            this.labelControl15.TabIndex = 33;
            this.labelControl15.Text = "см";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(8, 79);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(42, 13);
            this.labelControl14.TabIndex = 18;
            this.labelControl14.Text = "Глибина";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(8, 53);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(35, 13);
            this.labelControl12.TabIndex = 16;
            this.labelControl12.Text = "Висота";
            // 
            // TypesOfCellItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 257);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "TypesOfCellItemsForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "ItemsForm";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsVirtual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllowableWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Description.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Width.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Height.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Depth.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem RefreshMapsInfoButton;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem FillProductivity;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
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
        private DevExpress.XtraEditors.CalcEdit AllowableWeight;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit Description;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CalcEdit Width;
        private DevExpress.XtraEditors.CalcEdit Height;
        private DevExpress.XtraEditors.CalcEdit Depth;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit IsVirtual;
    }
}