using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_V11.Entities
{
    class Actions
    {
        public static Point3D MatrixMultPersp(Point3D matr1, double[,] matr2)
        {
            Point3D newMatr;

            if (matr1.z < 0.1 && matr1.z >= 0) matr1.z = 0.1F;
            if (matr1.z < 0 && matr1.z > -0.1) matr1.z = -0.1F;

            float[] num = new float[4];
            for (int j = 0; j < 4; j++)
            {
                num[j] += (float)(matr1.x * matr2[0, j]);
                num[j] += (float)(matr1.y * matr2[1, j]);
                num[j] += (float)(matr1.z * matr2[2, j]);
                num[j] += (float)(matr1.k * matr2[3, j]);
            }

            /*if (num[2] < 0.1 && num[2] >= 0) num[2] = 0.1F;
            if (num[2] < 0 && num[2] > -0.1) num[2] = -0.1F;*/

            newMatr = new Point3D(num[0] / num[3], num[1] / num[3], num[2] / num[3], num[3] / num[3]);

            //if (newMatr.z < 0.1) newMatr.z = 0.1F;

            return newMatr;
        }

        public static Point3D MatrixMult(Point3D matr1, double[,] matr2)
        {
            Point3D newMatr;

            float[] num = new float[4];
            for (int j = 0; j < 4; j++)
            {
                num[j] += (float)(matr1.x * matr2[0, j]);
                num[j] += (float)(matr1.y * matr2[1, j]);
                num[j] += (float)(matr1.z * matr2[2, j]);
                num[j] += (float)(matr1.k * matr2[3, j]);
            }

            newMatr = new Point3D(num[0], num[1], num[2], num[3]);

            return newMatr;
        }

        public static double[,] MatrixMult4(double[,] matr1, double[,] matr2)
        {
            double[,] num = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    num[i, j] += (matr1[i, 0] * matr2[0, j]);
                    num[i, j] += (matr1[i, 1] * matr2[1, j]);
                    num[i, j] += (matr1[i, 2] * matr2[2, j]);
                    num[i, j] += (matr1[i, 3] * matr2[3, j]);
                }
            }

            return num;
        }

        public static double[,] SetMatr()
        {
            double[,] matrByX = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                        matrByX[i, j] = 1.0;
                    else
                        matrByX[i, j] = 0.0;
                }
            }

            return matrByX;
        }

        public List<Point3D> RotationByX(List<Point3D> pointsOf, int angle)
        {
            List<Point3D> points3D = new List<Point3D>();
            double[,] matrByX = SetMatr();
            double alpha = angle * Math.PI / 180;

            matrByX[1, 1] = (Math.Cos(alpha));
            matrByX[1, 2] = (Math.Sin(alpha));
            matrByX[2, 1] = (-Math.Sin(alpha));
            matrByX[2, 2] = (Math.Cos(alpha));

            for (int i = 0; i < pointsOf.Count; i++)
            {
                points3D.Add(MatrixMult(pointsOf[i], matrByX));
            }

            return points3D;
        }

        public List<Point3D> RotationByY(List<Point3D> pointsOf, int angle)
        {
            List<Point3D> points3D = new List<Point3D>();
            double[,] matrByY = SetMatr();
            double alpha = angle * Math.PI / 180;

            matrByY[0, 0] = (Math.Cos(alpha));
            matrByY[0, 2] = (-Math.Sin(alpha));
            matrByY[2, 0] = (Math.Sin(alpha));
            matrByY[2, 2] = (Math.Cos(alpha));

            for (int i = 0; i < pointsOf.Count; i++)
            {
                points3D.Add(MatrixMult(pointsOf[i], matrByY));
            }

            return points3D;
        }

        public List<Point3D> RotationByZ(List<Point3D> pointsOf, int angle)
        {
            List<Point3D> points3D = new List<Point3D>();
            double[,] matrByZ = SetMatr();
            double alpha = angle * Math.PI / 180;

            matrByZ[0, 0] = (Math.Cos(alpha));
            matrByZ[0, 1] = (Math.Sin(alpha));
            matrByZ[1, 0] = (-Math.Sin(alpha));
            matrByZ[1, 1] = (Math.Cos(alpha));

            for (int i = 0; i < pointsOf.Count; i++)
            {
                points3D.Add(MatrixMult(pointsOf[i], matrByZ));
            }

            return points3D;
        }

        public List<Point3D> Moving(List<Point3D> pointsOf, int dx, int dy, int dz)
        {
            List<Point3D> points3D = new List<Point3D>();
            double[,] matrBy = SetMatr();

            matrBy[3, 0] = dx;
            matrBy[3, 1] = dy;
            matrBy[3, 2] = dz;

            for (int i = 0; i < pointsOf.Count; i++)
            {
                points3D.Add(MatrixMult(pointsOf[i], matrBy));
            }

            return points3D;
        }

        public List<Point3D> Scaling(List<Point3D> pointsOf, double sx, double sy, double sz)
        {
            List<Point3D> points3D = new List<Point3D>();
            double[,] matrBy = SetMatr();

            matrBy[0, 0] = sx;
            matrBy[1, 1] = sy;
            matrBy[2, 2] = sz;
            matrBy[3, 3] = 1;

            for (int i = 0; i < pointsOf.Count; i++)
            {
                points3D.Add(MatrixMult(pointsOf[i], matrBy));
            }

            return points3D;
        }

        public static List<Point2D> GetCoord2D(List<Point3D> points3D, float x0, float y0)
        {
            float xc = 0, yc = 0;
            double alpha = Math.PI / 0.5;
            double beta = Math.PI / 2;
            Point2D buf2D = new Point2D();
            List<Point2D> points2D = new List<Point2D>();
            for (int i = 0; i < points3D.Count; i++)
            {
                xc = (float)(x0 + ((-points3D[i].y * Math.Sin(alpha)) + (points3D[i].x * Math.Cos(alpha))));
                yc = (float)(y0 + ((-(points3D[i].y * Math.Cos(alpha) + points3D[i].x * Math.Sin(alpha))) * Math.Sin(beta) + points3D[i].z * Math.Cos(beta)));
                buf2D = new Point2D(xc, yc, 1, points3D[i].id);
                points2D.Add(buf2D);

            }
            return points2D;
        }
    }
}
