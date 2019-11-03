namespace ODM
{
    partial class FormCKHazirla
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbOturum = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblSinavNo = new System.Windows.Forms.Label();
            this.lblSinavAdi = new System.Windows.Forms.Label();
            this.bgwCkOlustur = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(1075, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(250, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cevap Kağıdı Oluştur";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.cbOturum);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblSinavNo);
            this.panel1.Controls.Add(this.lblSinavAdi);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1651, 100);
            this.panel1.TabIndex = 3;
            // 
            // cbOturum
            // 
            this.cbOturum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOturum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOturum.FormattingEnabled = true;
            this.cbOturum.Items.AddRange(new object[] {
            "1. Oturum",
            "2. Oturum",
            "3. Oturum",
            "4. Oturum",
            "5. Oturum",
            "6. Oturum"});
            this.cbOturum.Location = new System.Drawing.Point(1075, 19);
            this.cbOturum.Margin = new System.Windows.Forms.Padding(4);
            this.cbOturum.Name = "cbOturum";
            this.cbOturum.Size = new System.Drawing.Size(250, 24);
            this.cbOturum.TabIndex = 116;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(386, 48);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(682, 36);
            this.progressBar1.TabIndex = 115;
            // 
            // lblSinavNo
            // 
            this.lblSinavNo.AutoSize = true;
            this.lblSinavNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSinavNo.Location = new System.Drawing.Point(13, 64);
            this.lblSinavNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSinavNo.Name = "lblSinavNo";
            this.lblSinavNo.Size = new System.Drawing.Size(53, 20);
            this.lblSinavNo.TabIndex = 27;
            this.lblSinavNo.Text = "label1";
            // 
            // lblSinavAdi
            // 
            this.lblSinavAdi.AutoSize = true;
            this.lblSinavAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSinavAdi.Location = new System.Drawing.Point(13, 28);
            this.lblSinavAdi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSinavAdi.Name = "lblSinavAdi";
            this.lblSinavAdi.Size = new System.Drawing.Size(53, 20);
            this.lblSinavAdi.TabIndex = 26;
            this.lblSinavAdi.Text = "label1";
            // 
            // bgwCkOlustur
            // 
            this.bgwCkOlustur.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCkOlustur_DoWork);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ODM.Properties.Resources.Aralık_İzleme_Sınavı_A5_Dikey___Fen_Bilimleri;
            this.pictureBox1.Location = new System.Drawing.Point(2, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1649, 2265);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(856, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(212, 30);
            this.button2.TabIndex = 117;
            this.button2.Text = "Cevap Kağıdını Değiştir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormCKHazirla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1384, 735);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormCKHazirla";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCKHazirla";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSinavNo;
        private System.Windows.Forms.Label lblSinavAdi;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bgwCkOlustur;
        private System.Windows.Forms.ComboBox cbOturum;
        private System.Windows.Forms.Button button2;
    }
}