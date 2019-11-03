namespace ODM
{
    partial class FormCevaplar
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbBrans = new System.Windows.Forms.ComboBox();
            this.cbOturum = new System.Windows.Forms.ComboBox();
            this.lblSinavAdi = new System.Windows.Forms.Label();
            this.lblSinavId = new System.Windows.Forms.Label();
            this.cbSoruNo = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(675, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(441, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(71, 22);
            this.textBox1.TabIndex = 3;
            // 
            // cbBrans
            // 
            this.cbBrans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBrans.FormattingEnabled = true;
            this.cbBrans.Location = new System.Drawing.Point(135, 69);
            this.cbBrans.Margin = new System.Windows.Forms.Padding(4);
            this.cbBrans.Name = "cbBrans";
            this.cbBrans.Size = new System.Drawing.Size(135, 24);
            this.cbBrans.TabIndex = 130;
            // 
            // cbOturum
            // 
            this.cbOturum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOturum.FormattingEnabled = true;
            this.cbOturum.Location = new System.Drawing.Point(13, 69);
            this.cbOturum.Margin = new System.Windows.Forms.Padding(4);
            this.cbOturum.Name = "cbOturum";
            this.cbOturum.Size = new System.Drawing.Size(114, 24);
            this.cbOturum.TabIndex = 131;
            // 
            // lblSinavAdi
            // 
            this.lblSinavAdi.AutoSize = true;
            this.lblSinavAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSinavAdi.Location = new System.Drawing.Point(13, 39);
            this.lblSinavAdi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSinavAdi.Name = "lblSinavAdi";
            this.lblSinavAdi.Size = new System.Drawing.Size(59, 20);
            this.lblSinavAdi.TabIndex = 133;
            this.lblSinavAdi.Text = "label3";
            // 
            // lblSinavId
            // 
            this.lblSinavId.AutoSize = true;
            this.lblSinavId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSinavId.Location = new System.Drawing.Point(13, 9);
            this.lblSinavId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSinavId.Name = "lblSinavId";
            this.lblSinavId.Size = new System.Drawing.Size(19, 20);
            this.lblSinavId.TabIndex = 132;
            this.lblSinavId.Text = "0";
            // 
            // cbSoruNo
            // 
            this.cbSoruNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSoruNo.FormattingEnabled = true;
            this.cbSoruNo.Location = new System.Drawing.Point(278, 70);
            this.cbSoruNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbSoruNo.Name = "cbSoruNo";
            this.cbSoruNo.Size = new System.Drawing.Size(93, 24);
            this.cbSoruNo.TabIndex = 134;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(518, 71);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(71, 22);
            this.textBox2.TabIndex = 135;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(598, 71);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(71, 22);
            this.textBox3.TabIndex = 136;
            // 
            // FormCevaplar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 657);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.cbSoruNo);
            this.Controls.Add(this.lblSinavAdi);
            this.Controls.Add(this.lblSinavId);
            this.Controls.Add(this.cbOturum);
            this.Controls.Add(this.cbBrans);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "FormCevaplar";
            this.Text = "FormCevaplar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cbBrans;
        private System.Windows.Forms.ComboBox cbOturum;
        private System.Windows.Forms.Label lblSinavAdi;
        private System.Windows.Forms.Label lblSinavId;
        private System.Windows.Forms.ComboBox cbSoruNo;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
    }
}