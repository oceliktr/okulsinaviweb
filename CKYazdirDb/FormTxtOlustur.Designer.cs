namespace ODM.CKYazdirDb
{
    partial class FormTxtOlustur
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTxtOlustur));
            this.txtData = new System.Windows.Forms.TextBox();
            this.ndSutun = new System.Windows.Forms.NumericUpDown();
            this.ndKarakter = new System.Windows.Forms.NumericUpDown();
            this.lvBolumler = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.silToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yukarıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aşağıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAraKarakter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSonunaEkle = new System.Windows.Forms.CheckBox();
            this.blAciklama = new System.Windows.Forms.Label();
            this.btnTextOlustur = new System.Windows.Forms.Button();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnDataAc = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSabitDeger = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAlanAdi = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgBranslar = new System.Windows.Forms.DataGridView();
            this.btnOturumlariBirlestir = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ndSutun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndKarakter)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBranslar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtData
            // 
            this.txtData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtData.Location = new System.Drawing.Point(19, 34);
            this.txtData.Margin = new System.Windows.Forms.Padding(4);
            this.txtData.Name = "txtData";
            this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtData.Size = new System.Drawing.Size(1000, 28);
            this.txtData.TabIndex = 165;
            this.txtData.Click += new System.EventHandler(this.txtData_Click);
            this.txtData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtData_KeyUp);
            this.txtData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtData_MouseDown);
            this.txtData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.txtData_MouseUp);
            // 
            // ndSutun
            // 
            this.ndSutun.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ndSutun.Location = new System.Drawing.Point(16, 60);
            this.ndSutun.Margin = new System.Windows.Forms.Padding(4);
            this.ndSutun.Name = "ndSutun";
            this.ndSutun.Size = new System.Drawing.Size(130, 27);
            this.ndSutun.TabIndex = 0;
            this.ndSutun.ValueChanged += new System.EventHandler(this.ndSutun_ValueChanged);
            // 
            // ndKarakter
            // 
            this.ndKarakter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ndKarakter.Location = new System.Drawing.Point(175, 60);
            this.ndKarakter.Margin = new System.Windows.Forms.Padding(4);
            this.ndKarakter.Name = "ndKarakter";
            this.ndKarakter.Size = new System.Drawing.Size(130, 27);
            this.ndKarakter.TabIndex = 1;
            this.ndKarakter.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ndKarakter.ValueChanged += new System.EventHandler(this.ndKarakter_ValueChanged);
            // 
            // lvBolumler
            // 
            this.lvBolumler.ContextMenuStrip = this.contextMenuStrip1;
            this.lvBolumler.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lvBolumler.HideSelection = false;
            this.lvBolumler.Location = new System.Drawing.Point(13, 25);
            this.lvBolumler.Margin = new System.Windows.Forms.Padding(4);
            this.lvBolumler.Name = "lvBolumler";
            this.lvBolumler.Size = new System.Drawing.Size(460, 246);
            this.lvBolumler.TabIndex = 173;
            this.lvBolumler.UseCompatibleStateImageBehavior = false;
            this.lvBolumler.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvBolumler_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.silToolStripMenuItem,
            this.yukarıToolStripMenuItem,
            this.aşağıToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 76);
            // 
            // silToolStripMenuItem
            // 
            this.silToolStripMenuItem.Name = "silToolStripMenuItem";
            this.silToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.silToolStripMenuItem.Text = "Sil";
            this.silToolStripMenuItem.Click += new System.EventHandler(this.silToolStripMenuItem_Click);
            // 
            // yukarıToolStripMenuItem
            // 
            this.yukarıToolStripMenuItem.Name = "yukarıToolStripMenuItem";
            this.yukarıToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.yukarıToolStripMenuItem.Text = "Yukarı";
            this.yukarıToolStripMenuItem.Click += new System.EventHandler(this.yukarıToolStripMenuItem_Click);
            // 
            // aşağıToolStripMenuItem
            // 
            this.aşağıToolStripMenuItem.Name = "aşağıToolStripMenuItem";
            this.aşağıToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.aşağıToolStripMenuItem.Text = "Aşağı";
            this.aşağıToolStripMenuItem.Click += new System.EventHandler(this.aşağıToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 176;
            this.label1.Text = "Örnek :";
            // 
            // txtAraKarakter
            // 
            this.txtAraKarakter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtAraKarakter.Location = new System.Drawing.Point(165, 180);
            this.txtAraKarakter.Margin = new System.Windows.Forms.Padding(4);
            this.txtAraKarakter.Name = "txtAraKarakter";
            this.txtAraKarakter.Size = new System.Drawing.Size(42, 27);
            this.txtAraKarakter.TabIndex = 4;
            this.txtAraKarakter.Text = "#";
            this.txtAraKarakter.TextChanged += new System.EventHandler(this.txtAraKarakter_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(16, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 17);
            this.label2.TabIndex = 179;
            this.label2.Text = "Başlangıç Noktası";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(175, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 17);
            this.label3.TabIndex = 180;
            this.label3.Text = "Karakter Sayısı";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(16, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 181;
            this.label4.Text = "Açıklama:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(165, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 17);
            this.label6.TabIndex = 183;
            this.label6.Text = "Ara Karakter";
            // 
            // cbSonunaEkle
            // 
            this.cbSonunaEkle.AutoSize = true;
            this.cbSonunaEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSonunaEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbSonunaEkle.Location = new System.Drawing.Point(214, 182);
            this.cbSonunaEkle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSonunaEkle.Name = "cbSonunaEkle";
            this.cbSonunaEkle.Size = new System.Drawing.Size(81, 24);
            this.cbSonunaEkle.TabIndex = 5;
            this.cbSonunaEkle.Text = "ile bitir";
            this.cbSonunaEkle.UseVisualStyleBackColor = true;
            this.cbSonunaEkle.CheckedChanged += new System.EventHandler(this.cbSonunaEkle_CheckedChanged);
            // 
            // blAciklama
            // 
            this.blAciklama.BackColor = System.Drawing.SystemColors.ControlLight;
            this.blAciklama.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.blAciklama.Location = new System.Drawing.Point(12, 364);
            this.blAciklama.Name = "blAciklama";
            this.blAciklama.Size = new System.Drawing.Size(979, 154);
            this.blAciklama.TabIndex = 185;
            this.blAciklama.Text = resources.GetString("blAciklama.Text");
            this.blAciklama.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnTextOlustur
            // 
            this.btnTextOlustur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTextOlustur.Location = new System.Drawing.Point(998, 361);
            this.btnTextOlustur.Margin = new System.Windows.Forms.Padding(4);
            this.btnTextOlustur.Name = "btnTextOlustur";
            this.btnTextOlustur.Size = new System.Drawing.Size(121, 86);
            this.btnTextOlustur.TabIndex = 178;
            this.btnTextOlustur.Text = "Text Oluştur";
            this.btnTextOlustur.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTextOlustur.UseVisualStyleBackColor = true;
            this.btnTextOlustur.Click += new System.EventHandler(this.btnTextOlustur_Click);
            // 
            // btnEkle
            // 
            this.btnEkle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEkle.Image = ((System.Drawing.Image)(resources.GetObject("btnEkle.Image")));
            this.btnEkle.Location = new System.Drawing.Point(16, 220);
            this.btnEkle.Margin = new System.Windows.Forms.Padding(4);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(289, 51);
            this.btnEkle.TabIndex = 6;
            this.btnEkle.Text = "Kesme Noktası Ekle";
            this.btnEkle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnDataAc
            // 
            this.btnDataAc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDataAc.Image = ((System.Drawing.Image)(resources.GetObject("btnDataAc.Image")));
            this.btnDataAc.Location = new System.Drawing.Point(1026, 34);
            this.btnDataAc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDataAc.Name = "btnDataAc";
            this.btnDataAc.Size = new System.Drawing.Size(93, 30);
            this.btnDataAc.TabIndex = 164;
            this.btnDataAc.Text = "Data Aç";
            this.btnDataAc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDataAc.UseVisualStyleBackColor = true;
            this.btnDataAc.Click += new System.EventHandler(this.btnDataAc_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(16, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 17);
            this.label7.TabIndex = 187;
            this.label7.Text = "Sabit Değer";
            // 
            // txtSabitDeger
            // 
            this.txtSabitDeger.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSabitDeger.Location = new System.Drawing.Point(16, 180);
            this.txtSabitDeger.Margin = new System.Windows.Forms.Padding(4);
            this.txtSabitDeger.Name = "txtSabitDeger";
            this.txtSabitDeger.Size = new System.Drawing.Size(130, 27);
            this.txtSabitDeger.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbAlanAdi);
            this.groupBox1.Controls.Add(this.btnEkle);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ndSutun);
            this.groupBox1.Controls.Add(this.txtSabitDeger);
            this.groupBox1.Controls.Add(this.ndKarakter);
            this.groupBox1.Controls.Add(this.txtAraKarakter);
            this.groupBox1.Controls.Add(this.cbSonunaEkle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(19, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 282);
            this.groupBox1.TabIndex = 188;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kesme Nokta Ayarları";
            // 
            // cbAlanAdi
            // 
            this.cbAlanAdi.FormattingEnabled = true;
            this.cbAlanAdi.Items.AddRange(new object[] {
            "Sınav Kodu",
            "Opaq Id",
            "Ders Id",
            "Kitapçık Türü",
            "Cevap Tipi",
            "Katılım Durumu",
            "Cevaplar",
            "Oturum"});
            this.cbAlanAdi.Location = new System.Drawing.Point(16, 120);
            this.cbAlanAdi.Name = "cbAlanAdi";
            this.cbAlanAdi.Size = new System.Drawing.Size(289, 24);
            this.cbAlanAdi.TabIndex = 190;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvBolumler);
            this.groupBox2.Location = new System.Drawing.Point(358, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(488, 282);
            this.groupBox2.TabIndex = 189;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kesme Noktaları";
            // 
            // dgBranslar
            // 
            this.dgBranslar.AllowUserToAddRows = false;
            this.dgBranslar.AllowUserToDeleteRows = false;
            this.dgBranslar.AllowUserToOrderColumns = true;
            this.dgBranslar.AllowUserToResizeColumns = false;
            this.dgBranslar.AllowUserToResizeRows = false;
            this.dgBranslar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBranslar.ContextMenuStrip = this.contextMenuStrip1;
            this.dgBranslar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgBranslar.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgBranslar.Location = new System.Drawing.Point(862, 86);
            this.dgBranslar.Name = "dgBranslar";
            this.dgBranslar.RowHeadersVisible = false;
            this.dgBranslar.RowHeadersWidth = 51;
            this.dgBranslar.RowTemplate.Height = 24;
            this.dgBranslar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBranslar.ShowEditingIcon = false;
            this.dgBranslar.ShowRowErrors = false;
            this.dgBranslar.Size = new System.Drawing.Size(257, 268);
            this.dgBranslar.TabIndex = 190;
            this.dgBranslar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBranslar_CellDoubleClick);
            // 
            // btnOturumlariBirlestir
            // 
            this.btnOturumlariBirlestir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOturumlariBirlestir.Location = new System.Drawing.Point(997, 454);
            this.btnOturumlariBirlestir.Name = "btnOturumlariBirlestir";
            this.btnOturumlariBirlestir.Size = new System.Drawing.Size(121, 121);
            this.btnOturumlariBirlestir.TabIndex = 191;
            this.btnOturumlariBirlestir.Text = "Oturum Verilerini Birleştir";
            this.btnOturumlariBirlestir.UseVisualStyleBackColor = true;
            this.btnOturumlariBirlestir.Click += new System.EventHandler(this.btnOturumlariBirlestir_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(12, 528);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(979, 62);
            this.label5.TabIndex = 192;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // FormTxtOlustur
            // 
            this.AcceptButton = this.btnEkle;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 626);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnOturumlariBirlestir);
            this.Controls.Add(this.dgBranslar);
            this.Controls.Add(this.btnTextOlustur);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.blAciklama);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.btnDataAc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTxtOlustur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Txt Oluştur";
            this.Load += new System.EventHandler(this.FormTxtOlustur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ndSutun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndKarakter)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBranslar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDataAc;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.NumericUpDown ndSutun;
        private System.Windows.Forms.NumericUpDown ndKarakter;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.ListView lvBolumler;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem silToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yukarıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aşağıToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAraKarakter;
        private System.Windows.Forms.Button btnTextOlustur;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbSonunaEkle;
        private System.Windows.Forms.Label blAciklama;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSabitDeger;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbAlanAdi;
        private System.Windows.Forms.DataGridView dgBranslar;
        private System.Windows.Forms.Button btnOturumlariBirlestir;
        private System.Windows.Forms.Label label5;
    }
}