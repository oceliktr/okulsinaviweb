namespace ODM.CKYazdirDb
{
    partial class FormSonDegerlendirme
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
            this.btnDegerlendirme1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolSslKalanSure = new System.Windows.Forms.ToolStripStatusLabel();
            this.bgwDegerlendirme1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bgwIlIlceOrtalamasi = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.bgwOgrenciOrtalamalari = new System.ComponentModel.BackgroundWorker();
            this.pbAna = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblGecenSure = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDegerlendirme1
            // 
            this.btnDegerlendirme1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDegerlendirme1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDegerlendirme1.Location = new System.Drawing.Point(678, 167);
            this.btnDegerlendirme1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDegerlendirme1.Name = "btnDegerlendirme1";
            this.btnDegerlendirme1.Size = new System.Drawing.Size(203, 53);
            this.btnDegerlendirme1.TabIndex = 0;
            this.btnDegerlendirme1.Text = "Değerlendirmeye Başla";
            this.btnDegerlendirme1.UseVisualStyleBackColor = true;
            this.btnDegerlendirme1.Click += new System.EventHandler(this.btnDegerlendirme1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSslKalanSure});
            this.statusStrip1.Location = new System.Drawing.Point(0, 237);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(896, 26);
            this.statusStrip1.TabIndex = 189;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolSslKalanSure
            // 
            this.toolSslKalanSure.Name = "toolSslKalanSure";
            this.toolSslKalanSure.Size = new System.Drawing.Size(18, 20);
            this.toolSslKalanSure.Text = "...";
            // 
            // bgwDegerlendirme1
            // 
            this.bgwDegerlendirme1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDegerlendirme1_DoWork);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 39);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(872, 26);
            this.progressBar1.TabIndex = 190;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(872, 43);
            this.label2.TabIndex = 197;
            this.label2.Text = "Cevap text dosyasını (mükerrer kayıtlar, doğru kodlanmamış cevaplar vb.) kontrol " +
    "ederek kütüğe kaydeder ve sonuçları değerlendirir. Bu işlem sonucunda webe yükle" +
    "necek dosyalar oluşturulacaktır.";
            // 
            // pbAna
            // 
            this.pbAna.Location = new System.Drawing.Point(12, 9);
            this.pbAna.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbAna.Name = "pbAna";
            this.pbAna.Size = new System.Drawing.Size(872, 26);
            this.pbAna.TabIndex = 198;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblGecenSure
            // 
            this.lblGecenSure.AutoSize = true;
            this.lblGecenSure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGecenSure.Location = new System.Drawing.Point(684, 147);
            this.lblGecenSure.Name = "lblGecenSure";
            this.lblGecenSure.Size = new System.Drawing.Size(174, 18);
            this.lblGecenSure.TabIndex = 199;
            this.lblGecenSure.Text = "Geçen süre : 00:00:00";
            // 
            // FormSonDegerlendirme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 263);
            this.Controls.Add(this.lblGecenSure);
            this.Controls.Add(this.pbAna);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnDegerlendirme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSonDegerlendirme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sınav Değerlendirme";
            this.Load += new System.EventHandler(this.FormSonDegerlendirme_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDegerlendirme1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolSslKalanSure;
        private System.ComponentModel.BackgroundWorker bgwDegerlendirme1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bgwIlIlceOrtalamasi;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker bgwOgrenciOrtalamalari;
        private System.Windows.Forms.ProgressBar pbAna;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblGecenSure;
    }
}