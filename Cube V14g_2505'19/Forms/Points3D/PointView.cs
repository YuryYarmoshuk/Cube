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
    public partial class PointView : Form
    {
        int ind = 0;

        private List<Node> _nodes;
        private LayersList _layers;

        public PointView(List<Node> nodes, LayersList layers)
        {
            InitializeComponent();

            _nodes = nodes;
            _layers = layers;

            dataGridView1.DataSource = _nodes;
            dataGridView2.DataSource = _layers.GetLayer(ind);

            hScrollBar1.Maximum = layers.GetLayersCount() + 8;

            label3.Text = ind.ToString();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            ind = hScrollBar1.Value;

            dataGridView2.DataSource = _layers.GetLayer(ind);

            label3.Text = ind.ToString();
        }
    }
}
