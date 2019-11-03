namespace ODM.CKYazdirDb
{
    partial class FormCkOlustur
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCkOlustur));
            this.lblAyarlarBilgi = new System.Windows.Forms.Label();
            this.btnSinifSubetoExcel = new System.Windows.Forms.Button();
            this.btnSinifListesi = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSube = new System.Windows.Forms.ComboBox();
            this.cbSinifi = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOgrenciler = new System.Windows.Forms.ComboBox();
            this.cbOkullar = new System.Windows.Forms.ComboBox();
            this.cbIlceler = new System.Windows.Forms.ComboBox();
            this.btnCKDosyaOlustur = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.pbCkA5Dosyasi = new System.Windows.Forms.PictureBox();
            this.BgwCkOlustur = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolSslKalanSure = new System.Windows.Forms.ToolStripStatusLabel();
            this.BgwSinifListesi = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pbCkA5Dosyasi)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAyarlarBilgi
            // 
            this.lblAyarlarBilgi.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAyarlarBilgi.ForeColor = System.Drawing.Color.IndianRed;
            this.lblAyarlarBilgi.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblAyarlarBilgi.Location = new System.Drawing.Point(720, 227);
            this.lblAyarlarBilgi.Name = "lblAyarlarBilgi";
            this.lblAyarlarBilgi.Size = new System.Drawing.Size(163, 21);
            this.lblAyarlarBilgi.TabIndex = 186;
            this.lblAyarlarBilgi.Text = "label2";
            this.lblAyarlarBilgi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSinifSubetoExcel
            // 
            this.btnSinifSubetoExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSinifSubetoExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnSinifSubetoExcel.Image")));
            this.btnSinifSubetoExcel.Location = new System.Drawing.Point(528, 123);
            this.btnSinifSubetoExcel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSinifSubetoExcel.Name = "btnSinifSubetoExcel";
            this.btnSinifSubetoExcel.Size = new System.Drawing.Size(175, 39);
            this.btnSinifSubetoExcel.TabIndex = 185;
            this.btnSinifSubetoExcel.Text = "Şube Sayılarını Çıkar";
            this.btnSinifSubetoExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSinifSubetoExcel.UseVisualStyleBackColor = true;
            this.btnSinifSubetoExcel.Click += new System.EventHandler(this.BtnSinifSubetoExcel_Click);
            // 
            // btnSinifListesi
            // 
            this.btnSinifListesi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSinifListesi.Location = new System.Drawing.Point(528, 71);
            this.btnSinifListesi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSinifListesi.Name = "btnSinifListesi";
            this.btnSinifListesi.Size = new System.Drawing.Size(175, 39);
            this.btnSinifListesi.TabIndex = 184;
            this.btnSinifListesi.Text = "Sınıf Listesi Oluştur";
            this.btnSinifListesi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSinifListesi.UseVisualStyleBackColor = true;
            this.btnSinifListesi.Click += new System.EventHandler(this.BtnSinifListesi_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(9, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 24);
            this.label6.TabIndex = 183;
            this.label6.Text = "Sınıf - Şube :";
            // 
            // cbSube
            // 
            this.cbSube.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSube.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSube.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbSube.FormattingEnabled = true;
            this.cbSube.Location = new System.Drawing.Point(262, 122);
            this.cbSube.Margin = new System.Windows.Forms.Padding(4);
            this.cbSube.Name = "cbSube";
            this.cbSube.Size = new System.Drawing.Size(260, 37);
            this.cbSube.TabIndex = 182;
            this.cbSube.SelectedIndexChanged += new System.EventHandler(this.CbSube_SelectedIndexChanged);
            // 
            // cbSinifi
            // 
            this.cbSinifi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSinifi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSinifi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbSinifi.FormattingEnabled = true;
            this.cbSinifi.Location = new System.Drawing.Point(132, 122);
            this.cbSinifi.Margin = new System.Windows.Forms.Padding(4);
            this.cbSinifi.Name = "cbSinifi";
            this.cbSinifi.Size = new System.Drawing.Size(121, 37);
            this.cbSinifi.TabIndex = 181;
            this.cbSinifi.SelectedIndexChanged += new System.EventHandler(this.CbSinifi_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(37, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 24);
            this.label5.TabIndex = 180;
            this.label5.Text = "Öğrenci :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(66, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 24);
            this.label4.TabIndex = 179;
            this.label4.Text = "Okul :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(76, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 24);
            this.label3.TabIndex = 178;
            this.label3.Text = "İlçe :";
            // 
            // cbOgrenciler
            // 
            this.cbOgrenciler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbOgrenciler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOgrenciler.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbOgrenciler.FormattingEnabled = true;
            this.cbOgrenciler.Location = new System.Drawing.Point(132, 177);
            this.cbOgrenciler.Margin = new System.Windows.Forms.Padding(4);
            this.cbOgrenciler.Name = "cbOgrenciler";
            this.cbOgrenciler.Size = new System.Drawing.Size(389, 37);
            this.cbOgrenciler.TabIndex = 177;
            this.cbOgrenciler.SelectedIndexChanged += new System.EventHandler(this.CbOgrenciler_SelectedIndexChanged);
            // 
            // cbOkullar
            // 
            this.cbOkullar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbOkullar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOkullar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbOkullar.FormattingEnabled = true;
            this.cbOkullar.Location = new System.Drawing.Point(132, 67);
            this.cbOkullar.Margin = new System.Windows.Forms.Padding(4);
            this.cbOkullar.Name = "cbOkullar";
            this.cbOkullar.Size = new System.Drawing.Size(389, 37);
            this.cbOkullar.TabIndex = 176;
            this.cbOkullar.SelectedIndexChanged += new System.EventHandler(this.CbOkullar_SelectedIndexChanged);
            // 
            // cbIlceler
            // 
            this.cbIlceler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbIlceler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIlceler.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbIlceler.FormattingEnabled = true;
            this.cbIlceler.Location = new System.Drawing.Point(132, 13);
            this.cbIlceler.Margin = new System.Windows.Forms.Padding(4);
            this.cbIlceler.Name = "cbIlceler";
            this.cbIlceler.Size = new System.Drawing.Size(389, 37);
            this.cbIlceler.TabIndex = 175;
            this.cbIlceler.SelectedIndexChanged += new System.EventHandler(this.CbIlceler_SelectedIndexChanged);
            // 
            // btnCKDosyaOlustur
            // 
            this.btnCKDosyaOlustur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCKDosyaOlustur.Image = ((System.Drawing.Image)(resources.GetObject("btnCKDosyaOlustur.Image")));
            this.btnCKDosyaOlustur.Location = new System.Drawing.Point(528, 13);
            this.btnCKDosyaOlustur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCKDosyaOlustur.Name = "btnCKDosyaOlustur";
            this.btnCKDosyaOlustur.Size = new System.Drawing.Size(175, 39);
            this.btnCKDosyaOlustur.TabIndex = 173;
            this.btnCKDosyaOlustur.Text = "CK - Dosya Oluştur";
            this.btnCKDosyaOlustur.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCKDosyaOlustur.UseVisualStyleBackColor = true;
            this.btnCKDosyaOlustur.Click += new System.EventHandler(this.BtnCKDosyaOlustur_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 267);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(869, 28);
            this.progressBar1.TabIndex = 172;
            // 
            // lblBilgi
            // 
            this.lblBilgi.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBilgi.Location = new System.Drawing.Point(13, 227);
            this.lblBilgi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(691, 34);
            this.lblBilgi.TabIndex = 171;
            this.lblBilgi.Text = "E-Okuldan alınan kütük dosyalarını seçiniz.";
            this.lblBilgi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbCkA5Dosyasi
            // 
            this.pbCkA5Dosyasi.Location = new System.Drawing.Point(720, 13);
            this.pbCkA5Dosyasi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbCkA5Dosyasi.Name = "pbCkA5Dosyasi";
            this.pbCkA5Dosyasi.Size = new System.Drawing.Size(163, 206);
            this.pbCkA5Dosyasi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCkA5Dosyasi.TabIndex = 174;
            this.pbCkA5Dosyasi.TabStop = false;
            // 
            // BgwCkOlustur
            // 
            this.BgwCkOlustur.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwCkOlustur_DoWork);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSslKalanSure});
            this.statusStrip1.Location = new System.Drawing.Point(0, 308);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(907, 25);
            this.statusStrip1.TabIndex = 187;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolSslKalanSure
            // 
            this.toolSslKalanSure.Name = "toolSslKalanSure";
            this.toolSslKalanSure.Size = new System.Drawing.Size(18, 20);
            this.toolSslKalanSure.Text = "...";
            // 
            // BgwSinifListesi
            // 
            this.BgwSinifListesi.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgwSinifListesi_DoWork);
            // 
            // FormCkOlustur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 333);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblAyarlarBilgi);
            this.Controls.Add(this.btnSinifSubetoExcel);
            this.Controls.Add(this.btnSinifListesi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSube);
            this.Controls.Add(this.cbSinifi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbOgrenciler);
            this.Controls.Add(this.cbOkullar);
            this.Controls.Add(this.cbIlceler);
            this.Controls.Add(this.btnCKDosyaOlustur);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.pbCkA5Dosyasi);
            this.Name = "FormCkOlustur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCkOlustur";
            this.Load += new System.EventHandler(this.FormCkOlustur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCkA5Dosyasi)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAyarlarBilgi;
        private System.Windows.Forms.Button btnSinifSubetoExcel;
        private System.Windows.Forms.Button btnSinifListesi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSube;
        private System.Windows.Forms.ComboBox cbSinifi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbOgrenciler;
        private System.Windows.Forms.ComboBox cbOkullar;
        private System.Windows.Forms.ComboBox cbIlceler;
        private System.Windows.Forms.Button btnCKDosyaOlustur;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.PictureBox pbCkA5Dosyasi;
        private System.ComponentModel.BackgroundWorker BgwCkOlustur;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolSslKalanSure;
        private System.ComponentModel.BackgroundWorker BgwSinifListesi;
    }
}