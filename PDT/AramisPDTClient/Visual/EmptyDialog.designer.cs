﻿namespace WMS_client.Base.Visual
{
    partial class EmptyDialog
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
        this.closeFormTimer = new System.Windows.Forms.Timer();
        this.messageLabel = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // closeFormTimer
        // 
        this.closeFormTimer.Interval = 1000;
        this.closeFormTimer.Tick += new System.EventHandler(this.closeFormTimer_Tick);
        // 
        // messageLabel
        // 
        this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.messageLabel.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Bold);
        this.messageLabel.ForeColor = System.Drawing.Color.Navy;
        this.messageLabel.Location = new System.Drawing.Point(0, 33);
        this.messageLabel.Name = "messageLabel";
        this.messageLabel.Size = new System.Drawing.Size(237, 56);
        this.messageLabel.Text = "ATOS WMS";
        this.messageLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // EmptyDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.AutoScroll = true;
        this.ClientSize = new System.Drawing.Size(238, 127);
        this.ControlBox = false;
        this.Controls.Add(this.messageLabel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "EmptyDialog";
        this.Load += new System.EventHandler(this.emptyDialog_Load);
        this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer closeFormTimer;
        private System.Windows.Forms.Label messageLabel;
    }
}