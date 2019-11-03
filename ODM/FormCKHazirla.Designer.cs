namespace ODM
{
    partial class FormCKHazirla
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
            this.btnCkYazdir = new System.Windows.Forms.Button();
            this.cbSube = new System.Windows.Forms.ComboBox();
            this.cbSinif = new System.Windows.Forms.ComboBox();
            this.cbOkul = new System.Windows.Forms.ComboBox();
            this.cbIlce = new System.Windows.Forms.ComboBox();
            this.cbOturum = new System.Windows.Forms.ComboBox();
            this.lblSinavNo = new System.Windows.Forms.Label();
            this.lblSinavAdi = new System.Windows.Forms.Label();
            this.bgwCkOlustur = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbOgrenciler = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbYazicilar = new System.Windows.Forms.ComboBox();
            this.cbxDosyaOlustur = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCkYazdir
            // 
            this.btnCkYazdir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCkYazdir.Location = new System.Drawing.Point(13, 245);
            this.btnCkYazdir.Name = "btnCkYazdir";
            this.btnCkYazdir.Size = new System.Drawing.Size(195, 36);
            this.btnCkYazdir.TabIndex = 1;
            this.btnCkYazdir.Text = "Cevap Kağıdı Oluştur";
            this.btnCkYazdir.UseVisualStyleBackColor = true;
            this.btnCkYazdir.Click += new System.EventHandler(this.btnCkYazdir_Click);
            // 
            // cbSube
            // 
            this.cbSube.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSube.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbSube.FormattingEnabled = true;
            this.cbSube.Location = new System.Drawing.Point(172, 143);
            this.cbSube.Margin = new System.Windows.Forms.Padding(4);
            this.cbSube.Name = "cbSube";
            this.cbSube.Size = new System.Drawing.Size(148, 28);
            this.cbSube.TabIndex = 121;
            this.cbSube.SelectedIndexChanged += new System.EventHandler(this.cbSube_SelectedIndexChanged);
            // 
            // cbSinif
            // 
            this.cbSinif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSinif.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbSinif.FormattingEnabled = true;
            this.cbSinif.Location = new System.Drawing.Point(13, 143);
            this.cbSinif.Margin = new System.Windows.Forms.Padding(4);
            this.cbSinif.Name = "cbSinif";
            this.cbSinif.Size = new System.Drawing.Size(148, 28);
            this.cbSinif.TabIndex = 120;
            this.cbSinif.SelectedIndexChanged += new System.EventHandler(this.cbSinif_SelectedIndexChanged);
            // 
            // cbOkul
            // 
            this.cbOkul.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOkul.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbOkul.FormattingEnabled = true;
            this.cbOkul.Location = new System.Drawing.Point(13, 85);
            this.cbOkul.Margin = new System.Windows.Forms.Padding(4);
            this.cbOkul.Name = "cbOkul";
            this.cbOkul.Size = new System.Drawing.Size(466, 28);
            this.cbOkul.TabIndex = 119;
            this.cbOkul.SelectedIndexChanged += new System.EventHandler(this.cbOkul_SelectedIndexChanged);
            // 
            // cbIlce
            // 
            this.cbIlce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIlce.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbIlce.FormattingEnabled = true;
            this.cbIlce.Location = new System.Drawing.Point(13, 27);
            this.cbIlce.Margin = new System.Windows.Forms.Padding(4);
            this.cbIlce.Name = "cbIlce";
            this.cbIlce.Size = new System.Drawing.Size(466, 28);
            this.cbIlce.TabIndex = 118;
            this.cbIlce.SelectedIndexChanged += new System.EventHandler(this.cbIlce_SelectedIndexChanged);
            // 
            // cbOturum
            // 
            this.cbOturum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOturum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbOturum.FormattingEnabled = true;
            this.cbOturum.Items.AddRange(new object[] {
            "1. Oturum",
            "2. Oturum",
            "3. Oturum",
            "4. Oturum",
            "5. Oturum",
            "6. Oturum"});
            this.cbOturum.Location = new System.Drawing.Point(331, 143);
            this.cbOturum.Margin = new System.Windows.Forms.Padding(4);
            this.cbOturum.Name = "cbOturum";
            this.cbOturum.Size = new System.Drawing.Size(148, 28);
            this.cbOturum.TabIndex = 116;
            // 
            // lblSinavNo
            // 
            this.lblSinavNo.AutoSize = true;
            this.lblSinavNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSinavNo.Location = new System.Drawing.Point(13, 370);
            this.lblSinavNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSinavNo.Name = "lblSinavNo";
            this.lblSinavNo.Size = new System.Drawing.Size(53, 20);
            this.lblSinavNo.TabIndex = 27;
            this.lblSinavNo.Text = "label1";
            // 
            // lblSinavAdi
            // 
            this.lblSinavAdi.AutoSize = true;
            this.lblSinavAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSinavAdi.Location = new System.Drawing.Point(13, 341);
            this.lblSinavAdi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSinavAdi.Name = "lblSinavAdi";
            this.lblSinavAdi.Size = new System.Drawing.Size(53, 20);
            this.lblSinavAdi.TabIndex = 26;
            this.lblSinavAdi.Text = "label1";
            // 
            // bgwCkOlustur
            // 
            this.bgwCkOlustur.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCkOlustur_DoWork);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 122;
            this.label1.Text = "İlçe Seçiniz :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 123;
            this.label2.Text = "Okul Seçiniz :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 124;
            this.label3.Text = "Şube Seçiniz :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 125;
            this.label4.Text = "Sınıf Seçiniz :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(331, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 17);
            this.label5.TabIndex = 126;
            this.label5.Text = "Oturum Seçiniz :";
            // 
            // cbOgrenciler
            // 
            this.cbOgrenciler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOgrenciler.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbOgrenciler.FormattingEnabled = true;
            this.cbOgrenciler.Location = new System.Drawing.Point(13, 202);
            this.cbOgrenciler.Margin = new System.Windows.Forms.Padding(4);
            this.cbOgrenciler.Name = "cbOgrenciler";
            this.cbOgrenciler.Size = new System.Drawing.Size(466, 28);
            this.cbOgrenciler.TabIndex = 127;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 17);
            this.label6.TabIndex = 128;
            this.label6.Text = "Öğrenci Seçiniz :";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(17, 294);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(462, 28);
            this.progressBar1.TabIndex = 129;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ODM.Properties.Resources.Aralık_İzleme_Sınavı_A5_Dikey___Fen_Bilimleri;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 393);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1649, 2265);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cbYazicilar
            // 
            this.cbYazicilar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYazicilar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbYazicilar.FormattingEnabled = true;
            this.cbYazicilar.Location = new System.Drawing.Point(496, 27);
            this.cbYazicilar.Margin = new System.Windows.Forms.Padding(4);
            this.cbYazicilar.Name = "cbYazicilar";
            this.cbYazicilar.Size = new System.Drawing.Size(162, 28);
            this.cbYazicilar.TabIndex = 130;
            // 
            // cbxDosyaOlustur
            // 
            this.cbxDosyaOlustur.AutoSize = true;
            this.cbxDosyaOlustur.Checked = true;
            this.cbxDosyaOlustur.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxDosyaOlustur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxDosyaOlustur.Location = new System.Drawing.Point(214, 254);
            this.cbxDosyaOlustur.Name = "cbxDosyaOlustur";
            this.cbxDosyaOlustur.Size = new System.Drawing.Size(120, 21);
            this.cbxDosyaOlustur.TabIndex = 131;
            this.cbxDosyaOlustur.Text = "Dosya Oluştur";
            this.cbxDosyaOlustur.UseVisualStyleBackColor = true;
            this.cbxDosyaOlustur.CheckedChanged += new System.EventHandler(this.cbxDosyaOlustur_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(508, 122);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 77);
            this.button1.TabIndex = 132;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormCKHazirla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(702, 606);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbxDosyaOlustur);
            this.Controls.Add(this.cbYazicilar);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbOgrenciler);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCkYazdir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSinavAdi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSinavNo);
            this.Controls.Add(this.cbOturum);
            this.Controls.Add(this.cbSube);
            this.Controls.Add(this.cbIlce);
            this.Controls.Add(this.cbSinif);
            this.Controls.Add(this.cbOkul);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCKHazirla";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCKHazirla";
            this.Load += new System.EventHandler(this.FormCKHazirla_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCkYazdir;
        private System.Windows.Forms.Label lblSinavNo;
        private System.Windows.Forms.Label lblSinavAdi;
        private System.ComponentModel.BackgroundWorker bgwCkOlustur;
        private System.Windows.Forms.ComboBox cbOturum;
        private System.Windows.Forms.ComboBox cbOkul;
        private System.Windows.Forms.ComboBox cbIlce;
        private System.Windows.Forms.ComboBox cbSube;
        private System.Windows.Forms.ComboBox cbSinif;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbOgrenciler;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox cbYazicilar;
        private System.Windows.Forms.CheckBox cbxDosyaOlustur;
        private System.Windows.Forms.Button button1;
    }
}