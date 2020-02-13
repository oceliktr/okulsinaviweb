namespace ODM.CKYazdirDb
{
    partial class FormAyarlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAyarlar));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGozat = new System.Windows.Forms.Button();
            this.txtLogo = new System.Windows.Forms.TextBox();
            this.btnCkGozat = new System.Windows.Forms.Button();
            this.txtSinifListesi = new System.Windows.Forms.TextBox();
            this.btnSinifListesiGozat = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSinif = new System.Windows.Forms.ComboBox();
            this.txtIlAdi = new System.Windows.Forms.TextBox();
            this.txtSinavAdi = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEposta = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWeb = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtOdmAdres = new System.Windows.Forms.TextBox();
            this.rbKazanim = new System.Windows.Forms.RadioButton();
            this.rbKonu = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tümünüSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seçileniSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(159, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(341, 17);
            this.label5.TabIndex = 168;
            this.label5.Text = "Logo 1:1 oranında olmalı. İdeal boyut yaklaşık 500 px";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Location = new System.Drawing.Point(159, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(429, 17);
            this.label4.TabIndex = 178;
            this.label4.Text = "Şablonları http://erzurumodm.meb.gov.tr adresinden indirebilirsiniz.";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(159, 154);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(102, 93);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 163;
            this.pbLogo.TabStop = false;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.Location = new System.Drawing.Point(442, 427);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(83, 37);
            this.btnKaydet.TabIndex = 184;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(103, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Logo :";
            // 
            // btnGozat
            // 
            this.btnGozat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGozat.Location = new System.Drawing.Point(531, 107);
            this.btnGozat.Name = "btnGozat";
            this.btnGozat.Size = new System.Drawing.Size(58, 27);
            this.btnGozat.TabIndex = 164;
            this.btnGozat.Text = "Gözat";
            this.btnGozat.UseVisualStyleBackColor = true;
            this.btnGozat.Click += new System.EventHandler(this.btnGozat_Click);
            // 
            // txtLogo
            // 
            this.txtLogo.Enabled = false;
            this.txtLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLogo.Location = new System.Drawing.Point(159, 107);
            this.txtLogo.Name = "txtLogo";
            this.txtLogo.Size = new System.Drawing.Size(366, 24);
            this.txtLogo.TabIndex = 169;
            // 
            // btnCkGozat
            // 
            this.btnCkGozat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCkGozat.Location = new System.Drawing.Point(897, 12);
            this.btnCkGozat.Name = "btnCkGozat";
            this.btnCkGozat.Size = new System.Drawing.Size(112, 27);
            this.btnCkGozat.TabIndex = 187;
            this.btnCkGozat.Text = "Şablon Seç";
            this.btnCkGozat.UseVisualStyleBackColor = true;
            this.btnCkGozat.Click += new System.EventHandler(this.btnCkGozat_Click);
            // 
            // txtSinifListesi
            // 
            this.txtSinifListesi.Enabled = false;
            this.txtSinifListesi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSinifListesi.Location = new System.Drawing.Point(159, 259);
            this.txtSinifListesi.Name = "txtSinifListesi";
            this.txtSinifListesi.Size = new System.Drawing.Size(366, 24);
            this.txtSinifListesi.TabIndex = 191;
            // 
            // btnSinifListesiGozat
            // 
            this.btnSinifListesiGozat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSinifListesiGozat.Location = new System.Drawing.Point(531, 259);
            this.btnSinifListesiGozat.Name = "btnSinifListesiGozat";
            this.btnSinifListesiGozat.Size = new System.Drawing.Size(58, 27);
            this.btnSinifListesiGozat.TabIndex = 190;
            this.btnSinifListesiGozat.Text = "Gözat";
            this.btnSinifListesiGozat.UseVisualStyleBackColor = true;
            this.btnSinifListesiGozat.Click += new System.EventHandler(this.btnSinifListesiGozat_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(8, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 18);
            this.label7.TabIndex = 189;
            this.label7.Text = "Sınıf Listesi Şablonu :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(625, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 193;
            this.label2.Text = "Sınıf :";
            // 
            // cbSinif
            // 
            this.cbSinif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSinif.FormattingEnabled = true;
            this.cbSinif.Location = new System.Drawing.Point(674, 14);
            this.cbSinif.Name = "cbSinif";
            this.cbSinif.Size = new System.Drawing.Size(217, 24);
            this.cbSinif.TabIndex = 192;
            // 
            // txtIlAdi
            // 
            this.txtIlAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtIlAdi.Location = new System.Drawing.Point(159, 12);
            this.txtIlAdi.Name = "txtIlAdi";
            this.txtIlAdi.Size = new System.Drawing.Size(366, 24);
            this.txtIlAdi.TabIndex = 4;
            // 
            // txtSinavAdi
            // 
            this.txtSinavAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSinavAdi.Location = new System.Drawing.Point(159, 42);
            this.txtSinavAdi.Name = "txtSinavAdi";
            this.txtSinavAdi.Size = new System.Drawing.Size(366, 24);
            this.txtSinavAdi.TabIndex = 195;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(77, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 18);
            this.label8.TabIndex = 194;
            this.label8.Text = "Sınav Adı :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(103, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 18);
            this.label9.TabIndex = 196;
            this.label9.Text = "İl Adı :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 326);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 202;
            this.label1.Text = "ÖDM Adres :";
            // 
            // txtEposta
            // 
            this.txtEposta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtEposta.Location = new System.Drawing.Point(159, 385);
            this.txtEposta.Name = "txtEposta";
            this.txtEposta.Size = new System.Drawing.Size(366, 24);
            this.txtEposta.TabIndex = 200;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(43, 385);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 18);
            this.label10.TabIndex = 199;
            this.label10.Text = "E-posta Adresi :";
            // 
            // txtWeb
            // 
            this.txtWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtWeb.Location = new System.Drawing.Point(159, 352);
            this.txtWeb.Name = "txtWeb";
            this.txtWeb.Size = new System.Drawing.Size(366, 24);
            this.txtWeb.TabIndex = 198;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(64, 352);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 18);
            this.label11.TabIndex = 197;
            this.label11.Text = "Web Adresi :";
            // 
            // txtOdmAdres
            // 
            this.txtOdmAdres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtOdmAdres.Location = new System.Drawing.Point(159, 322);
            this.txtOdmAdres.Name = "txtOdmAdres";
            this.txtOdmAdres.Size = new System.Drawing.Size(366, 24);
            this.txtOdmAdres.TabIndex = 203;
            // 
            // rbKazanim
            // 
            this.rbKazanim.AutoSize = true;
            this.rbKazanim.Checked = true;
            this.rbKazanim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbKazanim.Location = new System.Drawing.Point(159, 72);
            this.rbKazanim.Name = "rbKazanim";
            this.rbKazanim.Size = new System.Drawing.Size(180, 21);
            this.rbKazanim.TabIndex = 204;
            this.rbKazanim.TabStop = true;
            this.rbKazanim.Text = "Kazanım Değerlendirme";
            this.rbKazanim.UseVisualStyleBackColor = true;
            // 
            // rbKonu
            // 
            this.rbKonu.AutoSize = true;
            this.rbKonu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbKonu.Location = new System.Drawing.Point(345, 72);
            this.rbKonu.Name = "rbKonu";
            this.rbKonu.Size = new System.Drawing.Size(159, 21);
            this.rbKonu.TabIndex = 205;
            this.rbKonu.Text = "Konu Değerlendirme";
            this.rbKonu.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(628, 59);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(381, 227);
            this.dataGridView1.TabIndex = 206;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.tümünüSilToolStripMenuItem,
            this.seçileniSilToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 58);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
            // 
            // tümünüSilToolStripMenuItem
            // 
            this.tümünüSilToolStripMenuItem.Name = "tümünüSilToolStripMenuItem";
            this.tümünüSilToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.tümünüSilToolStripMenuItem.Text = "Tümünü Sil";
            this.tümünüSilToolStripMenuItem.Click += new System.EventHandler(this.tümünüSilToolStripMenuItem_Click);
            // 
            // seçileniSilToolStripMenuItem
            // 
            this.seçileniSilToolStripMenuItem.Name = "seçileniSilToolStripMenuItem";
            this.seçileniSilToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.seçileniSilToolStripMenuItem.Text = "Seçileni Sil";
            this.seçileniSilToolStripMenuItem.Click += new System.EventHandler(this.seçileniSilToolStripMenuItem_Click);
            // 
            // FormAyarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 482);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.rbKonu);
            this.Controls.Add(this.rbKazanim);
            this.Controls.Add(this.txtOdmAdres);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEposta);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtWeb);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSinavAdi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSinif);
            this.Controls.Add(this.txtSinifListesi);
            this.Controls.Add(this.btnSinifListesiGozat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCkGozat);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLogo);
            this.Controls.Add(this.btnGozat);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.txtIlAdi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAyarlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ayarlar";
            this.Load += new System.EventHandler(this.FormAyarlar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGozat;
        private System.Windows.Forms.TextBox txtLogo;
        private System.Windows.Forms.Button btnCkGozat;
        private System.Windows.Forms.TextBox txtSinifListesi;
        private System.Windows.Forms.Button btnSinifListesiGozat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSinif;
        private System.Windows.Forms.TextBox txtIlAdi;
        private System.Windows.Forms.TextBox txtSinavAdi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEposta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWeb;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtOdmAdres;
        private System.Windows.Forms.RadioButton rbKazanim;
        private System.Windows.Forms.RadioButton rbKonu;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tümünüSilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seçileniSilToolStripMenuItem;
    }
}