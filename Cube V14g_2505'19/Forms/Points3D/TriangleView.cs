using Cube_V11.Entities.Triangles;
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
    public partial class TriangleView : Form
    {
        public TriangleView(TriangleTable triangleTable)
        {
            InitializeComponent();

            dataGridView1.DataSource = triangleTable.GetTriangles();
        }
    }
}
