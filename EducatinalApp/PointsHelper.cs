using System;
using System.Windows;

namespace EducatinalApp
{
    public class PointsHelper
    {
        static public double CalculateDistance(Point x, Point y) 
        {
            return Math.Sqrt(Math.Pow(x.X - y.X, 2) + Math.Pow(x.Y - y.Y, 2));
        }
    }
}
