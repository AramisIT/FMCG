namespace AtosFMCG.TouchScreen.Controls
    {
    partial class EditPlannedArrivalDoc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
            {
            this.editMode = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.car = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.driver = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.invoiceDate = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.invoiceNumber = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.editControlsArea = new System.Windows.Forms.Panel();
            this.finish = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LineNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Quantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Date = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // editMode
            // 
            this.editMode.BackColor = System.Drawing.Color.Bisque;
            this.editMode.Background = System.Drawing.Color.Bisque;
            this.editMode.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.editMode.Ico = global::AtosFMCG.Properties.Resources.edit;
            this.editMode.Image = global::AtosFMCG.Properties.Resources.edit;
            this.editMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editMode.IsEnabled = true;
            this.editMode.Location = new System.Drawing.Point(3, 700);
            this.editMode.Name = "editMode";
            this.editMode.Size = new System.Drawing.Size(210, 60);
            this.editMode.TabIndex = 40;
            this.editMode.Text = "          Редагувати";
            this.editMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editMode.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.editMode.UseVisualStyleBackColor = false;
            this.editMode.Click += new System.EventHandler(this.editMode_Click);
            // 
            // car
            // 
            this.car.BackColor = System.Drawing.Color.PowderBlue;
            this.car.Background = System.Drawing.Color.PowderBlue;
            this.car.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.car.Ico = global::AtosFMCG.Properties.Resources.car;
            this.car.Image = global::AtosFMCG.Properties.Resources.car;
            this.car.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.car.IsEnabled = true;
            this.car.Location = new System.Drawing.Point(219, 634);
            this.car.Name = "car";
            this.car.Size = new System.Drawing.Size(210, 60);
            this.car.TabIndex = 39;
            this.car.Text = "          Машина";
            this.car.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.car.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.car.UseVisualStyleBackColor = false;
            this.car.Click += new System.EventHandler(this.car_Click);
            // 
            // driver
            // 
            this.driver.BackColor = System.Drawing.Color.PowderBlue;
            this.driver.Background = System.Drawing.Color.PowderBlue;
            this.driver.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.driver.Ico = global::AtosFMCG.Properties.Resources.user;
            this.driver.Image = global::AtosFMCG.Properties.Resources.user;
            this.driver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.driver.IsEnabled = true;
            this.driver.Location = new System.Drawing.Point(3, 634);
            this.driver.Name = "driver";
            this.driver.Size = new System.Drawing.Size(210, 60);
            this.driver.TabIndex = 38;
            this.driver.Text = "          Водій";
            this.driver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.driver.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.driver.UseVisualStyleBackColor = false;
            this.driver.Click += new System.EventHandler(this.driver_Click);
            // 
            // invoiceDate
            // 
            this.invoiceDate.BackColor = System.Drawing.Color.PowderBlue;
            this.invoiceDate.Background = System.Drawing.Color.PowderBlue;
            this.invoiceDate.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.invoiceDate.Ico = global::AtosFMCG.Properties.Resources.date;
            this.invoiceDate.Image = global::AtosFMCG.Properties.Resources.date;
            this.invoiceDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.invoiceDate.IsEnabled = true;
            this.invoiceDate.Location = new System.Drawing.Point(219, 568);
            this.invoiceDate.Name = "invoiceDate";
            this.invoiceDate.Size = new System.Drawing.Size(210, 60);
            this.invoiceDate.TabIndex = 37;
            this.invoiceDate.Text = "          Дата накладної";
            this.invoiceDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.invoiceDate.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.invoiceDate.UseVisualStyleBackColor = false;
            this.invoiceDate.Click += new System.EventHandler(this.invoiceDate_Click);
            // 
            // invoiceNumber
            // 
            this.invoiceNumber.BackColor = System.Drawing.Color.PowderBlue;
            this.invoiceNumber.Background = System.Drawing.Color.PowderBlue;
            this.invoiceNumber.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.invoiceNumber.Ico = global::AtosFMCG.Properties.Resources.number;
            this.invoiceNumber.Image = global::AtosFMCG.Properties.Resources.number;
            this.invoiceNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.invoiceNumber.IsEnabled = true;
            this.invoiceNumber.Location = new System.Drawing.Point(3, 568);
            this.invoiceNumber.Name = "invoiceNumber";
            this.invoiceNumber.Size = new System.Drawing.Size(210, 60);
            this.invoiceNumber.TabIndex = 36;
            this.invoiceNumber.Text = "          Номер накладної";
            this.invoiceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.invoiceNumber.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.invoiceNumber.UseVisualStyleBackColor = false;
            this.invoiceNumber.Click += new System.EventHandler(this.invoiceNumber_Click);
            // 
            // editControlsArea
            // 
            this.editControlsArea.Location = new System.Drawing.Point(434, 3);
            this.editControlsArea.Name = "editControlsArea";
            this.editControlsArea.Size = new System.Drawing.Size(580, 755);
            this.editControlsArea.TabIndex = 41;
            // 
            // finish
            // 
            this.finish.BackColor = System.Drawing.Color.LightGreen;
            this.finish.Background = System.Drawing.Color.LightGreen;
            this.finish.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.finish.Ico = global::AtosFMCG.Properties.Resources.finish;
            this.finish.Image = global::AtosFMCG.Properties.Resources.finish;
            this.finish.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.finish.IsEnabled = true;
            this.finish.Location = new System.Drawing.Point(218, 700);
            this.finish.Name = "finish";
            this.finish.Size = new System.Drawing.Size(210, 60);
            this.finish.TabIndex = 42;
            this.finish.Text = "          Завершити";
            this.finish.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.finish.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.finish.UseVisualStyleBackColor = false;
            this.finish.Click += new System.EventHandler(this.finish_Click);
            // 
            // grid
            // 
            this.grid.Location = new System.Drawing.Point(3, 3);
            this.grid.MainView = this.gridView;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(425, 559);
            this.grid.TabIndex = 43;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.LineNumber,
            this.Description,
            this.Quantity,
            this.Date});
            this.gridView.GridControl = this.grid;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsBehavior.ReadOnly = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowHeight = 65;
            // 
            // LineNumber
            // 
            this.LineNumber.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.LineNumber.AppearanceCell.Options.UseFont = true;
            this.LineNumber.AppearanceCell.Options.UseTextOptions = true;
            this.LineNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LineNumber.Caption = " ";
            this.LineNumber.FieldName = "LineNumber";
            this.LineNumber.Name = "LineNumber";
            this.LineNumber.Visible = true;
            this.LineNumber.VisibleIndex = 0;
            this.LineNumber.Width = 37;
            // 
            // Description
            // 
            this.Description.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.Description.AppearanceCell.Options.UseFont = true;
            this.Description.Caption = "Найменування";
            this.Description.FieldName = "Description";
            this.Description.Name = "Description";
            this.Description.OptionsColumn.AllowEdit = false;
            this.Description.OptionsColumn.ReadOnly = true;
            this.Description.Visible = true;
            this.Description.VisibleIndex = 1;
            this.Description.Width = 264;
            // 
            // Quantity
            // 
            this.Quantity.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.Quantity.AppearanceCell.Options.UseFont = true;
            this.Quantity.AppearanceCell.Options.UseTextOptions = true;
            this.Quantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Quantity.Caption = "К-сть";
            this.Quantity.FieldName = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Visible = true;
            this.Quantity.VisibleIndex = 2;
            this.Quantity.Width = 90;
            // 
            // Date
            // 
            this.Date.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F);
            this.Date.AppearanceCell.Options.UseFont = true;
            this.Date.AppearanceCell.Options.UseTextOptions = true;
            this.Date.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Date.Caption = "Дата";
            this.Date.FieldName = "Date";
            this.Date.Name = "Date";
            this.Date.Visible = true;
            this.Date.VisibleIndex = 3;
            this.Date.Width = 125;
            // 
            // EditPlannedArrivalDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid);
            this.Controls.Add(this.invoiceNumber);
            this.Controls.Add(this.invoiceDate);
            this.Controls.Add(this.finish);
            this.Controls.Add(this.driver);
            this.Controls.Add(this.editControlsArea);
            this.Controls.Add(this.car);
            this.Controls.Add(this.editMode);
            this.Name = "EditPlannedArrivalDoc";
            this.Size = new System.Drawing.Size(1014, 763);
            this.Load += new System.EventHandler(this.EditPlannedArrivalDoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion

        private NavigatedButton invoiceNumber;
        private NavigatedButton invoiceDate;
        private NavigatedButton driver;
        private NavigatedButton car;
        private NavigatedButton editMode;
        private System.Windows.Forms.Panel editControlsArea;
        private NavigatedButton finish;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn Description;
        private DevExpress.XtraGrid.Columns.GridColumn LineNumber;
        private DevExpress.XtraGrid.Columns.GridColumn Quantity;
        private DevExpress.XtraGrid.Columns.GridColumn Date;
        }
    }
