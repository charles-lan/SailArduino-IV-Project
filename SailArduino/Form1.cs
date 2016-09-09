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
using ZedGraph;



namespace SailArduino
{
   

    public partial class Form1 : Form
    {
        static int numIMUs = 5;

        bool loaded = false;
        string RxString;
        double xtims;
        float[] angle = new float[numIMUs];
        float[][] EulerArray = new float[numIMUs][];
        Quaternion[] RawQArray    = new Quaternion[numIMUs];
        Quaternion[] SavedQArray  = new Quaternion[numIMUs];
        Quaternion[] TransitionQArray = new Quaternion[numIMUs];

        //TODO refactor with IMU class

        PointPairList listIMU1 = new PointPairList();
        PointPairList listIMU2 = new PointPairList();
        PointPairList listIMU3 = new PointPairList();
        PointPairList listIMU4 = new PointPairList();
        PointPairList listIMU5 = new PointPairList();

        private void Form1_Load(object sender, EventArgs e)
        {
            load_ListViewItem(listView1);
            load_ListViewItem(listView2);
            load_ListViewItem(listView3);
            load_ListViewItem(listView4);
            load_ListViewItem(listView5);
            // TODO Make this nicer.
            CreateGraph(zedGraphControl1,listIMU1,listIMU2, listIMU3, listIMU4, listIMU5);
        }

        #region Zedgraph



        private void CreateGraph(ZedGraphControl zgc,PointPairList listIMU1, PointPairList listIMU2, PointPairList listIMU3, PointPairList listIMU4, PointPairList listIMU5)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            // Set the Titles
            myPane.Title.Text = "My Test Graph\n(For CodeProject Sample)";
            myPane.XAxis.Title.Text = "My X Axis";
            myPane.YAxis.Title.Text = "My Y Axis";

            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend
            LineItem myCurve = myPane.AddCurve("IMU 1",
                  listIMU1, Color.Red,SymbolType.None);
            // Generate a blue curve with circle
            // symbols, and "Piper" in the legend
            LineItem myCurve2 = myPane.AddCurve("IMU 2",
                  listIMU2, Color.Blue, SymbolType.None);

            LineItem myCurve3 = myPane.AddCurve("IMU 3",
                   listIMU3, Color.Black, SymbolType.None);

            LineItem myCurve4 = myPane.AddCurve("IMU 4",
                    listIMU4, Color.Orange, SymbolType.None);

            LineItem myCurve5 = myPane.AddCurve("IMU 5",
                     listIMU5, Color.Purple, SymbolType.None);

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zgc.AxisChange();
        }
        #endregion

        // TODO delete these bloody listviews.
        private void load_ListViewItem(ListView listView)
        {
            listView.View = View.Details;
            listView.GridLines = true;
            listView.FullRowSelect = true;

            //Add column header
            listView.Columns.Add("Raw", 55);
            listView.Columns.Add("Saved", 45);
            listView.Columns.Add("Transition", 55);

            //Add items in the listview
            string[] arr = new string[4];
            ListViewItem itm;

            //w
            arr[0] = "w";
            arr[1] = "N/A";
            arr[2] = "N/A";
            itm = new ListViewItem(arr);
            listView.Items.Add(itm);

            //x
            arr[0] = "x";
            arr[1] = "N/A";
            arr[2] = "N/A";
            itm = new ListViewItem(arr);
            listView.Items.Add(itm);

            //y
            arr[0] = "y";
            arr[1] = "N/A";
            arr[2] = "N/A";
            itm = new ListViewItem(arr);
            listView.Items.Add(itm);

            //z
            arr[0] = "z";
            arr[1] = "N/A";
            arr[2] = "N/A";
            itm = new ListViewItem(arr);
            listView.Items.Add(itm);

            //EUX
            arr[0] = "z";
            arr[1] = "N/A";
            arr[2] = "N/A";
            itm = new ListViewItem(arr);
            listView.Items.Add(itm);

            //EUY
            arr[0] = "z";
            arr[1] = "N/A";
            arr[2] = "N/A";
            itm = new ListViewItem(arr);
            listView.Items.Add(itm);

            //EUZ
            arr[0] = "z";
            arr[1] = "N/A";
            arr[2] = "N/A";
            itm = new ListViewItem(arr);
            listView.Items.Add(itm);
        }

