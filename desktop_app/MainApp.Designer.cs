namespace Textexemplu2
{
    partial class Form2
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
            this.trackHueLow = new System.Windows.Forms.TrackBar();
            this.trackHueHigh = new System.Windows.Forms.TrackBar();
            this.tlpOuter = new System.Windows.Forms.TableLayoutPanel();
            this.DFwebcam = new Emgu.CV.UI.ImageBox();
            this.DFwebcamFiltered = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.DBwebcam = new Emgu.CV.UI.ImageBox();
            this.DBwebcamFiltered = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.UFwebcam = new Emgu.CV.UI.ImageBox();
            this.UFwebcamFiltered = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.UBwebcam = new Emgu.CV.UI.ImageBox();
            this.UBwebcamFiltered = new Emgu.CV.UI.ImageBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.trackValHigh = new System.Windows.Forms.TrackBar();
            this.trackSatHigh = new System.Windows.Forms.TrackBar();
            this.trackValLow = new System.Windows.Forms.TrackBar();
            this.trackSatLow = new System.Windows.Forms.TrackBar();
            this.lblValHigh = new System.Windows.Forms.Label();
            this.lblSatHigh = new System.Windows.Forms.Label();
            this.lblHueHigh = new System.Windows.Forms.Label();
            this.lblValLow = new System.Windows.Forms.Label();
            this.lblSatLow = new System.Windows.Forms.Label();
            this.lblHueLow = new System.Windows.Forms.Label();
            this.switchButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.dataAQ = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.fullCamera = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.kociembaTextBox = new System.Windows.Forms.TextBox();
            this.incomingMessagesTB = new System.Windows.Forms.TextBox();
            this.bldTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.resetBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.solveUsingKociembaBTN = new System.Windows.Forms.Button();
            this.solveUsingBLD = new System.Windows.Forms.Button();
            this.scrambleTheCubeBTN = new System.Windows.Forms.Button();
            this.randomScrambleBTN = new System.Windows.Forms.Button();
            this.gripTheCubeBTN = new System.Windows.Forms.Button();
            this.releaseTheCubeBTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.manualRB = new System.Windows.Forms.RadioButton();
            this.semiAutomaticRB = new System.Windows.Forms.RadioButton();
            this.automaticRB = new System.Windows.Forms.RadioButton();
            this.motorSpeedLabel = new System.Windows.Forms.Label();
            this.motorSpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sendMotorSpeed = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackHueLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackHueHigh)).BeginInit();
            this.tlpOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DFwebcam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DFwebcamFiltered)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBwebcam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBwebcamFiltered)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UFwebcam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UFwebcamFiltered)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UBwebcam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UBwebcamFiltered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackValHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSatHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackValLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSatLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorSpeedTrackBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackHueLow
            // 
            this.trackHueLow.Location = new System.Drawing.Point(887, 8);
            this.trackHueLow.Maximum = 180;
            this.trackHueLow.Name = "trackHueLow";
            this.trackHueLow.Size = new System.Drawing.Size(537, 45);
            this.trackHueLow.TabIndex = 97;
            this.trackHueLow.Scroll += new System.EventHandler(this.trackHueLow_Scroll);
            // 
            // trackHueHigh
            // 
            this.trackHueHigh.Location = new System.Drawing.Point(887, 46);
            this.trackHueHigh.Maximum = 180;
            this.trackHueHigh.Name = "trackHueHigh";
            this.trackHueHigh.Size = new System.Drawing.Size(537, 45);
            this.trackHueHigh.TabIndex = 98;
            this.trackHueHigh.Scroll += new System.EventHandler(this.trackHueHigh_Scroll);
            // 
            // tlpOuter
            // 
            this.tlpOuter.ColumnCount = 2;
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOuter.Controls.Add(this.DFwebcam, 0, 0);
            this.tlpOuter.Controls.Add(this.DFwebcamFiltered, 1, 0);
            this.tlpOuter.Location = new System.Drawing.Point(18, 83);
            this.tlpOuter.Name = "tlpOuter";
            this.tlpOuter.RowCount = 1;
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuter.Size = new System.Drawing.Size(365, 166);
            this.tlpOuter.TabIndex = 0;
            // 
            // DFwebcam
            // 
            this.DFwebcam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DFwebcam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DFwebcam.Location = new System.Drawing.Point(3, 3);
            this.DFwebcam.Name = "DFwebcam";
            this.DFwebcam.Size = new System.Drawing.Size(176, 160);
            this.DFwebcam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DFwebcam.TabIndex = 2;
            this.DFwebcam.TabStop = false;
            // 
            // DFwebcamFiltered
            // 
            this.DFwebcamFiltered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DFwebcamFiltered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DFwebcamFiltered.Location = new System.Drawing.Point(185, 3);
            this.DFwebcamFiltered.Name = "DFwebcamFiltered";
            this.DFwebcamFiltered.Size = new System.Drawing.Size(177, 160);
            this.DFwebcamFiltered.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DFwebcamFiltered.TabIndex = 2;
            this.DFwebcamFiltered.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.DBwebcam, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.DBwebcamFiltered, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(400, 83);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(365, 166);
            this.tableLayoutPanel2.TabIndex = 79;
            // 
            // DBwebcam
            // 
            this.DBwebcam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DBwebcam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBwebcam.Location = new System.Drawing.Point(3, 3);
            this.DBwebcam.Name = "DBwebcam";
            this.DBwebcam.Size = new System.Drawing.Size(176, 160);
            this.DBwebcam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DBwebcam.TabIndex = 2;
            this.DBwebcam.TabStop = false;
            // 
            // DBwebcamFiltered
            // 
            this.DBwebcamFiltered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DBwebcamFiltered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBwebcamFiltered.Location = new System.Drawing.Point(185, 3);
            this.DBwebcamFiltered.Name = "DBwebcamFiltered";
            this.DBwebcamFiltered.Size = new System.Drawing.Size(177, 160);
            this.DBwebcamFiltered.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DBwebcamFiltered.TabIndex = 2;
            this.DBwebcamFiltered.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.UFwebcam, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.UFwebcamFiltered, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(15, 303);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(365, 183);
            this.tableLayoutPanel3.TabIndex = 80;
            // 
            // UFwebcam
            // 
            this.UFwebcam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UFwebcam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UFwebcam.Location = new System.Drawing.Point(3, 3);
            this.UFwebcam.Name = "UFwebcam";
            this.UFwebcam.Size = new System.Drawing.Size(176, 177);
            this.UFwebcam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UFwebcam.TabIndex = 2;
            this.UFwebcam.TabStop = false;
            // 
            // UFwebcamFiltered
            // 
            this.UFwebcamFiltered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UFwebcamFiltered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UFwebcamFiltered.Location = new System.Drawing.Point(185, 3);
            this.UFwebcamFiltered.Name = "UFwebcamFiltered";
            this.UFwebcamFiltered.Size = new System.Drawing.Size(177, 177);
            this.UFwebcamFiltered.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UFwebcamFiltered.TabIndex = 2;
            this.UFwebcamFiltered.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.UBwebcam, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.UBwebcamFiltered, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(403, 303);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(362, 186);
            this.tableLayoutPanel4.TabIndex = 81;
            // 
            // UBwebcam
            // 
            this.UBwebcam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UBwebcam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UBwebcam.Location = new System.Drawing.Point(3, 3);
            this.UBwebcam.Name = "UBwebcam";
            this.UBwebcam.Size = new System.Drawing.Size(175, 180);
            this.UBwebcam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UBwebcam.TabIndex = 2;
            this.UBwebcam.TabStop = false;
            // 
            // UBwebcamFiltered
            // 
            this.UBwebcamFiltered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UBwebcamFiltered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UBwebcamFiltered.Location = new System.Drawing.Point(184, 3);
            this.UBwebcamFiltered.Name = "UBwebcamFiltered";
            this.UBwebcamFiltered.Size = new System.Drawing.Size(175, 180);
            this.UBwebcamFiltered.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UBwebcamFiltered.TabIndex = 2;
            this.UBwebcamFiltered.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("OCR A Extended", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(20, 274);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 17);
            this.label4.TabIndex = 86;
            this.label4.Text = "Camera 3 (Front-Up)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("OCR A Extended", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(403, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 17);
            this.label5.TabIndex = 87;
            this.label5.Text = "Camera 4 (Back-Up)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("OCR A Extended", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.SteelBlue;
            this.label6.Location = new System.Drawing.Point(20, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(218, 17);
            this.label6.TabIndex = 88;
            this.label6.Text = "Camera 1 (Front-Down)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("OCR A Extended", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(403, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(208, 17);
            this.label7.TabIndex = 89;
            this.label7.Text = "Camera 2 (Back-Down)";
            // 
            // trackValHigh
            // 
            this.trackValHigh.Location = new System.Drawing.Point(887, 222);
            this.trackValHigh.Maximum = 255;
            this.trackValHigh.Name = "trackValHigh";
            this.trackValHigh.Size = new System.Drawing.Size(537, 45);
            this.trackValHigh.TabIndex = 102;
            this.trackValHigh.Scroll += new System.EventHandler(this.trackValHigh_Scroll);
            // 
            // trackSatHigh
            // 
            this.trackSatHigh.Location = new System.Drawing.Point(887, 128);
            this.trackSatHigh.Maximum = 255;
            this.trackSatHigh.Name = "trackSatHigh";
            this.trackSatHigh.Size = new System.Drawing.Size(537, 45);
            this.trackSatHigh.TabIndex = 100;
            this.trackSatHigh.Scroll += new System.EventHandler(this.trackSatHigh_Scroll);
            // 
            // trackValLow
            // 
            this.trackValLow.Location = new System.Drawing.Point(887, 189);
            this.trackValLow.Maximum = 255;
            this.trackValLow.Name = "trackValLow";
            this.trackValLow.Size = new System.Drawing.Size(537, 45);
            this.trackValLow.TabIndex = 101;
            this.trackValLow.Scroll += new System.EventHandler(this.trackValLow_Scroll);
            // 
            // trackSatLow
            // 
            this.trackSatLow.Location = new System.Drawing.Point(887, 97);
            this.trackSatLow.Maximum = 255;
            this.trackSatLow.Name = "trackSatLow";
            this.trackSatLow.Size = new System.Drawing.Size(537, 45);
            this.trackSatLow.TabIndex = 99;
            this.trackSatLow.Scroll += new System.EventHandler(this.trackSatLow_Scroll);
            // 
            // lblValHigh
            // 
            this.lblValHigh.AutoSize = true;
            this.lblValHigh.Font = new System.Drawing.Font("OCR A Extended", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValHigh.ForeColor = System.Drawing.Color.Teal;
            this.lblValHigh.Location = new System.Drawing.Point(781, 222);
            this.lblValHigh.Name = "lblValHigh";
            this.lblValHigh.Size = new System.Drawing.Size(101, 12);
            this.lblValHigh.TabIndex = 108;
            this.lblValHigh.Text = "High Val = 0";
            // 
            // lblSatHigh
            // 
            this.lblSatHigh.AutoSize = true;
            this.lblSatHigh.Font = new System.Drawing.Font("OCR A Extended", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSatHigh.ForeColor = System.Drawing.Color.Teal;
            this.lblSatHigh.Location = new System.Drawing.Point(781, 130);
            this.lblSatHigh.Name = "lblSatHigh";
            this.lblSatHigh.Size = new System.Drawing.Size(101, 12);
            this.lblSatHigh.TabIndex = 106;
            this.lblSatHigh.Text = "High Sat = 0";
            // 
            // lblHueHigh
            // 
            this.lblHueHigh.AutoSize = true;
            this.lblHueHigh.Font = new System.Drawing.Font("OCR A Extended", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHueHigh.ForeColor = System.Drawing.Color.Teal;
            this.lblHueHigh.Location = new System.Drawing.Point(773, 46);
            this.lblHueHigh.Name = "lblHueHigh";
            this.lblHueHigh.Size = new System.Drawing.Size(109, 12);
            this.lblHueHigh.TabIndex = 104;
            this.lblHueHigh.Text = "High Hue = 0°";
            // 
            // lblValLow
            // 
            this.lblValLow.AutoSize = true;
            this.lblValLow.Font = new System.Drawing.Font("OCR A Extended", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValLow.ForeColor = System.Drawing.Color.Teal;
            this.lblValLow.Location = new System.Drawing.Point(789, 193);
            this.lblValLow.Name = "lblValLow";
            this.lblValLow.Size = new System.Drawing.Size(93, 12);
            this.lblValLow.TabIndex = 107;
            this.lblValLow.Text = "Low Val = 0";
            // 
            // lblSatLow
            // 
            this.lblSatLow.AutoSize = true;
            this.lblSatLow.Font = new System.Drawing.Font("OCR A Extended", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSatLow.ForeColor = System.Drawing.Color.Teal;
            this.lblSatLow.Location = new System.Drawing.Point(788, 106);
            this.lblSatLow.Name = "lblSatLow";
            this.lblSatLow.Size = new System.Drawing.Size(93, 12);
            this.lblSatLow.TabIndex = 105;
            this.lblSatLow.Text = "Low Sat = 0";
            // 
            // lblHueLow
            // 
            this.lblHueLow.AutoSize = true;
            this.lblHueLow.Font = new System.Drawing.Font("OCR A Extended", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHueLow.ForeColor = System.Drawing.Color.Teal;
            this.lblHueLow.Location = new System.Drawing.Point(780, 16);
            this.lblHueLow.Name = "lblHueLow";
            this.lblHueLow.Size = new System.Drawing.Size(101, 12);
            this.lblHueLow.TabIndex = 103;
            this.lblHueLow.Text = "Low Hue = 0°";
            // 
            // switchButton
            // 
            this.switchButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.switchButton.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switchButton.Location = new System.Drawing.Point(244, 5);
            this.switchButton.Name = "switchButton";
            this.switchButton.Size = new System.Drawing.Size(116, 37);
            this.switchButton.TabIndex = 113;
            this.switchButton.Text = "Manual";
            this.switchButton.UseVisualStyleBackColor = false;
            this.switchButton.Click += new System.EventHandler(this.switchButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.IndianRed;
            this.exitButton.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(607, 6);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(116, 35);
            this.exitButton.TabIndex = 114;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // dataAQ
            // 
            this.dataAQ.AutoSize = true;
            this.dataAQ.Font = new System.Drawing.Font("OCR A Extended", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataAQ.ForeColor = System.Drawing.Color.Maroon;
            this.dataAQ.Location = new System.Drawing.Point(4, 9);
            this.dataAQ.Name = "dataAQ";
            this.dataAQ.Size = new System.Drawing.Size(234, 23);
            this.dataAQ.TabIndex = 115;
            this.dataAQ.Text = "Data acquisition";
            // 
            // Connect
            // 
            this.Connect.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Connect.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Connect.ForeColor = System.Drawing.Color.DodgerBlue;
            this.Connect.Location = new System.Drawing.Point(23, 551);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(128, 39);
            this.Connect.TabIndex = 121;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = false;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.Font = new System.Drawing.Font("OCR A Extended", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(169, 562);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 122;
            // 
            // fullCamera
            // 
            this.fullCamera.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fullCamera.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullCamera.Location = new System.Drawing.Point(366, 5);
            this.fullCamera.Name = "fullCamera";
            this.fullCamera.Size = new System.Drawing.Size(235, 36);
            this.fullCamera.TabIndex = 133;
            this.fullCamera.Text = "Colors Extraction Setup";
            this.fullCamera.UseVisualStyleBackColor = false;
            this.fullCamera.Click += new System.EventHandler(this.fullCamera_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("OCR A Extended", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Teal;
            this.label8.Location = new System.Drawing.Point(780, 274);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 17);
            this.label8.TabIndex = 137;
            this.label8.Text = "Kociemba";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("OCR A Extended", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Teal;
            this.label9.Location = new System.Drawing.Point(780, 469);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 17);
            this.label9.TabIndex = 138;
            this.label9.Text = "Blindfolded";
            // 
            // kociembaTextBox
            // 
            this.kociembaTextBox.Location = new System.Drawing.Point(782, 303);
            this.kociembaTextBox.Multiline = true;
            this.kociembaTextBox.Name = "kociembaTextBox";
            this.kociembaTextBox.ReadOnly = true;
            this.kociembaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.kociembaTextBox.Size = new System.Drawing.Size(642, 147);
            this.kociembaTextBox.TabIndex = 139;
            // 
            // incomingMessagesTB
            // 
            this.incomingMessagesTB.Location = new System.Drawing.Point(395, 531);
            this.incomingMessagesTB.Multiline = true;
            this.incomingMessagesTB.Name = "incomingMessagesTB";
            this.incomingMessagesTB.ReadOnly = true;
            this.incomingMessagesTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.incomingMessagesTB.Size = new System.Drawing.Size(370, 72);
            this.incomingMessagesTB.TabIndex = 141;
            // 
            // bldTextBox
            // 
            this.bldTextBox.Location = new System.Drawing.Point(782, 492);
            this.bldTextBox.Multiline = true;
            this.bldTextBox.Name = "bldTextBox";
            this.bldTextBox.ReadOnly = true;
            this.bldTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.bldTextBox.Size = new System.Drawing.Size(642, 147);
            this.bldTextBox.TabIndex = 142;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("OCR A Extended", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(397, 508);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 17);
            this.label3.TabIndex = 143;
            this.label3.Text = "Incoming messages";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Inception", 99.74999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.CadetBlue;
            this.label10.Location = new System.Drawing.Point(784, 671);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(544, 130);
            this.label10.TabIndex = 144;
            this.label10.Text = "ARCAS";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("OCR A Extended", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.CadetBlue;
            this.label11.Location = new System.Drawing.Point(1291, 752);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 23);
            this.label11.TabIndex = 145;
            this.label11.Text = "v1.1 MDT";
            // 
            // resetBTN
            // 
            this.resetBTN.BackColor = System.Drawing.Color.IndianRed;
            this.resetBTN.Font = new System.Drawing.Font("OCR A Extended", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetBTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.resetBTN.Location = new System.Drawing.Point(23, 617);
            this.resetBTN.Name = "resetBTN";
            this.resetBTN.Size = new System.Drawing.Size(354, 46);
            this.resetBTN.TabIndex = 146;
            this.resetBTN.Text = "Reset";
            this.resetBTN.UseVisualStyleBackColor = false;
            this.resetBTN.Click += new System.EventHandler(this.resetBTN_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("OCR A Extended", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(17, 502);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 23);
            this.label1.TabIndex = 147;
            this.label1.Text = "Command Panel";
            // 
            // solveUsingKociembaBTN
            // 
            this.solveUsingKociembaBTN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.solveUsingKociembaBTN.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solveUsingKociembaBTN.Location = new System.Drawing.Point(21, 687);
            this.solveUsingKociembaBTN.Name = "solveUsingKociembaBTN";
            this.solveUsingKociembaBTN.Size = new System.Drawing.Size(171, 54);
            this.solveUsingKociembaBTN.TabIndex = 148;
            this.solveUsingKociembaBTN.Text = "Kociemba Solve";
            this.solveUsingKociembaBTN.UseVisualStyleBackColor = false;
            this.solveUsingKociembaBTN.Click += new System.EventHandler(this.solveUsingKociembaBTN_Click);
            // 
            // solveUsingBLD
            // 
            this.solveUsingBLD.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.solveUsingBLD.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.solveUsingBLD.Location = new System.Drawing.Point(203, 687);
            this.solveUsingBLD.Name = "solveUsingBLD";
            this.solveUsingBLD.Size = new System.Drawing.Size(171, 54);
            this.solveUsingBLD.TabIndex = 149;
            this.solveUsingBLD.Text = "Blindfolded Solve";
            this.solveUsingBLD.UseVisualStyleBackColor = false;
            this.solveUsingBLD.Click += new System.EventHandler(this.solveUsingBLD_Click);
            // 
            // scrambleTheCubeBTN
            // 
            this.scrambleTheCubeBTN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.scrambleTheCubeBTN.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrambleTheCubeBTN.Location = new System.Drawing.Point(21, 747);
            this.scrambleTheCubeBTN.Name = "scrambleTheCubeBTN";
            this.scrambleTheCubeBTN.Size = new System.Drawing.Size(171, 54);
            this.scrambleTheCubeBTN.TabIndex = 150;
            this.scrambleTheCubeBTN.Text = "Scramble the cube";
            this.scrambleTheCubeBTN.UseVisualStyleBackColor = false;
            this.scrambleTheCubeBTN.Click += new System.EventHandler(this.scrambleTheCubeBTN_Click);
            // 
            // randomScrambleBTN
            // 
            this.randomScrambleBTN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.randomScrambleBTN.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.randomScrambleBTN.Location = new System.Drawing.Point(203, 747);
            this.randomScrambleBTN.Name = "randomScrambleBTN";
            this.randomScrambleBTN.Size = new System.Drawing.Size(171, 54);
            this.randomScrambleBTN.TabIndex = 151;
            this.randomScrambleBTN.Text = "Random pattern";
            this.randomScrambleBTN.UseVisualStyleBackColor = false;
            this.randomScrambleBTN.Click += new System.EventHandler(this.randomScrambleBTN_Click);
            // 
            // gripTheCubeBTN
            // 
            this.gripTheCubeBTN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gripTheCubeBTN.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gripTheCubeBTN.Location = new System.Drawing.Point(395, 747);
            this.gripTheCubeBTN.Name = "gripTheCubeBTN";
            this.gripTheCubeBTN.Size = new System.Drawing.Size(171, 54);
            this.gripTheCubeBTN.TabIndex = 152;
            this.gripTheCubeBTN.Text = "Grip the cube";
            this.gripTheCubeBTN.UseVisualStyleBackColor = false;
            this.gripTheCubeBTN.Click += new System.EventHandler(this.gripTheCubeBTN_Click);
            // 
            // releaseTheCubeBTN
            // 
            this.releaseTheCubeBTN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.releaseTheCubeBTN.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.releaseTheCubeBTN.Location = new System.Drawing.Point(572, 747);
            this.releaseTheCubeBTN.Name = "releaseTheCubeBTN";
            this.releaseTheCubeBTN.Size = new System.Drawing.Size(171, 54);
            this.releaseTheCubeBTN.TabIndex = 153;
            this.releaseTheCubeBTN.Text = "Release the cube";
            this.releaseTheCubeBTN.UseVisualStyleBackColor = false;
            this.releaseTheCubeBTN.Click += new System.EventHandler(this.releaseTheCubeBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("OCR A Extended", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(393, 687);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 154;
            this.label2.Text = "Grip mode";
            // 
            // manualRB
            // 
            this.manualRB.AutoSize = true;
            this.manualRB.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualRB.Location = new System.Drawing.Point(3, 3);
            this.manualRB.Name = "manualRB";
            this.manualRB.Size = new System.Drawing.Size(79, 17);
            this.manualRB.TabIndex = 155;
            this.manualRB.TabStop = true;
            this.manualRB.Text = "Manual";
            this.manualRB.UseVisualStyleBackColor = true;
            this.manualRB.CheckedChanged += new System.EventHandler(this.manualRB_CheckedChanged);
            // 
            // semiAutomaticRB
            // 
            this.semiAutomaticRB.AutoSize = true;
            this.semiAutomaticRB.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.semiAutomaticRB.Location = new System.Drawing.Point(88, 3);
            this.semiAutomaticRB.Name = "semiAutomaticRB";
            this.semiAutomaticRB.Size = new System.Drawing.Size(151, 17);
            this.semiAutomaticRB.TabIndex = 156;
            this.semiAutomaticRB.TabStop = true;
            this.semiAutomaticRB.Text = "Semi-automatic";
            this.semiAutomaticRB.UseVisualStyleBackColor = true;
            this.semiAutomaticRB.CheckedChanged += new System.EventHandler(this.semiAutomaticRB_CheckedChanged);
            // 
            // automaticRB
            // 
            this.automaticRB.AutoSize = true;
            this.automaticRB.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.automaticRB.Location = new System.Drawing.Point(245, 3);
            this.automaticRB.Name = "automaticRB";
            this.automaticRB.Size = new System.Drawing.Size(106, 17);
            this.automaticRB.TabIndex = 157;
            this.automaticRB.TabStop = true;
            this.automaticRB.Text = "Automatic";
            this.automaticRB.UseVisualStyleBackColor = true;
            this.automaticRB.CheckedChanged += new System.EventHandler(this.automaticRB_CheckedChanged);
            // 
            // motorSpeedLabel
            // 
            this.motorSpeedLabel.AutoSize = true;
            this.motorSpeedLabel.Font = new System.Drawing.Font("OCR A Extended", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motorSpeedLabel.ForeColor = System.Drawing.Color.SteelBlue;
            this.motorSpeedLabel.Location = new System.Drawing.Point(393, 617);
            this.motorSpeedLabel.Name = "motorSpeedLabel";
            this.motorSpeedLabel.Size = new System.Drawing.Size(239, 17);
            this.motorSpeedLabel.TabIndex = 158;
            this.motorSpeedLabel.Text = "Motors speed: 160 RPM";
            // 
            // motorSpeedTrackBar
            // 
            this.motorSpeedTrackBar.Location = new System.Drawing.Point(392, 639);
            this.motorSpeedTrackBar.Maximum = 200;
            this.motorSpeedTrackBar.Minimum = 1;
            this.motorSpeedTrackBar.Name = "motorSpeedTrackBar";
            this.motorSpeedTrackBar.Size = new System.Drawing.Size(306, 45);
            this.motorSpeedTrackBar.TabIndex = 159;
            this.motorSpeedTrackBar.Value = 1;
            this.motorSpeedTrackBar.Scroll += new System.EventHandler(this.motorSpeedTrackBar_Scroll);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.automaticRB);
            this.panel1.Controls.Add(this.semiAutomaticRB);
            this.panel1.Controls.Add(this.manualRB);
            this.panel1.Location = new System.Drawing.Point(400, 713);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 28);
            this.panel1.TabIndex = 160;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // sendMotorSpeed
            // 
            this.sendMotorSpeed.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.sendMotorSpeed.Font = new System.Drawing.Font("OCR A Extended", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendMotorSpeed.Location = new System.Drawing.Point(691, 630);
            this.sendMotorSpeed.Name = "sendMotorSpeed";
            this.sendMotorSpeed.Size = new System.Drawing.Size(74, 42);
            this.sendMotorSpeed.TabIndex = 161;
            this.sendMotorSpeed.Text = "Update";
            this.sendMotorSpeed.UseVisualStyleBackColor = false;
            this.sendMotorSpeed.Click += new System.EventHandler(this.sendMotorSpeed_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 818);
            this.Controls.Add(this.sendMotorSpeed);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.motorSpeedTrackBar);
            this.Controls.Add(this.motorSpeedLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.releaseTheCubeBTN);
            this.Controls.Add(this.gripTheCubeBTN);
            this.Controls.Add(this.randomScrambleBTN);
            this.Controls.Add(this.scrambleTheCubeBTN);
            this.Controls.Add(this.solveUsingBLD);
            this.Controls.Add(this.solveUsingKociembaBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resetBTN);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bldTextBox);
            this.Controls.Add(this.incomingMessagesTB);
            this.Controls.Add(this.kociembaTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.fullCamera);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.dataAQ);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.switchButton);
            this.Controls.Add(this.lblValHigh);
            this.Controls.Add(this.lblSatHigh);
            this.Controls.Add(this.lblHueHigh);
            this.Controls.Add(this.lblValLow);
            this.Controls.Add(this.lblSatLow);
            this.Controls.Add(this.lblHueLow);
            this.Controls.Add(this.trackValHigh);
            this.Controls.Add(this.trackSatHigh);
            this.Controls.Add(this.trackValLow);
            this.Controls.Add(this.trackSatLow);
            this.Controls.Add(this.trackHueHigh);
            this.Controls.Add(this.trackHueLow);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tlpOuter);
            this.Name = "Form2";
            this.Text = "Arcas v1.0 MDT";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackHueLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackHueHigh)).EndInit();
            this.tlpOuter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DFwebcam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DFwebcamFiltered)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DBwebcam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBwebcamFiltered)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UFwebcam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UFwebcamFiltered)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UBwebcam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UBwebcamFiltered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackValHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSatHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackValLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSatLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.motorSpeedTrackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpOuter;
        private Emgu.CV.UI.ImageBox DFwebcam;
        private Emgu.CV.UI.ImageBox DFwebcamFiltered;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Emgu.CV.UI.ImageBox DBwebcam;
        private Emgu.CV.UI.ImageBox DBwebcamFiltered;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Emgu.CV.UI.ImageBox UFwebcam;
        private Emgu.CV.UI.ImageBox UFwebcamFiltered;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Emgu.CV.UI.ImageBox UBwebcam;
        private Emgu.CV.UI.ImageBox UBwebcamFiltered;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackValHigh;
        private System.Windows.Forms.TrackBar trackSatHigh;
        private System.Windows.Forms.TrackBar trackValLow;
        private System.Windows.Forms.TrackBar trackSatLow;
        private System.Windows.Forms.Label lblValHigh;
        private System.Windows.Forms.Label lblSatHigh;
        private System.Windows.Forms.Label lblHueHigh;
        private System.Windows.Forms.Label lblValLow;
        private System.Windows.Forms.Label lblSatLow;
        private System.Windows.Forms.Label lblHueLow;
        private System.Windows.Forms.Button switchButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label dataAQ;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button fullCamera;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox kociembaTextBox;
        private System.Windows.Forms.TrackBar trackHueLow;
        private System.Windows.Forms.TrackBar trackHueHigh;
        private System.Windows.Forms.TextBox incomingMessagesTB;
        private System.Windows.Forms.TextBox bldTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button resetBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button solveUsingKociembaBTN;
        private System.Windows.Forms.Button solveUsingBLD;
        private System.Windows.Forms.Button scrambleTheCubeBTN;
        private System.Windows.Forms.Button randomScrambleBTN;
        private System.Windows.Forms.Button gripTheCubeBTN;
        private System.Windows.Forms.Button releaseTheCubeBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton manualRB;
        private System.Windows.Forms.RadioButton semiAutomaticRB;
        private System.Windows.Forms.RadioButton automaticRB;
        private System.Windows.Forms.Label motorSpeedLabel;
        private System.Windows.Forms.TrackBar motorSpeedTrackBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button sendMotorSpeed;
    }
}