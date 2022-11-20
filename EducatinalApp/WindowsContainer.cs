using System;

namespace EducatinalApp
{
    public class WindowsContainer: IDisposable
    {
        public WindowIntroduction homeWindow { get; set; }
        public WindowFractals fractalWindow { get; set; }
        public WindowsColorModels colorWindow { get; set; }
        public WindowAffineTransform affineWindow { get; set; }
        public WindowTheory theoryWindow { get; set; }
        public App App { get; set; }
        public WindowsContainer(App app)
        {
            homeWindow = new WindowIntroduction(this);
            fractalWindow = new WindowFractals(this);
            colorWindow = new WindowsColorModels(this);
            affineWindow = new WindowAffineTransform(this);
            theoryWindow = new WindowTheory(this);
            App = app;
        }

        public void Dispose()
        {
            homeWindow.Close();
            fractalWindow.Close();
            colorWindow.Close();
            affineWindow.Close();
            theoryWindow.Close();
        }
    }
}