        public Form1()
        {            
            InitializeComponent();
            GetAvailablePorts();

            //Initialise IMUs
            for (int i = 0; i < numIMUs; i++)
            {
                RawQArray[i] =   new Quaternion();
                SavedQArray[i]=  new Quaternion();
                SavedQArray[i] = Quaternion.Identity;
                TransitionQArray[i]= new Quaternion();
                EulerArray[i] = new float[3];
            }
        }

        //int shows which number in array is received
        private void RefreshLabels(string label, ListView listview, int v)
        {
            switch (label)
            {
                case "euler":
                    RefreshQDisplay(listview, EulerArray[0]);
                    break;

                case "raw":
                    RefreshQDisplay(listview, RawQArray[v]);
                    goto case "calculate";

                case "saved":
                    //the 1 represents the subItems count.
                    RefreshQDisplay(listview, SavedQArray[v], 1);
                    goto case "calculate";

                case "calculate":
                    CalculateTransition(SavedQArray, RawQArray, v);
                    RefreshQDisplay(listview, TransitionQArray[v], 2);

                    angle[v] = CalculateAngle(TransitionQArray[v]);
                    switch (v)
                    {
                        case 0:
                            textBox2.Text = Convert.ToString(Math.Round(angle[v], 3));
                            listIMU1.Add(xtims, angle[v]);
                            break;
                        case 1:
                            textBox3.Text = Convert.ToString(Math.Round(angle[v], 3));
                            listIMU2.Add(xtims, angle[v]);
                            break;
                        case 2:
                            textBox4.Text = Convert.ToString(Math.Round(angle[v], 3));
                            listIMU3.Add(xtims, angle[v]);
                            break;
                        case 3:
                            textBox5.Text = Convert.ToString(Math.Round(angle[v], 3));
                            listIMU4.Add(xtims, angle[v]);
                            break;
                        case 4:
                            textBox6.Text = Convert.ToString(Math.Round(angle[v], 3));
                            listIMU5.Add(xtims, angle[v]);
                            break;
                    }
                    


                    break;

                default:
                    break;

            }
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();
            

        }

        #region Calculations
        private float CalculateAngle(Quaternion quaternion)
        {
           return 360 - (2 * (float)Math.Acos(quaternion.W) * (180 / (float)Math.PI)); ;
        }

        private void CalculateTransition(Quaternion[] savedQArray, Quaternion[] rawQArray, int v)
        {
            TransitionQArray[v] = Quaternion.Multiply(SavedQArray[v].Normalized(), RawQArray[v].Inverted().Normalized());
            TransitionQArray[v].Normalize();
        }
        #endregion

        //TODO Fix all these up.

        private void RefreshQDisplay(ListView listView, Quaternion QArray, int v)
        {
            listView.Items[0].SubItems[v].Text = Convert.ToString(QArray.W);
            listView.Items[1].SubItems[v].Text = Convert.ToString(QArray.X);
            listView.Items[2].SubItems[v].Text = Convert.ToString(QArray.Y);
            listView.Items[3].SubItems[v].Text = Convert.ToString(QArray.Z);
        }

        private void RefreshQDisplay(ListView listView, Quaternion QArray)
        {
            listView.Items[0].Text = Convert.ToString(QArray.W);
            listView.Items[1].Text = Convert.ToString(QArray.X);
            listView.Items[2].Text = Convert.ToString(QArray.Y);
            listView.Items[3].Text = Convert.ToString(QArray.Z);
        }

