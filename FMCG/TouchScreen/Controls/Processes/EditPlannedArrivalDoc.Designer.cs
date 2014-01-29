namespace AtosFMCG.TouchScreen.Controls
    {
    partial class EditAcceptancePlanDoc
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
            this.editButton = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.car = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.driver = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.invoiceDate = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.invoiceNumber = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.editControlsArea = new System.Windows.Forms.Panel();
            this.saveButton = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.mainView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.LineNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.Description = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.Quantity = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.dateColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.shelfLifeDaysGridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.standartPalletsCountColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.nonStandartPalletsCountColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.standartPalletCountPer1Column = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.nonStandartPalletCountPer1Column = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.unitsOnNotFullPalletColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.unitsOnNotFullNonStandartPalletColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
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
            this.scrollDown = new System.Windows.Forms.Button();
            this.scrollUp = new System.Windows.Forms.Button();
            this.palletsCountButton = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.currentNomenclatureLabel = new DevExpress.XtraEditors.LabelControl();
            this.finishButton = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainView)).BeginInit();
            this.editPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.Color.Bisque;
            this.editButton.Background = System.Drawing.Color.Bisque;
            this.editButton.Font = new System.Drawing.Font("Tahoma", 8F);
            this.editButton.Ico = global::FMCG.Properties.Resources.edit;
            this.editButton.Image = global::FMCG.Properties.Resources.edit;
            this.editButton.IsEnabled = true;
            this.editButton.Location = new System.Drawing.Point(219, 710);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(60, 50);
            this.editButton.TabIndex = 60;
            this.editButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editButton.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Small;
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.SingleClick += new System.EventHandler(this.editMode_SingleClick);
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
            this.car.Location = new System.Drawing.Point(219, 654);
            this.car.Name = "car";
            this.car.Size = new System.Drawing.Size(210, 50);
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
            this.driver.Location = new System.Drawing.Point(3, 654);
            this.driver.Name = "driver";
            this.driver.Size = new System.Drawing.Size(210, 50);
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
            this.invoiceDate.Location = new System.Drawing.Point(138, 598);
            this.invoiceDate.Name = "invoiceDate";
            this.invoiceDate.Size = new System.Drawing.Size(158, 50);
            this.invoiceDate.TabIndex = 37;
            this.invoiceDate.Text = "          Дата";
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
            this.invoiceNumber.Ico = null;
            this.invoiceNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.invoiceNumber.IsEnabled = true;
            this.invoiceNumber.Location = new System.Drawing.Point(3, 598);
            this.invoiceNumber.Name = "invoiceNumber";
            this.invoiceNumber.Size = new System.Drawing.Size(132, 50);
            this.invoiceNumber.TabIndex = 36;
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
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.LightGreen;
            this.saveButton.Background = System.Drawing.Color.LightGreen;
            this.saveButton.Font = new System.Drawing.Font("Tahoma", 8F);
            this.saveButton.Ico = global::FMCG.Properties.Resources.save;
            this.saveButton.Image = global::FMCG.Properties.Resources.save;
            this.saveButton.IsEnabled = true;
            this.saveButton.Location = new System.Drawing.Point(294, 710);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(60, 50);
            this.saveButton.TabIndex = 42;
            this.saveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveButton.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Small;
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.SingleClick += new System.EventHandler(this.saveButton_SingleClick);
            // 
            // grid
            // 
            this.grid.Location = new System.Drawing.Point(3, 33);
            this.grid.MainView = this.mainView;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(425, 559);
            this.grid.TabIndex = 43;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mainView});
            // 
            // mainView
            // 
            this.mainView.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.mainView.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainView.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.mainView.Appearance.FocusedCell.Options.UseBackColor = true;
            this.mainView.Appearance.FocusedCell.Options.UseFont = true;
            this.mainView.Appearance.FocusedCell.Options.UseForeColor = true;
            this.mainView.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.mainView.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.mainView.Appearance.FocusedRow.Options.UseBackColor = true;
            this.mainView.Appearance.FocusedRow.Options.UseForeColor = true;
            this.mainView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.mainView.Appearance.HeaderPanel.Options.UseFont = true;
            this.mainView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.mainView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.mainView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.mainView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.mainView.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.mainView.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.mainView.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.mainView.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.mainView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.mainView.Appearance.SelectedRow.Options.UseForeColor = true;
            this.mainView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3,
            this.gridBand4,
            this.gridBand5});
            this.mainView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.LineNumber,
            this.Description,
            this.Quantity,
            this.dateColumn,
            this.shelfLifeDaysGridColumn,
            this.standartPalletsCountColumn,
            this.nonStandartPalletsCountColumn,
            this.standartPalletCountPer1Column,
            this.nonStandartPalletCountPer1Column,
            this.unitsOnNotFullPalletColumn,
            this.unitsOnNotFullNonStandartPalletColumn});
            this.mainView.GridControl = this.grid;
            this.mainView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.mainView.Name = "mainView";
            this.mainView.OptionsBehavior.Editable = false;
            this.mainView.OptionsBehavior.ReadOnly = true;
            this.mainView.OptionsView.ShowBands = false;
            this.mainView.OptionsView.ShowGroupPanel = false;
            this.mainView.OptionsView.ShowIndicator = false;
            this.mainView.RowHeight = 25;
            this.mainView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.mainView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.mainView_RowClick);
            this.mainView.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.mainView_FocusedColumnChanged);
            this.mainView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.mainView_CustomColumnDisplayText);
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Columns.Add(this.LineNumber);
            this.gridBand1.Columns.Add(this.Description);
            this.gridBand1.Columns.Add(this.Quantity);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.Width = 328;
            // 
            // LineNumber
            // 
            this.LineNumber.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LineNumber.AppearanceCell.Options.UseFont = true;
            this.LineNumber.AppearanceCell.Options.UseTextOptions = true;
            this.LineNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LineNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LineNumber.AppearanceHeader.Options.UseFont = true;
            this.LineNumber.Caption = " ";
            this.LineNumber.FieldName = "LineNumber";
            this.LineNumber.Name = "LineNumber";
            this.LineNumber.OptionsColumn.AllowFocus = false;
            this.LineNumber.OptionsColumn.ReadOnly = true;
            this.LineNumber.RowCount = 2;
            this.LineNumber.Visible = true;
            this.LineNumber.Width = 32;
            // 
            // Description
            // 
            this.Description.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.Description.AppearanceCell.Options.UseFont = true;
            this.Description.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Description.AppearanceHeader.Options.UseFont = true;
            this.Description.Caption = "Найменування";
            this.Description.FieldName = "Description";
            this.Description.Name = "Description";
            this.Description.OptionsColumn.ReadOnly = true;
            this.Description.RowCount = 2;
            this.Description.Visible = true;
            this.Description.Width = 228;
            // 
            // Quantity
            // 
            this.Quantity.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Quantity.AppearanceCell.Options.UseFont = true;
            this.Quantity.AppearanceCell.Options.UseTextOptions = true;
            this.Quantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Quantity.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Quantity.AppearanceHeader.Options.UseFont = true;
            this.Quantity.Caption = "Кільк-ть";
            this.Quantity.DisplayFormat.FormatString = "{0:n0}";
            this.Quantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Quantity.FieldName = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.OptionsColumn.ReadOnly = true;
            this.Quantity.RowCount = 2;
            this.Quantity.Visible = true;
            this.Quantity.Width = 68;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "gridBand2";
            this.gridBand2.Columns.Add(this.dateColumn);
            this.gridBand2.Columns.Add(this.shelfLifeDaysGridColumn);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 92;
            // 
            // dateColumn
            // 
            this.dateColumn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateColumn.AppearanceCell.Options.UseFont = true;
            this.dateColumn.AppearanceCell.Options.UseTextOptions = true;
            this.dateColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateColumn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateColumn.AppearanceHeader.Options.UseFont = true;
            this.dateColumn.Caption = "Дата";
            this.dateColumn.DisplayFormat.FormatString = "d";
            this.dateColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateColumn.FieldName = "Date";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.OptionsColumn.ReadOnly = true;
            this.dateColumn.Visible = true;
            this.dateColumn.Width = 92;
            // 
            // shelfLifeDaysGridColumn
            // 
            this.shelfLifeDaysGridColumn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shelfLifeDaysGridColumn.AppearanceCell.Options.UseFont = true;
            this.shelfLifeDaysGridColumn.AppearanceCell.Options.UseTextOptions = true;
            this.shelfLifeDaysGridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.shelfLifeDaysGridColumn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shelfLifeDaysGridColumn.AppearanceHeader.Options.UseFont = true;
            this.shelfLifeDaysGridColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.shelfLifeDaysGridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.shelfLifeDaysGridColumn.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.shelfLifeDaysGridColumn.Caption = "Срок хран. дни";
            this.shelfLifeDaysGridColumn.FieldName = "ShelfLifeDays";
            this.shelfLifeDaysGridColumn.Name = "shelfLifeDaysGridColumn";
            this.shelfLifeDaysGridColumn.OptionsColumn.ReadOnly = true;
            this.shelfLifeDaysGridColumn.RowIndex = 1;
            this.shelfLifeDaysGridColumn.Visible = true;
            this.shelfLifeDaysGridColumn.Width = 92;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "gridBand3";
            this.gridBand3.Columns.Add(this.standartPalletsCountColumn);
            this.gridBand3.Columns.Add(this.nonStandartPalletsCountColumn);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.Width = 115;
            // 
            // standartPalletsCountColumn
            // 
            this.standartPalletsCountColumn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.standartPalletsCountColumn.AppearanceCell.Options.UseFont = true;
            this.standartPalletsCountColumn.AppearanceCell.Options.UseTextOptions = true;
            this.standartPalletsCountColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.standartPalletsCountColumn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.standartPalletsCountColumn.AppearanceHeader.Options.UseFont = true;
            this.standartPalletsCountColumn.Caption = "Кол-во палл.";
            this.standartPalletsCountColumn.DisplayFormat.FormatString = "{0:n0}";
            this.standartPalletsCountColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.standartPalletsCountColumn.FieldName = "StandartPalletsCount";
            this.standartPalletsCountColumn.Name = "standartPalletsCountColumn";
            this.standartPalletsCountColumn.OptionsColumn.ReadOnly = true;
            this.standartPalletsCountColumn.Visible = true;
            this.standartPalletsCountColumn.Width = 115;
            // 
            // nonStandartPalletsCountColumn
            // 
            this.nonStandartPalletsCountColumn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nonStandartPalletsCountColumn.AppearanceCell.Options.UseFont = true;
            this.nonStandartPalletsCountColumn.AppearanceCell.Options.UseTextOptions = true;
            this.nonStandartPalletsCountColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nonStandartPalletsCountColumn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nonStandartPalletsCountColumn.AppearanceHeader.Options.UseFont = true;
            this.nonStandartPalletsCountColumn.Caption = "Кол-во нест п.";
            this.nonStandartPalletsCountColumn.DisplayFormat.FormatString = "{0:n0}";
            this.nonStandartPalletsCountColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nonStandartPalletsCountColumn.FieldName = "NonStandartPalletsCount";
            this.nonStandartPalletsCountColumn.Name = "nonStandartPalletsCountColumn";
            this.nonStandartPalletsCountColumn.OptionsColumn.ReadOnly = true;
            this.nonStandartPalletsCountColumn.RowIndex = 1;
            this.nonStandartPalletsCountColumn.Visible = true;
            this.nonStandartPalletsCountColumn.Width = 115;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "gridBand4";
            this.gridBand4.Columns.Add(this.standartPalletCountPer1Column);
            this.gridBand4.Columns.Add(this.nonStandartPalletCountPer1Column);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.Width = 128;
            // 
            // standartPalletCountPer1Column
            // 
            this.standartPalletCountPer1Column.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.standartPalletCountPer1Column.AppearanceCell.Options.UseFont = true;
            this.standartPalletCountPer1Column.AppearanceCell.Options.UseTextOptions = true;
            this.standartPalletCountPer1Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.standartPalletCountPer1Column.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.standartPalletCountPer1Column.AppearanceHeader.Options.UseFont = true;
            this.standartPalletCountPer1Column.Caption = "Ед. в палл.";
            this.standartPalletCountPer1Column.DisplayFormat.FormatString = "{0:n0}";
            this.standartPalletCountPer1Column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.standartPalletCountPer1Column.FieldName = "UnitsAmountInOneStandartPallet";
            this.standartPalletCountPer1Column.Name = "standartPalletCountPer1Column";
            this.standartPalletCountPer1Column.OptionsColumn.ReadOnly = true;
            this.standartPalletCountPer1Column.Visible = true;
            this.standartPalletCountPer1Column.Width = 128;
            // 
            // nonStandartPalletCountPer1Column
            // 
            this.nonStandartPalletCountPer1Column.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nonStandartPalletCountPer1Column.AppearanceCell.Options.UseFont = true;
            this.nonStandartPalletCountPer1Column.AppearanceCell.Options.UseTextOptions = true;
            this.nonStandartPalletCountPer1Column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.nonStandartPalletCountPer1Column.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nonStandartPalletCountPer1Column.AppearanceHeader.Options.UseFont = true;
            this.nonStandartPalletCountPer1Column.Caption = "Ед. на нест п.";
            this.nonStandartPalletCountPer1Column.DisplayFormat.FormatString = "{0:n0}";
            this.nonStandartPalletCountPer1Column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.nonStandartPalletCountPer1Column.FieldName = "UnitsAmountInOneNonStandartPallet";
            this.nonStandartPalletCountPer1Column.Name = "nonStandartPalletCountPer1Column";
            this.nonStandartPalletCountPer1Column.OptionsColumn.ReadOnly = true;
            this.nonStandartPalletCountPer1Column.RowIndex = 1;
            this.nonStandartPalletCountPer1Column.Visible = true;
            this.nonStandartPalletCountPer1Column.Width = 128;
            // 
            // gridBand5
            // 
            this.gridBand5.Caption = "gridBand5";
            this.gridBand5.Columns.Add(this.unitsOnNotFullPalletColumn);
            this.gridBand5.Columns.Add(this.unitsOnNotFullNonStandartPalletColumn);
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.Width = 163;
            // 
            // unitsOnNotFullPalletColumn
            // 
            this.unitsOnNotFullPalletColumn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unitsOnNotFullPalletColumn.AppearanceCell.Options.UseFont = true;
            this.unitsOnNotFullPalletColumn.AppearanceCell.Options.UseTextOptions = true;
            this.unitsOnNotFullPalletColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.unitsOnNotFullPalletColumn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unitsOnNotFullPalletColumn.AppearanceHeader.Options.UseFont = true;
            this.unitsOnNotFullPalletColumn.Caption = "Остаток на пал.";
            this.unitsOnNotFullPalletColumn.DisplayFormat.FormatString = "{0:n0}";
            this.unitsOnNotFullPalletColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.unitsOnNotFullPalletColumn.FieldName = "UnitsOnNotFullPallet";
            this.unitsOnNotFullPalletColumn.Name = "unitsOnNotFullPalletColumn";
            this.unitsOnNotFullPalletColumn.OptionsColumn.ReadOnly = true;
            this.unitsOnNotFullPalletColumn.Visible = true;
            this.unitsOnNotFullPalletColumn.Width = 163;
            // 
            // unitsOnNotFullNonStandartPalletColumn
            // 
            this.unitsOnNotFullNonStandartPalletColumn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unitsOnNotFullNonStandartPalletColumn.AppearanceCell.Options.UseFont = true;
            this.unitsOnNotFullNonStandartPalletColumn.AppearanceCell.Options.UseTextOptions = true;
            this.unitsOnNotFullNonStandartPalletColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.unitsOnNotFullNonStandartPalletColumn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unitsOnNotFullNonStandartPalletColumn.AppearanceHeader.Options.UseFont = true;
            this.unitsOnNotFullNonStandartPalletColumn.Caption = "Остаток на нест п.";
            this.unitsOnNotFullNonStandartPalletColumn.DisplayFormat.FormatString = "{0:n0}";
            this.unitsOnNotFullNonStandartPalletColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.unitsOnNotFullNonStandartPalletColumn.FieldName = "UnitsOnNotFullNonStandartPallet";
            this.unitsOnNotFullNonStandartPalletColumn.Name = "unitsOnNotFullNonStandartPalletColumn";
            this.unitsOnNotFullNonStandartPalletColumn.OptionsColumn.ReadOnly = true;
            this.unitsOnNotFullNonStandartPalletColumn.RowIndex = 1;
            this.unitsOnNotFullNonStandartPalletColumn.Visible = true;
            this.unitsOnNotFullNonStandartPalletColumn.Width = 163;
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
            this.waresButton.Location = new System.Drawing.Point(3, 710);
            this.waresButton.Name = "waresButton";
            this.waresButton.Size = new System.Drawing.Size(105, 50);
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
            this.tareButton.Location = new System.Drawing.Point(108, 710);
            this.tareButton.Name = "tareButton";
            this.tareButton.Size = new System.Drawing.Size(105, 50);
            this.tareButton.TabIndex = 51;
            this.tareButton.Text = "Тара";
            this.tareButton.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.tareButton.UseVisualStyleBackColor = false;
            this.tareButton.Click += new System.EventHandler(this.tareButton_Click);
            // 
            // scrollDown
            // 
            this.scrollDown.Image = global::FMCG.Properties.Resources.down;
            this.scrollDown.Location = new System.Drawing.Point(0, 71);
            this.scrollDown.Name = "scrollDown";
            this.scrollDown.Size = new System.Drawing.Size(65, 65);
            this.scrollDown.TabIndex = 10;
            this.scrollDown.UseVisualStyleBackColor = true;
            // 
            // scrollUp
            // 
            this.scrollUp.Image = global::FMCG.Properties.Resources.up;
            this.scrollUp.Location = new System.Drawing.Point(0, 0);
            this.scrollUp.Name = "scrollUp";
            this.scrollUp.Size = new System.Drawing.Size(65, 65);
            this.scrollUp.TabIndex = 9;
            this.scrollUp.UseVisualStyleBackColor = true;
            // 
            // palletsCountButton
            // 
            this.palletsCountButton.BackColor = System.Drawing.Color.PowderBlue;
            this.palletsCountButton.Background = System.Drawing.Color.PowderBlue;
            this.palletsCountButton.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.palletsCountButton.Ico = null;
            this.palletsCountButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.palletsCountButton.IsEnabled = true;
            this.palletsCountButton.Location = new System.Drawing.Point(298, 598);
            this.palletsCountButton.Name = "palletsCountButton";
            this.palletsCountButton.Size = new System.Drawing.Size(131, 50);
            this.palletsCountButton.TabIndex = 52;
            this.palletsCountButton.Text = "<--     -->";
            this.palletsCountButton.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Normal;
            this.palletsCountButton.UseVisualStyleBackColor = false;
            this.palletsCountButton.Click += new System.EventHandler(this.palletsCountButton_Click);
            // 
            // currentNomenclatureLabel
            // 
            this.currentNomenclatureLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currentNomenclatureLabel.Location = new System.Drawing.Point(3, 10);
            this.currentNomenclatureLabel.Name = "currentNomenclatureLabel";
            this.currentNomenclatureLabel.Size = new System.Drawing.Size(88, 16);
            this.currentNomenclatureLabel.TabIndex = 53;
            this.currentNomenclatureLabel.Text = "Живчик 2,5 л";
            // 
            // finishButton
            // 
            this.finishButton.BackColor = System.Drawing.Color.LightGreen;
            this.finishButton.Background = System.Drawing.Color.LightGreen;
            this.finishButton.Font = new System.Drawing.Font("Tahoma", 8F);
            this.finishButton.Ico = global::FMCG.Properties.Resources.finish;
            this.finishButton.Image = global::FMCG.Properties.Resources.finish;
            this.finishButton.IsEnabled = true;
            this.finishButton.Location = new System.Drawing.Point(369, 710);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(60, 50);
            this.finishButton.TabIndex = 54;
            this.finishButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.finishButton.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Small;
            this.finishButton.UseVisualStyleBackColor = false;
            this.finishButton.SingleClick += new System.EventHandler(this.finishButton_SingleClick);
            // 
            // EditAcceptancePlanDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.finishButton);
            this.Controls.Add(this.currentNomenclatureLabel);
            this.Controls.Add(this.palletsCountButton);
            this.Controls.Add(this.tareButton);
            this.Controls.Add(this.waresButton);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.invoiceNumber);
            this.Controls.Add(this.invoiceDate);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.driver);
            this.Controls.Add(this.editControlsArea);
            this.Controls.Add(this.car);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.editPanel);
            this.Name = "EditAcceptancePlanDoc";
            this.Size = new System.Drawing.Size(1014, 763);
            this.Load += new System.EventHandler(this.EditAcceptancePlanDoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainView)).EndInit();
            this.editPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion

        private NavigatedButton invoiceNumber;
        private NavigatedButton invoiceDate;
        private NavigatedButton driver;
        private NavigatedButton car;
        private NavigatedButton editButton;
        private System.Windows.Forms.Panel editControlsArea;
        private NavigatedButton saveButton;
        private DevExpress.XtraGrid.GridControl grid;
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
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView mainView;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn LineNumber;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn Description;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn Quantity;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn dateColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn shelfLifeDaysGridColumn;
        private System.Windows.Forms.Button scrollDown;
        private System.Windows.Forms.Button scrollUp;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn standartPalletsCountColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn nonStandartPalletsCountColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn standartPalletCountPer1Column;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn nonStandartPalletCountPer1Column;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn unitsOnNotFullPalletColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn unitsOnNotFullNonStandartPalletColumn;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private NavigatedButton palletsCountButton;
        private DevExpress.XtraEditors.LabelControl currentNomenclatureLabel;
        private NavigatedButton finishButton;
        }
    }
