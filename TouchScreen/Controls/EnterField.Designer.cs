namespace AtosFMCG.TouchScreen.Controls
    {
    partial class EnterField
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
            this.oldValue = new System.Windows.Forms.Label();
            this.inputField = new System.Windows.Forms.TextBox();
            this.topic = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.oldValue);
            this.panel1.Controls.Add(this.inputField);
            this.panel1.Controls.Add(this.topic);
            this.panel1.Location = new System.Drawing.Point(3, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 175);
            this.panel1.TabIndex = 0;
            // 
            // oldValue
            // 
            this.oldValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oldValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.oldValue.ForeColor = System.Drawing.Color.Gray;
            this.oldValue.Location = new System.Drawing.Point(0, 141);
            this.oldValue.Name = "oldValue";
            this.oldValue.Size = new System.Drawing.Size(398, 20);
            this.oldValue.TabIndex = 2;
            this.oldValue.Text = "label1";
            this.oldValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inputField
            // 
            this.inputField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputField.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputField.Location = new System.Drawing.Point(64, 93);
            this.inputField.Name = "inputField";
            this.inputField.Size = new System.Drawing.Size(270, 45);
            this.inputField.TabIndex = 1;
            this.inputField.TextChanged += new System.EventHandler(this.inputField_TextChanged);
            // 
            // topic
            // 
            this.topic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topic.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.topic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(125)))), ((int)(((byte)(195)))));
            this.topic.Location = new System.Drawing.Point(0, 17);
            this.topic.Name = "topic";
            this.topic.Size = new System.Drawing.Size(398, 56);
            this.topic.TabIndex = 0;
            this.topic.Text = "label1";
            this.topic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EnterField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "EnterField";
            this.Size = new System.Drawing.Size(401, 281);
            this.Load += new System.EventHandler(this.EnterField_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

            }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox inputField;
        private System.Windows.Forms.Label topic;
        private System.Windows.Forms.Label oldValue;
        }
    }
