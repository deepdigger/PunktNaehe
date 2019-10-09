using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointSetProximityLibray;




namespace PunktNaehe
{
    public partial class Form1 : Form
    {
        private Image img;
        List<Point> points;
        readonly int width;
        readonly int height;
        private readonly int totalAlgorithmExecutionCount = 10;
        int count;
        string algorithm = "unknown";
        IPointProximity pointProximity;

        public Form1()
        {
            InitializeComponent();
            width = pictureBox1.Width;
            height = pictureBox1.Height;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetParameters();
            GenerateSquaredPoints();
            SetPointProximity();
            RenderPicture();
            Benchmark();
            ShowDrawing();
            //Application.Exit();
        }

        private void SetParameters()
        {
            string[] args = Environment.GetCommandLineArgs();
            try
            {
                algorithm = args[1];
                count = int.Parse(args[2]);
            }
            catch
            {
                Console.WriteLine("wrong cmd-params");
            }
        }

        private void GenerateRandomPoints()
        {
            IPointGenerator pointGenerator = new RandomPointListGenerator(10);
            pointGenerator.CreateList(width, height, count);
            points = pointGenerator.GetList();
        }

        private void GenerateSquaredPoints()
        {
            IPointGenerator pointGenerator = new XSquaredPointListGenerator(10);
            pointGenerator.CreateList(width, height, count);
            points = pointGenerator.GetList();
        }

        private void SetPointProximity()
        {
            double epsilon = Calculations.CalculateEpsilon(points);
            Dictionary<string, IPointProximity> pointProximities = new Dictionary<string, IPointProximity>();
            IPointProximity bruteforce = new Bruteforce(points, epsilon);
            IPointProximity grid = new Grid(points, epsilon);
            pointProximities.Add("bruteforce", bruteforce);
            pointProximities.Add("grid", grid);
            pointProximity = pointProximities[algorithm];
        }

        private void RenderPicture()
        {
            DrawPointsProximity image = new DrawPointsProximity(points, pointProximity);
            img = image.Render();
        }

        private void Benchmark()
        {
            IPointGenerator xsquared = new XSquaredPointListGenerator();
            Benchmark benchmark = new Benchmark("grid", xsquared);
            benchmark.Execute(100, 1000, 1000);
        }

        private void ShowDrawing()
        {
            pictureBox1.Image = img;
        }   
    }
}
