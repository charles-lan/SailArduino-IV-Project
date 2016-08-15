namespace SailArduino
{
    partial class Form1
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
            this.glControl1 = new OpenTK.GLControl();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.ComboCOMPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.savedw = new System.Windows.Forms.TextBox();
            this.savedx = new System.Windows.Forms.TextBox();
            this.savedy = new System.Windows.Forms.TextBox();
            this.savedz = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SaveQuat = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.IMU1 = new System.Windows.Forms.GroupBox();
            this.SaveAngles = new System.Windows.Forms.Button();
            this.IMU1.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(14, 345);
            this.glControl1.Margin = new System.Windows.Forms.Padding(5);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(1040, 184);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(36, 41);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(143, 40);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 81);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(622, 83);
            this.textBox1.TabIndex = 3;
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 230400;
            this.serialPort1.PortName = "COM3";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(25, 25);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(50, 22);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(25, 51);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(50, 22);
            this.textBox3.TabIndex = 5;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(25, 77);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(50, 22);
            this.textBox4.TabIndex = 6;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(25, 103);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(50, 22);
            this.textBox5.TabIndex = 7;
            // 
            // ComboCOMPort
            // 
            this.ComboCOMPort.FormattingEnabled = true;
            this.ComboCOMPort.Location = new System.Drawing.Point(266, 41);
            this.ComboCOMPort.Margin = new System.Windows.Forms.Padding(2);
            this.ComboCOMPort.Name = "ComboCOMPort";
            this.ComboCOMPort.Size = new System.Drawing.Size(95, 24);
            this.ComboCOMPort.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "w";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "z";
            // 
            // savedw
            // 
            this.savedw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.savedw.Location = new System.Drawing.Point(79, 25);
            this.savedw.Margin = new System.Windows.Forms.Padding(2);
            this.savedw.Name = "savedw";
            this.savedw.Size = new System.Drawing.Size(50, 22);
            this.savedw.TabIndex = 4;
            // 
            // savedx
            // 
            this.savedx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.savedx.Location = new System.Drawing.Point(79, 51);
            this.savedx.Margin = new System.Windows.Forms.Padding(2);
            this.savedx.Name = "savedx";
            this.savedx.Size = new System.Drawing.Size(50, 22);
            this.savedx.TabIndex = 5;
            // 
            // savedy
            // 
            this.savedy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.savedy.Location = new System.Drawing.Point(79, 77);
            this.savedy.Margin = new System.Windows.Forms.Padding(2);
            this.savedy.Name = "savedy";
            this.savedy.Size = new System.Drawing.Size(50, 22);
            this.savedy.TabIndex = 6;
            // 
            // savedz
            // 
            this.savedz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.savedz.Location = new System.Drawing.Point(79, 103);
            this.savedz.Margin = new System.Windows.Forms.Padding(2);
            this.savedz.Name = "savedz";
            this.savedz.Size = new System.Drawing.Size(50, 22);
            this.savedz.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(79, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Saved";
            // 
            // SaveQuat
            // 
            this.SaveQuat.Location = new System.Drawing.Point(409, 41);
            this.SaveQuat.Name = "SaveQuat";
            this.SaveQuat.Size = new System.Drawing.Size(94, 23);
            this.SaveQuat.TabIndex = 11;
            this.SaveQuat.Text = "Save Quat.";
            this.SaveQuat.UseVisualStyleBackColor = true;
            this.SaveQuat.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(136, 25);
            this.textBox6.Margin = new System.Windows.Forms.Padding(2);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(50, 22);
            this.textBox6.TabIndex = 4;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(136, 51);
            this.textBox7.Margin = new System.Windows.Forms.Padding(2);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(50, 22);
            this.textBox7.TabIndex = 5;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(136, 77);
            this.textBox8.Margin = new System.Windows.Forms.Padding(2);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(50, 22);
            this.textBox8.TabIndex = 6;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(136, 103);
            this.textBox9.Margin = new System.Windows.Forms.Padding(2);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(50, 22);
            this.textBox9.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(132, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Transition";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(79, 129);
            this.textBox10.Margin = new System.Windows.Forms.Padding(2);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(107, 22);
            this.textBox10.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(5, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Angle Diff.";
            // 
            // IMU1
            // 
            this.IMU1.Controls.Add(this.label7);
            this.IMU1.Controls.Add(this.label5);
            this.IMU1.Controls.Add(this.label6);
            this.IMU1.Controls.Add(this.label4);
            this.IMU1.Controls.Add(this.label2);
            this.IMU1.Controls.Add(this.label3);
            this.IMU1.Controls.Add(this.label1);
            this.IMU1.Controls.Add(this.savedz);
            this.IMU1.Controls.Add(this.savedy);
            this.IMU1.Controls.Add(this.textBox9);
            this.IMU1.Controls.Add(this.textBox5);
            this.IMU1.Controls.Add(this.textBox8);
            this.IMU1.Controls.Add(this.savedx);
            this.IMU1.Controls.Add(this.textBox4);
            this.IMU1.Controls.Add(this.textBox7);
            this.IMU1.Controls.Add(this.savedw);
            this.IMU1.Controls.Add(this.textBox10);
            this.IMU1.Controls.Add(this.textBox6);
            this.IMU1.Controls.Add(this.textBox3);
            this.IMU1.Controls.Add(this.textBox2);
            this.IMU1.Location = new System.Drawing.Point(14, 170);
            this.IMU1.Name = "IMU1";
            this.IMU1.Size = new System.Drawing.Size(203, 156);
            this.IMU1.TabIndex = 12;
            this.IMU1.TabStop = false;
            this.IMU1.Text = "IMU1";
            // 
            // SaveAngles
            // 
            this.SaveAngles.Location = new System.Drawing.Point(531, 41);
            this.SaveAngles.Name = "SaveAngles";
            this.SaveAngles.Size = new System.Drawing.Size(105, 23);
            this.SaveAngles.TabIndex = 14;
            this.SaveAngles.Text = "Save Angles";
            this.SaveAngles.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 543);
            this.Controls.Add(this.SaveAngles);
            this.Controls.Add(this.IMU1);
            this.Controls.Add(this.SaveQuat);
            this.Controls.Add(this.ComboCOMPort);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.glControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.IMU1.ResumeLayout(false);
            this.IMU1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBox1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox ComboCOMPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox savedw;
        private System.Windows.Forms.TextBox savedx;
        private System.Windows.Forms.TextBox savedy;
        private System.Windows.Forms.TextBox savedz;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SaveQuat;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox IMU1;
        private System.Windows.Forms.Button SaveAngles;
    }
}

