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
            Polygon polygon;
            switch (vertex)
            {
                case 1:
                    points = FigureScaling(firstPoint, points, 1 / DecreasingSlider.Value);
                    polygon = FigureTurning(firstPoint, points, TurtingAngleSlider.Value);
                    ATCanvas.Children.Clear();
                    ATCanvas.Children.Add(polygon);
                    break;
                case 2:
                    points = FigureScaling(secondPoint, points, 1 / DecreasingSlider.Value);
                    polygon = FigureTurning(secondPoint, points, TurtingAngleSlider.Value);
                    ATCanvas.Children.Clear();
                    ATCanvas.Children.Add(polygon);
                    break;
                case 3:
                    points = FigureScaling(thirdPoint, points, 1 / DecreasingSlider.Value);
                    polygon = FigureTurning(thirdPoint, points, TurtingAngleSlider.Value);
                    ATCanvas.Children.Clear();
                    ATCanvas.Children.Add(polygon);
                    break;
                case 4:
                    points = FigureScaling(fourthPoint, points, 1 / DecreasingSlider.Value);
                    polygon = FigureTurning(fourthPoint, points, TurtingAngleSlider.Value);
                    ATCanvas.Children.Clear();
                    ATCanvas.Children.Add(polygon);
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
    }
}
