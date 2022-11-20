using System;
using System.Drawing;
using System.Windows.Controls;

namespace EducatinalApp.ColorsHelper
{
    public class ColorHelper
    {
        static public bool IsColorToChange(Color pixel) 
        {
            var hue = pixel.GetHue();
            if (hue >= 90 && hue<=150)
                return true;

            return false;
        }

        static public Color ChangeSaturation(float saturation, Color pixel) 
        {
            var vis = GetVisibilityFromRgb(pixel);
            var hue = ConvertHueFromRgb(pixel);
            return ConvertFromHSVtoRGB(hue, saturation, vis);
        }

        static public float ConvertSaturationFromRgb(Color pixel) 
        {
            float max = pixel.R;
            if (pixel.G > max)
            {
                max = pixel.G;
            }
            else if (pixel.B > max)
            {
                max = pixel.B;
            }

            float min = pixel.R;
            if (pixel.G < min)
            {
                min = pixel.G;
            }
            else if (pixel.B < min)
            {
                min = pixel.B;
            }

            float saturation = 0;

            if (max != 0) 
            {
                saturation = (float)1 - (min / max);
            }
            return saturation;
        }

        static public float GetVisibilityFromRgb(Color pixel) 
        {
            float max = pixel.R;
            if (pixel.G > max)
            {
                max = pixel.G;
            }
            else if (pixel.B > max)
            {
                max = pixel.B;
            }

            return max/255;
        }

        static public float ConvertHueFromRgb(Color pixel) 
        {
            int r = pixel.R, g = pixel.G, b = pixel.B;

            var maxC = Rgb.Red;
            float max = r;
            if (g > max)
            {
                max = g;
                maxC = Rgb.Green;
            }
            else if (b > max) 
            {
                max = b;
                maxC = Rgb.Blue;
            }

            float min = r;
            if (g < min)
            {
                min = g;
            }
            else if (b < min) 
            {
                min = b;
            }
            double hue = 0;
            if (max == min) 
            {
                hue = pixel.GetHue();
            }
            else if(maxC == Rgb.Red && pixel.G >= pixel.B)
            {
                hue = 60 * (double)(g - b) / (double)(max - min);
            }
            else if (maxC == Rgb.Red && pixel.G < pixel.B) 
            {
                hue = 60 * (double)(g - b) / (double)(max - min) + 360;
            }
            else if (maxC == Rgb.Green) 
            {
                hue = 60 * (double)(b-r) / (double)(max - min) + 120;
            }
            else if(maxC == Rgb.Blue && pixel.G < pixel.B) 
            {
                hue = 60 * (double)(r - g) / (double)(max - min) + 240;
            }
            //hue = pixel.GetHue();
            return (float)hue;
        }

        static public Color ConvertFromHSVtoRGB(float hue, float saturation, float brightness) 
        {
            var H = hue % 360;
            var S = saturation;
            var V = brightness;
            var Hi = (int)(H / 60) % 6;
            var f = ((H / 60) - Hi);
            var p = V * (1 - S);
            var q = V * (1 - f * S);
            var t = V * (1 - (1 - f) * S);

            f *= 255;
            p *= 255;
            q *= 255;
            t *= 255;
            V *= 255;

            switch (Hi)
            {
                case 0:
                    return Color.FromArgb(Convert.ToInt32(V), Convert.ToInt32(t), Convert.ToInt32(p));
                case 1:
                    return Color.FromArgb(Convert.ToInt32(q), Convert.ToInt32(V), Convert.ToInt32(p));
                case 2:
                    return Color.FromArgb(Convert.ToInt32(p), Convert.ToInt32(V), Convert.ToInt32(t));
                case 3:
                    return Color.FromArgb(Convert.ToInt32(p), Convert.ToInt32(q), Convert.ToInt32(V));
                case 4:
                    return Color.FromArgb(Convert.ToInt32(t), Convert.ToInt32(p), Convert.ToInt32(V));
                case 5:
                    return Color.FromArgb(Convert.ToInt32(V), Convert.ToInt32(p), Convert.ToInt32(q));
                default:
                    break;
            }
            return new Color();
        }

        enum Rgb
        {
            Red,
            Blue,
            Green
        }
    }
}
