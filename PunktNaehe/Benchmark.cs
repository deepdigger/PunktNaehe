using PointSetProximityLibray;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunktNaehe
{
    public class Benchmark
    {
        readonly string KindOfAlgorithm;
        List<Point> points;
        IPointGenerator generator;
        IPointProximity pointproximity;

        public Benchmark(string KindOfAlgorithm, IPointGenerator generator)
        {
            this.KindOfAlgorithm = KindOfAlgorithm;
            this.generator = generator; 
        }

        public void Execute(int start, int end, int iterator)
        {
            WriteHeaderToFile();
            for (int AmountPoints = start; AmountPoints < end; AmountPoints += iterator)
            {
                MakeBenchmark(AmountPoints);
            }
        }

        private void MakeBenchmark(int AmountPoints)
        {
            GeneratePoints(AmountPoints);
            InitialsePointProximity();
            long totalprocessingtime = 0;
            for (int exection = 0; exection < 10; exection++)
            {
                totalprocessingtime += MeasureCommandProcessingTime(AssignColorToPoints);
            }
            long averageprocessingtime = totalprocessingtime / 10;
            WriteDataToFile(AmountPoints, averageprocessingtime);
        }

        private void WriteHeaderToFile()
        {
            using (StreamWriter outputFile = new StreamWriter("Benchmarkdata.txt", true))
            {
                outputFile.WriteLine(KindOfAlgorithm + "+" + generator);
                outputFile.WriteLine("AmountOfPoints, AverageProcessingTime,");
            }
        }

        private void WriteDataToFile(int AmountPoints, long averageprocessingtime)
        {
            using (StreamWriter outputFile = new StreamWriter("Benchmarkdata.txt", true))
            {
                outputFile.WriteLine(AmountPoints + "," + averageprocessingtime + ",");
            }
        }

        private void GeneratePoints(int AmountOfPoints)
        {

            generator.CreateList(30, 30, AmountOfPoints);
            points = generator.GetList();
        }

        private void InitialsePointProximity()
        {
            double epsilon = Calculations.CalculateEpsilon(points);
            Dictionary<string, IPointProximity> pointProximities = new Dictionary<string, IPointProximity>();
            IPointProximity bruteforce = new Bruteforce(points, epsilon);
            IPointProximity grid = new Grid(points, epsilon);
            pointProximities.Add("bruteforce", bruteforce);
            pointProximities.Add("grid", grid);
            SetPointProximity(pointProximities);
        }

        private void SetPointProximity(Dictionary<string, IPointProximity> pointProximities)
        {
            try
            {
                pointproximity = pointProximities[KindOfAlgorithm];
            }
            catch (KeyNotFoundException e)
            {
                throw new ArgumentException("Wrong algorithm parameter");
            }
        }

        private long MeasureCommandProcessingTime(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        private void AssignColorToPoints()
        {
            foreach (Point p in points)
            {
                AssignColor(p);
            }
        }

        public Color AssignColor(Point p)
        {
            bool isNear = pointproximity.PointIsCloseToOtherPoints(p);
            return isNear ? Color.LimeGreen : Color.Red;
        }

        

    }
}
