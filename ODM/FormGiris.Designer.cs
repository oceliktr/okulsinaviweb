namespace ODM
{
    partial class FormGiris
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGiris));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.bgwDizinSec = new System.ComponentModel.BackgroundWorker();
            this.pbDizinSec = new System.Windows.Forms.PictureBox();
            this.pbCKKontrol = new System.Windows.Forms.PictureBox();
            this.pbAyarlar = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ayarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.karekodOluşturToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.karekodKontrolToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.excelİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cKOluşturToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cKKontrolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sınıfListesiOluşturToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optikFormCevaplarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textDosyasıAlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formKarneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.branşlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilçelerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formRaporToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kütükİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbCKKonumAl = new System.Windows.Forms.PictureBox();
            this.pbYardim = new System.Windows.Forms.PictureBox();
            this.pbBilgi = new System.Windows.Forms.PictureBox();
            this.pbCikis = new System.Windows.Forms.PictureBox();
            this.lblIlAdi = new System.Windows.Forms.Label();
            this.lblOdm = new System.Windows.Forms.Label();
            this.cbCkKontrolDevamEt = new System.Windows.Forms.CheckBox();
            this.cbBilgisayariKapat = new System.Windows.Forms.CheckBox();
            this.lblGecenSure = new System.Windows.Forms.Label();
            this.bgvCkKontrol = new System.ComponentModel.BackgroundWorker();
            this.lblBitisSuresi = new System.Windows.Forms.Label();
            this.öğrenciBilgiDüzenleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pbDizinSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCKKontrol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAyarlar)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCKKonumAl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbYardim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBilgi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCikis)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Blue;
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressBar1.ForeColor = System.Drawing.Color.GreenYellow;
            this.progressBar1.Location = new System.Drawing.Point(27, 276);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(754, 30);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 0;
            // 
            // lblBilgi
            // 
            this.lblBilgi.BackColor = System.Drawing.Color.Transparent;
            this.lblBilgi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBilgi.Location = new System.Drawing.Point(24, 310);
            this.lblBilgi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(757, 47);
            this.lblBilgi.TabIndex = 3;
            this.lblBilgi.Text = "Önce dizin seçiniz.";
            this.lblBilgi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bgwDizinSec
            // 
            this.bgwDizinSec.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDizinSec_DoWork);
            // 
            // pbDizinSec
            // 
            this.pbDizinSec.BackColor = System.Drawing.Color.Transparent;
            this.pbDizinSec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDizinSec.Image = global::ODM.Properties.Resources.dizin_sec1;
            this.pbDizinSec.Location = new System.Drawing.Point(27, 105);
            this.pbDizinSec.Margin = new System.Windows.Forms.Padding(4);
            this.pbDizinSec.Name = "pbDizinSec";
            this.pbDizinSec.Size = new System.Drawing.Size(133, 148);
            this.pbDizinSec.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDizinSec.TabIndex = 26;
            this.pbDizinSec.TabStop = false;
            this.pbDizinSec.Click += new System.EventHandler(this.pbDizinSec_Click);
            // 
            // pbCKKontrol
            // 
            this.pbCKKontrol.BackColor = System.Drawing.Color.Transparent;
            this.pbCKKontrol.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCKKontrol.Image = global::ODM.Properties.Resources.ck_kontrol;
            this.pbCKKontrol.Location = new System.Drawing.Point(333, 105);
            this.pbCKKontrol.Margin = new System.Windows.Forms.Padding(4);
            this.pbCKKontrol.Name = "pbCKKontrol";
            this.pbCKKontrol.Size = new System.Drawing.Size(133, 148);
            this.pbCKKontrol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCKKontrol.TabIndex = 27;
            this.pbCKKontrol.TabStop = false;
            this.pbCKKontrol.Click += new System.EventHandler(this.pbCKKontrol_Click);
            // 
            // pbAyarlar
            // 
            this.pbAyarlar.BackColor = System.Drawing.Color.Transparent;
            this.pbAyarlar.ContextMenuStrip = this.contextMenuStrip2;
            this.pbAyarlar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbAyarlar.Image = global::ODM.Properties.Resources.ayarlar;
            this.pbAyarlar.Location = new System.Drawing.Point(490, 105);
            this.pbAyarlar.Margin = new System.Windows.Forms.Padding(4);
            this.pbAyarlar.Name = "pbAyarlar";
            this.pbAyarlar.Size = new System.Drawing.Size(133, 148);
            this.pbAyarlar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAyarlar.TabIndex = 29;
            this.pbAyarlar.TabStop = false;
            this.pbAyarlar.Click += new System.EventHandler(this.pbAyarlar_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ayarlarToolStripMenuItem,
            this.karekodOluşturToolStripMenuItem,
            this.karekodKontrolToolStripMenuItem1,
            this.excelİşlemleriToolStripMenuItem,
            this.cKOluşturToolStripMenuItem,
            this.cKKontrolToolStripMenuItem,
            this.sınıfListesiOluşturToolStripMenuItem,
            this.optikFormCevaplarToolStripMenuItem,
            this.textDosyasıAlToolStripMenuItem,
            this.formKarneToolStripMenuItem,
            this.toolStripMenuItem1,
            this.branşlarToolStripMenuItem,
            this.ilçelerToolStripMenuItem,
            this.formRaporToolStripMenuItem,
            this.kütükİşlemleriToolStripMenuItem,
            this.öğrenciBilgiDüzenleToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(223, 398);
            // 
            // ayarlarToolStripMenuItem
            // 
            this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            this.ayarlarToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.ayarlarToolStripMenuItem.Text = "Ayarlar";
            this.ayarlarToolStripMenuItem.Click += new System.EventHandler(this.ayarlarToolStripMenuItem_Click);
            // 
            // karekodOluşturToolStripMenuItem
            // 
            this.karekodOluşturToolStripMenuItem.Name = "karekodOluşturToolStripMenuItem";
            this.karekodOluşturToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.karekodOluşturToolStripMenuItem.Text = "Karekod Oluştur";
            this.karekodOluşturToolStripMenuItem.Click += new System.EventHandler(this.karekodOluşturToolStripMenuItem_Click);
            // 
            // karekodKontrolToolStripMenuItem1
            // 
            this.karekodKontrolToolStripMenuItem1.Name = "karekodKontrolToolStripMenuItem1";
            this.karekodKontrolToolStripMenuItem1.Size = new System.Drawing.Size(222, 24);
            this.karekodKontrolToolStripMenuItem1.Text = "Karekod Kontrol";
            this.karekodKontrolToolStripMenuItem1.Click += new System.EventHandler(this.karekodKontrolToolStripMenuItem1_Click);
            // 
            // excelİşlemleriToolStripMenuItem
            // 
            this.excelİşlemleriToolStripMenuItem.Name = "excelİşlemleriToolStripMenuItem";
            this.excelİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.excelİşlemleriToolStripMenuItem.Text = "Excel İşlemleri";
            this.excelİşlemleriToolStripMenuItem.Click += new System.EventHandler(this.excelİşlemleriToolStripMenuItem_Click);
            // 
            // cKOluşturToolStripMenuItem
            // 
            this.cKOluşturToolStripMenuItem.Name = "cKOluşturToolStripMenuItem";
            this.cKOluşturToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.cKOluşturToolStripMenuItem.Text = "CK Oluştur";
            this.cKOluşturToolStripMenuItem.Click += new System.EventHandler(this.cKOluşturToolStripMenuItem_Click);
            // 
            // cKKontrolToolStripMenuItem
            // 
            this.cKKontrolToolStripMenuItem.Name = "cKKontrolToolStripMenuItem";
            this.cKKontrolToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.cKKontrolToolStripMenuItem.Text = "CK Kontrol";
            // 
            // sınıfListesiOluşturToolStripMenuItem
            // 
            this.sınıfListesiOluşturToolStripMenuItem.Name = "sınıfListesiOluşturToolStripMenuItem";
            this.sınıfListesiOluşturToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.sınıfListesiOluşturToolStripMenuItem.Text = "Sınıf Listesi Oluştur";
            this.sınıfListesiOluşturToolStripMenuItem.Click += new System.EventHandler(this.sınıfListesiOluşturToolStripMenuItem_Click);
            // 
            // optikFormCevaplarToolStripMenuItem
            // 
            this.optikFormCevaplarToolStripMenuItem.Name = "optikFormCevaplarToolStripMenuItem";
            this.optikFormCevaplarToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.optikFormCevaplarToolStripMenuItem.Text = "Optik Form Cevaplar";
            this.optikFormCevaplarToolStripMenuItem.Click += new System.EventHandler(this.optikFormCevaplarToolStripMenuItem_Click);
            // 
            // textDosyasıAlToolStripMenuItem
            // 
            this.textDosyasıAlToolStripMenuItem.Name = "textDosyasıAlToolStripMenuItem";
            this.textDosyasıAlToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.textDosyasıAlToolStripMenuItem.Text = "Text Dosyası Al";
            this.textDosyasıAlToolStripMenuItem.Click += new System.EventHandler(this.textDosyasıAlToolStripMenuItem_Click);
            // 
            // formKarneToolStripMenuItem
            // 
            this.formKarneToolStripMenuItem.Name = "formKarneToolStripMenuItem";
            this.formKarneToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.formKarneToolStripMenuItem.Text = "Form Karne";
            this.formKarneToolStripMenuItem.Click += new System.EventHandler(this.formKarneToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(219, 6);
            // 
            // branşlarToolStripMenuItem
            // 
            this.branşlarToolStripMenuItem.Name = "branşlarToolStripMenuItem";
            this.branşlarToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.branşlarToolStripMenuItem.Text = "Branşlar";
            this.branşlarToolStripMenuItem.Click += new System.EventHandler(this.branşlarToolStripMenuItem_Click);
            // 
            // ilçelerToolStripMenuItem
            // 
            this.ilçelerToolStripMenuItem.Name = "ilçelerToolStripMenuItem";
            this.ilçelerToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.ilçelerToolStripMenuItem.Text = "İlçeler";
            this.ilçelerToolStripMenuItem.Click += new System.EventHandler(this.ilçelerToolStripMenuItem_Click);
            // 
            // formRaporToolStripMenuItem
            // 
            this.formRaporToolStripMenuItem.Name = "formRaporToolStripMenuItem";
            this.formRaporToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.formRaporToolStripMenuItem.Text = "Form Rapor";
            this.formRaporToolStripMenuItem.Click += new System.EventHandler(this.formRaporToolStripMenuItem_Click);
            // 
            // kütükİşlemleriToolStripMenuItem
            // 
            this.kütükİşlemleriToolStripMenuItem.Name = "kütükİşlemleriToolStripMenuItem";
            this.kütükİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.kütükİşlemleriToolStripMenuItem.Text = "Kütük İşlemleri";
            this.kütükİşlemleriToolStripMenuItem.Click += new System.EventHandler(this.kütükİşlemleriToolStripMenuItem_Click);
            // 
            // pbCKKonumAl
            // 
            this.pbCKKonumAl.BackColor = System.Drawing.Color.Transparent;
            this.pbCKKonumAl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCKKonumAl.Image = global::ODM.Properties.Resources.cevap_kagidi_konum;
            this.pbCKKonumAl.Location = new System.Drawing.Point(180, 105);
            this.pbCKKonumAl.Margin = new System.Windows.Forms.Padding(4);
            this.pbCKKonumAl.Name = "pbCKKonumAl";
            this.pbCKKonumAl.Size = new System.Drawing.Size(133, 148);
            this.pbCKKonumAl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCKKonumAl.TabIndex = 30;
            this.pbCKKonumAl.TabStop = false;
            this.pbCKKonumAl.Click += new System.EventHandler(this.pbCKKonumAl_Click);
            // 
            // pbYardim
            // 
            this.pbYardim.BackColor = System.Drawing.Color.Transparent;
            this.pbYardim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbYardim.Image = global::ODM.Properties.Resources.yardim;
            this.pbYardim.Location = new System.Drawing.Point(648, 106);
            this.pbYardim.Margin = new System.Windows.Forms.Padding(4);
            this.pbYardim.Name = "pbYardim";
            this.pbYardim.Size = new System.Drawing.Size(133, 47);
            this.pbYardim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbYardim.TabIndex = 33;
            this.pbYardim.TabStop = false;
            // 
            // pbBilgi
            // 
            this.pbBilgi.BackColor = System.Drawing.Color.Transparent;
            this.pbBilgi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbBilgi.Image = global::ODM.Properties.Resources.bilgi;
            this.pbBilgi.Location = new System.Drawing.Point(648, 156);
            this.pbBilgi.Margin = new System.Windows.Forms.Padding(4);
            this.pbBilgi.Name = "pbBilgi";
            this.pbBilgi.Size = new System.Drawing.Size(133, 47);
            this.pbBilgi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBilgi.TabIndex = 34;
            this.pbBilgi.TabStop = false;
            this.pbBilgi.Click += new System.EventHandler(this.pbBilgi_Click);
            // 
            // pbCikis
            // 
            this.pbCikis.BackColor = System.Drawing.Color.Transparent;
            this.pbCikis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCikis.Image = global::ODM.Properties.Resources.cikis;
            this.pbCikis.Location = new System.Drawing.Point(648, 206);
            this.pbCikis.Margin = new System.Windows.Forms.Padding(4);
            this.pbCikis.Name = "pbCikis";
            this.pbCikis.Size = new System.Drawing.Size(133, 47);
            this.pbCikis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCikis.TabIndex = 35;
            this.pbCikis.TabStop = false;
            this.pbCikis.Click += new System.EventHandler(this.pbCikis_Click);
            // 
            // lblIlAdi
            // 
            this.lblIlAdi.BackColor = System.Drawing.Color.Transparent;
            this.lblIlAdi.Font = new System.Drawing.Font("Trajan Pro", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblIlAdi.ForeColor = System.Drawing.Color.Black;
            this.lblIlAdi.Location = new System.Drawing.Point(27, 8);
            this.lblIlAdi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIlAdi.Name = "lblIlAdi";
            this.lblIlAdi.Size = new System.Drawing.Size(754, 49);
            this.lblIlAdi.TabIndex = 36;
            this.lblIlAdi.Text = "ERZURUM";
            this.lblIlAdi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIlAdi.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblIlAdi_MouseDown);
            this.lblIlAdi.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblIlAdi_MouseMove);
            this.lblIlAdi.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblIlAdi_MouseUp);
            // 
            // lblOdm
            // 
            this.lblOdm.BackColor = System.Drawing.Color.Transparent;
            this.lblOdm.Font = new System.Drawing.Font("Trajan Pro", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOdm.Location = new System.Drawing.Point(27, 57);
            this.lblOdm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOdm.Name = "lblOdm";
            this.lblOdm.Size = new System.Drawing.Size(754, 43);
            this.lblOdm.TabIndex = 37;
            this.lblOdm.Text = "ÖLÇME DEĞERLENDİRME MERKEZİ";
            this.lblOdm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOdm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblOdm_MouseDown);
            this.lblOdm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblOdm_MouseMove);
            this.lblOdm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblOdm_MouseUp);
            // 
            // cbCkKontrolDevamEt
            // 
            this.cbCkKontrolDevamEt.AutoSize = true;
            this.cbCkKontrolDevamEt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCkKontrolDevamEt.Location = new System.Drawing.Point(27, 256);
            this.cbCkKontrolDevamEt.Name = "cbCkKontrolDevamEt";
            this.cbCkKontrolDevamEt.Size = new System.Drawing.Size(367, 21);
            this.cbCkKontrolDevamEt.TabIndex = 38;
            this.cbCkKontrolDevamEt.Text = "Dizin seç işlemleri bitince CK Kontrol işlemlerine başla";
            this.cbCkKontrolDevamEt.UseVisualStyleBackColor = true;
            this.cbCkKontrolDevamEt.CheckedChanged += new System.EventHandler(this.cbCkKontrolDevamEt_CheckedChanged);
            // 
            // cbBilgisayariKapat
            // 
            this.cbBilgisayariKapat.AutoSize = true;
            this.cbBilgisayariKapat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbBilgisayariKapat.Location = new System.Drawing.Point(490, 256);
            this.cbBilgisayariKapat.Name = "cbBilgisayariKapat";
            this.cbBilgisayariKapat.Size = new System.Drawing.Size(303, 21);
            this.cbBilgisayariKapat.TabIndex = 39;
            this.cbBilgisayariKapat.Text = "CK Kontrol işlemleri bitince bilgisayarı kapat";
            this.cbBilgisayariKapat.UseVisualStyleBackColor = true;
            // 
            // lblGecenSure
            // 
            this.lblGecenSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGecenSure.BackColor = System.Drawing.Color.Transparent;
            this.lblGecenSure.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGecenSure.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGecenSure.Location = new System.Drawing.Point(490, 364);
            this.lblGecenSure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGecenSure.Name = "lblGecenSure";
            this.lblGecenSure.Size = new System.Drawing.Size(291, 18);
            this.lblGecenSure.TabIndex = 41;
            this.lblGecenSure.Text = "0 saat, 0 dakika, 0 saniye";
            this.lblGecenSure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bgvCkKontrol
            // 
            this.bgvCkKontrol.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgvCkKontrol_DoWork);
            // 
            // lblBitisSuresi
            // 
            this.lblBitisSuresi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBitisSuresi.BackColor = System.Drawing.Color.Transparent;
            this.lblBitisSuresi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBitisSuresi.Location = new System.Drawing.Point(24, 357);
            this.lblBitisSuresi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBitisSuresi.Name = "lblBitisSuresi";
            this.lblBitisSuresi.Size = new System.Drawing.Size(485, 25);
            this.lblBitisSuresi.TabIndex = 44;
            this.lblBitisSuresi.Text = "0 saat, 0 dakika, 0 saniye";
            // 
            // öğrenciBilgiDüzenleToolStripMenuItem
            // 
            this.öğrenciBilgiDüzenleToolStripMenuItem.Name = "öğrenciBilgiDüzenleToolStripMenuItem";
            this.öğrenciBilgiDüzenleToolStripMenuItem.Size = new System.Drawing.Size(222, 24);
            this.öğrenciBilgiDüzenleToolStripMenuItem.Text = "Öğrenci Bilgi Düzenle";
            // 
            // FormGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(796, 403);
            this.Controls.Add(this.lblBitisSuresi);
            this.Controls.Add(this.lblGecenSure);
            this.Controls.Add(this.cbBilgisayariKapat);
            this.Controls.Add(this.cbCkKontrolDevamEt);
            this.Controls.Add(this.lblOdm);
            this.Controls.Add(this.lblIlAdi);
            this.Controls.Add(this.pbCikis);
            this.Controls.Add(this.pbBilgi);
            this.Controls.Add(this.pbYardim);
            this.Controls.Add(this.pbCKKonumAl);
            this.Controls.Add(this.pbAyarlar);
            this.Controls.Add(this.pbCKKontrol);
            this.Controls.Add(this.pbDizinSec);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGiris";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Erzurum Ölçme ve Değerlendirme Merkezi";
            this.Activated += new System.EventHandler(this.FormCKIslemleri_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGiris_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGiris_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormGiris_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormGiris_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbDizinSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCKKontrol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAyarlar)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCKKonumAl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbYardim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBilgi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCikis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblBilgi;
        private System.ComponentModel.BackgroundWorker bgwDizinSec;
        private System.Windows.Forms.PictureBox pbDizinSec;
        private System.Windows.Forms.PictureBox pbCKKontrol;
        private System.Windows.Forms.PictureBox pbAyarlar;
        private System.Windows.Forms.PictureBox pbCKKonumAl;
        private System.Windows.Forms.PictureBox pbYardim;
        private System.Windows.Forms.PictureBox pbBilgi;
        private System.Windows.Forms.PictureBox pbCikis;
        private System.Windows.Forms.Label lblIlAdi;
        private System.Windows.Forms.Label lblOdm;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem ayarlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem karekodOluşturToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem karekodKontrolToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem excelİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cKOluşturToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sınıfListesiOluşturToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optikFormCevaplarToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbCkKontrolDevamEt;
        private System.Windows.Forms.CheckBox cbBilgisayariKapat;
        private System.Windows.Forms.ToolStripMenuItem textDosyasıAlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formKarneToolStripMenuItem;
        private System.Windows.Forms.Label lblGecenSure;
        private System.ComponentModel.BackgroundWorker bgvCkKontrol;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem branşlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ilçelerToolStripMenuItem;
        private System.Windows.Forms.Label lblBitisSuresi;
        private System.Windows.Forms.ToolStripMenuItem cKKontrolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formRaporToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kütükİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem öğrenciBilgiDüzenleToolStripMenuItem;
    }
}