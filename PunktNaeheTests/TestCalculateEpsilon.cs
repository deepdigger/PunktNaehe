using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace PunktNaehe.Tests
{
    [TestClass]
    public class TestCalculateEpsilon
    {
        [TestMethod]
        public void CalculateEpsilonEmtpyList()
        {
            List<Point> points = new List<Point>()
            {
                new Point(0,0)
            };
            double actual = Calculations.CalculateEpsilon(points);
            double expected = 0;
            Assert.AreEqual(expected, actual);
        }
    }
}
