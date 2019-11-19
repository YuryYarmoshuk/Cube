using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_V11.Entities
{
    public class Point3D
    {
        public float x, y, z, k, scale;
        private int index;
        public int id;

        public Point3D()
        {
            new Point3D(0, 0, 0, 1);
        }

        public Point3D(float x1, float y1, float z1, float k1)
        {
            x = x1;
            y = y1;
            z = z1;
            k = k1;
        }

        public Point3D(float x1, float y1, float z1, float k1, float scale1)
        {
            x = x1 * scale1;
            y = y1 * scale1;
            z = z1 * scale1;
            k = k1;
            scale = scale1;
        }

        public Point3D(float x1, float y1, float z1, float k1, float scale1, int id)
        {
            x = x1 * scale1;
            y = y1 * scale1;
            z = z1 * scale1;
            k = k1;
            scale = scale1;
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public int GetIndex()
        {
            return index;
        }
    }
}
