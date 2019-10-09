using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointSetProximityLibray;
using MyExtensions;

namespace PunktNaehe.Tests
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void CellDepth()
        {
            List<Point> points = generatePoints(10,10,1);
            double[,] bounds = points.CalculateBounds(); 
            Cell cell = new Cell(10, bounds);
            foreach (Point p in points)
            {
                cell.Add(p);
            }
            int depth = cell.Depth();
            Assert.AreEqual(1, depth);
        }

        [TestMethod]
        public void OneElementList()
        {
            List<Point> points = generatePoints(10,10,1);
            double[,] bounds = points.CalculateBounds();
            Cell cell = new Cell(10, bounds);
            foreach (Point p in points)
            {
                cell.Add(p);
            }
            List<Point> actual = cell.Elements;
            List<Point> expected = new List<Point>();
            foreach (Point p in points)
            {
                expected.Add(p);
            }
            
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ElevenElementsDepth()
        {
            List<Point> points = generatePoints(10,10,11);
            double[,] bounds = points.CalculateBounds();
            Cell cell = new Cell(10, bounds);
            foreach (Point p in points)
            {
                cell.Add(p);
            }
            int depth = cell.Depth();
            Assert.AreEqual(2, depth);
        }

        [TestMethod]
        public void MaxDepthTenByTenGrid()
        {
            List<Point> points = generatePoints(10,10,10000);
            double[,] bounds = points.CalculateBounds();
            Cell cell = new Cell(10, bounds);
            foreach (Point p in points)
            {
                cell.Add(p);
            }
            int depth = cell.Depth();
            Assert.AreEqual(3, depth);
        }

        [TestMethod]
        public void PointIsNear()
        {
            List<Point> points = generatePoints(10,10,10);
            double[,] bounds = points.CalculateBounds();
            Cell cell = new Cell(10, bounds);
            foreach (Point p in points)
            {
                cell.Add(p);
            }
            double epsilon = 1;
            bool IsNear = cell.HasNearNeighbourUntested(points[0], epsilon);
            Assert.IsTrue(IsNear);
        }

        [TestMethod]
        public void VirtualBoundsNotInCellbounds()
        {
            List<Point> points = generatePoints(10, 10, 100);
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
            List<Point> points = generatePoints(10, 10, 100);
            double[,] bounds = points.CalculateBounds();
            Cell cell = new Cell(10, bounds);
            foreach (Point p in points)
            {
                cell.Add(p);
            }
            double[,] virtualbounds = new double[,]
            {
                {9,30 },
                {11,30 }
            };
            bool DoesIntersect = cell.EpsilonboundIntersectsCellBounds(virtualbounds);
            Assert.IsTrue(DoesIntersect);
        }


        private List<Point> generatePoints(int width, int height, int AmountOfPoints)
        {
            IPointGenerator generator = new RandomPointListGenerator(11);
            generator.CreateList(width, height, AmountOfPoints);
            List<Point> points = generator.GetList();
            return points;
        }

  


    }
}
