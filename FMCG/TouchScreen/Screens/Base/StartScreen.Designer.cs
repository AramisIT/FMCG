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
            this.AcceptancePlanProcess = new AtosFMCG.TouchScreen.Controls.NavigatedButton();
            this.SuspendLayout();
            // 
            // AcceptancePlanProcess
            // 
            this.AcceptancePlanProcess.BackColor = System.Drawing.Color.PowderBlue;
            this.AcceptancePlanProcess.Background = System.Drawing.Color.PowderBlue;
            this.AcceptancePlanProcess.Font = new System.Drawing.Font("Tahoma", 25F, System.Drawing.FontStyle.Bold);
            this.AcceptancePlanProcess.Ico = global::FMCG.Properties.Resources.package_add;
            this.AcceptancePlanProcess.Image = global::FMCG.Properties.Resources.package_add;
            this.AcceptancePlanProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AcceptancePlanProcess.IsEnabled = true;
            this.AcceptancePlanProcess.Location = new System.Drawing.Point(262, 266);
            this.AcceptancePlanProcess.Name = "AcceptancePlanProcess";
            this.AcceptancePlanProcess.Size = new System.Drawing.Size(437, 180);
            this.AcceptancePlanProcess.TabIndex = 34;
            this.AcceptancePlanProcess.Text = "План приходу";
            this.AcceptancePlanProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AcceptancePlanProcess.TypeOfFont = AtosFMCG.TouchScreen.Enums.TypesOfFont.Big;
            this.AcceptancePlanProcess.UseVisualStyleBackColor = false;
            this.AcceptancePlanProcess.Click += new System.EventHandler(this.AcceptancePlanProcess_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AcceptancePlanProcess);
            this.Name = "StartScreen";
            this.Size = new System.Drawing.Size(1024, 768);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.NavigatedButton AcceptancePlanProcess;

    }
}
