using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EducatinalApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        WindowsContainer windowsContainer;

        public App()
        {
            windowsContainer = new WindowsContainer(this);
            windowsContainer.homeWindow.Show();
        }
        protected override void OnExit(ExitEventArgs args)
        {
            try
            {
                windowsContainer.Dispose();
            }
            catch (Exception)
            {
                base.OnExit(args);
                throw;
            }
        }
    }
}