        private void RefreshQDisplay(ListView listView, float[] QArray)
        {
            listView.Items[4].Text = Convert.ToString(QArray[0]);
            listView.Items[5].Text = Convert.ToString(QArray[1]);
            listView.Items[6].Text = Convert.ToString(QArray[2]);
        }


        private void GetAvailablePorts()
        {
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                ComboCOMPort.Items.Add(s);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = Convert.ToString(ComboCOMPort.SelectedItem);
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

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numIMUs; i++)
            {
                SavedQArray[i] = RawQArray[i];
            }
            
            RefreshLabels("saved", listView1, 0);
            RefreshLabels("saved", listView2, 1);
            RefreshLabels("saved", listView3, 2);
            RefreshLabels("saved", listView4, 3);
            RefreshLabels("saved", listView5, 4);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        #region Arduino input data
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            RxString = serialPort1.ReadLine();
            this.Invoke(new EventHandler(UpdateTextLog));
            this.Invoke(new EventHandler(ProcessString));
        }

        private void UpdateTextLog(object sender, EventArgs e)
        {
            // TextBox1 keeps a log of what Strings are coming out
            textBox1.Text = RxString; 
        }

        private void ProcessString(object sender, EventArgs e)
        {
            string rawstring;
            string[] processed;
            string[] sentences;
            string[] sentenceSeperators = new String[] { "$" };
            string[] stringSeperators = new string[] { ",", " " };

            rawstring = RxString;

            sentences = rawstring.Split(sentenceSeperators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in sentences)
            {
                processed = s.Split(stringSeperators, StringSplitOptions.RemoveEmptyEntries);

                switch (processed[0])
                {
                    //Somewhat inelegant, will figure out something else to do here.
                    case "EUX":
                        ReadEuler(processed,0);
                        RefreshLabels("euler", listView5, 0);
                        break;
                    case "EUY":
                        ReadEuler(processed, 1);
                        RefreshLabels("euler", listView5, 0);
                        break;
                    case "EUZ":
                        ReadEuler(processed, 2);
                        RefreshLabels("euler", listView5, 0);
                        break;
                    case "QUAS1":
                        ReadQuaternion(processed, 0);
                        RefreshLabels("raw", listView1, 0);
                        break;
                    case "QUAS2":
                        ReadQuaternion(processed, 1);
                        RefreshLabels("raw", listView2, 1);
                        break;
                    case "QUAS3":
                        ReadQuaternion(processed, 2);
                        RefreshLabels("raw", listView3, 2);
                        break;
                    case "QUAS4":
                        ReadQuaternion(processed, 3);
                        RefreshLabels("raw", listView4, 3);
                        break;
                    case "QUAS5":
                        ReadQuaternion(processed, 4);
                        RefreshLabels("raw", listView5, 4);
                        break;
                    case "TIMS1":
                        xtims = Convert.ToDouble(processed[1]);
                        break;

                    default:
                        //Skip string if 
                        break;
                }
            }
        }

        private void ReadEuler(string[] processed, int v)
        {
            EulerArray[0][v] = Convert.ToSingle(processed[1]);
        }

        private void ReadQuaternion(string[] processed, int v)
        {
            RawQArray[v].W = Convert.ToSingle(processed[1]);
            RawQArray[v].X = Convert.ToSingle(processed[3]);
            RawQArray[v].Y = Convert.ToSingle(processed[2]);
            RawQArray[v].Z = Convert.ToSingle(processed[4]);
        }

        #endregion

        #region OpenGL
        double xrot, yrot, zrot;


        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;
            GL.ClearColor(Color.SkyBlue);
            SetupViewport();

            GL.Enable(EnableCap.DepthTest);
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {

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
            GL.Vertex3(10, 20, 10);
            GL.Vertex3(100, 20, 10);
            GL.Vertex3(100, 50, 10);
            GL.End();

            GLDraw.Axis();
            glControl1.Invalidate();
            glControl1.SwapBuffers();
        }
    }

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
    #endregion
}
