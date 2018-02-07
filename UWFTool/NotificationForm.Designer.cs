namespace UWFTool
{
    partial class NotificationForm
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
            this.currentStatus = new System.Windows.Forms.Label();
            this.enableButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // currentStatus
            // 
            this.currentStatus.AutoSize = true;
            this.currentStatus.Location = new System.Drawing.Point(56, 46);
            this.currentStatus.Name = "currentStatus";
            this.currentStatus.Size = new System.Drawing.Size(183, 15);
            this.currentStatus.TabIndex = 0;
            this.currentStatus.Text = "Current UWF status is disabled";
            // 
            // enableButton
            // 
            this.enableButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.enableButton.Location = new System.Drawing.Point(44, 74);
            this.enableButton.Name = "enableButton";
            this.enableButton.Size = new System.Drawing.Size(204, 30);
            this.enableButton.TabIndex = 1;
            this.enableButton.Text = "Enable UWF and Restart";
            this.enableButton.UseVisualStyleBackColor = true;
            this.enableButton.Click += new System.EventHandler(this.enableButton_Click);
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 125);
            this.Controls.Add(this.enableButton);
            this.Controls.Add(this.currentStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotificationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotificationForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NotificationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label currentStatus;
        private System.Windows.Forms.Button enableButton;
    }
}