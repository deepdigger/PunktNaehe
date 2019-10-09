using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MyExtensions;
using System.Drawing;

namespace PunktNaehe.Tests
{
    [TestClass]
    public class EpsilonBoundIntersectCellBoundsTest
    {
        [TestMethod]
        public void VirtualBoundsNotInCellbounds()
        {
            List<Point> points = StaticMethods.generatePoints(10, 10, 100);
            double[,] bounds = points.CalculateBounds();
            Cell cell = new Cell(10, bounds);
            foreach (Point p in points)
            {
                cell.Add(p);
            }
            double[,] virtualbounds = new double[,]
            {
                {11,30 },
                {11,30 }
            };
            bool DoesIntersect = cell.EpsilonboundIntersectsCellBounds(virtualbounds);
            Assert.IsFalse(DoesIntersect);
        }

        [TestMethod]
        public void XBoundTouchesCellbounds()
        {
            List<Point> points = StaticMethods.generatePoints(10, 10, 100);
            double[,] bounds = points.CalculateBounds();
            Cell cell = new Cell(10, bounds);
            foreach (Point p in points)
            {
                cell.Add(p);
            }
            double[,] virtualbounds = new double[,]
            {
                {9,9 },
                {3,9 }
            };
            bool DoesIntersect = cell.EpsilonboundIntersectsCellBounds(virtualbounds);
            Assert.IsTrue(DoesIntersect);
        }
    }
}
