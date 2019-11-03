namespace ODM
{
    partial class FormKareKodOlustur
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKareKodOlustur));
            this.lblSinavAdi = new System.Windows.Forms.Label();
            this.cboCorrectionLevel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEncode = new System.Windows.Forms.Button();
            this.cbSayfaSayisi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbQrCode = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblSinavNo = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pbQrCode)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSinavAdi
            // 
            this.lblSinavAdi.AutoSize = true;
            this.lblSinavAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSinavAdi.Location = new System.Drawing.Point(8, 9);
            this.lblSinavAdi.Name = "lblSinavAdi";
            this.lblSinavAdi.Size = new System.Drawing.Size(46, 17);
            this.lblSinavAdi.TabIndex = 0;
            this.lblSinavAdi.Text = "label1";
            // 
            // cboCorrectionLevel
            // 
            this.cboCorrectionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCorrectionLevel.FormattingEnabled = true;
            this.cboCorrectionLevel.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.cboCorrectionLevel.Location = new System.Drawing.Point(151, 75);
            this.cboCorrectionLevel.Name = "cboCorrectionLevel";
            this.cboCorrectionLevel.Size = new System.Drawing.Size(162, 21);
            this.cboCorrectionLevel.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Düzeltme Seviyesi :";
            // 
            // btnEncode
            // 
            this.btnEncode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEncode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnEncode.Location = new System.Drawing.Point(319, 75);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(115, 53);
            this.btnEncode.TabIndex = 11;
            this.btnEncode.Text = "Karekod Oluştur";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // cbSayfaSayisi
            // 
            this.cbSayfaSayisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSayfaSayisi.FormattingEnabled = true;
            this.cbSayfaSayisi.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cbSayfaSayisi.Location = new System.Drawing.Point(151, 107);
            this.cbSayfaSayisi.Name = "cbSayfaSayisi";
            this.cbSayfaSayisi.Size = new System.Drawing.Size(162, 21);
            this.cbSayfaSayisi.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Cevap Kağıdı Sayfa Sayısı :";
            // 
            // pbQrCode
            // 
            this.pbQrCode.Location = new System.Drawing.Point(12, 163);
            this.pbQrCode.Name = "pbQrCode";
            this.pbQrCode.Size = new System.Drawing.Size(88, 83);
            this.pbQrCode.TabIndex = 23;
            this.pbQrCode.TabStop = false;
            this.pbQrCode.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(11, 134);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(423, 23);
            this.progressBar1.TabIndex = 24;
            // 
            // lblSinavNo
            // 
            this.lblSinavNo.AutoSize = true;
            this.lblSinavNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSinavNo.Location = new System.Drawing.Point(8, 38);
            this.lblSinavNo.Name = "lblSinavNo";
            this.lblSinavNo.Size = new System.Drawing.Size(46, 17);
            this.lblSinavNo.TabIndex = 25;
            this.lblSinavNo.Text = "label1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // FormKareKodOlustur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 176);
            this.Controls.Add(this.lblSinavNo);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pbQrCode);
            this.Controls.Add(this.cbSayfaSayisi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboCorrectionLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnEncode);
            this.Controls.Add(this.lblSinavAdi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKareKodOlustur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KareKod Oluştur - Erzurum Ölçme ve Değerlendirme Merkezi";
            this.Activated += new System.EventHandler(this.FormKareKodKontrol_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKareKodOlustur_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbQrCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSinavAdi;
        private System.Windows.Forms.ComboBox cboCorrectionLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.ComboBox cbSayfaSayisi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbQrCode;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblSinavNo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}