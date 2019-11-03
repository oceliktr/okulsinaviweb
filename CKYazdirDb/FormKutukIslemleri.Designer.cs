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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKutukIslemleri));
            this.btnKutukDosyasiniAc = new System.Windows.Forms.Button();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dgvKutuk = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bgwTumunuSil = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKutuk)).BeginInit();
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
            this.dgvKutuk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvKutuk.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvKutuk.Location = new System.Drawing.Point(12, 95);
            this.dgvKutuk.MultiSelect = false;
            this.dgvKutuk.Name = "dgvKutuk";
            this.dgvKutuk.RowHeadersVisible = false;
            this.dgvKutuk.RowHeadersWidth = 51;
            this.dgvKutuk.RowTemplate.Height = 24;
            this.dgvKutuk.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKutuk.ShowEditingIcon = false;
            this.dgvKutuk.ShowRowErrors = false;
            this.dgvKutuk.Size = new System.Drawing.Size(1229, 608);
            this.dgvKutuk.TabIndex = 117;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // FormKutukIslemleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 715);
            this.Controls.Add(this.dgvKutuk);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.btnKutukDosyasiniAc);
            this.Name = "FormKutukIslemleri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kütük İşlemleri";
            this.Load += new System.EventHandler(this.FormKutukIslemleri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKutuk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKutukDosyasiniAc;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dgvKutuk;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker bgwTumunuSil;
    }
}