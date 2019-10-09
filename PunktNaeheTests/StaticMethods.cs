using PointSetProximityLibray;
using System.Collections.Generic;
using System.Drawing;

namespace PunktNaehe.Tests
{
    public static class StaticMethods
    {
        public static List<Point> generatePoints(int width, int height, int AmountOfPoints)
        {
            IPointGenerator generator = new RandomPointListGenerator(11);
            generator.CreateList(10, 10, AmountOfPoints);
            List<Point> points = generator.GetList();
            return points;
        }

    }
}
