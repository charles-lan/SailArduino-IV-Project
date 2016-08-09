using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Drawing;
using System.IO;

namespace SailArduino
{
   
    public partial class Form1 : Form
    {
        bool loaded = false;
        string RxString;

        public Form1()
        {

            InitializeComponent();
            GetAvailablePorts();
        }

        private void GetAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
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


        //shader and fragment and program IDs that will be read in by graphics card.
        int pgmID;
        int vsID;
        int fsID;

        int attribute_vcol;
        int attribute_vpos;
        int uniform_mview;

        int vbo_position;
        int vbo_color;
        int vbo_mview;

        Vector3[] vertdata;
        Vector3[] coldata;
        Matrix4[] mviewdata;

        void initProgram()
        {
            pgmID = GL.CreateProgram();
            loadShader("vs.glsl", ShaderType.VertexShader, pgmID, out vsID);
            loadShader("fs.glsl", ShaderType.FragmentShader, pgmID, out fsID);
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));

            //getting position, colour etc to bind.
            attribute_vpos = GL.GetAttribLocation(pgmID, "vPosition");
            attribute_vcol = GL.GetAttribLocation(pgmID, "vColor");
            uniform_mview = GL.GetUniformLocation(pgmID, "modelview");

            if (attribute_vpos == -1 || attribute_vcol == -1 || uniform_mview == -1)
            {
                Console.WriteLine("Error binding attributes");
            }

            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mview);
        }

        //loading the shaders
        void loadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }


        void Application_Idle(object sender, EventArgs e)
        {
            double milliseconds = ComputeTimeSlice();
            Accumulate(milliseconds);
            Animate(milliseconds);
        }

        float rotation = 0;
        private void Animate(double milliseconds)
        {
            float deltaRotation = (float)milliseconds / 20.0f;
            rotation += deltaRotation;
            glControl1.Invalidate();
        }

        double accumulator = 0;
        int idleCounter = 0;
        private void Accumulate(double milliseconds)
        {
            idleCounter++;
            accumulator += milliseconds;
            if (accumulator > 1000)
            {
                accumulator -= 1000;
                idleCounter = 0; // don't forget to reset the counter!
            }
        }

        private double ComputeTimeSlice()
        {
            sw.Stop();
            double timeslice = sw.Elapsed.TotalMilliseconds;
            sw.Reset();
            sw.Start();
            return timeslice;
        }


        //Drawing window methods
        Stopwatch sw = new Stopwatch(); // available to all event handlers
        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;

            initProgram();

            vertdata = new Vector3[] { new Vector3(-0.8f, -0.8f, 0f),
                new Vector3( 0.8f, -0.8f, 0f),
                new Vector3( 0f,  0.8f, 0f)};


            coldata = new Vector3[] { new Vector3(1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f,  1f, 0f)};


            mviewdata = new Matrix4[]{
                Matrix4.Identity
            };

            GL.ClearColor(Color.SkyBlue); // Yey! .NET Colors can be used directly!
            GL.PointSize(5f);

            SetupViewport();
            Application.Idle += Application_Idle; // press TAB twice after +=
            sw.Start(); // start at application boot
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

        // Technically OnRenderFrame
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;

            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadIdentity();

            //GL.Translate(x, 0, 0);

            //if (glControl1.Focused)
            //    GL.Color3(Color.Yellow);
            //else
            //    GL.Color3(Color.Blue);
            //GL.Rotate(rotation, Vector3.UnitZ); // OpenTK has this nice Vector3 class!
            //GL.Begin(BeginMode.Triangles);
            //GL.Vertex2(10, 20);
            //GL.Vertex2(100, 20);
            //GL.Vertex2(100, 50);
            //GL.End();

            //glControl1.SwapBuffers();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);


            GL.EnableVertexAttribArray(attribute_vpos);
            GL.EnableVertexAttribArray(attribute_vcol);

            GL.DrawArrays(BeginMode.Triangles, 0, 3);

            GL.DisableVertexAttribArray(attribute_vpos);
            GL.DisableVertexAttribArray(attribute_vcol);


            GL.Flush();
            glControl1.SwapBuffers();
        }


        int x = 0;
        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                x++;
                glControl1.Invalidate();
            }
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            SetupViewport();
            glControl1.Invalidate();
        }


    }

    public abstract class Volume
    {
        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;

        public int VertCount;
        public int IndiceCount;
        public int ColorDataCount;
        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public abstract Vector3[] GetVerts();
        public abstract int[] GetIndices(int offset = 0);
        public abstract Vector3[] GetColorData();
        public abstract void CalculateModelMatrix();
    }

    static class GLDraw
    {
        static GLDraw()
        {
        }

        public class Cube : Volume
        {
            public Cube()
            {
                VertCount = 8;
                IndiceCount = 36;
                ColorDataCount = 8;
            }

            public override void CalculateModelMatrix()
            {
                ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
            }

            public override Vector3[] GetColorData()
            {
                return new Vector3[] {
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f, 1f, 0f),
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f, 1f, 0f),
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f)
            };
            }

            public override int[] GetIndices(int offset = 0)
            {
                int[] inds = new int[] {
                //left
                0, 2, 1,
                0, 3, 2,
                //back
                1, 2, 6,
                6, 5, 1,
                //right
                4, 5, 6,
                6, 7, 4,
                //top
                2, 3, 6,
                6, 3, 7,
                //front
                0, 7, 3,
                0, 4, 7,
                //bottom
                0, 1, 5,
                0, 5, 4
            };

                if (offset != 0)
                {
                    for (int i = 0; i < inds.Length; i++)
                    {
                        inds[i] += offset;
                    }
                }

                return inds;
            }

            public override Vector3[] GetVerts()
            {
                return new Vector3[] {new Vector3(-0.5f, -0.5f,  0.0f),
                new Vector3(0.5f, -0.5f,  0.0f),
                new Vector3(0.5f, 0.5f,  0.0f),
                new Vector3(-0.5f, 0.5f,  0.0f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, 0.5f,  0.5f),
                new Vector3(-0.5f, 0.5f,  0.5f),
            };
            }
        }

    }

}
