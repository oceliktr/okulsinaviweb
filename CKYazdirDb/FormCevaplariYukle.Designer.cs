namespace ODM.CKYazdirDb
{
    partial class FormCevaplariYukle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCevaplariYukle));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSinif = new System.Windows.Forms.ComboBox();
            this.cbBranslar = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tümünüSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seçileniSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCevabiKaydet = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtDogruCevaplar = new System.Windows.Forms.TextBox();
            this.txtKitapcikTuru = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.düzenleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVazgec = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnVazgec);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbSinif);
            this.groupBox1.Controls.Add(this.cbBranslar);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnCevabiKaydet);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.txtDogruCevaplar);
            this.groupBox1.Controls.Add(this.txtKitapcikTuru);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 646);
            this.groupBox1.TabIndex = 169;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cevap Anahtarları";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 59);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 17);
            this.label7.TabIndex = 174;
            this.label7.Text = "Sınıf :";
            // 
            // cbSinif
            // 
            this.cbSinif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSinif.FormattingEnabled = true;
            this.cbSinif.Location = new System.Drawing.Point(87, 56);
            this.cbSinif.Margin = new System.Windows.Forms.Padding(4);
            this.cbSinif.Name = "cbSinif";
            this.cbSinif.Size = new System.Drawing.Size(311, 24);
            this.cbSinif.TabIndex = 173;
            // 
            // cbBranslar
            // 
            this.cbBranslar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranslar.FormattingEnabled = true;
            this.cbBranslar.Location = new System.Drawing.Point(87, 24);
            this.cbBranslar.Margin = new System.Windows.Forms.Padding(4);
            this.cbBranslar.Name = "cbBranslar";
            this.cbBranslar.Size = new System.Drawing.Size(311, 24);
            this.cbBranslar.TabIndex = 172;
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
            this.dataGridView1.Location = new System.Drawing.Point(11, 186);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(598, 317);
            this.dataGridView1.TabIndex = 168;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.düzenleToolStripMenuItem,
            this.toolStripMenuItem1,
            this.tümünüSilToolStripMenuItem,
            this.seçileniSilToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 82);
            // 
            // tümünüSilToolStripMenuItem
            // 
            this.tümünüSilToolStripMenuItem.Name = "tümünüSilToolStripMenuItem";
            this.tümünüSilToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.tümünüSilToolStripMenuItem.Text = "Tümünü Sil";
            this.tümünüSilToolStripMenuItem.Click += new System.EventHandler(this.TümünüSilToolStripMenuItem_Click);
            // 
            // seçileniSilToolStripMenuItem
            // 
            this.seçileniSilToolStripMenuItem.Name = "seçileniSilToolStripMenuItem";
            this.seçileniSilToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.seçileniSilToolStripMenuItem.Text = "Seçileni Sil";
            this.seçileniSilToolStripMenuItem.Click += new System.EventHandler(this.SeçileniSilToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(168)), true);
            this.label5.Location = new System.Drawing.Point(10, 520);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(599, 122);
            this.label5.TabIndex = 171;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // btnCevabiKaydet
            // 
            this.btnCevabiKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCevabiKaydet.Image = ((System.Drawing.Image)(resources.GetObject("btnCevabiKaydet.Image")));
            this.btnCevabiKaydet.Location = new System.Drawing.Point(409, 123);
            this.btnCevabiKaydet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCevabiKaydet.Name = "btnCevabiKaydet";
            this.btnCevabiKaydet.Size = new System.Drawing.Size(134, 58);
            this.btnCevabiKaydet.TabIndex = 167;
            this.btnCevabiKaydet.Text = "Cevabı Kaydet";
            this.btnCevabiKaydet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCevabiKaydet.UseVisualStyleBackColor = true;
            this.btnCevabiKaydet.Click += new System.EventHandler(this.BtnCevabiKaydet_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(549, 123);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(59, 58);
            this.btnRefresh.TabIndex = 170;
            this.btnRefresh.Tag = "Cevapları Listele";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // txtDogruCevaplar
            // 
            this.txtDogruCevaplar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDogruCevaplar.Location = new System.Drawing.Point(86, 87);
            this.txtDogruCevaplar.Name = "txtDogruCevaplar";
            this.txtDogruCevaplar.Size = new System.Drawing.Size(523, 22);
            this.txtDogruCevaplar.TabIndex = 7;
            this.txtDogruCevaplar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDogruCevaplar_KeyPress);
            // 
            // txtKitapcikTuru
            // 
            this.txtKitapcikTuru.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKitapcikTuru.Location = new System.Drawing.Point(523, 24);
            this.txtKitapcikTuru.MaxLength = 1;
            this.txtKitapcikTuru.Name = "txtKitapcikTuru";
            this.txtKitapcikTuru.Size = new System.Drawing.Size(84, 22);
            this.txtKitapcikTuru.TabIndex = 6;
            this.txtKitapcikTuru.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDogruCevaplar_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Cevaplar :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(419, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Kitapçık Türü :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ders Kodu :";
            // 
            // düzenleToolStripMenuItem
            // 
            this.düzenleToolStripMenuItem.Name = "düzenleToolStripMenuItem";
            this.düzenleToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.düzenleToolStripMenuItem.Text = "Düzenle";
            this.düzenleToolStripMenuItem.Click += new System.EventHandler(this.düzenleToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // btnVazgec
            // 
            this.btnVazgec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVazgec.Location = new System.Drawing.Point(11, 123);
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(121, 49);
            this.btnVazgec.TabIndex = 175;
            this.btnVazgec.Text = "Vazgeç";
            this.btnVazgec.UseVisualStyleBackColor = true;
            this.btnVazgec.Click += new System.EventHandler(this.btnVazgec_Click);
            // 
            // FormCevaplariYukle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 673);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormCevaplariYukle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cevap Yükleme Formu";
            this.Load += new System.EventHandler(this.FormCevaplariYukle_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCevabiKaydet;
        private System.Windows.Forms.TextBox txtDogruCevaplar;
        private System.Windows.Forms.TextBox txtKitapcikTuru;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tümünüSilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seçileniSilToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbBranslar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbSinif;
        private System.Windows.Forms.ToolStripMenuItem düzenleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Button btnVazgec;
    }
}