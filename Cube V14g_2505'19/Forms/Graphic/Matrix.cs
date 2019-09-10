using Cube_V11.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cube_V11.Forms.Graphic
{
    public partial class Matrix : Form
    {
        private List<List<Node>> nodes;
        private List<List<double>> matrix = new List<List<double>>();

        public Matrix(List<object[]> listNode)
        {
            InitializeComponent();

            nodes = GetList(listNode);

            //dataGridView1.ColumnAdded += new DataGridViewColumnEventHandler(dataGridView1_ColumnAdded);
            
            //SetDistanceMatrix(0);

            SaveNodes("coube0cellsN.txt");
        }

        public void SaveNodes(string fileName)
        {
            string line = "";

            if (!File.Exists(fileName))
            {
                StreamReader sr = new StreamReader(File.Create(fileName));
                sr.Close();
            }

            StreamWriter sw = new StreamWriter(File.OpenWrite(fileName));

            foreach(Node node in nodes[0])
            {
                //line = String.Format("{0} {1} {2} {3}", node.X, node.Y, node.Z, node.Id);
                line = String.Format("{0} {1} {2}", node.X, node.Y, node.Z);

                sw.WriteLine(line);

                line = "";                
            }

            sw.Close();
        }

        public List<List<Node>> GetList(List<object[]> listNode)
        {
            List<List<Node>> nodeList = new List<List<Node>>();
            int index = 0;

            foreach (object[] nodes in listNode)
            {
                nodeList.Add(new List<Node>());

                for (int i = 0; i <= nodes.Length - 4; i+= 4)
                {
                    nodeList[index].Add(new Node()
                    {
                        Id = (int)nodes[i],
                        X = (float)nodes[i+1],
                        Y = (float)nodes[i+2],
                        Z = (float)nodes[i+3]
                    });
                }

                index++;
            }

            return nodeList;
        }
        
        void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void SetDistanceMatrix(int id)
        {
            int count = nodes[id][nodes[id].Count - 1].Id;

            SetGrid(count);

            for (int i = 0; i < count; i++)
            {
                matrix.Add(new List<double>());
                for (int j = 0; j < count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = GetDistance(nodes[id][i].X, nodes[id][j].X,
                                                                       nodes[id][i].Y, nodes[id][j].Y,
                                                                       nodes[id][i].Z, nodes[id][j].Z);
                    matrix[i].Add((double)dataGridView1.Rows[i].Cells[j].Value);
                }
            }
        }

        private void SetGrid(int index)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            for (int i = 1; i <= index; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), i.ToString());
            }

            dataGridView1.Rows.Add(index);

            for (int i = 0; i < index; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private double GetDistance(float x1, float x2, float y1, float y2, float z1, float z2)
        {
            return Math.Round(Math.Sqrt(Math.Pow((x2 - x1),2) + Math.Pow((y2 - y1), 2) + Math.Pow((z2 - z1), 2)), 3);
        }
    }
}
