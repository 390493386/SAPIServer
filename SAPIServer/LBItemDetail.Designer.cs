namespace SiweiSoft.SAPIServer
{
    partial class LBItemDetail
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
            this.ldRichTextBox = new System.Windows.Forms.RichTextBox();
            this.CloseDetails = new System.Windows.Forms.Button();
            this.CopyLog = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ldRichTextBox
            // 
            this.ldRichTextBox.Location = new System.Drawing.Point(1, 3);
            this.ldRichTextBox.Name = "ldRichTextBox";
            this.ldRichTextBox.Size = new System.Drawing.Size(358, 75);
            this.ldRichTextBox.TabIndex = 0;
            this.ldRichTextBox.Text = "";
            // 
            // CloseDetails
            // 
            this.CloseDetails.Location = new System.Drawing.Point(273, 84);
            this.CloseDetails.Name = "CloseDetails";
            this.CloseDetails.Size = new System.Drawing.Size(75, 23);
            this.CloseDetails.TabIndex = 1;
            this.CloseDetails.Text = "关闭";
            this.CloseDetails.UseVisualStyleBackColor = true;
            this.CloseDetails.Click += new System.EventHandler(this.CloseDetails_Click);
            // 
            // CopyLog
            // 
            this.CopyLog.Location = new System.Drawing.Point(192, 84);
            this.CopyLog.Name = "CopyLog";
            this.CopyLog.Size = new System.Drawing.Size(75, 23);
            this.CopyLog.TabIndex = 2;
            this.CopyLog.Text = "复制信息";
            this.CopyLog.UseVisualStyleBackColor = true;
            this.CopyLog.Click += new System.EventHandler(this.CopyLog_Click);
            // 
            // message
            // 
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(6, 89);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(0, 12);
            this.message.TabIndex = 3;
            // 
            // LBItemDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 113);
            this.Controls.Add(this.message);
            this.Controls.Add(this.CopyLog);
            this.Controls.Add(this.CloseDetails);
            this.Controls.Add(this.ldRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LBItemDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "详细信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ldRichTextBox;
        private System.Windows.Forms.Button CloseDetails;
        private System.Windows.Forms.Button CopyLog;
        private System.Windows.Forms.Label message;
    }
}