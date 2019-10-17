namespace UdiskSpeed_ASSD
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TestBtn = new System.Windows.Forms.Button();
            this.seq_write_label = new System.Windows.Forms.Label();
            this.seq_read_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pathTB = new System.Windows.Forms.TextBox();
            this.fileSizeTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.TimerLB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(54, 123);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(112, 43);
            this.TestBtn.TabIndex = 1;
            this.TestBtn.Text = "Test";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // seq_write_label
            // 
            this.seq_write_label.AutoSize = true;
            this.seq_write_label.Location = new System.Drawing.Point(51, 267);
            this.seq_write_label.Name = "seq_write_label";
            this.seq_write_label.Size = new System.Drawing.Size(55, 15);
            this.seq_write_label.TabIndex = 3;
            this.seq_write_label.Text = "label1";
            // 
            // seq_read_label
            // 
            this.seq_read_label.AutoSize = true;
            this.seq_read_label.Location = new System.Drawing.Point(51, 316);
            this.seq_read_label.Name = "seq_read_label";
            this.seq_read_label.Size = new System.Drawing.Size(55, 15);
            this.seq_read_label.TabIndex = 4;
            this.seq_read_label.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Path:";
            // 
            // pathTB
            // 
            this.pathTB.Location = new System.Drawing.Point(104, 44);
            this.pathTB.Name = "pathTB";
            this.pathTB.Size = new System.Drawing.Size(100, 25);
            this.pathTB.TabIndex = 6;
            this.pathTB.Text = "H:";
            // 
            // fileSizeTB
            // 
            this.fileSizeTB.Location = new System.Drawing.Point(385, 41);
            this.fileSizeTB.Name = "fileSizeTB";
            this.fileSizeTB.Size = new System.Drawing.Size(100, 25);
            this.fileSizeTB.TabIndex = 8;
            this.fileSizeTB.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Size:(16M*)";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(54, 215);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(431, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // TimerLB
            // 
            this.TimerLB.AutoSize = true;
            this.TimerLB.Location = new System.Drawing.Point(256, 137);
            this.TimerLB.Name = "TimerLB";
            this.TimerLB.Size = new System.Drawing.Size(55, 15);
            this.TimerLB.TabIndex = 10;
            this.TimerLB.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TimerLB);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.fileSizeTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pathTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.seq_read_label);
            this.Controls.Add(this.seq_write_label);
            this.Controls.Add(this.TestBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button TestBtn;
        private System.Windows.Forms.Label seq_write_label;
        private System.Windows.Forms.Label seq_read_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pathTB;
        private System.Windows.Forms.TextBox fileSizeTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label TimerLB;
    }
}

