namespace ODM
{
    partial class FormKarne
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
            this.btnKarneHazirla = new System.Windows.Forms.Button();
            this.lblSinavId = new System.Windows.Forms.Label();
            this.lblSinavAdi = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.lblGecenSure = new System.Windows.Forms.Label();
            this.cbBilgisayariKapat = new System.Windows.Forms.CheckBox();
            this.lblBitisSuresi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnKarneHazirla
            // 
            this.btnKarneHazirla.Location = new System.Drawing.Point(709, 9);
            this.btnKarneHazirla.Name = "btnKarneHazirla";
            this.btnKarneHazirla.Size = new System.Drawing.Size(123, 42);
            this.btnKarneHazirla.TabIndex = 0;
            this.btnKarneHazirla.Text = "Sınıf Karnesi";
            this.btnKarneHazirla.UseVisualStyleBackColor = true;
            this.btnKarneHazirla.Click += new System.EventHandler(this.btnKarneHazirla_Click);
            // 
            // lblSinavId
            // 
            this.lblSinavId.AutoSize = true;
            this.lblSinavId.Location = new System.Drawing.Point(22, 9);
            this.lblSinavId.Name = "lblSinavId";
            this.lblSinavId.Size = new System.Drawing.Size(46, 17);
            this.lblSinavId.TabIndex = 1;
            this.lblSinavId.Text = "label1";
            // 
            // lblSinavAdi
            // 
            this.lblSinavAdi.AutoSize = true;
            this.lblSinavAdi.Location = new System.Drawing.Point(22, 36);
            this.lblSinavAdi.Name = "lblSinavAdi";
            this.lblSinavAdi.Size = new System.Drawing.Size(46, 17);
            this.lblSinavAdi.TabIndex = 2;
            this.lblSinavAdi.Text = "label2";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Blue;
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressBar1.ForeColor = System.Drawing.Color.GreenYellow;
            this.progressBar1.Location = new System.Drawing.Point(15, 68);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(817, 30);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 3;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // lblBilgi
            // 
            this.lblBilgi.Location = new System.Drawing.Point(15, 102);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(817, 35);
            this.lblBilgi.TabIndex = 4;
            this.lblBilgi.Text = "...";
            // 
            // lblGecenSure
            // 
            this.lblGecenSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGecenSure.BackColor = System.Drawing.Color.Transparent;
            this.lblGecenSure.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGecenSure.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGecenSure.Location = new System.Drawing.Point(488, 150);
            this.lblGecenSure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGecenSure.Name = "lblGecenSure";
            this.lblGecenSure.Size = new System.Drawing.Size(344, 23);
            this.lblGecenSure.TabIndex = 42;
            this.lblGecenSure.Text = "0 saat, 0 dakika, 0 saniye";
            this.lblGecenSure.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbBilgisayariKapat
            // 
            this.cbBilgisayariKapat.AutoSize = true;
            this.cbBilgisayariKapat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbBilgisayariKapat.Location = new System.Drawing.Point(603, 179);
            this.cbBilgisayariKapat.Name = "cbBilgisayariKapat";
            this.cbBilgisayariKapat.Size = new System.Drawing.Size(229, 21);
            this.cbBilgisayariKapat.TabIndex = 44;
            this.cbBilgisayariKapat.Text = "İşlemler bitince bilgisayarı kapat";
            this.cbBilgisayariKapat.UseVisualStyleBackColor = true;
            // 
            // lblBitisSuresi
            // 
            this.lblBitisSuresi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBitisSuresi.BackColor = System.Drawing.Color.Transparent;
            this.lblBitisSuresi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBitisSuresi.Location = new System.Drawing.Point(12, 150);
            this.lblBitisSuresi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBitisSuresi.Name = "lblBitisSuresi";
            this.lblBitisSuresi.Size = new System.Drawing.Size(485, 91);
            this.lblBitisSuresi.TabIndex = 43;
            this.lblBitisSuresi.Text = "0 saat, 0 dakika, 0 saniye";
            // 
            // FormKarne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 280);
            this.Controls.Add(this.cbBilgisayariKapat);
            this.Controls.Add(this.lblBitisSuresi);
            this.Controls.Add(this.lblGecenSure);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblSinavAdi);
            this.Controls.Add(this.lblSinavId);
            this.Controls.Add(this.btnKarneHazirla);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKarne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Karne";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKarneHazirla;
        private System.Windows.Forms.Label lblSinavId;
        private System.Windows.Forms.Label lblSinavAdi;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.Label lblGecenSure;
        private System.Windows.Forms.CheckBox cbBilgisayariKapat;
        private System.Windows.Forms.Label lblBitisSuresi;
    }
}