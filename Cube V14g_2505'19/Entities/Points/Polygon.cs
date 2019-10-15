using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_V11.Entities
{
    public class Polygon
    {
        List<Point3D> points3D;
        List<Point2D> points2D;

        public Polygon()
        {
            points3D = new List<Point3D>();
            points2D = new List<Point2D>();
        }

        public Polygon(List<Point3D> points3D)
        {
            new Polygon();
            this.points3D = points3D;
        }

        public List<Point3D> GetPoints()
        {
            return points3D;
        }

        public void SetPoints(List<Point3D> points)
        {
            this.points3D = points;
        }

        public void DrawPolygon(Graphics gr, Pen pen, float x0, float y0)
        {
            points2D = Actions.GetCoord2D(points3D, x0, y0);
            int i = 0;

            for (i = 0; i < points2D.Count - 1; i++)
            {
                gr.DrawLine(pen, points2D[i].x, points2D[i].y, points2D[i + 1].x, points2D[i + 1].y);
            }

            gr.DrawLine(pen, points2D[i].x, points2D[i].y, points2D[0].x, points2D[0].y);

            return ;
        }
    }
}
