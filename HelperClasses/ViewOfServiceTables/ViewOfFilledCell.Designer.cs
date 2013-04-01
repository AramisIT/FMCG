namespace AtosFMCG.HelperClasses.ViewOfServiceTables
    {
    partial class ViewOfFilledCell
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
            this.goodsMoving = new DevExpress.XtraGrid.GridControl();
            this.view = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.goodsMoving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // goodsMoving
            // 
            this.goodsMoving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goodsMoving.Location = new System.Drawing.Point(0, 0);
            this.goodsMoving.MainView = this.view;
            this.goodsMoving.Name = "goodsMoving";
            this.goodsMoving.Size = new System.Drawing.Size(743, 436);
            this.goodsMoving.TabIndex = 0;
            this.goodsMoving.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.view});
            // 
            // view
            // 
            this.view.GridControl = this.goodsMoving;
            this.view.Name = "view";
            this.view.OptionsView.ShowGroupPanel = false;
            // 
            // ViewOfGoodsMoving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 436);
            this.Controls.Add(this.goodsMoving);
            this.Name = "ViewOfGoodsMoving";
            this.Text = "Відображення таблиці \"Переміщення товару\"";
            ((System.ComponentModel.ISupportInitialize)(this.goodsMoving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion

        private DevExpress.XtraGrid.GridControl goodsMoving;
        private DevExpress.XtraGrid.Views.Grid.GridView view;
        }
    }