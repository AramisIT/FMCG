namespace AtosFMCG.TouchScreen.Controls
    {
    partial class Message
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.topic = new System.Windows.Forms.Label();
            this.detailInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.detailInfo);
            this.panel1.Controls.Add(this.topic);
            this.panel1.Location = new System.Drawing.Point(3, 98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(452, 429);
            this.panel1.TabIndex = 0;
            // 
            // topic
            // 
            this.topic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topic.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.topic.ForeColor = System.Drawing.Color.DarkOrange;
            this.topic.Location = new System.Drawing.Point(3, 60);
            this.topic.Name = "topic";
            this.topic.Size = new System.Drawing.Size(446, 56);
            this.topic.TabIndex = 0;
            this.topic.Text = "label1";
            this.topic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailInfo
            // 
            this.detailInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.detailInfo.ForeColor = System.Drawing.Color.DimGray;
            this.detailInfo.Location = new System.Drawing.Point(43, 150);
            this.detailInfo.Name = "detailInfo";
            this.detailInfo.Size = new System.Drawing.Size(367, 279);
            this.detailInfo.TabIndex = 47;
            this.detailInfo.Text = "....";
            this.detailInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "Message";
            this.Size = new System.Drawing.Size(455, 559);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label topic;
        private System.Windows.Forms.Label detailInfo;
        }
    }
