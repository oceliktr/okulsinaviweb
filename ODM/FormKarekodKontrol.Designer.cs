namespace ODM
{
    partial class FormKarekodKontrol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKarekodKontrol));
            this.btnKarekod = new System.Windows.Forms.Button();
            this.pcKarekodOcr = new System.Windows.Forms.PictureBox();
            this.txtSonuc = new System.Windows.Forms.TextBox();
            this.btnOcr = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pcKarekodOcr)).BeginInit();
            this.SuspendLayout();
            // 
            // btnKarekod
            // 
            this.btnKarekod.Location = new System.Drawing.Point(14, 203);
            this.btnKarekod.Name = "btnKarekod";
            this.btnKarekod.Size = new System.Drawing.Size(92, 23);
            this.btnKarekod.TabIndex = 0;
            this.btnKarekod.Text = "Karekod Seç";
            this.btnKarekod.UseVisualStyleBackColor = true;
            this.btnKarekod.Click += new System.EventHandler(this.btnKarekod_Click);
            // 
            // pcKarekodOcr
            // 
            this.pcKarekodOcr.Location = new System.Drawing.Point(12, 12);
            this.pcKarekodOcr.Name = "pcKarekodOcr";
            this.pcKarekodOcr.Size = new System.Drawing.Size(192, 185);
            this.pcKarekodOcr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcKarekodOcr.TabIndex = 1;
            this.pcKarekodOcr.TabStop = false;
            // 
            // txtSonuc
            // 
            this.txtSonuc.Location = new System.Drawing.Point(14, 232);
            this.txtSonuc.Name = "txtSonuc";
            this.txtSonuc.Size = new System.Drawing.Size(190, 20);
            this.txtSonuc.TabIndex = 2;
            this.txtSonuc.TextChanged += new System.EventHandler(this.txtSonuc_TextChanged);
            // 
            // btnOcr
            // 
            this.btnOcr.Location = new System.Drawing.Point(112, 203);
            this.btnOcr.Name = "btnOcr";
            this.btnOcr.Size = new System.Drawing.Size(92, 23);
            this.btnOcr.TabIndex = 3;
            this.btnOcr.Text = "OCR Seç";
            this.btnOcr.UseVisualStyleBackColor = true;
            this.btnOcr.Click += new System.EventHandler(this.btnOcr_Click);
            // 
            // FormKarekodKontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 261);
            this.Controls.Add(this.btnOcr);
            this.Controls.Add(this.txtSonuc);
            this.Controls.Add(this.pcKarekodOcr);
            this.Controls.Add(this.btnKarekod);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormKarekodKontrol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Karekod / OCR Kontrol";
            ((System.ComponentModel.ISupportInitialize)(this.pcKarekodOcr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKarekod;
        private System.Windows.Forms.PictureBox pcKarekodOcr;
        private System.Windows.Forms.TextBox txtSonuc;
        private System.Windows.Forms.Button btnOcr;
    }
}