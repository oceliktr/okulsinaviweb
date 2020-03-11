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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCkOlustur));
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.şablonSeçToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BgwCkOlustur = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolSslKalanSure = new System.Windows.Forms.ToolStripStatusLabel();
            this.BgwSinifListesi = new System.ComponentModel.BackgroundWorker();
            this.btnDosyaAdresleri = new System.Windows.Forms.Button();
            this.btnKutukCek = new System.Windows.Forms.Button();
            this.btnCkPdfOlustur = new System.Windows.Forms.Button();
            this.btnCkYazdir = new System.Windows.Forms.Button();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ndOgrBilgiX = new System.Windows.Forms.NumericUpDown();
            this.ndOgrBilgiY = new System.Windows.Forms.NumericUpDown();
            this.ndOgrBilgiH = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOrnekGoster = new System.Windows.Forms.Button();
            this.ndBubbleW = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.ndBubbleH = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.ndBubbleX = new System.Windows.Forms.NumericUpDown();
            this.ndBubbleArtim = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.ndBubbleY = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCkA5Dosyasi)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndOgrBilgiX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndOgrBilgiY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndOgrBilgiH)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleArtim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleY)).BeginInit();
            this.SuspendLayout();
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
            this.btnSinifListesi.Location = new System.Drawing.Point(528, 80);
            this.btnSinifListesi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSinifListesi.Name = "btnSinifListesi";
            this.btnSinifListesi.Size = new System.Drawing.Size(175, 28);
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
            this.cbSube.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbSube.FormattingEnabled = true;
            this.cbSube.Location = new System.Drawing.Point(262, 122);
            this.cbSube.Margin = new System.Windows.Forms.Padding(4);
            this.cbSube.Name = "cbSube";
            this.cbSube.Size = new System.Drawing.Size(260, 33);
            this.cbSube.TabIndex = 182;
            this.cbSube.SelectedIndexChanged += new System.EventHandler(this.CbSube_SelectedIndexChanged);
            // 
            // cbSinifi
            // 
            this.cbSinifi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSinifi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSinifi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbSinifi.FormattingEnabled = true;
            this.cbSinifi.Location = new System.Drawing.Point(132, 122);
            this.cbSinifi.Margin = new System.Windows.Forms.Padding(4);
            this.cbSinifi.Name = "cbSinifi";
            this.cbSinifi.Size = new System.Drawing.Size(121, 33);
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
            this.cbOgrenciler.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbOgrenciler.FormattingEnabled = true;
            this.cbOgrenciler.Location = new System.Drawing.Point(132, 177);
            this.cbOgrenciler.Margin = new System.Windows.Forms.Padding(4);
            this.cbOgrenciler.Name = "cbOgrenciler";
            this.cbOgrenciler.Size = new System.Drawing.Size(389, 33);
            this.cbOgrenciler.TabIndex = 177;
            this.cbOgrenciler.SelectedIndexChanged += new System.EventHandler(this.CbOgrenciler_SelectedIndexChanged);
            // 
            // cbOkullar
            // 
            this.cbOkullar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbOkullar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOkullar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbOkullar.FormattingEnabled = true;
            this.cbOkullar.Location = new System.Drawing.Point(132, 67);
            this.cbOkullar.Margin = new System.Windows.Forms.Padding(4);
            this.cbOkullar.Name = "cbOkullar";
            this.cbOkullar.Size = new System.Drawing.Size(389, 33);
            this.cbOkullar.TabIndex = 176;
            this.cbOkullar.SelectedIndexChanged += new System.EventHandler(this.CbOkullar_SelectedIndexChanged);
            // 
            // cbIlceler
            // 
            this.cbIlceler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbIlceler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIlceler.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbIlceler.FormattingEnabled = true;
            this.cbIlceler.Location = new System.Drawing.Point(132, 13);
            this.cbIlceler.Margin = new System.Windows.Forms.Padding(4);
            this.cbIlceler.Name = "cbIlceler";
            this.cbIlceler.Size = new System.Drawing.Size(389, 33);
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
            this.btnCKDosyaOlustur.Size = new System.Drawing.Size(175, 28);
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
            this.pbCkA5Dosyasi.ContextMenuStrip = this.contextMenuStrip1;
            this.pbCkA5Dosyasi.Location = new System.Drawing.Point(720, 13);
            this.pbCkA5Dosyasi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbCkA5Dosyasi.Name = "pbCkA5Dosyasi";
            this.pbCkA5Dosyasi.Size = new System.Drawing.Size(163, 206);
            this.pbCkA5Dosyasi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCkA5Dosyasi.TabIndex = 174;
            this.pbCkA5Dosyasi.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.şablonSeçToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 28);
            // 
            // şablonSeçToolStripMenuItem
            // 
            this.şablonSeçToolStripMenuItem.Name = "şablonSeçToolStripMenuItem";
            this.şablonSeçToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.şablonSeçToolStripMenuItem.Text = "Şablon Seç";
            this.şablonSeçToolStripMenuItem.Click += new System.EventHandler(this.şablonSeçToolStripMenuItem_Click);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 371);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1115, 26);
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
            // btnDosyaAdresleri
            // 
            this.btnDosyaAdresleri.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDosyaAdresleri.Location = new System.Drawing.Point(528, 177);
            this.btnDosyaAdresleri.Name = "btnDosyaAdresleri";
            this.btnDosyaAdresleri.Size = new System.Drawing.Size(175, 37);
            this.btnDosyaAdresleri.TabIndex = 188;
            this.btnDosyaAdresleri.Tag = "CK dosyalarının oluşturulduğu dizini seçerek ck dosya adreslerini txt dosyasında " +
    "açar";
            this.btnDosyaAdresleri.Text = "CK Dizin Listesi (txt)";
            this.btnDosyaAdresleri.UseVisualStyleBackColor = true;
            this.btnDosyaAdresleri.Click += new System.EventHandler(this.btnDosyaAdresleri_Click);
            // 
            // btnKutukCek
            // 
            this.btnKutukCek.Location = new System.Drawing.Point(720, 230);
            this.btnKutukCek.Name = "btnKutukCek";
            this.btnKutukCek.Size = new System.Drawing.Size(162, 23);
            this.btnKutukCek.TabIndex = 189;
            this.btnKutukCek.Text = "Verileri Al";
            this.btnKutukCek.UseVisualStyleBackColor = true;
            this.btnKutukCek.Click += new System.EventHandler(this.btnKutukCek_Click);
            // 
            // btnCkPdfOlustur
            // 
            this.btnCkPdfOlustur.Location = new System.Drawing.Point(528, 226);
            this.btnCkPdfOlustur.Name = "btnCkPdfOlustur";
            this.btnCkPdfOlustur.Size = new System.Drawing.Size(175, 36);
            this.btnCkPdfOlustur.TabIndex = 190;
            this.btnCkPdfOlustur.Text = "CK Okulştur Pdf";
            this.btnCkPdfOlustur.UseVisualStyleBackColor = true;
            this.btnCkPdfOlustur.Click += new System.EventHandler(this.btnCkPdfOlustur_Click);
            // 
            // btnCkYazdir
            // 
            this.btnCkYazdir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCkYazdir.Image = ((System.Drawing.Image)(resources.GetObject("btnCkYazdir.Image")));
            this.btnCkYazdir.Location = new System.Drawing.Point(528, 45);
            this.btnCkYazdir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCkYazdir.Name = "btnCkYazdir";
            this.btnCkYazdir.Size = new System.Drawing.Size(175, 28);
            this.btnCkYazdir.TabIndex = 191;
            this.btnCkYazdir.Text = "CK - Yazdır";
            this.btnCkYazdir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCkYazdir.UseVisualStyleBackColor = true;
            this.btnCkYazdir.Click += new System.EventHandler(this.btnCkYazdir_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 192;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 193;
            this.label2.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 17);
            this.label7.TabIndex = 194;
            this.label7.Text = "H";
            // 
            // ndOgrBilgiX
            // 
            this.ndOgrBilgiX.Location = new System.Drawing.Point(54, 42);
            this.ndOgrBilgiX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ndOgrBilgiX.Name = "ndOgrBilgiX";
            this.ndOgrBilgiX.Size = new System.Drawing.Size(63, 22);
            this.ndOgrBilgiX.TabIndex = 195;
            this.ndOgrBilgiX.Value = new decimal(new int[] {
            465,
            0,
            0,
            0});
            this.ndOgrBilgiX.ValueChanged += new System.EventHandler(this.ndOgrBilgiX_ValueChanged);
            // 
            // ndOgrBilgiY
            // 
            this.ndOgrBilgiY.Location = new System.Drawing.Point(125, 42);
            this.ndOgrBilgiY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ndOgrBilgiY.Name = "ndOgrBilgiY";
            this.ndOgrBilgiY.Size = new System.Drawing.Size(63, 22);
            this.ndOgrBilgiY.TabIndex = 196;
            this.ndOgrBilgiY.Value = new decimal(new int[] {
            330,
            0,
            0,
            0});
            this.ndOgrBilgiY.ValueChanged += new System.EventHandler(this.ndOgrBilgiY_ValueChanged);
            // 
            // ndOgrBilgiH
            // 
            this.ndOgrBilgiH.Location = new System.Drawing.Point(54, 70);
            this.ndOgrBilgiH.Name = "ndOgrBilgiH";
            this.ndOgrBilgiH.Size = new System.Drawing.Size(63, 22);
            this.ndOgrBilgiH.TabIndex = 197;
            this.ndOgrBilgiH.Value = new decimal(new int[] {
            62,
            0,
            0,
            0});
            this.ndOgrBilgiH.ValueChanged += new System.EventHandler(this.ndOgrBilgiH_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ndOgrBilgiX);
            this.groupBox1.Controls.Add(this.ndOgrBilgiH);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ndOgrBilgiY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(889, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 99);
            this.groupBox1.TabIndex = 198;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Öğrenci Bilgileri";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOrnekGoster);
            this.groupBox2.Controls.Add(this.ndBubbleW);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.ndBubbleH);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.ndBubbleX);
            this.groupBox2.Controls.Add(this.ndBubbleArtim);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.ndBubbleY);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(889, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 213);
            this.groupBox2.TabIndex = 199;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OPAQ Bilgileri";
            // 
            // btnOrnekGoster
            // 
            this.btnOrnekGoster.Location = new System.Drawing.Point(26, 175);
            this.btnOrnekGoster.Name = "btnOrnekGoster";
            this.btnOrnekGoster.Size = new System.Drawing.Size(162, 23);
            this.btnOrnekGoster.TabIndex = 203;
            this.btnOrnekGoster.Text = "Örnek";
            this.btnOrnekGoster.UseVisualStyleBackColor = true;
            this.btnOrnekGoster.Click += new System.EventHandler(this.btnOrnekGoster_Click);
            // 
            // ndBubbleW
            // 
            this.ndBubbleW.Location = new System.Drawing.Point(54, 134);
            this.ndBubbleW.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ndBubbleW.Name = "ndBubbleW";
            this.ndBubbleW.Size = new System.Drawing.Size(63, 22);
            this.ndBubbleW.TabIndex = 200;
            this.ndBubbleW.Value = new decimal(new int[] {
            39,
            0,
            0,
            0});
            this.ndBubbleW.ValueChanged += new System.EventHandler(this.ndBubbleW_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(68, 112);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 17);
            this.label11.TabIndex = 198;
            this.label11.Text = "W";
            // 
            // ndBubbleH
            // 
            this.ndBubbleH.Location = new System.Drawing.Point(125, 134);
            this.ndBubbleH.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ndBubbleH.Name = "ndBubbleH";
            this.ndBubbleH.Size = new System.Drawing.Size(63, 22);
            this.ndBubbleH.TabIndex = 201;
            this.ndBubbleH.Value = new decimal(new int[] {
            39,
            0,
            0,
            0});
            this.ndBubbleH.ValueChanged += new System.EventHandler(this.ndBubbleH_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(139, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 17);
            this.label12.TabIndex = 199;
            this.label12.Text = "H";
            // 
            // ndBubbleX
            // 
            this.ndBubbleX.Location = new System.Drawing.Point(54, 42);
            this.ndBubbleX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ndBubbleX.Name = "ndBubbleX";
            this.ndBubbleX.Size = new System.Drawing.Size(63, 22);
            this.ndBubbleX.TabIndex = 195;
            this.ndBubbleX.Value = new decimal(new int[] {
            1104,
            0,
            0,
            0});
            this.ndBubbleX.ValueChanged += new System.EventHandler(this.ndBubbleX_ValueChanged);
            // 
            // ndBubbleArtim
            // 
            this.ndBubbleArtim.Location = new System.Drawing.Point(54, 70);
            this.ndBubbleArtim.Name = "ndBubbleArtim";
            this.ndBubbleArtim.Size = new System.Drawing.Size(63, 22);
            this.ndBubbleArtim.TabIndex = 197;
            this.ndBubbleArtim.Value = new decimal(new int[] {
            62,
            0,
            0,
            0});
            this.ndBubbleArtim.ValueChanged += new System.EventHandler(this.ndBubbleArtim_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(68, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 17);
            this.label8.TabIndex = 192;
            this.label8.Text = "X";
            // 
            // ndBubbleY
            // 
            this.ndBubbleY.Location = new System.Drawing.Point(125, 42);
            this.ndBubbleY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ndBubbleY.Name = "ndBubbleY";
            this.ndBubbleY.Size = new System.Drawing.Size(63, 22);
            this.ndBubbleY.TabIndex = 196;
            this.ndBubbleY.Value = new decimal(new int[] {
            1554,
            0,
            0,
            0});
            this.ndBubbleY.ValueChanged += new System.EventHandler(this.ndBubbleY_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(139, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 17);
            this.label9.TabIndex = 193;
            this.label9.Text = "Y";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 17);
            this.label10.TabIndex = 194;
            this.label10.Text = "Aralık";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 136);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 17);
            this.label13.TabIndex = 202;
            this.label13.Text = "Bubble";
            // 
            // FormCkOlustur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 397);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCkYazdir);
            this.Controls.Add(this.btnCkPdfOlustur);
            this.Controls.Add(this.btnKutukCek);
            this.Controls.Add(this.btnDosyaAdresleri);
            this.Controls.Add(this.statusStrip1);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbCkA5Dosyasi)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndOgrBilgiX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndOgrBilgiY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndOgrBilgiH)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleArtim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndBubbleY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Button btnDosyaAdresleri;
        private System.Windows.Forms.Button btnKutukCek;
        private System.Windows.Forms.Button btnCkPdfOlustur;
        private System.Windows.Forms.Button btnCkYazdir;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown ndOgrBilgiX;
        private System.Windows.Forms.NumericUpDown ndOgrBilgiY;
        private System.Windows.Forms.NumericUpDown ndOgrBilgiH;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown ndBubbleX;
        private System.Windows.Forms.NumericUpDown ndBubbleArtim;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown ndBubbleY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown ndBubbleW;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown ndBubbleH;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem şablonSeçToolStripMenuItem;
        private System.Windows.Forms.Button btnOrnekGoster;
    }
}