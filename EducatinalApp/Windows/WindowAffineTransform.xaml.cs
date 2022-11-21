using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EducatinalApp
{
    /// <summary>
    /// Interaction logic for WindowAffineTransform.xaml
    /// </summary>
    public partial class WindowAffineTransform : Window
    {
        protected WindowsContainer windowsContainer { get; set; }
        int vertex = 1;
        Point firstPoint;
        Point secondPoint;
        Point thirdPoint;
        Point fourthPoint;
        public WindowAffineTransform(WindowsContainer windowsContainer)
        {
            InitializeComponent();
            this.windowsContainer = windowsContainer;
        }

        private void TurtingAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TurtingAngleSliderLable != null)
                TurtingAngleSliderLable.Content = TurtingAngleSlider.Value + "°";
        }

        private void DecreasingSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DecreasingSliderLable != null)
                DecreasingSliderLable.Content = DecreasingSlider.Value + "";
        }

        private void CreateInitialParallelogram()
        {

            var recWidth = ATCanvas.ActualWidth / 3;
            var recHeight = recWidth / 2;
            var xdif = recWidth / 3;
            var centerPoint = new Point(ATCanvas.ActualWidth / 2, ATCanvas.ActualHeight / 2);
            firstPoint = new Point(centerPoint.X - recWidth / 2, centerPoint.Y + recHeight / 2);
            secondPoint = new Point(centerPoint.X - recWidth / 2 + xdif, centerPoint.Y - recHeight / 2);
            thirdPoint = new Point(centerPoint.X + recWidth / 2, centerPoint.Y - recHeight / 2);
            fourthPoint = new Point(centerPoint.X + recWidth / 2 - xdif, centerPoint.Y + recHeight / 2);

            Polygon polygon = new Polygon();
            polygon.Stroke = Brushes.Black;
            polygon.StrokeThickness = 2;
            polygon.Points.Add(firstPoint);
            polygon.Points.Add(secondPoint);
            polygon.Points.Add(thirdPoint);
            polygon.Points.Add(fourthPoint);

            ATCanvas.Children.Add(polygon);
        }

        private void Transform_Click(object sender, RoutedEventArgs e)
        {
            List<Point> points = new List<Point>();
            points.Add(firstPoint);
            points.Add(secondPoint);
            points.Add(thirdPoint);
            points.Add(fourthPoint);
            switch (vertex)
            {
                case 1:
                    var pol = MyTransforming(firstPoint, points, DecreasingSlider.Value, TurtingAngleSlider.Value);
                    ATCanvas.Children.Clear();
                    ATCanvas.Children.Add(pol);
                    break;
                case 2:
                    var pol1 = MyTransforming(secondPoint, points, DecreasingSlider.Value, TurtingAngleSlider.Value);
                    ATCanvas.Children.Clear();
                    ATCanvas.Children.Add(pol1);
                    break;
                case 3:
                    var pol2 = MyTransforming(thirdPoint, points, DecreasingSlider.Value, TurtingAngleSlider.Value);
                    ATCanvas.Children.Clear();
                    ATCanvas.Children.Add(pol2);
                    break;
                case 4:
                    var pol3 = MyTransforming(firstPoint, points, DecreasingSlider.Value, TurtingAngleSlider.Value);
                    ATCanvas.Children.Clear();
                    ATCanvas.Children.Add(pol3);
                    break;
                default:
                    break;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                if (((RadioButton)sender).Content.Equals("1"))
                    vertex = 1;
                else if (((RadioButton)sender).Content.Equals("2"))
                    vertex = 2;
                else if (((RadioButton)sender).Content.Equals("3"))
                    vertex = 3;
                else if (((RadioButton)sender).Content.Equals("4"))
                    vertex = 4;
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            CreateInitialParallelogram();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            windowsContainer.App.Shutdown();
        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            windowsContainer.theoryWindow.Show();
            this.Hide();
        }

        private void StackPanel_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            windowsContainer.fractalWindow.Show();
            this.Hide();
        }

        private void StackPanel_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            windowsContainer.colorWindow.Show();
            this.Hide();
        }

        private void StackPanel_MouseUp_3(object sender, MouseButtonEventArgs e)
        {
            windowsContainer.homeWindow.Show();
            this.Hide();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderThickness = new Thickness(2);
            ((Border)sender).BorderBrush = Brushes.LightBlue;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderThickness = new Thickness(0);
            ((Border)sender).BorderBrush = null;
        }

        private List<Point> FigureScaling(Point mainPoint, List<Point> figure, double multiplier) 
        {
            List<Point> newFigure = new List<Point>(figure);

            newFigure = newFigure
                .Select(p => new Point((p.X - mainPoint.X) * multiplier + mainPoint.X, (p.Y - mainPoint.Y) * multiplier + mainPoint.Y))
                .ToList();

            return newFigure;
        }

        private Polygon FigureTurning(Point mainPoint, List<Point> figure, double degrees)
        {
            List<Point> newFigure = new List<Point>(figure);
            var angle = (-1) * (Math.PI * degrees / 180.0);
            newFigure = newFigure
                .Select(p => new Point(((p.X - mainPoint.X) * Math.Cos(angle) + (p.Y - mainPoint.Y) * Math.Sin(angle)) + mainPoint.X,
                    ((p.X - mainPoint.X) * (-1) * Math.Sin(angle) + (p.Y - mainPoint.Y) * Math.Cos(angle)) + mainPoint.Y))
                .ToList();

            Polygon polygon = new Polygon();
            polygon.Stroke = Brushes.Black;
            polygon.StrokeThickness = 2;

            newFigure
                .ForEach(p => polygon.Points.Add(p));

            return polygon;
        }

        private Polygon MyTransforming(Point mainPoint, List<Point> figure, double multiplier, double degrees) {
            double[,] scaling = {
                { 1 / multiplier, 0 },
                {0, 1 / multiplier }
            };
            //[x, y]
            var angle = (Math.PI * degrees / 180.0);

            double[,] turning = {
                { Math.Cos(angle), Math.Sin(angle) },
                { (-1) * Math.Sin(angle), Math.Cos(angle) }
            };

            double[,] centerPoint = new double[,] { { mainPoint.X, mainPoint.Y } };
            double[,] targetPoint = new double[,] { { 0, 0 } };

            List<Point> points = new List<Point>();

            for (int i = 0; i < figure.Count; ++i)
            {
                targetPoint[0, 0] = figure[i].X;
                targetPoint[0, 1] = figure[i].Y;

                targetPoint = MatrixSubstraction(targetPoint, centerPoint);
                targetPoint = MatrixMultiplication(targetPoint, scaling);
                targetPoint = MatrixMultiplication(targetPoint, turning);
                targetPoint = MatrixAddition(targetPoint, centerPoint);

                points.Add(new Point(targetPoint[0,0], targetPoint[0, 1]));
            }

            Polygon polygon = new Polygon();
            polygon.Stroke = Brushes.Black;
            polygon.StrokeThickness = 2;

            points
                .ForEach(p => polygon.Points.Add(p));

            return polygon;
        }

        private double[,] MatrixMultiplication(double[,] matrix1, double[,] matrix2)
        {
            double[,] result = new double[matrix1.GetLength(0), matrix1.GetLength(1)];

            var d1 = matrix1.GetLength(0);
            var d2 = matrix1.GetLength(1);

            for (int i = 0; i < result.GetLength(0); ++i)
            {
                for (int j = 0; j < result.GetLength(1); ++j)
                {
                    result[i, j] = 0;

                    for (int k = 0; k < matrix2.GetLength(1); ++k) {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return result;
        }

        private double[,] MatrixSubstraction(double[,] matrix1, double[,] matrix2)
        {
            var result = new double[matrix1.GetLength(0), matrix1.GetLength(1)];
            var d1 = matrix1.GetLength(0);
            var d2 = matrix1.GetLength(1);
            for (int i = 0; i < matrix1.GetLength(0); i++) {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    result[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }
            return result;
        }

        private double[,] MatrixAddition(double[,] matrix1, double[,] matrix2)
        {
            var result = new double[matrix1.GetLength(0), matrix1.GetLength(1)];
            var d1 = matrix1.GetLength(0);
            var d2 = matrix1.GetLength(1);
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return result;
        }
    }
}
