namespace AtosFMCG.TouchScreen.Controls
    {
    partial class SelectFromObjectList
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
            this.topicLabel = new System.Windows.Forms.Label();
            this.inputField = new System.Windows.Forms.TextBox();
            this.gridArea = new System.Windows.Forms.Panel();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.selectedRowLabel = new System.Windows.Forms.Label();
            this.scrollUp = new System.Windows.Forms.Button();
            this.scrollDown = new System.Windows.Forms.Button();
            this.goNext = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.goPrev = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.gridArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // topicLabel
            // 
            this.topicLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topicLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.topicLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(125)))), ((int)(((byte)(195)))));
            this.topicLabel.Location = new System.Drawing.Point(29, 7);
            this.topicLabel.Name = "topicLabel";
            this.topicLabel.Size = new System.Drawing.Size(960, 39);
            this.topicLabel.TabIndex = 0;
            this.topicLabel.Text = "Оберіть ...";
            this.topicLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inputField
            // 
            this.inputField.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.inputField.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputField.Location = new System.Drawing.Point(284, 61);
            this.inputField.Name = "inputField";
            this.inputField.Size = new System.Drawing.Size(450, 45);
            this.inputField.TabIndex = 1;
            this.inputField.TextChanged += new System.EventHandler(this.inputField_TextChanged);
            // 
            // gridArea
            // 
            this.gridArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridArea.Controls.Add(this.grid);
            this.gridArea.Controls.Add(this.label2);
            this.gridArea.Controls.Add(this.label1);
            this.gridArea.Location = new System.Drawing.Point(77, 149);
            this.gridArea.Name = "gridArea";
            this.gridArea.Size = new System.Drawing.Size(858, 506);
            this.gridArea.TabIndex = 2;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.MainView = this.gridView;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(858, 506);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.gridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridView.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridView.Appearance.FocusedCell.Options.UseFont = true;
            this.gridView.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.gridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Description,
            this.Id});
            this.gridView.GridControl = this.grid;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.OptionsView.ShowIndicator = false;
            this.gridView.RowHeight = 65;
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            // 
            // Description
            // 
            this.Description.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 18F);
            this.Description.AppearanceCell.Options.UseFont = true;
            this.Description.FieldName = "Description";
            this.Description.Name = "Description";
            this.Description.OptionsColumn.AllowEdit = false;
            this.Description.OptionsColumn.ReadOnly = true;
            this.Description.Visible = true;
            this.Description.VisibleIndex = 0;
            // 
            // Id
            // 
            this.Id.Caption = "gridColumn1";
            this.Id.FieldName = "Id";
            this.Id.Name = "Id";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(0, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(858, 67);
            this.label2.TabIndex = 2;
            this.label2.Text = "Перехід далі не можливий доки не буде обрано рядок з таблиці!";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(0, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(858, 67);
            this.label1.TabIndex = 1;
            this.label1.Text = "Обрані дані не коректні";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectedRowLabel
            // 
            this.selectedRowLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedRowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectedRowLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.selectedRowLabel.Location = new System.Drawing.Point(29, 108);
            this.selectedRowLabel.Name = "selectedRowLabel";
            this.selectedRowLabel.Size = new System.Drawing.Size(960, 39);
            this.selectedRowLabel.TabIndex = 3;
            this.selectedRowLabel.Text = "Обраний рядок: ...";
            this.selectedRowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scrollUp
            // 
            this.scrollUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollUp.Image = global::AtosFMCG.Properties.Resources.up;
            this.scrollUp.Location = new System.Drawing.Point(945, 149);
            this.scrollUp.Name = "scrollUp";
            this.scrollUp.Size = new System.Drawing.Size(65, 65);
            this.scrollUp.TabIndex = 4;
            this.scrollUp.UseVisualStyleBackColor = true;
            this.scrollUp.Click += new System.EventHandler(this.scrollUp_Click);
            // 
            // scrollDown
            // 
            this.scrollDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollDown.Image = global::AtosFMCG.Properties.Resources.down;
            this.scrollDown.Location = new System.Drawing.Point(945, 590);
            this.scrollDown.Name = "scrollDown";
            this.scrollDown.Size = new System.Drawing.Size(65, 65);
            this.scrollDown.TabIndex = 5;
            this.scrollDown.UseVisualStyleBackColor = true;
            this.scrollDown.Click += new System.EventHandler(this.scrollDown_Click);
            // 
            // goNext
            // 
            this.goNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.goNext.BackColor = System.Drawing.Color.LightGreen;
            this.goNext.Background = System.Drawing.Color.LightGreen;
            this.goNext.Font = new System.Drawing.Font("Tahoma", 25F, System.Drawing.FontStyle.Bold);
            this.goNext.Ico = global::AtosFMCG.Properties.Resources.next64;
            this.goNext.Image = global::AtosFMCG.Properties.Resources.next64;
            this.goNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.goNext.IsEnabled = true;
            this.goNext.Location = new System.Drawing.Point(520, 671);
            this.goNext.Name = "goNext";
            this.goNext.Size = new System.Drawing.Size(264, 80);
            this.goNext.TabIndex = 35;
            this.goNext.Text = "Далі     ";
            this.goNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.goNext.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Big;
            this.goNext.UseVisualStyleBackColor = false;
            this.goNext.Click += new System.EventHandler(this.goNext_Click);
            // 
            // goPrev
            // 
            this.goPrev.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.goPrev.BackColor = System.Drawing.Color.AntiqueWhite;
            this.goPrev.Background = System.Drawing.Color.AntiqueWhite;
            this.goPrev.Font = new System.Drawing.Font("Tahoma", 25F, System.Drawing.FontStyle.Bold);
            this.goPrev.Ico = global::AtosFMCG.Properties.Resources.back641;
            this.goPrev.Image = global::AtosFMCG.Properties.Resources.back641;
            this.goPrev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.goPrev.IsEnabled = true;
            this.goPrev.Location = new System.Drawing.Point(240, 671);
            this.goPrev.Name = "goPrev";
            this.goPrev.Size = new System.Drawing.Size(264, 80);
            this.goPrev.TabIndex = 37;
            this.goPrev.Text = "Назад    ";
            this.goPrev.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.goPrev.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Big;
            this.goPrev.UseVisualStyleBackColor = false;
            this.goPrev.Click += new System.EventHandler(this.goPrev_Click);
            // 
            // SelectFromObjectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.goPrev);
            this.Controls.Add(this.goNext);
            this.Controls.Add(this.scrollDown);
            this.Controls.Add(this.scrollUp);
            this.Controls.Add(this.selectedRowLabel);
            this.Controls.Add(this.gridArea);
            this.Controls.Add(this.inputField);
            this.Controls.Add(this.topicLabel);
            this.Name = "SelectFromObjectList";
            this.Size = new System.Drawing.Size(1024, 768);
            this.gridArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion

        private System.Windows.Forms.Label topicLabel;
        private System.Windows.Forms.TextBox inputField;
        private System.Windows.Forms.Panel gridArea;
        private System.Windows.Forms.Label selectedRowLabel;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn Description;
        private DevExpress.XtraGrid.Columns.GridColumn Id;
        private System.Windows.Forms.Button scrollUp;
        private System.Windows.Forms.Button scrollDown;
        private NavigatedButton goNext;
        private NavigatedButton goPrev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        }
    }
