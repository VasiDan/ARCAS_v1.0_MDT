namespace Textexemplu2
{
    partial class PixelCalibration
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
            this.exitButton = new System.Windows.Forms.Button();
            this.tlpOuter = new System.Windows.Forms.TableLayoutPanel();
            this.upBack = new Emgu.CV.UI.ImageBox();
            this.upFront = new Emgu.CV.UI.ImageBox();
            this.downFront = new Emgu.CV.UI.ImageBox();
            this.downBack = new Emgu.CV.UI.ImageBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.Copy = new System.Windows.Forms.Button();
            this.tlpOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upFront)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downFront)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downBack)).BeginInit();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.IndianRed;
            this.exitButton.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(1277, 301);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(272, 32);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // tlpOuter
            // 
            this.tlpOuter.ColumnCount = 2;
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOuter.Controls.Add(this.upBack, 0, 1);
            this.tlpOuter.Controls.Add(this.upFront, 0, 0);
            this.tlpOuter.Controls.Add(this.downFront, 0, 0);
            this.tlpOuter.Controls.Add(this.downBack, 0, 0);
            this.tlpOuter.Location = new System.Drawing.Point(12, 41);
            this.tlpOuter.Name = "tlpOuter";
            this.tlpOuter.RowCount = 2;
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpOuter.Size = new System.Drawing.Size(1201, 848);
            this.tlpOuter.TabIndex = 1;
            // 
            // upBack
            // 
            this.upBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.upBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.upBack.Location = new System.Drawing.Point(603, 427);
            this.upBack.Name = "upBack";
            this.upBack.Size = new System.Drawing.Size(595, 418);
            this.upBack.TabIndex = 7;
            this.upBack.TabStop = false;
            this.upBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.upBack_MouseDown);
            // 
            // upFront
            // 
            this.upFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.upFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.upFront.Location = new System.Drawing.Point(3, 427);
            this.upFront.Name = "upFront";
            this.upFront.Size = new System.Drawing.Size(594, 418);
            this.upFront.TabIndex = 6;
            this.upFront.TabStop = false;
            this.upFront.MouseDown += new System.Windows.Forms.MouseEventHandler(this.upFront_MouseDown);
            // 
            // downFront
            // 
            this.downFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.downFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downFront.Location = new System.Drawing.Point(3, 3);
            this.downFront.Name = "downFront";
            this.downFront.Size = new System.Drawing.Size(594, 418);
            this.downFront.TabIndex = 5;
            this.downFront.TabStop = false;
            this.downFront.MouseDown += new System.Windows.Forms.MouseEventHandler(this.downFront_MouseDown);
            // 
            // downBack
            // 
            this.downBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.downBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downBack.Location = new System.Drawing.Point(603, 3);
            this.downBack.Name = "downBack";
            this.downBack.Size = new System.Drawing.Size(595, 418);
            this.downBack.TabIndex = 4;
            this.downBack.TabStop = false;
            this.downBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.downBack_MouseDown);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1219, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(397, 151);
            this.textBox1.TabIndex = 2;
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.clearButton.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(1277, 213);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(112, 51);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Copy
            // 
            this.Copy.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Copy.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Copy.Location = new System.Drawing.Point(1437, 213);
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(112, 51);
            this.Copy.TabIndex = 4;
            this.Copy.Text = "Copy";
            this.Copy.UseVisualStyleBackColor = false;
            this.Copy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // PixelCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1641, 928);
            this.Controls.Add(this.Copy);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tlpOuter);
            this.Controls.Add(this.exitButton);
            this.Name = "PixelCalibration";
            this.Text = "PixelCalibration";
            this.Load += new System.EventHandler(this.PixelCalibration_Load);
            this.tlpOuter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upFront)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downFront)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TableLayoutPanel tlpOuter;
        private Emgu.CV.UI.ImageBox upBack;
        private Emgu.CV.UI.ImageBox upFront;
        private Emgu.CV.UI.ImageBox downFront;
        private Emgu.CV.UI.ImageBox downBack;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button Copy;
    }
}