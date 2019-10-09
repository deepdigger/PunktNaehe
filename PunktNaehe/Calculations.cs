using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace PunktNaehe
{
    public static class Calculations
    {
        public static double CalculateEpsilon(List<Point> points)
        {
            int count = points.Count();
            int width = points.Max(c => c.X);
            int height = points.Max(c => c.Y);
            return Math.Sqrt(width * height / count) / 2.0;
        }

        public static long MeasureCommandProcessingTime(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
