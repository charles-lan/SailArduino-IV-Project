using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace SailArduino
{
    static class GLDraw
    {
        static GLDraw()
        {
        }

        public static void Axis()
        {
            GL.LineWidth(2f);
            GL.Begin(PrimitiveType.Lines);

            GL.Color3(1f, 0f, 0f);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(50f, 0f, 0f);

            GL.Color3(0f, 1f, 0f);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0f, 50f, 0f);

            GL.Color3(0f, 0f, 1f);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0f, 0f, 50f);

            GL.End();
            GL.LineWidth(1f);
        }

    }

    public partial class Form1 : Form
    {
        bool loaded = false;
        string RxString;
        double xrot, yrot, zrot;



        public Form1()
        {

            InitializeComponent();
            GetAvailablePorts();
        }

        private void GetAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();

        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.ClearColor(Color.SkyBlue);
            SetupViewport();

            GL.Enable(EnableCap.DepthTest);
        }

        private void SetupViewport()
        {
            int w = glControl1.Width;
            int h = glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, w, 0, h, -1, 1); // Bottom-left corner pixel has coordinate (0, 0)
            GL.Viewport(0, 0, w, h); // Use all of the glControl painting area
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded) // Don't start drawing till loaded.
                return;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            //For now, try draw everything in immediate mode. 
            GL.Translate(50, 50, 0);
            

            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex3(10, 20,10);
            GL.Vertex3(100, 20,10);
            GL.Vertex3(100, 50,10);
            GL.End();

            GLDraw.Axis();
            glControl1.Invalidate();
            glControl1.SwapBuffers();
        }





        private void buttonStart_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM15";
            serialPort1.BaudRate = 230400;
            serialPort1.Handshake = Handshake.None;
            serialPort1.Encoding = System.Text.Encoding.Default;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.DtrEnable = true;

            serialPort1.Open();
            if (serialPort1.IsOpen)
            {
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            RxString = serialPort1.ReadLine();
            this.Invoke(new EventHandler(UpdateTextLog));
            this.Invoke(new EventHandler(ProcessString));
        }


        private void UpdateTextLog(object sender, EventArgs e)
        {
            // TextBox1 keeps a log of what Strings are coming out
            textBox1.AppendText(RxString);  
        }

        private void ProcessString(object sender, EventArgs e)
        {
            string rawstring;
            string[] processed;
            string[] sentences;
            string[] sentenceSeperators = new String[] {"$"};
            string[] stringSeperators = new string[] { ","," "};

            rawstring = RxString;

            sentences = rawstring.Split(sentenceSeperators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in sentences)
            {
                processed = s.Split(stringSeperators, StringSplitOptions.RemoveEmptyEntries);

                switch (processed[0])
                {
                    //Somewhat inelegant, will figure out something else to do here.
                    //case "EUX":
                    //    for (int i = 0; i < processed.Length; i++)
                    //    {

                    //    }
                    //    break;
                    //case "EUY":
                    //    break;
                    //case "EUZ":
                    //    break;
                    //case "QUAS1":
                    //    break;
                    //case "QUAS2":
                    //    break;
                    //case "QUAS3":
                    //    break;
                    //case "QUAS4":
                    //    break;
                    case "QUAS5":
                        textBox2.Text = processed[1];
                        textBox3.Text = processed[2];
                        textBox4.Text = processed[3];
                        textBox5.Text = processed[4];
                        break;
                    case "TIMS1":
                        break;


                    default:
                        //Skip string if 
                        break;
                }
            }
        }

    }
}
