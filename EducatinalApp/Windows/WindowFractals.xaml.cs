using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EducatinalApp
{
    /// <summary>
    /// Interaction logic for WindowFractals.xaml
    /// </summary>
    public partial class WindowFractals : Window
    {
        protected WindowsContainer windowsContainer { get; set; }
        public WindowFractals(WindowsContainer windowsContainer)
        {
            InitializeComponent();
            this.windowsContainer = windowsContainer;
        }
        private void FractalGrid_RealtionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FractalPage_RelationLabel != null)
                FractalPage_RelationLabel.Content = (Math.Round(FractalPage_RelationSlider.Value, 1)).ToString();
        }
        private void FractalGrid_IterationsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FractalPage_IterationsLabel != null)
                FractalPage_IterationsLabel.Content = FractalPage_IterationsSlider.Value.ToString();
        }

        public List<Point> InitialTriangle()
        {
            var centerPoint = new Point(
                FractalPage_Canvas.ActualWidth - FractalPage_Canvas.ActualWidth / 2,
                FractalPage_Canvas.ActualHeight - FractalPage_Canvas.ActualHeight / 2
                );

            var topPoint = new Point(
                centerPoint.X,
                centerPoint.Y - FractalPage_Canvas.ActualHeight / 3 + 50
                );

            var R = Math.Sqrt(Math.Pow(topPoint.X - centerPoint.X, 2) + Math.Pow(topPoint.Y - centerPoint.Y, 2));

            var h = 3 / 2 * R;

            var bottomCenterPoint = new Point(centerPoint.X, centerPoint.Y + h);

            var b = 2 * h / Math.Sqrt(3);

            var leftPoint = new Point(bottomCenterPoint.X - b, bottomCenterPoint.Y);

            var rightPoint = new Point(bottomCenterPoint.X + b, bottomCenterPoint.Y);

            return new List<Point>() { topPoint, leftPoint, rightPoint };
        }

        public List<Point> InitialRegtangle()
        {
            var centerPoint = new Point(
                FractalPage_Canvas.ActualWidth - FractalPage_Canvas.ActualWidth / 2,
                FractalPage_Canvas.ActualHeight - FractalPage_Canvas.ActualHeight / 2
                );

            var topPoint = new Point(
                centerPoint.X,
                centerPoint.Y - FractalPage_Canvas.ActualHeight / 3
                );

            var R = Math.Sqrt(Math.Pow(topPoint.X - centerPoint.X, 2) + Math.Pow(topPoint.Y - centerPoint.Y, 2));

            var rightTop = new Point(topPoint.X + R, topPoint.Y);
            var rightBottom = new Point(topPoint.X + R, topPoint.Y + 2 * R);

            var leftTop = new Point(topPoint.X - R, topPoint.Y);
            var leftBottom = new Point(topPoint.X - R, topPoint.Y + 2 * R);

            return new List<Point>() { leftTop, rightTop, rightBottom, leftBottom };
        }


        private async void FractalPage_DrawButton_Click(object sender, RoutedEventArgs e)
        {
            var centerPoint = new Point(
                FractalPage_Canvas.ActualWidth - FractalPage_Canvas.ActualWidth / 2,
                FractalPage_Canvas.ActualHeight - FractalPage_Canvas.ActualHeight / 2
            );
            DrawButtonImage.Source = new BitmapImage(new Uri("D:\\Polytechnic\\ComputerGraphics\\EducatinalApp\\EducatinalApp\\loading.png"));
            var points = new List<Point>();

            if (TriangleSelect.IsChecked == true)
            {
                points = await DrawKochSnowflake((int)FractalPage_IterationsSlider.Value, FractalPage_RelationSlider.Value);
            }
            else
                points = await DrawKochCurveWithRectangle((int)FractalPage_IterationsSlider.Value, FractalPage_RelationSlider.Value);

            var minx = points.Select(p => p.X).Min();
            var maxx = points.Select(p => p.X).Max();
            var miny = points.Select(p => p.Y).Min();
            var maxy = points.Select(p => p.Y).Max();

            Polygon polygon = new Polygon();

            polygon.Stroke = Brushes.Black;
            polygon.StrokeThickness = 1;
            foreach (var p in points)
            {
                polygon.Points.Add(new Point(p.X - minx,
                    p.Y - miny));
            }
            polygon.RenderTransform = new ScaleTransform(
                FractalPage_Canvas.ActualHeight / (maxx - minx) < FractalPage_Canvas.ActualHeight / (maxy - miny)? FractalPage_Canvas.ActualHeight / (maxx - minx): FractalPage_Canvas.ActualHeight / (maxy - miny),
                FractalPage_Canvas.ActualHeight / (maxx - minx) < FractalPage_Canvas.ActualHeight / (maxy - miny) ? FractalPage_Canvas.ActualHeight / (maxx - minx) : FractalPage_Canvas.ActualHeight / (maxy - miny)
            );

            DrawButtonImage.Source = new BitmapImage(new Uri("D:\\Polytechnic\\ComputerGraphics\\EducatinalApp\\EducatinalApp\\pen.png"));
            FractalPage_Canvas.Children.Clear();
            FractalPage_Canvas.Children.Add(polygon);
        }

        private Task<List<Point>> DrawKochSnowflake(int itNumber, double relation)
        {
            return Task.Run(() =>
            {
                var points = InitialTriangle();

                double[] x = points.Select(p => p.X).ToArray();
                double[] y = points.Select(p => p.Y).ToArray();

                for (int j = 0; j < itNumber; ++j)
                {
                    double[] xNew = new double[x.Length * 4];
                    double[] yNew = new double[y.Length * 4];

                    for (int i = 0; i < x.Length; i++)
                    {
                        xNew[4 * i] = x[i];
                        yNew[4 * i] = y[i];
                        double dx = x[(i + 1) % x.Length] - x[i];
                        double dy = y[(i + 1) % y.Length] - y[i];

                        xNew[4 * i + 1] = x[i] + dx * (1 - relation) / 2;
                        yNew[4 * i + 1] = y[i] + dy * (1 - relation) / 2;
                        xNew[4 * i + 3] = x[i] + dx * ((1 - relation) / 2 + relation);
                        yNew[4 * i + 3] = y[i] + dy * ((1 - relation) / 2 + relation);

                        double a = dx * relation / 2 - Math.Sqrt(3.0) / 2 * dy * relation;
                        double b = dy * relation / 2 + Math.Sqrt(3.0) / 2 * dx * relation;

                        xNew[4 * i + 2] = xNew[4 * i + 1] + a;
                        yNew[4 * i + 2] = yNew[4 * i + 1] + b;
                    }

                    x = xNew;
                    y = yNew;
                }

                points.Clear();

                for (int i = 0; i < x.Length; ++i)
                {
                    points.Add(new Point(x[i], y[i]));
                }

                return points;
            });

        }

        private Task<List<Point>> DrawKochCurveWithRectangle(int itNumber, double relation)
        {
            return Task.Run(() =>
            {
                var points = InitialRegtangle();

                double[] x = points.Select(p => p.X).ToArray();
                double[] y = points.Select(p => p.Y).ToArray();

                for (int j = 0; j < itNumber; ++j)
                {
                    double[] xNew = new double[x.Length * 5];
                    double[] yNew = new double[y.Length * 5];

                    for (int i = 0; i < x.Length; i++)
                    {
                        xNew[5 * i] = x[i];
                        yNew[5 * i] = y[i];
                        double dx = x[(i + 1) % x.Length] - x[i];
                        double dy = y[(i + 1) % y.Length] - y[i];

                        xNew[5 * i + 1] = x[i] + dx * (1 - relation) / 2;
                        yNew[5 * i + 1] = y[i] + dy * (1 - relation) / 2;
                        xNew[5 * i + 4] = x[i] + dx * ((1 - relation) / 2 + relation);
                        yNew[5 * i + 4] = y[i] + dy * ((1 - relation) / 2 + relation);

                        double a2 = xNew[5 * i + 1] + dy * relation;
                        double b2 = yNew[5 * i + 1] - dx * relation;
                        double a3 = xNew[5 * i + 4] + dy * relation;
                        double b3 = yNew[5 * i + 4] - dx * relation;

                        xNew[5 * i + 2] = a2;
                        yNew[5 * i + 2] = b2;
                        xNew[5 * i + 3] = a3;
                        yNew[5 * i + 3] = b3;
                    }

                    x = xNew;
                    y = yNew;
                }

                points.Clear();

                for (int i = 0; i < x.Length; ++i)
                {
                    points.Add(new Point(x[i], y[i]));
                }

                return points;
            });
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
            windowsContainer.affineWindow.Show();
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
    }
}
