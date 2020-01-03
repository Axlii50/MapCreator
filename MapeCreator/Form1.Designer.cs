namespace MapeCreator
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.textBox_WidthCell = new System.Windows.Forms.TextBox();
            this.textBox_HeightCell = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSaveChar = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 68);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(263, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(465, 426);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseDown);
            this.pictureBox3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseMove);
            this.pictureBox3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseUp);
            // 
            // textBox_WidthCell
            // 
            this.textBox_WidthCell.Location = new System.Drawing.Point(12, 125);
            this.textBox_WidthCell.Name = "textBox_WidthCell";
            this.textBox_WidthCell.Size = new System.Drawing.Size(100, 20);
            this.textBox_WidthCell.TabIndex = 5;
            this.textBox_WidthCell.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_WidthCell_KeyDown);
            this.textBox_WidthCell.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_WidthCell_KeyUp);
            // 
            // textBox_HeightCell
            // 
            this.textBox_HeightCell.Location = new System.Drawing.Point(12, 151);
            this.textBox_HeightCell.Name = "textBox_HeightCell";
            this.textBox_HeightCell.Size = new System.Drawing.Size(100, 20);
            this.textBox_HeightCell.TabIndex = 6;
            this.textBox_HeightCell.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_WidthCell_KeyDown);
            this.textBox_HeightCell.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_WidthCell_KeyUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "stwórz siatke ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            this.button1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.button1_KeyUp);
            // 
            // buttonSaveChar
            // 
            this.buttonSaveChar.Location = new System.Drawing.Point(12, 233);
            this.buttonSaveChar.Name = "buttonSaveChar";
            this.buttonSaveChar.Size = new System.Drawing.Size(100, 23);
            this.buttonSaveChar.TabIndex = 10;
            this.buttonSaveChar.Text = "Save";
            this.buttonSaveChar.UseVisualStyleBackColor = true;
            this.buttonSaveChar.Click += new System.EventHandler(this.buttonSaveChar_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(12, 177);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 11;
            this.textBoxName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_WidthCell_KeyDown);
            this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_WidthCell_KeyUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonSaveChar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_HeightCell);
            this.Controls.Add(this.textBox_WidthCell);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox textBox_WidthCell;
        private System.Windows.Forms.TextBox textBox_HeightCell;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSaveChar;
        private System.Windows.Forms.TextBox textBoxName;
    }
}

