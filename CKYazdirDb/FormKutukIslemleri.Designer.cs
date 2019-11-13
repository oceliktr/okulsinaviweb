namespace ODM.CKYazdirDb
{
    partial class FormKutukIslemleri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKutukIslemleri));
            this.btnKutukDosyasiniAc = new System.Windows.Forms.Button();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dgvKutuk = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.seçiliİlçeyiSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bgwTumunuSil = new System.ComponentModel.BackgroundWorker();
            this.txtAra = new System.Windows.Forms.TextBox();
            this.btnAra = new System.Windows.Forms.Button();
            this.seçiliOkuluSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seçiliKayıtıSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.özelSilmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.düzenleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.yeniKayıtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKutuk)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKutukDosyasiniAc
            // 
            this.btnKutukDosyasiniAc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKutukDosyasiniAc.Image = ((System.Drawing.Image)(resources.GetObject("btnKutukDosyasiniAc.Image")));
            this.btnKutukDosyasiniAc.Location = new System.Drawing.Point(1066, 47);
            this.btnKutukDosyasiniAc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnKutukDosyasiniAc.Name = "btnKutukDosyasiniAc";
            this.btnKutukDosyasiniAc.Size = new System.Drawing.Size(175, 39);
            this.btnKutukDosyasiniAc.TabIndex = 1;
            this.btnKutukDosyasiniAc.Text = "Kütük Dosyası Seç";
            this.btnKutukDosyasiniAc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnKutukDosyasiniAc.UseVisualStyleBackColor = true;
            this.btnKutukDosyasiniAc.Click += new System.EventHandler(this.BtnKutukDosyasiniAc_Click);
            // 
            // lblBilgi
            // 
            this.lblBilgi.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBilgi.Location = new System.Drawing.Point(11, 11);
            this.lblBilgi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(1047, 34);
            this.lblBilgi.TabIndex = 115;
            this.lblBilgi.Text = "E-Okuldan alınan kütük dosyalarını seçiniz. Çalışma kitabı adı \'Sheet1\' olmalıdır" +
    ". E-okuldan alınan kütükte sutun silmeyiniz.";
            this.lblBilgi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 47);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1048, 39);
            this.progressBar1.TabIndex = 116;
            // 
            // dgvKutuk
            // 
            this.dgvKutuk.AllowUserToAddRows = false;
            this.dgvKutuk.AllowUserToDeleteRows = false;
            this.dgvKutuk.AllowUserToOrderColumns = true;
            this.dgvKutuk.AllowUserToResizeColumns = false;
            this.dgvKutuk.AllowUserToResizeRows = false;
            this.dgvKutuk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKutuk.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvKutuk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvKutuk.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvKutuk.Location = new System.Drawing.Point(12, 156);
            this.dgvKutuk.MultiSelect = false;
            this.dgvKutuk.Name = "dgvKutuk";
            this.dgvKutuk.RowHeadersVisible = false;
            this.dgvKutuk.RowHeadersWidth = 51;
            this.dgvKutuk.RowTemplate.Height = 24;
            this.dgvKutuk.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKutuk.ShowEditingIcon = false;
            this.dgvKutuk.ShowRowErrors = false;
            this.dgvKutuk.Size = new System.Drawing.Size(1229, 567);
            this.dgvKutuk.TabIndex = 117;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yeniKayıtToolStripMenuItem,
            this.düzenleToolStripMenuItem,
            this.toolStripMenuItem1,
            this.seçiliKayıtıSilToolStripMenuItem,
            this.seçiliOkuluSilToolStripMenuItem,
            this.seçiliİlçeyiSilToolStripMenuItem,
            this.özelSilmeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 182);
            // 
            // seçiliİlçeyiSilToolStripMenuItem
            // 
            this.seçiliİlçeyiSilToolStripMenuItem.Name = "seçiliİlçeyiSilToolStripMenuItem";
            this.seçiliİlçeyiSilToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.seçiliİlçeyiSilToolStripMenuItem.Text = "Seçili İlçeyi Sil";
            this.seçiliİlçeyiSilToolStripMenuItem.Click += new System.EventHandler(this.seçiliİlçeyiSilToolStripMenuItem_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // txtAra
            // 
            this.txtAra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtAra.Location = new System.Drawing.Point(12, 117);
            this.txtAra.Name = "txtAra";
            this.txtAra.Size = new System.Drawing.Size(338, 30);
            this.txtAra.TabIndex = 118;
            // 
            // btnAra
            // 
            this.btnAra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAra.Location = new System.Drawing.Point(356, 117);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(82, 33);
            this.btnAra.TabIndex = 119;
            this.btnAra.Text = "Bul";
            this.btnAra.UseVisualStyleBackColor = true;
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);
            // 
            // seçiliOkuluSilToolStripMenuItem
            // 
            this.seçiliOkuluSilToolStripMenuItem.Name = "seçiliOkuluSilToolStripMenuItem";
            this.seçiliOkuluSilToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.seçiliOkuluSilToolStripMenuItem.Text = "Seçili Okulu Sil";
            this.seçiliOkuluSilToolStripMenuItem.Click += new System.EventHandler(this.seçiliOkuluSilToolStripMenuItem_Click);
            // 
            // seçiliKayıtıSilToolStripMenuItem
            // 
            this.seçiliKayıtıSilToolStripMenuItem.Name = "seçiliKayıtıSilToolStripMenuItem";
            this.seçiliKayıtıSilToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.seçiliKayıtıSilToolStripMenuItem.Text = "Seçili Kayıtı Sil";
            this.seçiliKayıtıSilToolStripMenuItem.Click += new System.EventHandler(this.seçiliKayıtıSilToolStripMenuItem_Click);
            // 
            // özelSilmeToolStripMenuItem
            // 
            this.özelSilmeToolStripMenuItem.Name = "özelSilmeToolStripMenuItem";
            this.özelSilmeToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.özelSilmeToolStripMenuItem.Text = "Özel Silme";
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
            // yeniKayıtToolStripMenuItem
            // 
            this.yeniKayıtToolStripMenuItem.Name = "yeniKayıtToolStripMenuItem";
            this.yeniKayıtToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.yeniKayıtToolStripMenuItem.Text = "Yeni Kayıt";
            this.yeniKayıtToolStripMenuItem.Click += new System.EventHandler(this.yeniKayıtToolStripMenuItem_Click);
            // 
            // FormKutukIslemleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 733);
            this.Controls.Add(this.btnAra);
            this.Controls.Add(this.txtAra);
            this.Controls.Add(this.dgvKutuk);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.btnKutukDosyasiniAc);
            this.Name = "FormKutukIslemleri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kütük İşlemleri";
            this.Load += new System.EventHandler(this.FormKutukIslemleri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKutuk)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKutukDosyasiniAc;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dgvKutuk;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker bgwTumunuSil;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem seçiliİlçeyiSilToolStripMenuItem;
        private System.Windows.Forms.TextBox txtAra;
        private System.Windows.Forms.Button btnAra;
        private System.Windows.Forms.ToolStripMenuItem seçiliKayıtıSilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seçiliOkuluSilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem düzenleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem özelSilmeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yeniKayıtToolStripMenuItem;
    }
}