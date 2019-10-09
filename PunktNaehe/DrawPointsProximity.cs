using PointSetProximityLibray;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PunktNaehe
{
    public class DrawPointsProximity
    {
        private readonly List<Point> points;
        private readonly IPointProximity pointProximity;
        private readonly int width;
        private readonly int height;
        private Image img;
        private Graphics g;
        const int pointSize = 1;

        public DrawPointsProximity(List<Point> points, IPointProximity pointProximity)
        {
            this.points = points;
            this.pointProximity = pointProximity;
            width = this.points.Max(p => p.X);
            height = this.points.Max(p => p.Y);
        }

        public Image Render()
        {
            InitializeDrawing();
            DrawPoints();
            return img;
        }

        private void InitializeDrawing()
        {
            img = new Bitmap(width, height);
            g = Graphics.FromImage(img);
            g.Clear(Color.Black);
        }
      
        public void MakeBenchmark(int totalAlgorithmExecutionCount)
        {
            for (int i = 1; i <= totalAlgorithmExecutionCount; i++)
            {
                AssignColorToPoints();
            }
        }

        private void AssignColorToPoints()
        {
            foreach (Point p in points)
            {
                AssignColor(p);
            }
        }

        private void DrawPoints()
        {
            foreach (Point p in points)
            {
                DrawPoint(p, AssignColor(p));
            }
        }

        private void DrawPoint(Point p, Color c)
        {
            SolidBrush brush = new SolidBrush(c);
            g.FillRectangle(brush, p.X, p.Y, pointSize, pointSize);
        }

        public Color AssignColor(Point p)
        {
            bool isNear = pointProximity.PointIsCloseToOtherPoints(p);
            return isNear ? Color.LimeGreen : Color.Red;
        }


    }
}
