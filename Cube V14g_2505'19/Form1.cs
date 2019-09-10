using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cube_V11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bodyParametrsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BodyConfigForm form = new BodyConfigForm();
            form.MdiParent = this;
            form.Show();
        }

        private void rowColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void matrixConstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NandMForm form = new NandMForm();
            form.MdiParent = this;
            form.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NAngleForm form = new NAngleForm();
            form.MdiParent = this;
            form.Show();
        }

        private void nугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NAngleEmprovedForm form = new NAngleEmprovedForm();
            form.MdiParent = this;
            form.Show();
        }
    }
}
