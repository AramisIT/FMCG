namespace FMCG.DatabaseObjects.SystemObjects
    {
    partial class CellsProcessingForm
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
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.tabPageControl = new DevExpress.XtraTab.XtraTabControl();
            this.printTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.TypeOfCell = new Aramis.AramisSearchLookUpEdit();
            this.FinishRack = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.StartRow = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.FinishRow = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.StartRack = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.createTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.Prefix = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.ParentOfCell = new Aramis.AramisSearchLookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.TypeOfCellControl = new Aramis.AramisSearchLookUpEdit();
            this.FinishRackControl = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.StartRowControl = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.FinishRowControl = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.StartRackControl = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageControl)).BeginInit();
            this.tabPageControl.SuspendLayout();
            this.printTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfCell.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishRack.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartRow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishRow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartRack.Properties)).BeginInit();
            this.createTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Prefix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentOfCell.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfCellControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishRackControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartRowControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishRowControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartRackControl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 1;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(442, 27);
            // 
            // tabPageControl
            // 
            this.tabPageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageControl.Location = new System.Drawing.Point(0, 27);
            this.tabPageControl.Name = "tabPageControl";
            this.tabPageControl.SelectedTabPage = this.printTabPage;
            this.tabPageControl.Size = new System.Drawing.Size(442, 418);
            this.tabPageControl.TabIndex = 2;
            this.tabPageControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.printTabPage,
            this.createTabPage});
            // 
            // printTabPage
            // 
            this.printTabPage.Controls.Add(this.groupControl1);
            this.printTabPage.Controls.Add(this.simpleButton1);
            this.printTabPage.Name = "printTabPage";
            this.printTabPage.Size = new System.Drawing.Size(436, 390);
            this.printTabPage.Text = "Роздрукувати етикетки";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.TypeOfCell);
            this.groupControl1.Controls.Add(this.FinishRack);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.StartRow);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.FinishRow);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.StartRack);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(40, 44);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(360, 196);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Фильтр ячеек";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(37, 151);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(57, 13);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Тип ячейки";
            this.labelControl5.Visible = false;
            // 
            // TypeOfCell
            // 
            this.TypeOfCell.BaseFilter = null;
            this.TypeOfCell.Location = new System.Drawing.Point(117, 148);
            this.TypeOfCell.Name = "TypeOfCell";
            this.TypeOfCell.Properties.BaseFilter = null;
            this.TypeOfCell.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.TypeOfCell.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.TypeOfCell.Properties.NullText = "";
            this.TypeOfCell.Size = new System.Drawing.Size(198, 20);
            this.TypeOfCell.TabIndex = 9;
            this.TypeOfCell.Visible = false;
            // 
            // FinishRack
            // 
            this.FinishRack.Location = new System.Drawing.Point(236, 99);
            this.FinishRack.MenuManager = this.ribbon;
            this.FinishRack.Name = "FinishRack";
            this.FinishRack.Size = new System.Drawing.Size(50, 20);
            this.FinishRack.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(210, 103);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(12, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "по";
            // 
            // StartRow
            // 
            this.StartRow.Location = new System.Drawing.Point(143, 47);
            this.StartRow.MenuManager = this.ribbon;
            this.StartRow.Name = "StartRow";
            this.StartRow.Size = new System.Drawing.Size(50, 20);
            this.StartRow.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(63, 103);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(66, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Ряд паллет с";
            // 
            // FinishRow
            // 
            this.FinishRow.Location = new System.Drawing.Point(236, 47);
            this.FinishRow.MenuManager = this.ribbon;
            this.FinishRow.Name = "FinishRow";
            this.FinishRow.Size = new System.Drawing.Size(50, 20);
            this.FinishRow.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(210, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(12, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "по";
            // 
            // StartRack
            // 
            this.StartRack.Location = new System.Drawing.Point(143, 99);
            this.StartRack.MenuManager = this.ribbon;
            this.StartRack.Name = "StartRack";
            this.StartRack.Size = new System.Drawing.Size(50, 20);
            this.StartRack.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(63, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Проход с";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(95, 322);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(249, 38);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Розрукувати";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // createTabPage
            // 
            this.createTabPage.Controls.Add(this.groupControl2);
            this.createTabPage.Controls.Add(this.simpleButton2);
            this.createTabPage.Name = "createTabPage";
            this.createTabPage.Size = new System.Drawing.Size(436, 390);
            this.createTabPage.Text = "Створити етикетки";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupControl2.Controls.Add(this.Prefix);
            this.groupControl2.Controls.Add(this.labelControl12);
            this.groupControl2.Controls.Add(this.labelControl11);
            this.groupControl2.Controls.Add(this.ParentOfCell);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.TypeOfCellControl);
            this.groupControl2.Controls.Add(this.FinishRackControl);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.StartRowControl);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.FinishRowControl);
            this.groupControl2.Controls.Add(this.labelControl9);
            this.groupControl2.Controls.Add(this.StartRackControl);
            this.groupControl2.Controls.Add(this.labelControl10);
            this.groupControl2.Location = new System.Drawing.Point(38, 29);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(360, 267);
            this.groupControl2.TabIndex = 10;
            this.groupControl2.Text = "Фильтр ячеек";
            // 
            // Prefix
            // 
            this.Prefix.Location = new System.Drawing.Point(143, 209);
            this.Prefix.MenuManager = this.ribbon;
            this.Prefix.Name = "Prefix";
            this.Prefix.Size = new System.Drawing.Size(172, 20);
            this.Prefix.TabIndex = 15;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(37, 213);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(44, 13);
            this.labelControl12.TabIndex = 14;
            this.labelControl12.Text = "Префикс";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(37, 182);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(88, 13);
            this.labelControl11.TabIndex = 12;
            this.labelControl11.Text = "Родитель ячейки";
            // 
            // ParentOfCell
            // 
            this.ParentOfCell.BaseFilter = null;
            this.ParentOfCell.Location = new System.Drawing.Point(143, 179);
            this.ParentOfCell.Name = "ParentOfCell";
            this.ParentOfCell.Properties.BaseFilter = null;
            this.ParentOfCell.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.ParentOfCell.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.ParentOfCell.Properties.NullText = "";
            this.ParentOfCell.Size = new System.Drawing.Size(172, 20);
            this.ParentOfCell.TabIndex = 11;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(37, 151);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(57, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "Тип ячейки";
            // 
            // TypeOfCellControl
            // 
            this.TypeOfCellControl.BaseFilter = null;
            this.TypeOfCellControl.Location = new System.Drawing.Point(143, 148);
            this.TypeOfCellControl.Name = "TypeOfCellControl";
            this.TypeOfCellControl.Properties.BaseFilter = null;
            this.TypeOfCellControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.TypeOfCellControl.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.TypeOfCellControl.Properties.NullText = "";
            this.TypeOfCellControl.Size = new System.Drawing.Size(172, 20);
            this.TypeOfCellControl.TabIndex = 9;
            // 
            // FinishRackControl
            // 
            this.FinishRackControl.Location = new System.Drawing.Point(236, 99);
            this.FinishRackControl.MenuManager = this.ribbon;
            this.FinishRackControl.Name = "FinishRackControl";
            this.FinishRackControl.Size = new System.Drawing.Size(50, 20);
            this.FinishRackControl.TabIndex = 3;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(210, 103);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(12, 13);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "по";
            // 
            // StartRowControl
            // 
            this.StartRowControl.Location = new System.Drawing.Point(143, 47);
            this.StartRowControl.MenuManager = this.ribbon;
            this.StartRowControl.Name = "StartRowControl";
            this.StartRowControl.Size = new System.Drawing.Size(50, 20);
            this.StartRowControl.TabIndex = 0;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(63, 103);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(66, 13);
            this.labelControl8.TabIndex = 7;
            this.labelControl8.Text = "Ряд паллет с";
            // 
            // FinishRowControl
            // 
            this.FinishRowControl.Location = new System.Drawing.Point(236, 47);
            this.FinishRowControl.MenuManager = this.ribbon;
            this.FinishRowControl.Name = "FinishRowControl";
            this.FinishRowControl.Size = new System.Drawing.Size(50, 20);
            this.FinishRowControl.TabIndex = 1;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(210, 51);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(12, 13);
            this.labelControl9.TabIndex = 6;
            this.labelControl9.Text = "по";
            // 
            // StartRackControl
            // 
            this.StartRackControl.Location = new System.Drawing.Point(143, 99);
            this.StartRackControl.MenuManager = this.ribbon;
            this.StartRackControl.Name = "StartRackControl";
            this.StartRackControl.Size = new System.Drawing.Size(50, 20);
            this.StartRackControl.TabIndex = 2;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(63, 51);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(45, 13);
            this.labelControl10.TabIndex = 5;
            this.labelControl10.Text = "Проход с";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(95, 322);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(249, 38);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "Створити";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // CellsProcessingForm
            // 
            this.AllowDisplayRibbon = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 445);
            this.Controls.Add(this.tabPageControl);
            this.Controls.Add(this.ribbon);
            this.MaximizeBox = false;
            this.Name = "CellsProcessingForm";
            this.Ribbon = this.ribbon;
            this.Text = "Обробка комірок";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageControl)).EndInit();
            this.tabPageControl.ResumeLayout(false);
            this.printTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfCell.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishRack.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartRow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishRow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartRack.Properties)).EndInit();
            this.createTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Prefix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentOfCell.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeOfCellControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishRackControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartRowControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishRowControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartRackControl.Properties)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraTab.XtraTabControl tabPageControl;
        private DevExpress.XtraTab.XtraTabPage createTabPage;
        private DevExpress.XtraTab.XtraTabPage printTabPage;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.CalcEdit FinishRack;
        private DevExpress.XtraEditors.CalcEdit StartRack;
        private DevExpress.XtraEditors.CalcEdit FinishRow;
        private DevExpress.XtraEditors.CalcEdit StartRow;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Aramis.AramisSearchLookUpEdit TypeOfCell;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit Prefix;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private Aramis.AramisSearchLookUpEdit ParentOfCell;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private Aramis.AramisSearchLookUpEdit TypeOfCellControl;
        private DevExpress.XtraEditors.CalcEdit FinishRackControl;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CalcEdit StartRowControl;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.CalcEdit FinishRowControl;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.CalcEdit StartRackControl;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        }
    }