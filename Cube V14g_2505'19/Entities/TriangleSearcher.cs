using Cube_V11.Entities.Triangles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cube_V11.Entities
{
    class TriangleSearcher
    {
        private List<List<double>> matrix = new List<List<double>>();
        private List<List<double>> tempMatrix = new List<List<double>>();
        private TriangleTable triangleTable;
        private List<int> connectedNode = new List<int>();
        private List<double> connectedNodeDistance = new List<double>();

        public TriangleSearcher()
        {
            triangleTable = new TriangleTable();
        }

        public void Search(List<Node> nodes)
        {
            triangleTable.ClearTriangles();
            SetDistanceMatrix(nodes);
            CopyMatrix();
            TriangleSearch();
        }

        private void CopyMatrix()
        {
            tempMatrix.Clear();

            for (int i = 0; i < matrix.Count; i++)
            {
                tempMatrix.Add(new List<double>());
                for (int j = 0; j < matrix.Count; j++)
                {

                    tempMatrix[i].Add(matrix[i][j]);
                }
            }
        }

        public TriangleTable GetTriangleTable()
        {
            return triangleTable;
        }

        private void SetDistanceMatrix(List<Node> nodes)
        {
            int count = nodes.Count;
            matrix.Clear();

            for (int i = 0; i < count; i++)
            {
                matrix.Add(new List<double>());
                for (int j = 0; j < count; j++)
                {
                    matrix[i].Add(GetDistance(nodes[i].X, nodes[j].X,
                                                nodes[i].Y, nodes[j].Y,
                                                nodes[i].Z, nodes[j].Z));
                }
            }
        }

        private double GetDistance(float x1, float x2, float y1, float y2, float z1, float z2)
        {
            return Math.Round(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2) + Math.Pow((z2 - z1), 2)), 3);
        }

        private void TriangleSearch()
        {
            int v1 = 0;

            double primaMin;
            
            double min;
            int v2;

            for (int jV1 = 0; jV1 < tempMatrix.Count; jV1++)
            {
                v1 = jV1;
                primaMin = MinValue(tempMatrix[v1]);
                CopyMatrix();

                for (int i = 0; i < tempMatrix[v1].Count; i++)
                {
                    min = MinValue(tempMatrix[v1]);

                    if (min <= primaMin + primaMin * 0.5 && min >= primaMin - primaMin * 0.5)
                    {
                        v2 = IndSearch(min, tempMatrix[v1]);
                        MatrixZeroing(v1, v2);
                        MatrixZeroing(v2, v1);

                        connectedNode.Add(v2);
                    }
                }

                for (int i = 0; i < connectedNode.Count - 1; i++)
                {
                    Triangle tr = new Triangle();
                    tr.V1 = v1 + 1;
                    tr.V2 = connectedNode[i] + 1;
                    tr.V3 = connectedNode[i + 1] + 1;

                    if (triangleTable.CheckTriangle(tr.V1, tr.V2, tr.V3))
                    {
                        triangleTable.AddTriangle(tr);
                    }
                }

                connectedNode.Clear();
            }
            
            //int V1 = v1;

            ////В цикле
            //int ind = 0;

            //min = MinValue(matrix[ind]);

            //Triangle triangle = new Triangle();

            //triangle.V1 = V1;
            //triangle.V2 = IndSearch(min, matrix[ind]) + 1;

            //MatrixZeroing(triangle.V2 - 1, ind);

            //min = MinValue(matrix[triangle.V2 - 1]);

            //ConnectedNodeSearch(triangle.V2 - 1, min);

            //ConnectedNodeDistanceSearch(triangle.V1 - 1);

            ///*foreach(double val in connectedNode)
            //{
            //    labelText = labelText + " " + val;
            //}*/

            //if (connectedNodeDistance.Count >= 2)
            //{
            //    if (connectedNodeDistance.Count == 2)
            //    {
            //        if (connectedNodeDistance[0] == connectedNodeDistance[1])
            //        {
            //            triangle.V3 = connectedNode[0] + 1;
            //            if (!triangleTable.CheckTriangle(triangle.V1, triangle.V2, triangle.V3))
            //            {
            //                triangleTable.AddTriangle(triangle);
            //                MatrixZeroing(ind, triangle.V3 - 1);
            //            }

            //            Triangle triangleNext = new Triangle();
            //            triangleNext.V1 = triangle.V1;
            //            triangleNext.V2 = triangle.V2;
            //            triangleNext.V3 = connectedNode[1] + 1; ;
            //            if (!triangleTable.CheckTriangle(triangle.V1, triangle.V2, triangle.V3))
            //            {
            //                triangleTable.AddTriangle(triangleNext);
            //                MatrixZeroing(ind, triangleNext.V3 - 1);
            //            }

            //            MatrixZeroing(ind, triangle.V1 - 1);
            //        }
            //        else
            //        {
            //            //search min
            //        }
            //    }
            //}
            //else
            //{
            //    triangle.V3 = connectedNode[0] + 1;
            //    if (!triangleTable.CheckTriangle(triangle.V1, triangle.V2, triangle.V3))
            //    {
            //        triangleTable.AddTriangle(triangle);
            //        MatrixZeroing(triangle.V3 - 1, ind);
            //    }
            //}
        }

        private double MinValue(List<double> list)
        {
            double min = list[FirstIndSearch(list)];

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != 0)
                {
                    if (list[i] < min)
                    {
                        min = list[i];
                    }
                }
            }

            return min;
        }

        private double MaxValue(List<double> list)
        {
            double max = list[FirstIndSearch(list)];

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > max)
                {
                    max = list[i];
                }
            }

            return max;
        }

        private int FirstIndSearch(List<double> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != 0)
                {
                    return i;
                }
            }

            return 0;
        }

        private int IndSearch(double val, List<double> list)
        {
            int ind = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == val)
                {
                    return i;
                }
            }

            return ind;
        }

        private void MatrixZeroing(int i, int j)
        {
            tempMatrix[i][j] = 0;
        }

        private void ConnectedNodeSearch(int ind, double min)
        {
            connectedNode.Clear();

            for (int i = 0; i < matrix[ind].Count; i++)
            {
                if (matrix[ind][i] < min + min * 0.1 &&
                    matrix[ind][i] > min - min * 0.1 &&
                    matrix[ind][i] != 0)
                {
                    connectedNode.Add(i);
                }
            }
        }

        private void ConnectedNodeDistanceSearch(int V1)
        {
            foreach (int ind in connectedNode)
            {
                connectedNodeDistance.Add(matrix[ind][V1]);
            }
        }
    }
}
