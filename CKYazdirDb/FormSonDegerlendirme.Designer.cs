namespace ODM.CKYazdirDb
{
    partial class FormSonDegerlendirme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSonDegerlendirme));
            this.btnDegerlendirme1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolSslKalanSure = new System.Windows.Forms.ToolStripStatusLabel();
            this.bgwDegerlendirme1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnDegerlendirme2 = new System.Windows.Forms.Button();
            this.bgwDegerlendirme2 = new System.ComponentModel.BackgroundWorker();
            this.bgwOkulOrtalamalari = new System.ComponentModel.BackgroundWorker();
            this.bgwIlIlceOrtalamasi = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bgwOgrenciOrtalamalari = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDegerlendirme1
            // 
            this.btnDegerlendirme1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDegerlendirme1.Location = new System.Drawing.Point(42, 112);
            this.btnDegerlendirme1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDegerlendirme1.Name = "btnDegerlendirme1";
            this.btnDegerlendirme1.Size = new System.Drawing.Size(179, 47);
            this.btnDegerlendirme1.TabIndex = 0;
            this.btnDegerlendirme1.Text = "Değerlendirme 1";
            this.btnDegerlendirme1.UseVisualStyleBackColor = true;
            this.btnDegerlendirme1.Click += new System.EventHandler(this.btnDegerlendirme1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSslKalanSure});
            this.statusStrip1.Location = new System.Drawing.Point(0, 282);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(723, 26);
            this.statusStrip1.TabIndex = 189;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolSslKalanSure
            // 
            this.toolSslKalanSure.Name = "toolSslKalanSure";
            this.toolSslKalanSure.Size = new System.Drawing.Size(18, 20);
            this.toolSslKalanSure.Text = "...";
            // 
            // bgwDegerlendirme1
            // 
            this.bgwDegerlendirme1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDegerlendirme1_DoWork);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(42, 39);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(635, 26);
            this.progressBar1.TabIndex = 190;
            // 
            // btnDegerlendirme2
            // 
            this.btnDegerlendirme2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDegerlendirme2.Location = new System.Drawing.Point(42, 195);
            this.btnDegerlendirme2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDegerlendirme2.Name = "btnDegerlendirme2";
            this.btnDegerlendirme2.Size = new System.Drawing.Size(179, 46);
            this.btnDegerlendirme2.TabIndex = 194;
            this.btnDegerlendirme2.Text = "Değerlendirme 2";
            this.btnDegerlendirme2.UseVisualStyleBackColor = true;
            this.btnDegerlendirme2.Click += new System.EventHandler(this.btnDegerlendirme2_Click);
            // 
            // bgwDegerlendirme2
            // 
            this.bgwDegerlendirme2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDegerlendirme2_DoWork);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(227, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(450, 78);
            this.label2.TabIndex = 197;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(227, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(450, 51);
            this.label3.TabIndex = 198;
            this.label3.Text = "İl ilçe karnelerinin oluşturulması için hesaplamalar yapar.";
            // 
            // FormSonDegerlendirme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 308);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDegerlendirme2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnDegerlendirme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSonDegerlendirme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Değerlendirme";
            this.Load += new System.EventHandler(this.FormSonDegerlendirme_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDegerlendirme1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolSslKalanSure;
        private System.ComponentModel.BackgroundWorker bgwDegerlendirme1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnDegerlendirme2;
        private System.ComponentModel.BackgroundWorker bgwDegerlendirme2;
        private System.ComponentModel.BackgroundWorker bgwOkulOrtalamalari;
        private System.ComponentModel.BackgroundWorker bgwIlIlceOrtalamasi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bgwOgrenciOrtalamalari;
    }
}