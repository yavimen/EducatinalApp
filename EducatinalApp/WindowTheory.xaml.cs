using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EducatinalApp
{
    /// <summary>
    /// Interaction logic for WindowTheory.xaml
    /// </summary>
    public partial class WindowTheory : Window
    {
        WindowsContainer windowsContainer;
        public WindowTheory(WindowsContainer windowsContainer)
        {
            InitializeComponent();
            this.windowsContainer = windowsContainer;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            windowsContainer.App.Shutdown();
        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            windowsContainer.affineWindow.Show();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            windowsContainer.fractalWindow.Show();
            this.Hide();
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            windowsContainer.colorWindow.Show();
            this.Hide();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            windowsContainer.affineWindow.Show();
            this.Hide();
        }
    }
}
