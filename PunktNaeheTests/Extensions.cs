using PointSetProximityLibray;
using PunktNaehe;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExtensions
{
    public static class Extensions
    {
        public static int Depth(this Cell cell)
        {
            int[] depth = { 1, 1, 1, 1 };
            for (int i = 0; i < 4; i++)
            {
                if (cell.c[i] != null)
                {
                    depth[i] += Depth(cell.c[i]);
                }
            }
            return depth.Max();
        }

        public static double[,] CalculateBounds(this List<Point> points)
        {
            double Xmin = points.Min(p => p.X);
            double Xmax = points.Max(p => p.X);
            double Ymin = points.Min(p => p.Y);
            double Ymax = points.Max(p => p.Y);
            double[,] bounds = new double[,] {
                { Xmin, Xmax },
                { Ymin, Ymax }
            };
            return bounds;

        }
    }
}
