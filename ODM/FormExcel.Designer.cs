namespace ODM
{
    partial class FormExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExcel));
            this.dgvHata = new System.Windows.Forms.DataGridView();
            this.btnVerileriCek = new System.Windows.Forms.Button();
            this.btnDBYukle = new System.Windows.Forms.Button();
            this.cbSinavlar = new System.Windows.Forms.ComboBox();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.bgwDbEkle = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHata)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHata
            // 
            this.dgvHata.AllowUserToAddRows = false;
            this.dgvHata.AllowUserToDeleteRows = false;
            this.dgvHata.AllowUserToOrderColumns = true;
            this.dgvHata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHata.Location = new System.Drawing.Point(12, 86);
            this.dgvHata.Name = "dgvHata";
            this.dgvHata.ReadOnly = true;
            this.dgvHata.RowHeadersVisible = false;
            this.dgvHata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHata.Size = new System.Drawing.Size(950, 324);
            this.dgvHata.TabIndex = 20;
            // 
            // btnVerileriCek
            // 
            this.btnVerileriCek.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerileriCek.Location = new System.Drawing.Point(880, 12);
            this.btnVerileriCek.Name = "btnVerileriCek";
            this.btnVerileriCek.Size = new System.Drawing.Size(75, 35);
            this.btnVerileriCek.TabIndex = 21;
            this.btnVerileriCek.Text = "Exceli Seç";
            this.btnVerileriCek.UseVisualStyleBackColor = true;
            this.btnVerileriCek.Click += new System.EventHandler(this.btnVerileriCek_Click);
            // 
            // btnDBYukle
            // 
            this.btnDBYukle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDBYukle.Enabled = false;
            this.btnDBYukle.Location = new System.Drawing.Point(218, 12);
            this.btnDBYukle.Name = "btnDBYukle";
            this.btnDBYukle.Size = new System.Drawing.Size(108, 35);
            this.btnDBYukle.TabIndex = 22;
            this.btnDBYukle.Text = "Veritabanına Yükle";
            this.btnDBYukle.UseVisualStyleBackColor = true;
            this.btnDBYukle.Click += new System.EventHandler(this.btnDBYukle_Click);
            // 
            // cbSinavlar
            // 
            this.cbSinavlar.DropDownHeight = 200;
            this.cbSinavlar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSinavlar.Enabled = false;
            this.cbSinavlar.IntegralHeight = false;
            this.cbSinavlar.ItemHeight = 13;
            this.cbSinavlar.Location = new System.Drawing.Point(15, 20);
            this.cbSinavlar.Name = "cbSinavlar";
            this.cbSinavlar.Size = new System.Drawing.Size(197, 21);
            this.cbSinavlar.TabIndex = 111;
            // 
            // lblBilgi
            // 
            this.lblBilgi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBilgi.Location = new System.Drawing.Point(12, 50);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(961, 33);
            this.lblBilgi.TabIndex = 113;
            this.lblBilgi.Text = "Çalışma kitabı ismi Sayfa1 olan ve içeriği \'Kurum Kodu,Kurum adı,Tc kimlik,Adı,So" +
    "yadı,Okul No,Sınıfı,Şubesi\' başlıklarından oluşan excel dosyasını seçiniz.";
            this.lblBilgi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bgwDbEkle
            // 
            this.bgwDbEkle.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDbEkle_DoWork);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(332, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(542, 35);
            this.progressBar1.TabIndex = 114;
            // 
            // FormExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 440);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.cbSinavlar);
            this.Controls.Add(this.btnDBYukle);
            this.Controls.Add(this.btnVerileriCek);
            this.Controls.Add(this.dgvHata);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormExcel";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormExcel_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHata;
        private System.Windows.Forms.Button btnVerileriCek;
        private System.Windows.Forms.Button btnDBYukle;
        private System.Windows.Forms.ComboBox cbSinavlar;
        private System.Windows.Forms.Label lblBilgi;
        private System.ComponentModel.BackgroundWorker bgwDbEkle;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}