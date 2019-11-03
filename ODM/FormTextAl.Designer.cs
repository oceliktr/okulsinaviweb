namespace ODM
{
    partial class FormTextAl
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
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnTxteCevir = new System.Windows.Forms.Button();
            this.lblBilgi = new System.Windows.Forms.Label();
            this.lblGecenSure = new System.Windows.Forms.Label();
            this.btnDbKaydetEokul = new System.Windows.Forms.Button();
            this.lblBitisSuresi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Dosya Al";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 80);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1014, 436);
            this.listBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(131, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 41);
            this.button2.TabIndex = 3;
            this.button2.Text = "Db Kaydet";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Blue;
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressBar1.ForeColor = System.Drawing.Color.GreenYellow;
            this.progressBar1.Location = new System.Drawing.Point(416, 33);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(613, 40);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 4;
            // 
            // btnTxteCevir
            // 
            this.btnTxteCevir.Location = new System.Drawing.Point(16, 522);
            this.btnTxteCevir.Name = "btnTxteCevir";
            this.btnTxteCevir.Size = new System.Drawing.Size(113, 41);
            this.btnTxteCevir.TabIndex = 5;
            this.btnTxteCevir.Text = "Texte Çevir";
            this.btnTxteCevir.UseVisualStyleBackColor = true;
            this.btnTxteCevir.Click += new System.EventHandler(this.btnTxteCevir_Click);
            // 
            // lblBilgi
            // 
            this.lblBilgi.BackColor = System.Drawing.Color.Transparent;
            this.lblBilgi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBilgi.Location = new System.Drawing.Point(13, 566);
            this.lblBilgi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBilgi.Name = "lblBilgi";
            this.lblBilgi.Size = new System.Drawing.Size(757, 47);
            this.lblBilgi.TabIndex = 6;
            this.lblBilgi.Text = "...";
            this.lblBilgi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGecenSure
            // 
            this.lblGecenSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGecenSure.BackColor = System.Drawing.Color.Transparent;
            this.lblGecenSure.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblGecenSure.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGecenSure.Location = new System.Drawing.Point(513, 667);
            this.lblGecenSure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGecenSure.Name = "lblGecenSure";
            this.lblGecenSure.Size = new System.Drawing.Size(516, 18);
            this.lblGecenSure.TabIndex = 42;
            this.lblGecenSure.Text = "0 saat, 0 dakika, 0 saniye";
            this.lblGecenSure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDbKaydetEokul
            // 
            this.btnDbKaydetEokul.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDbKaydetEokul.Location = new System.Drawing.Point(263, 33);
            this.btnDbKaydetEokul.Name = "btnDbKaydetEokul";
            this.btnDbKaydetEokul.Size = new System.Drawing.Size(146, 41);
            this.btnDbKaydetEokul.TabIndex = 43;
            this.btnDbKaydetEokul.Text = "Db Kaydet (E-okul)";
            this.btnDbKaydetEokul.UseVisualStyleBackColor = true;
            this.btnDbKaydetEokul.Click += new System.EventHandler(this.btnDbKaydetEokul_Click);
            // 
            // lblBitisSuresi
            // 
            this.lblBitisSuresi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBitisSuresi.BackColor = System.Drawing.Color.Transparent;
            this.lblBitisSuresi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBitisSuresi.Location = new System.Drawing.Point(13, 660);
            this.lblBitisSuresi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBitisSuresi.Name = "lblBitisSuresi";
            this.lblBitisSuresi.Size = new System.Drawing.Size(485, 25);
            this.lblBitisSuresi.TabIndex = 45;
            this.lblBitisSuresi.Text = "0 saat, 0 dakika, 0 saniye";
            // 
            // FormTextAl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 694);
            this.Controls.Add(this.lblBitisSuresi);
            this.Controls.Add(this.btnDbKaydetEokul);
            this.Controls.Add(this.lblGecenSure);
            this.Controls.Add(this.lblBilgi);
            this.Controls.Add(this.btnTxteCevir);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Name = "FormTextAl";
            this.Text = "FormTextAl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnTxteCevir;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.Label lblGecenSure;
        private System.Windows.Forms.Button btnDbKaydetEokul;
        private System.Windows.Forms.Label lblBitisSuresi;
    }
}