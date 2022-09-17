namespace ExcelProcesser
{
    partial class DeviationCalculator
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtValidationLosses = new System.Windows.Forms.TextBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMaxDifference = new System.Windows.Forms.Label();
            this.lblMinDifference = new System.Windows.Forms.Label();
            this.lblComparedValuesForGettingMin = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblComparedValuesForGettingMax = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSameValueForConsectivePairs = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(437, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "The following option will calculate the difference between subsequent 2 rows in e" +
    "xcel sheet";
            // 
            // txtValidationLosses
            // 
            this.txtValidationLosses.Location = new System.Drawing.Point(156, 39);
            this.txtValidationLosses.Name = "txtValidationLosses";
            this.txtValidationLosses.Size = new System.Drawing.Size(540, 20);
            this.txtValidationLosses.TabIndex = 1;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(715, 37);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(89, 23);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Start Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Max Difference: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Min Difference: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Comma Separated Text: ";
            // 
            // lblMaxDifference
            // 
            this.lblMaxDifference.AutoSize = true;
            this.lblMaxDifference.Location = new System.Drawing.Point(156, 81);
            this.lblMaxDifference.Name = "lblMaxDifference";
            this.lblMaxDifference.Size = new System.Drawing.Size(86, 13);
            this.lblMaxDifference.TabIndex = 6;
            this.lblMaxDifference.Text = "lblMaxDifference";
            // 
            // lblMinDifference
            // 
            this.lblMinDifference.AutoSize = true;
            this.lblMinDifference.Location = new System.Drawing.Point(155, 113);
            this.lblMinDifference.Name = "lblMinDifference";
            this.lblMinDifference.Size = new System.Drawing.Size(83, 13);
            this.lblMinDifference.TabIndex = 7;
            this.lblMinDifference.Text = "lblMinDifference";
            // 
            // lblComparedValuesForGettingMin
            // 
            this.lblComparedValuesForGettingMin.AutoSize = true;
            this.lblComparedValuesForGettingMin.Location = new System.Drawing.Point(429, 113);
            this.lblComparedValuesForGettingMin.Name = "lblComparedValuesForGettingMin";
            this.lblComparedValuesForGettingMin.Size = new System.Drawing.Size(163, 13);
            this.lblComparedValuesForGettingMin.TabIndex = 9;
            this.lblComparedValuesForGettingMin.Text = "lblComparedValuesForGettingMin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(301, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Compared Values: ";
            // 
            // lblComparedValuesForGettingMax
            // 
            this.lblComparedValuesForGettingMax.AutoSize = true;
            this.lblComparedValuesForGettingMax.Location = new System.Drawing.Point(429, 81);
            this.lblComparedValuesForGettingMax.Name = "lblComparedValuesForGettingMax";
            this.lblComparedValuesForGettingMax.Size = new System.Drawing.Size(166, 13);
            this.lblComparedValuesForGettingMax.TabIndex = 11;
            this.lblComparedValuesForGettingMax.Text = "lblComparedValuesForGettingMax";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(301, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Compared Values: ";
            // 
            // lblSameValueForConsectivePairs
            // 
            this.lblSameValueForConsectivePairs.AutoSize = true;
            this.lblSameValueForConsectivePairs.Location = new System.Drawing.Point(232, 148);
            this.lblSameValueForConsectivePairs.Name = "lblSameValueForConsectivePairs";
            this.lblSameValueForConsectivePairs.Size = new System.Drawing.Size(162, 13);
            this.lblSameValueForConsectivePairs.TabIndex = 13;
            this.lblSameValueForConsectivePairs.Text = "lblSameValueForConsectivePairs";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Same Value for Consective Pairs: ";
            // 
            // DeviationCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 370);
            this.Controls.Add(this.lblSameValueForConsectivePairs);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblComparedValuesForGettingMax);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblComparedValuesForGettingMin);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblMinDifference);
            this.Controls.Add(this.lblMaxDifference);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtValidationLosses);
            this.Controls.Add(this.label1);
            this.Name = "DeviationCalculator";
            this.Text = "DeviationCalculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtValidationLosses;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMaxDifference;
        private System.Windows.Forms.Label lblMinDifference;
        private System.Windows.Forms.Label lblComparedValuesForGettingMin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblComparedValuesForGettingMax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblSameValueForConsectivePairs;
        private System.Windows.Forms.Label label7;
    }
}