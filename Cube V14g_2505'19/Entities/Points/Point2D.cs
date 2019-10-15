using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_V11.Entities
{
    public class Point2D
    {
        public float x, y, k;
        public int id;

        public Point2D()
        {
            new Point2D(0, 0, 1);
        }

        public Point2D(float x1, float y1, float k1)
        {
            x = x1;
            y = y1;
            k = k1;
        }

        public Point2D(float x1, float y1, float k1, int id1)
        {
            x = x1;
            y = y1;
            k = k1;
            id = id1;
        }
    }
}
