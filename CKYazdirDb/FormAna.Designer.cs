namespace ODM.CKYazdirDb
{
    partial class FormAna
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
            this.btnKutuk = new System.Windows.Forms.Button();
            this.btnAyarlar = new System.Windows.Forms.Button();
            this.btnCkOlustur = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnCevapYukle = new System.Windows.Forms.Button();
            this.btnTextOlustur = new System.Windows.Forms.Button();
            this.btnKazanimlar = new System.Windows.Forms.Button();
            this.btnBrans = new System.Windows.Forms.Button();
            this.btnDegerlenirmeKarne = new System.Windows.Forms.Button();
            this.btnSqlExport = new System.Windows.Forms.Button();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnKutuk
            // 
            this.btnKutuk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKutuk.Location = new System.Drawing.Point(12, 22);
            this.btnKutuk.Name = "btnKutuk";
            this.btnKutuk.Size = new System.Drawing.Size(140, 62);
            this.btnKutuk.TabIndex = 1;
            this.btnKutuk.Text = "Kütük İşlemleri";
            this.btnKutuk.UseVisualStyleBackColor = true;
            this.btnKutuk.Click += new System.EventHandler(this.BtnKutuk_Click);
            // 
            // btnAyarlar
            // 
            this.btnAyarlar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAyarlar.Location = new System.Drawing.Point(12, 166);
            this.btnAyarlar.Name = "btnAyarlar";
            this.btnAyarlar.Size = new System.Drawing.Size(140, 63);
            this.btnAyarlar.TabIndex = 2;
            this.btnAyarlar.Text = "Ayarlar";
            this.btnAyarlar.UseVisualStyleBackColor = true;
            this.btnAyarlar.Click += new System.EventHandler(this.BtnAyarlar_Click);
            // 
            // btnCkOlustur
            // 
            this.btnCkOlustur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCkOlustur.Location = new System.Drawing.Point(158, 22);
            this.btnCkOlustur.Name = "btnCkOlustur";
            this.btnCkOlustur.Size = new System.Drawing.Size(140, 62);
            this.btnCkOlustur.TabIndex = 3;
            this.btnCkOlustur.Text = "CK Oluştur";
            this.btnCkOlustur.UseVisualStyleBackColor = true;
            this.btnCkOlustur.Click += new System.EventHandler(this.BtnCkOlustur_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // btnCevapYukle
            // 
            this.btnCevapYukle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCevapYukle.Location = new System.Drawing.Point(12, 90);
            this.btnCevapYukle.Name = "btnCevapYukle";
            this.btnCevapYukle.Size = new System.Drawing.Size(140, 62);
            this.btnCevapYukle.TabIndex = 4;
            this.btnCevapYukle.Text = "Cevapları Yükle";
            this.btnCevapYukle.UseVisualStyleBackColor = true;
            this.btnCevapYukle.Click += new System.EventHandler(this.BtnCevapYukle_Click);
            // 
            // btnTextOlustur
            // 
            this.btnTextOlustur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTextOlustur.Location = new System.Drawing.Point(304, 22);
            this.btnTextOlustur.Name = "btnTextOlustur";
            this.btnTextOlustur.Size = new System.Drawing.Size(140, 62);
            this.btnTextOlustur.TabIndex = 5;
            this.btnTextOlustur.Text = "Text Oluştur";
            this.btnTextOlustur.UseVisualStyleBackColor = true;
            this.btnTextOlustur.Click += new System.EventHandler(this.BtnTextOlustur_Click);
            // 
            // btnKazanimlar
            // 
            this.btnKazanimlar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKazanimlar.Location = new System.Drawing.Point(158, 90);
            this.btnKazanimlar.Name = "btnKazanimlar";
            this.btnKazanimlar.Size = new System.Drawing.Size(140, 62);
            this.btnKazanimlar.TabIndex = 6;
            this.btnKazanimlar.Text = "Kazanımlar";
            this.btnKazanimlar.UseVisualStyleBackColor = true;
            this.btnKazanimlar.Click += new System.EventHandler(this.BtnKazanimlar_Click);
            // 
            // btnBrans
            // 
            this.btnBrans.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrans.Location = new System.Drawing.Point(304, 90);
            this.btnBrans.Name = "btnBrans";
            this.btnBrans.Size = new System.Drawing.Size(140, 62);
            this.btnBrans.TabIndex = 7;
            this.btnBrans.Text = "Branşlar";
            this.btnBrans.UseVisualStyleBackColor = true;
            this.btnBrans.Click += new System.EventHandler(this.BtnBrans_Click);
            // 
            // btnDegerlenirmeKarne
            // 
            this.btnDegerlenirmeKarne.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDegerlenirmeKarne.Location = new System.Drawing.Point(158, 166);
            this.btnDegerlenirmeKarne.Name = "btnDegerlenirmeKarne";
            this.btnDegerlenirmeKarne.Size = new System.Drawing.Size(286, 63);
            this.btnDegerlenirmeKarne.TabIndex = 8;
            this.btnDegerlenirmeKarne.Text = "Değerlendirme ve Karne";
            this.btnDegerlenirmeKarne.UseVisualStyleBackColor = true;
            this.btnDegerlenirmeKarne.Click += new System.EventHandler(this.btnDegerlenirmeKarne_Click);
            // 
            // btnSqlExport
            // 
            this.btnSqlExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSqlExport.Location = new System.Drawing.Point(12, 235);
            this.btnSqlExport.Name = "btnSqlExport";
            this.btnSqlExport.Size = new System.Drawing.Size(432, 63);
            this.btnSqlExport.TabIndex = 10;
            this.btnSqlExport.Text = "Data Export (Web İçin)";
            this.btnSqlExport.UseVisualStyleBackColor = true;
            this.btnSqlExport.Click += new System.EventHandler(this.btnSqlExport_Click);
            // 
            // lblBilgi
            // 
            this.lblBilgi.Location = new System.Drawing.Point(12, 301);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(432, 34);
            this.lblBilgi.TabIndex = 177;
            this.lblBilgi.Text = "...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 350);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(432, 26);
            this.progressBar1.TabIndex = 176;
            // 
            // FormAna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 395);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSqlExport);
            this.Controls.Add(this.btnDegerlenirmeKarne);
            this.Controls.Add(this.btnBrans);
            this.Controls.Add(this.btnKazanimlar);
            this.Controls.Add(this.btnTextOlustur);
            this.Controls.Add(this.btnCevapYukle);
            this.Controls.Add(this.btnCkOlustur);
            this.Controls.Add(this.btnAyarlar);
            this.Controls.Add(this.btnKutuk);
            this.Name = "FormAna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CK Yazdır";
            this.Load += new System.EventHandler(this.FormAna_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnKutuk;
        private System.Windows.Forms.Button btnAyarlar;
        private System.Windows.Forms.Button btnCkOlustur;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnCevapYukle;
        private System.Windows.Forms.Button btnTextOlustur;
        private System.Windows.Forms.Button btnKazanimlar;
        private System.Windows.Forms.Button btnBrans;
        private System.Windows.Forms.Button btnDegerlenirmeKarne;
        private System.Windows.Forms.Button btnSqlExport;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
    }
}

