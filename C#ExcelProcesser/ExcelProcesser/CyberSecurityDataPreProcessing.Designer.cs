namespace ExcelProcesser
{
    partial class CyberSecurityDataPreProcessing
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
            this.btnStartPreProcessing = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProcessByteAttribute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartPreProcessing
            // 
            this.btnStartPreProcessing.Location = new System.Drawing.Point(251, 81);
            this.btnStartPreProcessing.Name = "btnStartPreProcessing";
            this.btnStartPreProcessing.Size = new System.Drawing.Size(159, 23);
            this.btnStartPreProcessing.TabIndex = 0;
            this.btnStartPreProcessing.Text = "Start PreProcessing";
            this.btnStartPreProcessing.UseVisualStyleBackColor = true;
            this.btnStartPreProcessing.Click += new System.EventHandler(this.btnStartPreProcessing_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(47, 31);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(617, 20);
            this.txtFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File :";
            // 
            // btnProcessByteAttribute
            // 
            this.btnProcessByteAttribute.Location = new System.Drawing.Point(437, 81);
            this.btnProcessByteAttribute.Name = "btnProcessByteAttribute";
            this.btnProcessByteAttribute.Size = new System.Drawing.Size(159, 23);
            this.btnProcessByteAttribute.TabIndex = 3;
            this.btnProcessByteAttribute.Text = "Process Bytes Attribute";
            this.btnProcessByteAttribute.UseVisualStyleBackColor = true;
            this.btnProcessByteAttribute.Click += new System.EventHandler(this.btnProcessByteAttribute_Click);
            // 
            // CyberSecurityDataPreProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 325);
            this.Controls.Add(this.btnProcessByteAttribute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btnStartPreProcessing);
            this.Name = "CyberSecurityDataPreProcessing";
            this.Text = "CyberSecurityDataPreProcessing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartPreProcessing;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProcessByteAttribute;
    }
}