namespace AtosFMCG.HelperClasses.ViewOfServiceTables
    {
    partial class ViewBaseForm
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
            this.dataGrid = new DevExpress.XtraGrid.GridControl();
            this.view = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.refresh = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.rowsCount = new DevExpress.XtraEditors.CalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsCount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 44);
            this.dataGrid.MainView = this.view;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(743, 392);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.view});
            this.dataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid_KeyDown);
            // 
            // view
            // 
            this.view.GridControl = this.dataGrid;
            this.view.Name = "view";
            this.view.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Controls.Add(this.refresh);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.rowsCount);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(743, 44);
            this.panelControl1.TabIndex = 1;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit1.EditValue = global::AtosFMCG.Properties.Resources._1317825614_information_balloon;
            this.pictureEdit1.Location = new System.Drawing.Point(2, 2);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Size = new System.Drawing.Size(20, 40);
            this.pictureEdit1.TabIndex = 4;
            this.pictureEdit1.ToolTip = "Якщо кількість строк вказана не вірно, то буде використано значення по замовченню" +
    " - 250";
            this.pictureEdit1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.pictureEdit1.ToolTipTitle = "Довідка:";
            // 
            // refresh
            // 
            this.refresh.Image = global::AtosFMCG.Properties.Resources.order;
            this.refresh.Location = new System.Drawing.Point(225, 10);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(95, 23);
            this.refresh.TabIndex = 2;
            this.refresh.Text = "Оновити";
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            this.refresh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.refresh_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Кількість строк: ";
            // 
            // rowsCount
            // 
            this.rowsCount.Location = new System.Drawing.Point(119, 12);
            this.rowsCount.Name = "rowsCount";
            this.rowsCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rowsCount.Properties.Mask.EditMask = "d";
            this.rowsCount.Size = new System.Drawing.Size(100, 20);
            this.rowsCount.TabIndex = 0;
            this.rowsCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rowsCount_KeyDown);
            // 
            // BaseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 436);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.panelControl1);
            this.Name = "BaseView";
            this.Text = "Відображення таблиці \"Переміщення товару\"";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsCount.Properties)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion

        private DevExpress.XtraGrid.GridControl dataGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView view;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton refresh;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CalcEdit rowsCount;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        }
    }