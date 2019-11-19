using Cube_V11.Entities;
using Cube_V11.Entities.Triangles;
using Cube_V11.Forms.Points3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cube_V11.Forms.Graphic
{
    public partial class Layers : Form
    {
        private List<List<Node>> _nodes;
        private LayersList _layers;

        private TriangleSearcher _triangleSearcher;
        private TriangleTable _triangleTable;

        int ind = 0;

        public Layers(List<object[]> listNode)
        {
            InitializeComponent();

            _nodes = CreateListOfNode(listNode);

            _layers = new LayersList();

            _triangleSearcher = new TriangleSearcher();

            pictureBox1.Image = Properties.Resources.emptyCoube;
            pictureBox2.Image = Properties.Resources.Axes;

            radioButton4.Checked = false;
            radioButton4.Checked = false;
        }

        public List<List<Node>> CreateListOfNode(List<object[]> listNode)
        {
            List<List<Node>> nodeList = new List<List<Node>>();
            int index = 0;

            foreach (object[] nodes in listNode)
            {
                nodeList.Add(new List<Node>());

                for (int i = 0; i <= nodes.Length - 4; i += 4)
                {
                    nodeList[index].Add(new Node()
                    {
                        Id = (int)nodes[i],
                        X = (float)nodes[i + 1],
                        Y = (float)nodes[i + 2],
                        Z = (float)nodes[i + 3]
                    });
                }

                index++;
            }

            return nodeList;
        }

        private void SetImage()
        {
            SetEmptyImage();
            SetXplusImage();
            SetXminusImage();
            SetYplusImage();
            SetYminusImage();
            SetZplusImage();
            SetZminusImage();
        }

        private void SetEmptyImage()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == false &&
                radioButton3.Checked == false &&
                radioButton4.Checked == false &&
                radioButton5.Checked == false)
            {
                pictureBox1.Image = Properties.Resources.emptyCoube;
            }
        }

        private void SetXplusImage()
        {
            if (radioButton1.Checked == true &&
                radioButton2.Checked == false &&
                radioButton3.Checked == false &&
                radioButton4.Checked == true &&
                radioButton5.Checked == false)
            {
                pictureBox1.Image = Properties.Resources.PNCoube;
            }
        }

        private void SetXminusImage()
        {
            if (radioButton1.Checked == true &&
                radioButton2.Checked == false &&
                radioButton3.Checked == false &&
                radioButton4.Checked == false &&
                radioButton5.Checked == true)
            {
                pictureBox1.Image = Properties.Resources.LVCoube;
            }
        }

        private void SetYplusImage()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == true &&
                radioButton3.Checked == false &&
                radioButton4.Checked == true &&
                radioButton5.Checked == false)
            {
                pictureBox1.Image = Properties.Resources.VOCoube;
            }
        }

        private void SetYminusImage()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == true &&
                radioButton3.Checked == false &&
                radioButton4.Checked == false &&
                radioButton5.Checked == true)
            {
                pictureBox1.Image = Properties.Resources.NOCoube;
            }
        }

        private void SetZplusImage()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == false &&
                radioButton3.Checked == true &&
                radioButton4.Checked == true &&
                radioButton5.Checked == false)
            {
                pictureBox1.Image = Properties.Resources.LNCoube;
            }
        }

        private void SetZminusImage()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == false &&
                radioButton3.Checked == true &&
                radioButton4.Checked == false &&
                radioButton5.Checked == true)
            {
                pictureBox1.Image = Properties.Resources.PVCoube;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetImage();
            SortNodes();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetImage();
            SortNodes();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SetImage();
            SortNodes();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            SetImage();
            SortNodes();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            SetImage();
            SortNodes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hScrollBar1.Value = 0;

            if (radioButton1.Checked == true ||
                radioButton2.Checked == true ||
                radioButton3.Checked == true &&
                radioButton4.Checked == true ||
                radioButton5.Checked == true)
            {
                SeporateNodes(GetAxis());
                ShowLayers();
                hScrollBar1.Maximum = _layers.GetLayersCount() + 8;
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private string GetAxis()
        {
            string axis = "";

            if (radioButton1.Checked == true)
            {
                axis = "X";
            }
            else if (radioButton2.Checked == true)
            {
                axis = "Y";
            }
            else
            {
                axis = "Z";
            }

            return axis;
        }

        private void ShowLayers()
        {
            dataGridView1.DataSource = _layers.GetLayer(ind);
        }

        private void SortNodes()
        {
            SotrReset();
            XplusSort();
            XminusSort();
            YplusSort();
            YminusSort();
            ZplusSort();
            ZminusSort();
        }

        private void SotrReset()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == false &&
                radioButton3.Checked == false &&
                radioButton4.Checked == false &&
                radioButton5.Checked == false)
            {
                _nodes[0] = _nodes[0].OrderByDescending(o => o.Id).ToList();
            }
        }

        private void XplusSort()
        {
            if (radioButton1.Checked == true &&
                radioButton2.Checked == false &&
                radioButton3.Checked == false &&
                radioButton4.Checked == true &&
                radioButton5.Checked == false)
            {
                _nodes[0] = _nodes[0].OrderByDescending(o => o.X).ToList(); ;
            }
        }

        private void XminusSort()
        {
            if (radioButton1.Checked == true &&
                radioButton2.Checked == false &&
                radioButton3.Checked == false &&
                radioButton4.Checked == false &&
                radioButton5.Checked == true)
            {
                _nodes[0] = _nodes[0].OrderBy(o => o.X).ToList(); ;
            }
        }

        private void YplusSort()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == true &&
                radioButton3.Checked == false &&
                radioButton4.Checked == true &&
                radioButton5.Checked == false)
            {
                _nodes[0] = _nodes[0].OrderByDescending(o => o.Y).ToList();
            }
        }

        private void YminusSort()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == true &&
                radioButton3.Checked == false &&
                radioButton4.Checked == false &&
                radioButton5.Checked == true)
            {
                _nodes[0] = _nodes[0].OrderBy(o => o.Y).ToList();
            }
        }

        private void ZplusSort()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == false &&
                radioButton3.Checked == true &&
                radioButton4.Checked == true &&
                radioButton5.Checked == false)
            {
                _nodes[0] = _nodes[0].OrderByDescending(o => o.Z).ToList();
            }
        }

        private void ZminusSort()
        {
            if (radioButton1.Checked == false &&
                radioButton2.Checked == false &&
                radioButton3.Checked == true &&
                radioButton4.Checked == false &&
                radioButton5.Checked == true)
            {
                _nodes[0] = _nodes[0].OrderBy(o => o.Z).ToList();
            }
        }

        private void SeporateNodes(string axis)
        {
            List<Node> layer = new List<Node>();
            _layers.ClearLayers();

            for(int i = 0; i < _nodes[0].Count - 1; i++)
            {
                if (axis.Equals("X"))
                {
                    if (_nodes[0][i].X == _nodes[0][i+1].X)
                    {
                        layer.Add(_nodes[0][i]);
                    }
                    else
                    {
                        layer.Add(_nodes[0][i]);
                        _layers.AddLayer(layer);
                        layer = new List<Node>();
                    }
                }
                else if (axis.Equals("Y"))
                {
                    if (_nodes[0][i].Y == _nodes[0][i + 1].Y)
                    {
                        layer.Add(_nodes[0][i]);
                    }
                    else
                    {
                        layer.Add(_nodes[0][i]);
                        _layers.AddLayer(layer);
                        layer = new List<Node>();
                    }
                }
                else
                {
                    if (_nodes[0][i].Z == _nodes[0][i + 1].Z)
                    {
                        layer.Add(_nodes[0][i]);
                    }
                    else
                    {
                        layer.Add(_nodes[0][i]);
                        _layers.AddLayer(layer);
                        layer = new List<Node>();
                    }
                } 
                
                if (i + 1 >= _nodes[0].Count - 1)
                {
                    layer.Add(_nodes[0][i+1]);
                    _layers.AddLayer(layer);
                }
            }
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            ind = hScrollBar1.Value;
            ShowLayers();
            label1.Text = String.Format("Уровень: {0} из {1}", ind, _layers.GetLayersCount() - 1);
            dataGridView2.DataSource = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Points3dMain points3D;

            if (!checkBox1.Checked)
            {
                points3D = new Points3dMain(_nodes[0], _layers);
            }
            else
            {
                points3D = new Points3dMain(_nodes[0], _layers, _triangleTable);
            }
            points3D.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _triangleSearcher.Search(_layers.GetLayer(ind));
            _triangleTable = _triangleSearcher.GetTriangleTable();
            dataGridView2.DataSource = _triangleTable.GetTriangles();
            checkBox1.Enabled = true;
        }
    }
}
