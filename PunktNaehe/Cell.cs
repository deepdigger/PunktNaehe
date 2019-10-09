using PointSetProximityLibray;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PunktNaehe
{
    public class Cell
    {
        public Cell[] c = new Cell[4];
        public List<Point> Elements = new List<Point>();
        private int capacity;
        private double Xmin;
        private double Xmax;
        private double Ymin;
        private double Ymax;
        

        public Cell(int capacity, double[,] bounds)
        {
            this.capacity = capacity;
            Xmin = bounds[0, 0];
            Xmax = bounds[0, 1];
            Ymin = bounds[1, 0];
            Ymax = bounds[1, 1];
        }

        public void Add(Point p)
        {
            if(!IsInBounds(p))
            {
                return;
            }
            if (HasChildren())
            {
                AddPointInChildren(p);
            }
            else
            {
                AddPointToCellAndOrSplit(p);
            }
        }

        private void AddPointToCellAndOrSplit(Point p)
        {
            AddPointToCellIfPossible(p);
            SplitCellIfNecessary();
        }

        private void AddPointToCellIfPossible(Point p)
        {
            bool pEqualsItemFromList = Elements.Any(c => c == p);
            if (!pEqualsItemFromList)
            {
                Elements.Add(p);
            }
        }

        private void SplitCellIfNecessary()
        {
            if (Elements.Count() > capacity)
            {
                Split();
            }
        }

        private bool IsInBounds(Point p)
        {
            bool betweenX = (p.X >= Xmin) && (p.X <= Xmax);
            bool betweenY = (p.Y >= Ymin) && (p.Y <= Ymax);
            return betweenX && betweenY;
        }

        private bool HasChildren()
        {
            return (c[0] != null) ? true : false;
        }

        private void AddPointInChildren(Point p)
        {
            c[0].Add(p);
            c[1].Add(p);
            c[2].Add(p);
            c[3].Add(p);
        }

        private void Split()
        {
            for(int i = 0; i < 4; i++)
            {
                MakeNewCellAndAddPoints(i);
            }
        }

        private void MakeNewCellAndAddPoints(int i)
        {
            MakeNewCell(i);
            AddNewPoints(i);
            Elements.Clear();
        }

        private void MakeNewCell(int i)
        {
            c[i] = new Cell(capacity, CalculateBoundsForChildren(i));
        }

        private double[,] CalculateBoundsForChildren(int i)
        {
            double halfwidth = (Xmax - Xmin) / 2;
            double halfheight = (Ymax - Ymin) / 2;

            double BoxXmin = Xmin + (i % 2 * halfheight);
            double BoxXmax = BoxXmin + halfheight;
            double BoxYmin = Ymin + (i / 2 * halfwidth);
            double BoxYmax = BoxYmin + halfwidth;

            double[,] bounds = new double[,]
            {
                {BoxXmin, BoxXmax},
                {BoxYmin, BoxYmax}
            };

            return bounds;
        }

        private void AddNewPoints(int i)
        {
            foreach (Point p in Elements)
            {
                c[i].Add(p);
            }
        }

        public bool HasNearNeighbourUntested(Point p, double epsilon)
        {
            double[,] bounds = CalculateBoundEpsilon(p, epsilon);
            if (EpsilonboundIntersectsCellBounds(bounds))
            {
                if (HasChildren())
                {
                    foreach (Cell cell in c)
                    {
                        if(cell.HasNearNeighbourUntested(p, epsilon))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    foreach (Point point in Elements)
                    {
                        if (p != point)
                        {
                            if (DistanceComputer.ComputeSquaredDistance(p, point) <= epsilon * epsilon)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool EpsilonboundIntersectsCellBounds(double[,] ebounds)
        {
            double eXmin = ebounds[0, 0];
            double eXmax = ebounds[0, 1];
            double eYmin = ebounds[1, 0];
            double eYmax = ebounds[1, 1];
            bool xBoundIntersect = eXmax >= Xmin && eXmin <= Xmax;
            bool yBoundIntersect = eYmax >= Ymin && eYmin <= Ymax;

            return xBoundIntersect && yBoundIntersect;
        }

        private double[,] CalculateBoundEpsilon(Point p, double epsilon)
        {
            double xMin = p.X - epsilon;
            double xMax = p.X + epsilon;
            double yMin = p.Y - epsilon;
            double yMax = p.Y + epsilon;

            double[,] bounds = new double[,]
            {
                {xMin, xMax},
                {yMin, yMax}
            };

            return bounds;
        }
    }
}