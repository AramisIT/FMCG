namespace WMS_client.Base.Visual
{
    partial class RemoteCallDialog
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
        this.messageLabel = new System.Windows.Forms.Label();
        this.cancelQuery = new System.Windows.Forms.Button();
        this.timer = new System.Windows.Forms.Timer();
        this.SuspendLayout();
        // 
        // messageLabel
        // 
        this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.messageLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
        this.messageLabel.ForeColor = System.Drawing.Color.Navy;
        this.messageLabel.Location = new System.Drawing.Point(0, 16);
        this.messageLabel.Name = "messageLabel";
        this.messageLabel.Size = new System.Drawing.Size(239, 56);
        this.messageLabel.Text = "Обращение к серверу";
        this.messageLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // cancelQuery
        // 
        this.cancelQuery.BackColor = System.Drawing.Color.RoyalBlue;
        this.cancelQuery.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
        this.cancelQuery.ForeColor = System.Drawing.Color.White;
        this.cancelQuery.Location = new System.Drawing.Point(19, 106);
        this.cancelQuery.Name = "cancelQuery";
        this.cancelQuery.Size = new System.Drawing.Size(199, 41);
        this.cancelQuery.TabIndex = 1;
        this.cancelQuery.Text = "ОТМЕНА";
        this.cancelQuery.Click += new System.EventHandler(this.cancelQuery_Click);
        // 
        // timer
        // 
        this.timer.Enabled = true;
        this.timer.Interval = 300;
        this.timer.Tick += new System.EventHandler(this.timer_Tick);
        // 
        // RemoteCallDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.ClientSize = new System.Drawing.Size(240, 150);
        this.ControlBox = false;
        this.Controls.Add(this.cancelQuery);
        this.Controls.Add(this.messageLabel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Location = new System.Drawing.Point(0, 90);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "RemoteCallDialog";
        this.TopMost = true;
        this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button cancelQuery;
        private System.Windows.Forms.Timer timer;
    }
}