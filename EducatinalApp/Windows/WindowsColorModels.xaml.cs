using EducatinalApp.ColorsHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for WindowsColorModels.xaml
    /// </summary>
    public partial class WindowsColorModels : Window
    {
        protected Bitmap? originalPicture;
        protected Bitmap? saturatedPicture;
        protected WindowsContainer windowsContainer { get; set; }
        public WindowsColorModels(WindowsContainer windowsContainer)
        {
            InitializeComponent();
            this.windowsContainer = windowsContainer;
        }
        private void ColorModelsPage_DrawButton_Click(object sender, RoutedEventArgs e)
        {
            saturatedPicture = new Bitmap(originalPicture);

            for (int i = 0; i < saturatedPicture.Height; ++i)
            {
                for (int j = 0; j < saturatedPicture.Width; ++j)
                {
                    var pixel = saturatedPicture.GetPixel(j, i);
                    if (ColorHelper.IsColorToChange(pixel))
                        saturatedPicture
                            .SetPixel(j, i, ColorHelper.ChangeSaturation((float)ColorModelsPage_SaturationSlider.Value, pixel));
                }
            }
            ColorModelsPage_ImageOutput.Source = System.Windows.Interop.Imaging
               .CreateBitmapSourceFromHBitmap(saturatedPicture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "image";
            dialog.DefaultExt = ".png";
            dialog.Filter = "Images (.png)|*.png|(.jpeg)|*.jpeg|(.jpg)|*.jpg";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                originalPicture = new Bitmap(filename);
                ColorModelsPage_Image.Source = System.Windows.Interop.Imaging
                    .CreateBitmapSourceFromHBitmap(originalPicture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }

        }

        private void FractalPage_IterationsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ColorModelsPage_SaturationLabel != null)
                ColorModelsPage_SaturationLabel.Content = ColorModelsPage_SaturationSlider.Value.ToString();
        }

        private void ColorModelsPage_AutoLoadButton_Click(object sender, RoutedEventArgs e)
        {
            string filename = "D:\\Polytechnic\\ComputerGraphics\\EducatinalApp\\EducatinalApp\\green-forest-26752813.jpg";
            originalPicture = new Bitmap(filename);
            ColorModelsPage_Image.Source = System.Windows.Interop.Imaging
                .CreateBitmapSourceFromHBitmap(originalPicture.GetHbitmap(),
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void ColorModelsPage_SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "picture.png";
            dialog.DefaultExt = ".png";
            dialog.Filter = "Images (.png)|*.png";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                saturatedPicture.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void ColorModelsPage_Image_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var pos = e.GetPosition(this);
            ImageSource imageSource = ColorModelsPage_Image.Source;
            BitmapSource bitmapImage = (BitmapSource)imageSource;
            var x = (Math.Round(e.GetPosition(ColorModelsPage_Image).X * bitmapImage.PixelWidth / ColorModelsPage_Image.ActualWidth, 0)).ToString();
            var y = (Math.Round(e.GetPosition(ColorModelsPage_Image).Y * bitmapImage.PixelHeight / ColorModelsPage_Image.ActualHeight, 0)).ToString();
            aboveCoord.Content = "(" + x + ", " + y + ")";

            var pixel = originalPicture?.GetPixel(int.Parse(x), int.Parse(y));

            R_value.Content = pixel.Value.R;
            G_value.Content = pixel.Value.G;
            B_value.Content = pixel.Value.B;

            Hue_value.Content = (Math.Round(ColorHelper.ConvertHueFromRgb(pixel.Value), 1)) + "°";
            Sat_value.Content = (Math.Round(ColorHelper.ConvertSaturationFromRgb(pixel.Value), 1)) + "%";
            V_value.Content = (Math.Round(ColorHelper.GetVisibilityFromRgb(pixel.Value), 1));
        }

        private void ColorModelsPage_ImageOutput_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var pos = e.GetPosition(this);
            ImageSource imageSource = ColorModelsPage_ImageOutput.Source;
            BitmapSource bitmapImage = (BitmapSource)imageSource;
            var x = (Math.Round(e.GetPosition(ColorModelsPage_ImageOutput).X * bitmapImage.PixelWidth / ColorModelsPage_ImageOutput.ActualWidth, 0)).ToString();
            var y = (Math.Round(e.GetPosition(ColorModelsPage_ImageOutput).Y * bitmapImage.PixelHeight / ColorModelsPage_ImageOutput.ActualHeight, 0)).ToString();
            underCoord.Content = "(" + x + ", " + y + ")";

            var pixel = saturatedPicture?.GetPixel(int.Parse(x), int.Parse(y));

            R_value.Content = pixel.Value.R;
            G_value.Content = pixel.Value.G;
            B_value.Content = pixel.Value.B;

            Hue_value.Content = (Math.Round(ColorHelper.ConvertHueFromRgb(pixel.Value), 1)) + "°";
            Sat_value.Content = (Math.Round(ColorHelper.ConvertSaturationFromRgb(pixel.Value), 1)) + "%";
            V_value.Content = (Math.Round(ColorHelper.GetVisibilityFromRgb(pixel.Value), 1));
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
            windowsContainer.affineWindow.Show();
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
            ((Border)sender).BorderBrush = System.Windows.Media.Brushes.LightBlue;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderThickness = new Thickness(0);

        }
    }
}
