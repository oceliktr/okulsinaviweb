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
            this.dgvCKEksikOlanlar = new System.Windows.Forms.DataGridView();
            this.dgvHata = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kareKodKontrolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kareKoduDosyayıAçToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cevapKağıdınıAçToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.okunmayanlarıTaşıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwDizinSec = new System.ComponentModel.BackgroundWorker();
            this.bgwKarekodOcrKontrol = new System.ComponentModel.BackgroundWorker();
            this.bgwCKirp = new System.ComponentModel.BackgroundWorker();
            this.bgwOptikOkuma = new System.ComponentModel.BackgroundWorker();
            this.pbDizinSec = new System.Windows.Forms.PictureBox();
            this.pbCKKontrol = new System.Windows.Forms.PictureBox();
            this.pbAuKirpma = new System.Windows.Forms.PictureBox();
            this.pbAyarlar = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ayarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.karekodOluşturToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.karekodKontrolToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.excelİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cKOluşturToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbCKKonumAl = new System.Windows.Forms.PictureBox();
            this.pbOptikOkuma = new System.Windows.Forms.PictureBox();
            this.pbYardim = new System.Windows.Forms.PictureBox();
            this.pbBilgi = new System.Windows.Forms.PictureBox();
            this.pbCikis = new System.Windows.Forms.PictureBox();
            this.lblIlAdi = new System.Windows.Forms.Label();
            this.lblOdm = new System.Windows.Forms.Label();
            this.sınıfListesiOluşturToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCKEksikOlanlar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHata)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDizinSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCKKontrol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAuKirpma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAyarlar)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCKKonumAl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOptikOkuma)).BeginInit();
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
            this.progressBar1.Location = new System.Drawing.Point(27, 293);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1041, 30);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 0;
            // 
            // lblBilgi
            // 
            this.lblBilgi.BackColor = System.Drawing.Color.Transparent;
            this.lblBilgi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBilgi.Location = new System.Drawing.Point(19, 338);
            this.lblBilgi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(1049, 28);
            this.lblBilgi.TabIndex = 3;
            this.lblBilgi.Text = "Önce dizin seçiniz.";
            this.lblBilgi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvCKEksikOlanlar
            // 
            this.dgvCKEksikOlanlar.AllowUserToAddRows = false;
            this.dgvCKEksikOlanlar.AllowUserToDeleteRows = false;
            this.dgvCKEksikOlanlar.AllowUserToOrderColumns = true;
            this.dgvCKEksikOlanlar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCKEksikOlanlar.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvCKEksikOlanlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCKEksikOlanlar.Location = new System.Drawing.Point(0, 390);
            this.dgvCKEksikOlanlar.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCKEksikOlanlar.Name = "dgvCKEksikOlanlar";
            this.dgvCKEksikOlanlar.ReadOnly = true;
            this.dgvCKEksikOlanlar.RowHeadersVisible = false;
            this.dgvCKEksikOlanlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCKEksikOlanlar.Size = new System.Drawing.Size(1253, 338);
            this.dgvCKEksikOlanlar.TabIndex = 11;
            // 
            // dgvHata
            // 
            this.dgvHata.AllowUserToAddRows = false;
            this.dgvHata.AllowUserToDeleteRows = false;
            this.dgvHata.AllowUserToOrderColumns = true;
            this.dgvHata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHata.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvHata.Location = new System.Drawing.Point(1095, 15);
            this.dgvHata.Margin = new System.Windows.Forms.Padding(4);
            this.dgvHata.Name = "dgvHata";
            this.dgvHata.ReadOnly = true;
            this.dgvHata.RowHeadersVisible = false;
            this.dgvHata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHata.Size = new System.Drawing.Size(588, 272);
            this.dgvHata.TabIndex = 19;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kareKodKontrolToolStripMenuItem,
            this.kareKoduDosyayıAçToolStripMenuItem,
            this.cevapKağıdınıAçToolStripMenuItem,
            this.okunmayanlarıTaşıToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(221, 100);
            // 
            // kareKodKontrolToolStripMenuItem
            // 
            this.kareKodKontrolToolStripMenuItem.Name = "kareKodKontrolToolStripMenuItem";
            this.kareKodKontrolToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.kareKodKontrolToolStripMenuItem.Text = "KareKod Kontrol";
            this.kareKodKontrolToolStripMenuItem.Click += new System.EventHandler(this.kareKodKontrolToolStripMenuItem_Click);
            // 
            // kareKoduDosyayıAçToolStripMenuItem
            // 
            this.kareKoduDosyayıAçToolStripMenuItem.Name = "kareKoduDosyayıAçToolStripMenuItem";
            this.kareKoduDosyayıAçToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.kareKoduDosyayıAçToolStripMenuItem.Text = "KareKodu Dosyayı Aç";
            // 
            // cevapKağıdınıAçToolStripMenuItem
            // 
            this.cevapKağıdınıAçToolStripMenuItem.Name = "cevapKağıdınıAçToolStripMenuItem";
            this.cevapKağıdınıAçToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.cevapKağıdınıAçToolStripMenuItem.Text = "Cevap Kağıdını Aç";
            // 
            // okunmayanlarıTaşıToolStripMenuItem
            // 
            this.okunmayanlarıTaşıToolStripMenuItem.Name = "okunmayanlarıTaşıToolStripMenuItem";
            this.okunmayanlarıTaşıToolStripMenuItem.Size = new System.Drawing.Size(220, 24);
            this.okunmayanlarıTaşıToolStripMenuItem.Text = "Okunmayanları Taşı";
            this.okunmayanlarıTaşıToolStripMenuItem.Click += new System.EventHandler(this.okunmayanlariTasiToolStripMenuItem_Click);
            // 
            // bgwDizinSec
            // 
            this.bgwDizinSec.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDizinSec_DoWork);
            // 
            // bgwKarekodOcrKontrol
            // 
            this.bgwKarekodOcrKontrol.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwKarekodOcrKontrol_DoWork);
            // 
            // bgwCKirp
            // 
            this.bgwCKirp.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCKirp_DoWork);
            // 
            // bgwOptikOkuma
            // 
            this.bgwOptikOkuma.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwOptikOkuma_DoWork);
            // 
            // pbDizinSec
            // 
            this.pbDizinSec.BackColor = System.Drawing.Color.Transparent;
            this.pbDizinSec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDizinSec.Image = global::ODM.Properties.Resources.dizin_sec1;
            this.pbDizinSec.Location = new System.Drawing.Point(23, 110);
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
            this.pbCKKontrol.Location = new System.Drawing.Point(329, 110);
            this.pbCKKontrol.Margin = new System.Windows.Forms.Padding(4);
            this.pbCKKontrol.Name = "pbCKKontrol";
            this.pbCKKontrol.Size = new System.Drawing.Size(133, 148);
            this.pbCKKontrol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCKKontrol.TabIndex = 27;
            this.pbCKKontrol.TabStop = false;
            this.pbCKKontrol.Click += new System.EventHandler(this.pbCKKontrol_Click);
            // 
            // pbAuKirpma
            // 
            this.pbAuKirpma.BackColor = System.Drawing.Color.Transparent;
            this.pbAuKirpma.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbAuKirpma.Image = global::ODM.Properties.Resources.au_kirpma;
            this.pbAuKirpma.Location = new System.Drawing.Point(636, 110);
            this.pbAuKirpma.Margin = new System.Windows.Forms.Padding(4);
            this.pbAuKirpma.Name = "pbAuKirpma";
            this.pbAuKirpma.Size = new System.Drawing.Size(133, 148);
            this.pbAuKirpma.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAuKirpma.TabIndex = 28;
            this.pbAuKirpma.TabStop = false;
            this.pbAuKirpma.Click += new System.EventHandler(this.pbAuKirpma_Click);
            // 
            // pbAyarlar
            // 
            this.pbAyarlar.BackColor = System.Drawing.Color.Transparent;
            this.pbAyarlar.ContextMenuStrip = this.contextMenuStrip2;
            this.pbAyarlar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbAyarlar.Image = global::ODM.Properties.Resources.ayarlar;
            this.pbAyarlar.Location = new System.Drawing.Point(787, 107);
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
            this.sınıfListesiOluşturToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(203, 176);
            // 
            // ayarlarToolStripMenuItem
            // 
            this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            this.ayarlarToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.ayarlarToolStripMenuItem.Text = "Ayarlar";
            this.ayarlarToolStripMenuItem.Click += new System.EventHandler(this.ayarlarToolStripMenuItem_Click);
            // 
            // karekodOluşturToolStripMenuItem
            // 
            this.karekodOluşturToolStripMenuItem.Name = "karekodOluşturToolStripMenuItem";
            this.karekodOluşturToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.karekodOluşturToolStripMenuItem.Text = "Karekod Oluştur";
            this.karekodOluşturToolStripMenuItem.Click += new System.EventHandler(this.karekodOluşturToolStripMenuItem_Click);
            // 
            // karekodKontrolToolStripMenuItem1
            // 
            this.karekodKontrolToolStripMenuItem1.Name = "karekodKontrolToolStripMenuItem1";
            this.karekodKontrolToolStripMenuItem1.Size = new System.Drawing.Size(202, 24);
            this.karekodKontrolToolStripMenuItem1.Text = "Karekod Kontrol";
            this.karekodKontrolToolStripMenuItem1.Click += new System.EventHandler(this.karekodKontrolToolStripMenuItem1_Click);
            // 
            // excelİşlemleriToolStripMenuItem
            // 
            this.excelİşlemleriToolStripMenuItem.Name = "excelİşlemleriToolStripMenuItem";
            this.excelİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.excelİşlemleriToolStripMenuItem.Text = "Excel İşlemleri";
            this.excelİşlemleriToolStripMenuItem.Click += new System.EventHandler(this.excelİşlemleriToolStripMenuItem_Click);
            // 
            // cKOluşturToolStripMenuItem
            // 
            this.cKOluşturToolStripMenuItem.Name = "cKOluşturToolStripMenuItem";
            this.cKOluşturToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.cKOluşturToolStripMenuItem.Text = "CK Oluştur";
            this.cKOluşturToolStripMenuItem.Click += new System.EventHandler(this.cKOluşturToolStripMenuItem_Click);
            // 
            // pbCKKonumAl
            // 
            this.pbCKKonumAl.BackColor = System.Drawing.Color.Transparent;
            this.pbCKKonumAl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCKKonumAl.Image = global::ODM.Properties.Resources.cevap_kagidi_konum;
            this.pbCKKonumAl.Location = new System.Drawing.Point(176, 110);
            this.pbCKKonumAl.Margin = new System.Windows.Forms.Padding(4);
            this.pbCKKonumAl.Name = "pbCKKonumAl";
            this.pbCKKonumAl.Size = new System.Drawing.Size(133, 148);
            this.pbCKKonumAl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCKKonumAl.TabIndex = 30;
            this.pbCKKonumAl.TabStop = false;
            this.pbCKKonumAl.Click += new System.EventHandler(this.pbCKKonumAl_Click);
            // 
            // pbOptikOkuma
            // 
            this.pbOptikOkuma.BackColor = System.Drawing.Color.Transparent;
            this.pbOptikOkuma.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbOptikOkuma.Image = global::ODM.Properties.Resources.optik_okuma;
            this.pbOptikOkuma.Location = new System.Drawing.Point(483, 110);
            this.pbOptikOkuma.Margin = new System.Windows.Forms.Padding(4);
            this.pbOptikOkuma.Name = "pbOptikOkuma";
            this.pbOptikOkuma.Size = new System.Drawing.Size(133, 148);
            this.pbOptikOkuma.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOptikOkuma.TabIndex = 31;
            this.pbOptikOkuma.TabStop = false;
            this.pbOptikOkuma.Click += new System.EventHandler(this.pbOptikOkuma_Click);
            // 
            // pbYardim
            // 
            this.pbYardim.BackColor = System.Drawing.Color.Transparent;
            this.pbYardim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbYardim.Image = global::ODM.Properties.Resources.yardim;
            this.pbYardim.Location = new System.Drawing.Point(935, 110);
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
            this.pbBilgi.Location = new System.Drawing.Point(935, 160);
            this.pbBilgi.Margin = new System.Windows.Forms.Padding(4);
            this.pbBilgi.Name = "pbBilgi";
            this.pbBilgi.Size = new System.Drawing.Size(133, 47);
            this.pbBilgi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBilgi.TabIndex = 34;
            this.pbBilgi.TabStop = false;
            // 
            // pbCikis
            // 
            this.pbCikis.BackColor = System.Drawing.Color.Transparent;
            this.pbCikis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCikis.Image = global::ODM.Properties.Resources.cikis;
            this.pbCikis.Location = new System.Drawing.Point(935, 210);
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
            this.lblIlAdi.Location = new System.Drawing.Point(23, 11);
            this.lblIlAdi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIlAdi.Name = "lblIlAdi";
            this.lblIlAdi.Size = new System.Drawing.Size(1045, 49);
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
            this.lblOdm.Location = new System.Drawing.Point(31, 60);
            this.lblOdm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOdm.Name = "lblOdm";
            this.lblOdm.Size = new System.Drawing.Size(1037, 43);
            this.lblOdm.TabIndex = 37;
            this.lblOdm.Text = "ÖLÇME DEĞERLENDİRME MERKEZİ";
            this.lblOdm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOdm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblOdm_MouseDown);
            this.lblOdm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblOdm_MouseMove);
            this.lblOdm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblOdm_MouseUp);
            // 
            // sınıfListesiOluşturToolStripMenuItem
            // 
            this.sınıfListesiOluşturToolStripMenuItem.Name = "sınıfListesiOluşturToolStripMenuItem";
            this.sınıfListesiOluşturToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.sınıfListesiOluşturToolStripMenuItem.Text = "Sınıf Listesi Oluştur";
            this.sınıfListesiOluşturToolStripMenuItem.Click += new System.EventHandler(this.sınıfListesiOluşturToolStripMenuItem_Click);
            // 
            // FormGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1719, 734);
            this.Controls.Add(this.lblOdm);
            this.Controls.Add(this.lblIlAdi);
            this.Controls.Add(this.pbCikis);
            this.Controls.Add(this.pbBilgi);
            this.Controls.Add(this.pbYardim);
            this.Controls.Add(this.pbOptikOkuma);
            this.Controls.Add(this.pbCKKonumAl);
            this.Controls.Add(this.pbAyarlar);
            this.Controls.Add(this.pbAuKirpma);
            this.Controls.Add(this.pbCKKontrol);
            this.Controls.Add(this.pbDizinSec);
            this.Controls.Add(this.dgvHata);
            this.Controls.Add(this.dgvCKEksikOlanlar);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvCKEksikOlanlar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHata)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDizinSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCKKontrol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAuKirpma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAyarlar)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCKKonumAl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOptikOkuma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbYardim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBilgi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCikis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.DataGridView dgvCKEksikOlanlar;
        private System.Windows.Forms.DataGridView dgvHata;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kareKodKontrolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kareKoduDosyayıAçToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cevapKağıdınıAçToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem okunmayanlarıTaşıToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgwDizinSec;
        private System.ComponentModel.BackgroundWorker bgwKarekodOcrKontrol;
        private System.ComponentModel.BackgroundWorker bgwCKirp;
        private System.ComponentModel.BackgroundWorker bgwOptikOkuma;
        private System.Windows.Forms.PictureBox pbDizinSec;
        private System.Windows.Forms.PictureBox pbCKKontrol;
        private System.Windows.Forms.PictureBox pbAuKirpma;
        private System.Windows.Forms.PictureBox pbAyarlar;
        private System.Windows.Forms.PictureBox pbCKKonumAl;
        private System.Windows.Forms.PictureBox pbOptikOkuma;
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
    }
}