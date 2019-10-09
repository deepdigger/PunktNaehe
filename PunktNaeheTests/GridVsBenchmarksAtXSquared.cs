using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointSetProximityLibray;

namespace PunktNaehe.Tests
{
    [TestClass]
    public class BenchmarkDifferentCounts
    {
        [TestMethod]
        public void ExecuteBenchmarkOnePoints()
        {
            IPointGenerator xsquared = new XSquaredPointListGenerator(10);
            Benchmark benchmark = new Benchmark("grid", xsquared);
            benchmark.Execute(1,10001,1000);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WrongAlgorithmParameter()
        {
            IPointGenerator xsquared = new XSquaredPointListGenerator(10);
            Benchmark benchmark = new Benchmark("WrongParameter", xsquared);
            benchmark.Execute(3,100,1);
        }

        
    }
}
