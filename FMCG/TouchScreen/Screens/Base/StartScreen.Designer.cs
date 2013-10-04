namespace AtosFMCG.TouchScreen.Screens.Base
{
    partial class StartScreen
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
            this.PlannedArrivalProcess = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.SuspendLayout();
            // 
            // PlannedArrivalProcess
            // 
            this.PlannedArrivalProcess.BackColor = System.Drawing.Color.PowderBlue;
            this.PlannedArrivalProcess.Background = System.Drawing.Color.PowderBlue;
            this.PlannedArrivalProcess.Font = new System.Drawing.Font("Tahoma", 25F, System.Drawing.FontStyle.Bold);
            this.PlannedArrivalProcess.Ico = global::FMCG.Properties.Resources.package_add;
            this.PlannedArrivalProcess.Image = global::FMCG.Properties.Resources.package_add;
            this.PlannedArrivalProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PlannedArrivalProcess.IsEnabled = true;
            this.PlannedArrivalProcess.Location = new System.Drawing.Point(262, 266);
            this.PlannedArrivalProcess.Name = "PlannedArrivalProcess";
            this.PlannedArrivalProcess.Size = new System.Drawing.Size(437, 180);
            this.PlannedArrivalProcess.TabIndex = 34;
            this.PlannedArrivalProcess.Text = "План приходу";
            this.PlannedArrivalProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PlannedArrivalProcess.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Big;
            this.PlannedArrivalProcess.UseVisualStyleBackColor = false;
            this.PlannedArrivalProcess.Click += new System.EventHandler(this.PlannedArrivalProcess_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PlannedArrivalProcess);
            this.Name = "StartScreen";
            this.Size = new System.Drawing.Size(1024, 768);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.NavigatedButton PlannedArrivalProcess;

    }
}
