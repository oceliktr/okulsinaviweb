namespace ODM.CKYazdirDb
{
    partial class FormKazanimlar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKazanimlar));
            this.cbBranslar = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKazanimNo = new System.Windows.Forms.TextBox();
            this.txtKazanim = new System.Windows.Forms.TextBox();
            this.txtKazanimOgrenci = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.dgvKazanimlar = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.düzenleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.silToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.seçiliSınıfKazanımlarınıSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tümünüSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnVazgec = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSorulari = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSinif = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDosyadanYukle = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKazanimlar)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbBranslar
            // 
            this.cbBranslar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranslar.FormattingEnabled = true;
            this.cbBranslar.Location = new System.Drawing.Point(195, 15);
            this.cbBranslar.Margin = new System.Windows.Forms.Padding(4);
            this.cbBranslar.Name = "cbBranslar";
            this.cbBranslar.Size = new System.Drawing.Size(311, 28);
            this.cbBranslar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Branş :";
            // 
            // txtKazanimNo
            // 
            this.txtKazanimNo.Location = new System.Drawing.Point(195, 94);
            this.txtKazanimNo.Name = "txtKazanimNo";
            this.txtKazanimNo.Size = new System.Drawing.Size(311, 26);
            this.txtKazanimNo.TabIndex = 2;
            // 
            // txtKazanim
            // 
            this.txtKazanim.Location = new System.Drawing.Point(195, 134);
            this.txtKazanim.Name = "txtKazanim";
            this.txtKazanim.Size = new System.Drawing.Size(739, 26);
            this.txtKazanim.TabIndex = 3;
            // 
            // txtKazanimOgrenci
            // 
            this.txtKazanimOgrenci.Location = new System.Drawing.Point(195, 174);
            this.txtKazanimOgrenci.Name = "txtKazanimOgrenci";
            this.txtKazanimOgrenci.Size = new System.Drawing.Size(739, 26);
            this.txtKazanimOgrenci.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Kazanım No :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 138);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Kazanım :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 176);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kazanım (Öğrenci) :";
            // 
            // btnKaydet
            // 
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.Location = new System.Drawing.Point(813, 242);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(121, 49);
            this.btnKaydet.TabIndex = 6;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // dgvKazanimlar
            // 
            this.dgvKazanimlar.AllowUserToAddRows = false;
            this.dgvKazanimlar.AllowUserToDeleteRows = false;
            this.dgvKazanimlar.AllowUserToOrderColumns = true;
            this.dgvKazanimlar.AllowUserToResizeColumns = false;
            this.dgvKazanimlar.AllowUserToResizeRows = false;
            this.dgvKazanimlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKazanimlar.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvKazanimlar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvKazanimlar.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvKazanimlar.Location = new System.Drawing.Point(12, 300);
            this.dgvKazanimlar.MultiSelect = false;
            this.dgvKazanimlar.Name = "dgvKazanimlar";
            this.dgvKazanimlar.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvKazanimlar.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKazanimlar.RowHeadersVisible = false;
            this.dgvKazanimlar.RowHeadersWidth = 51;
            this.dgvKazanimlar.RowTemplate.Height = 24;
            this.dgvKazanimlar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKazanimlar.ShowCellErrors = false;
            this.dgvKazanimlar.ShowCellToolTips = false;
            this.dgvKazanimlar.ShowEditingIcon = false;
            this.dgvKazanimlar.ShowRowErrors = false;
            this.dgvKazanimlar.Size = new System.Drawing.Size(924, 450);
            this.dgvKazanimlar.TabIndex = 7;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.düzenleToolStripMenuItem,
            this.silToolStripMenuItem,
            this.toolStripMenuItem1,
            this.seçiliSınıfKazanımlarınıSilToolStripMenuItem,
            this.tümünüSilToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(261, 106);
            // 
            // düzenleToolStripMenuItem
            // 
            this.düzenleToolStripMenuItem.Name = "düzenleToolStripMenuItem";
            this.düzenleToolStripMenuItem.Size = new System.Drawing.Size(260, 24);
            this.düzenleToolStripMenuItem.Text = "Düzenle";
            this.düzenleToolStripMenuItem.Click += new System.EventHandler(this.DüzenleToolStripMenuItem_Click);
            // 
            // silToolStripMenuItem
            // 
            this.silToolStripMenuItem.Name = "silToolStripMenuItem";
            this.silToolStripMenuItem.Size = new System.Drawing.Size(260, 24);
            this.silToolStripMenuItem.Text = "Sil";
            this.silToolStripMenuItem.Click += new System.EventHandler(this.SilToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(257, 6);
            // 
            // seçiliSınıfKazanımlarınıSilToolStripMenuItem
            // 
            this.seçiliSınıfKazanımlarınıSilToolStripMenuItem.Name = "seçiliSınıfKazanımlarınıSilToolStripMenuItem";
            this.seçiliSınıfKazanımlarınıSilToolStripMenuItem.Size = new System.Drawing.Size(260, 24);
            this.seçiliSınıfKazanımlarınıSilToolStripMenuItem.Text = "Seçili Sınıf Kazanımlarını Sil";
            this.seçiliSınıfKazanımlarınıSilToolStripMenuItem.Click += new System.EventHandler(this.seçiliSınıfKazanımlarınıSilToolStripMenuItem_Click);
            // 
            // tümünüSilToolStripMenuItem
            // 
            this.tümünüSilToolStripMenuItem.Name = "tümünüSilToolStripMenuItem";
            this.tümünüSilToolStripMenuItem.Size = new System.Drawing.Size(260, 24);
            this.tümünüSilToolStripMenuItem.Text = "Tümünü Sil";
            this.tümünüSilToolStripMenuItem.Click += new System.EventHandler(this.tümünüSilToolStripMenuItem_Click);
            // 
            // btnVazgec
            // 
            this.btnVazgec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVazgec.Location = new System.Drawing.Point(12, 245);
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(121, 49);
            this.btnVazgec.TabIndex = 8;
            this.btnVazgec.Text = "Vazgeç";
            this.btnVazgec.UseVisualStyleBackColor = true;
            this.btnVazgec.Click += new System.EventHandler(this.BtnVazgec_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(109, 212);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Soruları :";
            // 
            // txtSorulari
            // 
            this.txtSorulari.Location = new System.Drawing.Point(195, 210);
            this.txtSorulari.Name = "txtSorulari";
            this.txtSorulari.Size = new System.Drawing.Size(739, 26);
            this.txtSorulari.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(191, 245);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(615, 49);
            this.label6.TabIndex = 12;
            this.label6.Text = "Bu kazanıma ait soru numaralarını kitapçık türü ile birlikte arasına virgül \',\' k" +
    "ullanarak giriniz. A1,B3,A8, gibi";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(134, 57);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Sınıf :";
            // 
            // cbSinif
            // 
            this.cbSinif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSinif.FormattingEnabled = true;
            this.cbSinif.Location = new System.Drawing.Point(195, 54);
            this.cbSinif.Margin = new System.Windows.Forms.Padding(4);
            this.cbSinif.Name = "cbSinif";
            this.cbSinif.Size = new System.Drawing.Size(311, 28);
            this.cbSinif.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(894, 9);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(40, 39);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Tag = "Cevapları Listele";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnDosyadanYukle
            // 
            this.btnDosyadanYukle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDosyadanYukle.Location = new System.Drawing.Point(788, 812);
            this.btnDosyadanYukle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDosyadanYukle.Name = "btnDosyadanYukle";
            this.btnDosyadanYukle.Size = new System.Drawing.Size(146, 40);
            this.btnDosyadanYukle.TabIndex = 178;
            this.btnDosyadanYukle.Text = "Dosyadan Yükle";
            this.btnDosyadanYukle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDosyadanYukle.UseVisualStyleBackColor = true;
            this.btnDosyadanYukle.Click += new System.EventHandler(this.btnDosyadanYukle_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(168)), true);
            this.label8.Location = new System.Drawing.Point(12, 753);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(924, 99);
            this.label8.TabIndex = 177;
            this.label8.Text = "Derskodu#KazanımNo#Kazanım#ÖğrenciKazanım#Soruları formatında verileri dosyadan y" +
    "üklemek için \'Dosyadan Yükle\' butonunu kullanınınz.\r\nÖrnek:\r\n3#İTA.8.2.4#..... g" +
    "elişmeleri kavrar#Batı Çephesi#A8,B1";
            // 
            // FormKazanimlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 861);
            this.Controls.Add(this.btnDosyadanYukle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbSinif);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSorulari);
            this.Controls.Add(this.btnVazgec);
            this.Controls.Add(this.dgvKazanimlar);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKazanimOgrenci);
            this.Controls.Add(this.txtKazanim);
            this.Controls.Add(this.txtKazanimNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbBranslar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKazanimlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kazanımlar";
            this.Load += new System.EventHandler(this.FormKazanimlar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKazanimlar)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbBranslar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKazanimNo;
        private System.Windows.Forms.TextBox txtKazanim;
        private System.Windows.Forms.TextBox txtKazanimOgrenci;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem düzenleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem silToolStripMenuItem;
        private System.Windows.Forms.Button btnVazgec;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSorulari;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbSinif;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvKazanimlar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem seçiliSınıfKazanımlarınıSilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tümünüSilToolStripMenuItem;
        private System.Windows.Forms.Button btnDosyadanYukle;
        private System.Windows.Forms.Label label8;
    }
}