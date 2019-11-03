namespace ODM.CKYazdirDb
{
    partial class FormDegerlendirmeKarne
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDegerlendirmeKarne));
            this.btnDegerlendir = new System.Windows.Forms.Button();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnDataAc = new System.Windows.Forms.Button();
            this.bgwDegerlendir = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnDegerlendir
            // 
            this.btnDegerlendir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDegerlendir.Image = ((System.Drawing.Image)(resources.GetObject("btnDegerlendir.Image")));
            this.btnDegerlendir.Location = new System.Drawing.Point(254, 52);
            this.btnDegerlendir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDegerlendir.Name = "btnDegerlendir";
            this.btnDegerlendir.Size = new System.Drawing.Size(236, 45);
            this.btnDegerlendir.TabIndex = 176;
            this.btnDegerlendir.Text = "Karneleri Oluştur";
            this.btnDegerlendir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDegerlendir.UseVisualStyleBackColor = true;
            this.btnDegerlendir.Click += new System.EventHandler(this.btnDegerlendir_Click);
            // 
            // lblBilgi
            // 
            this.lblBilgi.Location = new System.Drawing.Point(12, 99);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(478, 59);
            this.lblBilgi.TabIndex = 175;
            this.lblBilgi.Text = "...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 22);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(478, 26);
            this.progressBar1.TabIndex = 174;
            // 
            // btnDataAc
            // 
            this.btnDataAc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDataAc.Image = ((System.Drawing.Image)(resources.GetObject("btnDataAc.Image")));
            this.btnDataAc.Location = new System.Drawing.Point(12, 52);
            this.btnDataAc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDataAc.Name = "btnDataAc";
            this.btnDataAc.Size = new System.Drawing.Size(236, 45);
            this.btnDataAc.TabIndex = 173;
            this.btnDataAc.Text = "Cevap Dosyası Yükle";
            this.btnDataAc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDataAc.UseVisualStyleBackColor = true;
            this.btnDataAc.Click += new System.EventHandler(this.btnDataAc_Click);
            // 
            // bgwDegerlendir
            // 
            this.bgwDegerlendir.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDegerlendir_DoWork);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // FormDegerlendirmeKarne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 154);
            this.Controls.Add(this.btnDegerlendir);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnDataAc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDegerlendirmeKarne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Değerlendirme ve Karne Oluşturma Formu";
            this.Load += new System.EventHandler(this.FormDegerlendirmeKarne_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDegerlendir;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnDataAc;
        private System.ComponentModel.BackgroundWorker bgwDegerlendir;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}