namespace ODM
{
    partial class FormKutukDbAktar
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnDBYukle = new System.Windows.Forms.Button();
            this.lblKutukBilgi = new System.Windows.Forms.Label();
            this.lblOgrenciSayisi = new System.Windows.Forms.Label();
            this.txtRapor = new System.Windows.Forms.TextBox();
            this.bgwOgrenciYukle = new System.ComponentModel.BackgroundWorker();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.btnKutukVerileriniSil = new System.Windows.Forms.Button();
            this.bgwKutukSil = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(10, 13);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(413, 24);
            this.progressBar1.TabIndex = 117;
            // 
            // btnDBYukle
            // 
            this.btnDBYukle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDBYukle.Location = new System.Drawing.Point(430, 8);
            this.btnDBYukle.Margin = new System.Windows.Forms.Padding(4);
            this.btnDBYukle.Name = "btnDBYukle";
            this.btnDBYukle.Size = new System.Drawing.Size(144, 33);
            this.btnDBYukle.TabIndex = 115;
            this.btnDBYukle.Text = "Öğrencileri Yükle";
            this.btnDBYukle.UseVisualStyleBackColor = true;
            this.btnDBYukle.Click += new System.EventHandler(this.btnDBYukle_Click);
            // 
            // lblKutukBilgi
            // 
            this.lblKutukBilgi.Location = new System.Drawing.Point(10, 88);
            this.lblKutukBilgi.Name = "lblKutukBilgi";
            this.lblKutukBilgi.Size = new System.Drawing.Size(558, 27);
            this.lblKutukBilgi.TabIndex = 118;
            this.lblKutukBilgi.Text = "...";
            // 
            // lblOgrenciSayisi
            // 
            this.lblOgrenciSayisi.Location = new System.Drawing.Point(10, 115);
            this.lblOgrenciSayisi.Name = "lblOgrenciSayisi";
            this.lblOgrenciSayisi.Size = new System.Drawing.Size(558, 29);
            this.lblOgrenciSayisi.TabIndex = 119;
            this.lblOgrenciSayisi.Text = "...";
            // 
            // txtRapor
            // 
            this.txtRapor.BackColor = System.Drawing.SystemColors.Control;
            this.txtRapor.Location = new System.Drawing.Point(10, 147);
            this.txtRapor.Multiline = true;
            this.txtRapor.Name = "txtRapor";
            this.txtRapor.Size = new System.Drawing.Size(558, 177);
            this.txtRapor.TabIndex = 120;
            // 
            // bgwOgrenciYukle
            // 
            this.bgwOgrenciYukle.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // lblBilgi
            // 
            this.lblBilgi.Location = new System.Drawing.Point(10, 41);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(558, 35);
            this.lblBilgi.TabIndex = 121;
            this.lblBilgi.Text = "Kütük işlemleri tamamlandıysa öğrenci tablosuna yükleyiniz. Mükerrer kayıt olmama" +
    "sı için kütük kayıtlarını siliniz.";
            // 
            // btnKutukVerileriniSil
            // 
            this.btnKutukVerileriniSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKutukVerileriniSil.Location = new System.Drawing.Point(383, 330);
            this.btnKutukVerileriniSil.Name = "btnKutukVerileriniSil";
            this.btnKutukVerileriniSil.Size = new System.Drawing.Size(185, 31);
            this.btnKutukVerileriniSil.TabIndex = 122;
            this.btnKutukVerileriniSil.Text = "Kütük verilerini sil";
            this.btnKutukVerileriniSil.UseVisualStyleBackColor = true;
            this.btnKutukVerileriniSil.Click += new System.EventHandler(this.btnKutukVerileriniSil_Click);
            // 
            // bgwKutukSil
            // 
            this.bgwKutukSil.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwKutukSil_DoWork);
            // 
            // FormKutukDbAktar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 368);
            this.Controls.Add(this.btnKutukVerileriniSil);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.txtRapor);
            this.Controls.Add(this.lblOgrenciSayisi);
            this.Controls.Add(this.lblKutukBilgi);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnDBYukle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKutukDbAktar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kütük Db Aktar";
            this.Load += new System.EventHandler(this.FormKutukDbAktar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnDBYukle;
        private System.Windows.Forms.Label lblKutukBilgi;
        private System.Windows.Forms.Label lblOgrenciSayisi;
        private System.Windows.Forms.TextBox txtRapor;
        private System.ComponentModel.BackgroundWorker bgwOgrenciYukle;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.Button btnKutukVerileriniSil;
        private System.ComponentModel.BackgroundWorker bgwKutukSil;
    }
}