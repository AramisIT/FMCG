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
            this.save = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LineNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Quantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.shelfLifeDaysGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.editPanel = new System.Windows.Forms.Panel();
            this.selectedRowInfo = new System.Windows.Forms.Label();
            this.finishEditMode = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.navigatedButton2 = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.navigatedButton3 = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.exit = new System.Windows.Forms.Button();
            this.waresButton = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.tareButton = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.editPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // editMode
            // 
            this.editMode.BackColor = System.Drawing.Color.Bisque;
            this.editMode.Background = System.Drawing.Color.Bisque;
            this.editMode.Font = new System.Drawing.Font("Tahoma", 8F);
            this.editMode.Ico = global::FMCG.Properties.Resources.edit;
            this.editMode.Image = global::FMCG.Properties.Resources.edit;
            this.editMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editMode.IsEnabled = true;
            this.editMode.Location = new System.Drawing.Point(219, 700);
            this.editMode.Name = "editMode";
            this.editMode.Size = new System.Drawing.Size(105, 60);
            this.editMode.TabIndex = 40;
            this.editMode.Text = "Редагувати";
            this.editMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editMode.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Small;
            this.editMode.UseVisualStyleBackColor = false;
            this.editMode.SingleClick += new System.EventHandler(this.editMode_SingleClick);
            // 
            // car
            // 
            this.car.BackColor = System.Drawing.Color.PowderBlue;
            this.car.Background = System.Drawing.Color.PowderBlue;
            this.car.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.car.Ico = global::FMCG.Properties.Resources.car;
            this.car.Image = global::FMCG.Properties.Resources.car;
            this.car.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.car.IsEnabled = true;
            this.car.Location = new System.Drawing.Point(219, 634);
            this.car.Name = "car";
            this.car.Size = new System.Drawing.Size(210, 60);
            this.car.TabIndex = 39;
            this.car.Text = "          Машинашшшшшшш";
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
            this.driver.Ico = global::FMCG.Properties.Resources.user;
            this.driver.Image = global::FMCG.Properties.Resources.user;
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
            this.invoiceDate.Ico = global::FMCG.Properties.Resources.date;
            this.invoiceDate.Image = global::FMCG.Properties.Resources.date;
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
            this.invoiceNumber.Ico = global::FMCG.Properties.Resources.number;
            this.invoiceNumber.Image = global::FMCG.Properties.Resources.number;
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
            // save
            // 
            this.save.BackColor = System.Drawing.Color.LightGreen;
            this.save.Background = System.Drawing.Color.LightGreen;
            this.save.Font = new System.Drawing.Font("Tahoma", 8F);
            this.save.Ico = global::FMCG.Properties.Resources.save;
            this.save.Image = global::FMCG.Properties.Resources.save;
            this.save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.save.IsEnabled = true;
            this.save.Location = new System.Drawing.Point(324, 700);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(105, 60);
            this.save.TabIndex = 42;
            this.save.Text = "Завершити";
            this.save.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.save.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Small;
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.finish_Click);
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
            this.gridView.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.gridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gridView.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridView.Appearance.FocusedCell.Options.UseFont = true;
            this.gridView.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gridView.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.White;
            this.gridView.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.White;
            this.gridView.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.White;
            this.gridView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridView.Appearance.SelectedRow.BackColor = System.Drawing.Color.White;
            this.gridView.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.White;
            this.gridView.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.LineNumber,
            this.Description,
            this.Quantity,
            this.Date,
            this.shelfLifeDaysGridColumn});
            this.gridView.GridControl = this.grid;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsBehavior.ReadOnly = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.OptionsView.ShowIndicator = false;
            this.gridView.RowHeight = 65;
            this.gridView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView_RowClick);
            this.gridView.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gridView_FocusedColumnChanged);
            this.gridView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView_CustomColumnDisplayText);
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
            this.LineNumber.OptionsColumn.AllowFocus = false;
            this.LineNumber.Visible = true;
            this.LineNumber.VisibleIndex = 0;
            this.LineNumber.Width = 37;
            // 
            // Description
            // 
            this.Description.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.Quantity.Caption = "К-ть";
            this.Quantity.FieldName = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Visible = true;
            this.Quantity.VisibleIndex = 2;
            this.Quantity.Width = 67;
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
            this.Date.Width = 80;
            // 
            // shelfLifeDaysGridColumn
            // 
            this.shelfLifeDaysGridColumn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shelfLifeDaysGridColumn.AppearanceCell.Options.UseFont = true;
            this.shelfLifeDaysGridColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.shelfLifeDaysGridColumn.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.shelfLifeDaysGridColumn.Caption = "Срок хранения";
            this.shelfLifeDaysGridColumn.FieldName = "ShelfLifeDays";
            this.shelfLifeDaysGridColumn.Name = "shelfLifeDaysGridColumn";
            this.shelfLifeDaysGridColumn.Visible = true;
            this.shelfLifeDaysGridColumn.VisibleIndex = 4;
            this.shelfLifeDaysGridColumn.Width = 70;
            // 
            // editPanel
            // 
            this.editPanel.Controls.Add(this.selectedRowInfo);
            this.editPanel.Controls.Add(this.finishEditMode);
            this.editPanel.Controls.Add(this.label3);
            this.editPanel.Controls.Add(this.label4);
            this.editPanel.Controls.Add(this.navigatedButton2);
            this.editPanel.Controls.Add(this.navigatedButton3);
            this.editPanel.Location = new System.Drawing.Point(434, 3);
            this.editPanel.Name = "editPanel";
            this.editPanel.Size = new System.Drawing.Size(580, 755);
            this.editPanel.TabIndex = 48;
            // 
            // selectedRowInfo
            // 
            this.selectedRowInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectedRowInfo.ForeColor = System.Drawing.Color.DimGray;
            this.selectedRowInfo.Location = new System.Drawing.Point(3, 406);
            this.selectedRowInfo.Name = "selectedRowInfo";
            this.selectedRowInfo.Size = new System.Drawing.Size(574, 67);
            this.selectedRowInfo.TabIndex = 48;
            this.selectedRowInfo.Text = "...";
            this.selectedRowInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // finishEditMode
            // 
            this.finishEditMode.BackColor = System.Drawing.Color.LightGray;
            this.finishEditMode.Background = System.Drawing.Color.LightGray;
            this.finishEditMode.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.finishEditMode.Ico = global::FMCG.Properties.Resources.finish;
            this.finishEditMode.Image = global::FMCG.Properties.Resources.finish;
            this.finishEditMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.finishEditMode.IsEnabled = true;
            this.finishEditMode.Location = new System.Drawing.Point(74, 565);
            this.finishEditMode.Name = "finishEditMode";
            this.finishEditMode.Size = new System.Drawing.Size(425, 60);
            this.finishEditMode.TabIndex = 47;
            this.finishEditMode.Text = "                       Вийти з режиму редагування";
            this.finishEditMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.finishEditMode.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.finishEditMode.UseVisualStyleBackColor = false;
            this.finishEditMode.Click += new System.EventHandler(this.finishEditMode_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(3, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(574, 67);
            this.label3.TabIndex = 46;
            this.label3.Text = "Додайте необхідну кількість рядків або видаліть виділені рядки";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.DarkOrange;
            this.label4.Location = new System.Drawing.Point(3, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(574, 67);
            this.label4.TabIndex = 45;
            this.label4.Text = "Режим редагування таблиці";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // navigatedButton2
            // 
            this.navigatedButton2.BackColor = System.Drawing.Color.LightGreen;
            this.navigatedButton2.Background = System.Drawing.Color.LightGreen;
            this.navigatedButton2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.navigatedButton2.Ico = global::FMCG.Properties.Resources.addnew;
            this.navigatedButton2.Image = global::FMCG.Properties.Resources.addnew;
            this.navigatedButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navigatedButton2.IsEnabled = true;
            this.navigatedButton2.Location = new System.Drawing.Point(289, 499);
            this.navigatedButton2.Name = "navigatedButton2";
            this.navigatedButton2.Size = new System.Drawing.Size(210, 60);
            this.navigatedButton2.TabIndex = 44;
            this.navigatedButton2.Text = "             Додати рядок";
            this.navigatedButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navigatedButton2.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.navigatedButton2.UseVisualStyleBackColor = false;
            this.navigatedButton2.Click += new System.EventHandler(this.addRow_Click);
            // 
            // navigatedButton3
            // 
            this.navigatedButton3.BackColor = System.Drawing.Color.Bisque;
            this.navigatedButton3.Background = System.Drawing.Color.Bisque;
            this.navigatedButton3.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.navigatedButton3.Ico = global::FMCG.Properties.Resources.delete;
            this.navigatedButton3.Image = global::FMCG.Properties.Resources.delete;
            this.navigatedButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navigatedButton3.IsEnabled = true;
            this.navigatedButton3.Location = new System.Drawing.Point(74, 499);
            this.navigatedButton3.Name = "navigatedButton3";
            this.navigatedButton3.Size = new System.Drawing.Size(210, 60);
            this.navigatedButton3.TabIndex = 43;
            this.navigatedButton3.Text = "            Видалити рядок";
            this.navigatedButton3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.navigatedButton3.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.navigatedButton3.UseVisualStyleBackColor = false;
            this.navigatedButton3.Click += new System.EventHandler(this.deleteRow_Click);
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.exit.Font = new System.Drawing.Font("Tahoma", 8F);
            this.exit.Image = global::FMCG.Properties.Resources.Exit;
            this.exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exit.Location = new System.Drawing.Point(952, 3);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(62, 30);
            this.exit.TabIndex = 49;
            this.exit.Text = "Вихід";
            this.exit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // waresButton
            // 
            this.waresButton.BackColor = System.Drawing.Color.PowderBlue;
            this.waresButton.Background = System.Drawing.Color.PowderBlue;
            this.waresButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.waresButton.Ico = null;
            this.waresButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.waresButton.IsEnabled = true;
            this.waresButton.Location = new System.Drawing.Point(3, 700);
            this.waresButton.Name = "waresButton";
            this.waresButton.Size = new System.Drawing.Size(105, 60);
            this.waresButton.TabIndex = 50;
            this.waresButton.Text = "Продукція";
            this.waresButton.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.waresButton.UseVisualStyleBackColor = false;
            this.waresButton.Click += new System.EventHandler(this.waresButton_Click);
            // 
            // tareButton
            // 
            this.tareButton.BackColor = System.Drawing.Color.PowderBlue;
            this.tareButton.Background = System.Drawing.Color.PowderBlue;
            this.tareButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.tareButton.Ico = null;
            this.tareButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tareButton.IsEnabled = true;
            this.tareButton.Location = new System.Drawing.Point(108, 700);
            this.tareButton.Name = "tareButton";
            this.tareButton.Size = new System.Drawing.Size(105, 60);
            this.tareButton.TabIndex = 51;
            this.tareButton.Text = "Тара";
            this.tareButton.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.tareButton.UseVisualStyleBackColor = false;
            this.tareButton.Click += new System.EventHandler(this.tareButton_Click);
            // 
            // EditPlannedArrivalDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tareButton);
            this.Controls.Add(this.waresButton);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.invoiceNumber);
            this.Controls.Add(this.invoiceDate);
            this.Controls.Add(this.save);
            this.Controls.Add(this.driver);
            this.Controls.Add(this.editControlsArea);
            this.Controls.Add(this.car);
            this.Controls.Add(this.editMode);
            this.Controls.Add(this.editPanel);
            this.Name = "EditPlannedArrivalDoc";
            this.Size = new System.Drawing.Size(1014, 763);
            this.Load += new System.EventHandler(this.EditPlannedArrivalDoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.editPanel.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        #endregion

        private NavigatedButton invoiceNumber;
        private NavigatedButton invoiceDate;
        private NavigatedButton driver;
        private NavigatedButton car;
        private NavigatedButton editMode;
        private System.Windows.Forms.Panel editControlsArea;
        private NavigatedButton save;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn Description;
        private DevExpress.XtraGrid.Columns.GridColumn LineNumber;
        private DevExpress.XtraGrid.Columns.GridColumn Quantity;
        private DevExpress.XtraGrid.Columns.GridColumn Date;
        private System.Windows.Forms.Panel editPanel;
        private NavigatedButton finishEditMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private NavigatedButton navigatedButton2;
        private NavigatedButton navigatedButton3;
        private System.Windows.Forms.Label selectedRowInfo;
        private System.Windows.Forms.Button exit;
        private NavigatedButton waresButton;
        private NavigatedButton tareButton;
        private DevExpress.XtraGrid.Columns.GridColumn shelfLifeDaysGridColumn;
        }
    }
