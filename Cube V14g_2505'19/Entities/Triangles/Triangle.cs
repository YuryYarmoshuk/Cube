using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_V11.Entities.Triangles
{
    public class Triangle
    {
        public int N { get; set; }
        public int V1 { get; set; }
        public int V2 { get; set; }
        public int V3 { get; set; }

        public string GetAllV()
        {
            return string.Format("{0};{1};{2}", V1, V2, V3);
        }
    }
}
