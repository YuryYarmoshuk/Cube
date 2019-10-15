using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_V11.Entities.Triangles
{
    class TriangleTable
    {
        private List<Triangle> triangles = new List<Triangle>();

        public void AddTriangle(Triangle tr)
        {
            tr.N = triangles.Count + 1;
            triangles.Add(tr);
        }

        public List<Triangle> GetTriangles()
        {
            return triangles;
        }

        public bool CheckTriangle(int v1, int v2, int v3)
        {
            List<string> allV = new List<string>()
            {
                string.Format("{0};{1};{2}", v1, v2, v3),
                string.Format("{0};{2};{1}", v1, v2, v3),
                string.Format("{1};{0};{2}", v1, v2, v3),
                string.Format("{1};{2};{0}", v1, v2, v3),
                string.Format("{2};{1};{0}", v1, v2, v3),
                string.Format("{2};{0};{1}", v1, v2, v3)
            };

            foreach (Triangle tr in triangles)
            {
                if (allV.IndexOf(tr.GetAllV()) != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
