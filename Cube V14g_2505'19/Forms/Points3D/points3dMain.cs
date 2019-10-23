using Cube_V11.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cube_V11.Forms.Points3D
{
    public partial class Points3dMain : Form
    {
        private List<Point2D> points2D = new List<Point2D>();
        private List<Point3D> points3D = new List<Point3D>();
        private Point3D buf3D = new Point3D();
        private Point2D buf2D = new Point2D();
        private double[,] matr = new double[4, 4];
        private static int x0, y0, z0;
        private Actions actions = new Actions();

        private List<Polygon> polygons = new List<Polygon>();

        private static Bitmap bmp;
        private Graphics gr;

        private Pen pen = new Pen(Color.Black, 2);

        private List<Node> _nodes;
        private LayersList _layersList;

        private int currentAngleX;
        private int currentAngleY;
        private int currentAngleZ;
        private int offset;

        private int layerInd;

        public Points3dMain(List<Node> nodes, LayersList layersList)
        {
            InitializeComponent();

            RotateSetup();
            
            _nodes = nodes;
            _layersList = layersList;

            hScrollBar4.Maximum = layersList.GetLayersCount() + 8;
            layerInd = hScrollBar4.Value;

            x0 = pictureBox1.Width/2;
            y0 = pictureBox1.Height - pictureBox1.Height/3;
            z0 = 0;

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bmp);

            AddPoints(_nodes);
        }

        private void RotateSetup()
        {
            hScrollBar1.Maximum = 188;
            hScrollBar2.Maximum = 188;
            hScrollBar3.Maximum = 188;

            hScrollBar1.Minimum = -179;
            hScrollBar2.Minimum = -179;
            hScrollBar3.Minimum = -179;

            currentAngleX = hScrollBar1.Value;
            currentAngleY = hScrollBar2.Value;
            currentAngleZ = hScrollBar3.Value;

            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";

            offset = 0;
        }

        private void AddPoints(List<Node> nodes)
        {
            points3D.Clear();

            foreach(Node node in nodes)
            {
                points3D.Add(new Point3D(node.X, node.Y, node.Z, 1, 10, node.Id));
            }            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gr.Clear(Color.White);


            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "0";
            }

            int angleByX = Int32.Parse(textBox1.Text),
                angleByY = Int32.Parse(textBox2.Text),
                angleByZ = Int32.Parse(textBox3.Text);
            RotateByX(angleByX);
            RotateByX(angleByY);
            RotateByX(angleByZ);
        }

        private void RotateByX(int angle)
        {
            points3D = actions.RotationByX(points3D, angle);

            DrawFigure(bmp, gr, pen, Actions.GetCoord2D(points3D, x0, y0));

            pictureBox1.Image = bmp;
        }

        private void RotateByY(int angle)
        {
            points3D = actions.RotationByY(points3D, angle);

            DrawFigure(bmp, gr, pen, Actions.GetCoord2D(points3D, x0, y0));

            pictureBox1.Image = bmp;
        }

        private void RotateByZ(int angle)
        {
            points3D = actions.RotationByZ(points3D, angle);

            DrawFigure(bmp, gr, pen, Actions.GetCoord2D(points3D, x0, y0));

            pictureBox1.Image = bmp;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SwitchLayerForm();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SwitchLayerForm();
        }

        private void SwitchForm()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;

            hScrollBar1.Enabled = true;
            hScrollBar2.Enabled = true;
            hScrollBar3.Enabled = true;

            button4.Enabled = true;
            button5.Enabled = true;

            groupBox1.Enabled = true;
        }

        private void SwitchLayerForm()
        {
            if (radioButton1.Checked)
            {
                hScrollBar4.Visible = false;
                label4.Visible = false;
            }
            else
            {
                hScrollBar4.Visible = true;
                label4.Visible = true;
            }

            ResetParam();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bmp);
            points2D = new List<Point2D>(Actions.GetCoord2D(points3D, x0, y0));
            DrawFigure(bmp, gr, pen, points2D);

            SwitchForm();
        }

        private void DrawFigure(Bitmap bmp, Graphics gr, Pen pen, List<Point2D> points2D)
        {
            gr.Clear(Color.White);

            foreach (Point2D point2D in points2D)
            {
                gr.DrawEllipse(pen, point2D.x, point2D.y, 3, 3);
            }
            /*
            polygons = new List<Polygon>();

            if (points3D.Count != 0)
            {
                polygons.Add(new Polygon(points3D.GetRange(0, 3)));
                polygons.Add(new Polygon(points3D.GetRange(1, 3)));
                polygons.Add(new Polygon((new List<Point3D>() { points3D[2], points3D[3], points3D[0] })));
            }
            */

            //РИСУЕМ ПОЛИГОНЫ
            /*
            foreach (Polygon polygon in polygons)
            {
                polygon.DrawPolygon(gr, pen, x0, y0);
            }
            */

            //ВЫВОД ПОДПИСЕЙ

            foreach (Point2D point2D in points2D)
            {
                gr.DrawString(point2D.id.ToString(),
                    new Font("", 1),
                    Brushes.Red, 
                    point2D.x - 15, 
                    point2D.y - 15);
            }

            for (int i = 0; i < points2D.Count; i++)
            {
                gr.DrawString((i + 1).ToString(), new Font("", 10), Brushes.Red, points2D[i].x - 15, points2D[i].y - 15);
            }

            pictureBox1.Image = bmp;
        }
        
        //ВРАЩЕНИЕ ПО СКРОЛАМ

        private void OffsetSetup(int currentAngle, HScrollBar hScrollBar)
        {
            if (currentAngle < hScrollBar.Value)
            {
                offset = 1;
            }
            else if (currentAngle > hScrollBar.Value)
            {
                offset = -1;
            }
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox1.Text = hScrollBar1.Value.ToString();

                OffsetSetup(currentAngleX, hScrollBar1);

                currentAngleX = hScrollBar1.Value;

                RotateByX(offset);
            }
        }
             
        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                textBox2.Text = hScrollBar2.Value.ToString();

                OffsetSetup(currentAngleY, hScrollBar2);

                currentAngleY = hScrollBar2.Value;

                RotateByY(offset);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PointView pointView = new PointView(_nodes, _layersList);
            pointView.ShowDialog();
        }

        private void hScrollBar4_ValueChanged(object sender, EventArgs e)
        {
            layerInd = hScrollBar4.Value;

            label4.Text = String.Format("Уровень: {0}", layerInd);

            button5_Click(sender, e);
        }

        private void hScrollBar3_ValueChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                textBox3.Text = hScrollBar3.Value.ToString();

                OffsetSetup(currentAngleZ, hScrollBar3);

                currentAngleZ = hScrollBar3.Value;

                RotateByZ(offset);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResetParam();
        }

        private void ResetParam()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            offset = 0;

            hScrollBar1.Value = 0;
            hScrollBar2.Value = 0;
            hScrollBar3.Value = 0;

            currentAngleX = hScrollBar1.Value;
            currentAngleY = hScrollBar2.Value;
            currentAngleZ = hScrollBar3.Value;

            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";

            if (radioButton1.Checked)
            {
                AddPoints(_nodes);
            }
            else
            {
                AddPoints(_layersList.GetLayer(layerInd));
            }

            button3_Click(this, new EventArgs());
        }
    }
}
