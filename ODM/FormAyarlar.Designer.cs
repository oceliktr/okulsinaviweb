namespace ODM
{
    partial class FormAyarlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAyarlar));
            this.txtCKBulunduguDizin = new System.Windows.Forms.TextBox();
            this.btnCKKontrolDizin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCKW = new System.Windows.Forms.TextBox();
            this.txtCKH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIlAdi = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCKBulunduguDizin
            // 
            this.txtCKBulunduguDizin.BackColor = System.Drawing.SystemColors.Menu;
            this.txtCKBulunduguDizin.Enabled = false;
            this.txtCKBulunduguDizin.Location = new System.Drawing.Point(12, 114);
            this.txtCKBulunduguDizin.Name = "txtCKBulunduguDizin";
            this.txtCKBulunduguDizin.Size = new System.Drawing.Size(342, 20);
            this.txtCKBulunduguDizin.TabIndex = 1;
            // 
            // btnCKKontrolDizin
            // 
            this.btnCKKontrolDizin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCKKontrolDizin.Location = new System.Drawing.Point(357, 114);
            this.btnCKKontrolDizin.Name = "btnCKKontrolDizin";
            this.btnCKKontrolDizin.Size = new System.Drawing.Size(33, 20);
            this.btnCKKontrolDizin.TabIndex = 2;
            this.btnCKKontrolDizin.Text = "...";
            this.btnCKKontrolDizin.UseVisualStyleBackColor = true;
            this.btnCKKontrolDizin.Click += new System.EventHandler(this.btnCKKontrolDizin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cevap kağıtlarının tutulacağı dizin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 105;
            this.label2.Text = "Cevap Kağıtlarının Boyutları";
            // 
            // txtCKW
            // 
            this.txtCKW.Location = new System.Drawing.Point(153, 163);
            this.txtCKW.Name = "txtCKW";
            this.txtCKW.Size = new System.Drawing.Size(30, 20);
            this.txtCKW.TabIndex = 106;
            this.txtCKW.Text = "800";
            // 
            // txtCKH
            // 
            this.txtCKH.Location = new System.Drawing.Point(204, 163);
            this.txtCKH.Name = "txtCKH";
            this.txtCKH.Size = new System.Drawing.Size(30, 20);
            this.txtCKH.TabIndex = 107;
            this.txtCKH.Text = "1200";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(181, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 17);
            this.label3.TabIndex = 108;
            this.label3.Text = " X";
            // 
            // btnKaydet
            // 
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.Location = new System.Drawing.Point(315, 190);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 35);
            this.btnKaydet.TabIndex = 109;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 110;
            this.label4.Text = "px";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 111;
            this.label5.Text = "İl Adı";
            // 
            // txtIlAdi
            // 
            this.txtIlAdi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIlAdi.Location = new System.Drawing.Point(12, 30);
            this.txtIlAdi.Name = "txtIlAdi";
            this.txtIlAdi.Size = new System.Drawing.Size(378, 20);
            this.txtIlAdi.TabIndex = 112;
            this.txtIlAdi.Text = "ERZURUM";
            // 
            // FormAyarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 237);
            this.Controls.Add(this.txtIlAdi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.txtCKH);
            this.Controls.Add(this.txtCKW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCKKontrolDizin);
            this.Controls.Add(this.txtCKBulunduguDizin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAyarlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ayarlar - Erzurum Ölçme ve Değerlendirme Merkezi";
            this.Activated += new System.EventHandler(this.FormAyarlar_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAyarlar_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCKBulunduguDizin;
        private System.Windows.Forms.Button btnCKKontrolDizin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCKW;
        private System.Windows.Forms.TextBox txtCKH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIlAdi;
    }
}