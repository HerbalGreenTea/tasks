using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public struct Pixel
    {
        public Pixel(double r, double g, double b)
        {
            this.r = this.g = this.b = 0;
            this.R = r;
            this.G = g;
            this.B = b;
        }


        private double r;
        public double R
        {
            set { r = Check(value); }
            get { return r; }
        }

        private double g;
        public double G
        {
            set { g = Check(value); }
            get { return g; }
        }

        private double b;
        public double B
        {
            set { b = Check(value); }
            get { return b; }
        }

        public double Check(double value)
        {
            if (value < 0 || value > 1)
                throw new ArgumentException();

            return value;
        }

        public static double Trim(double value)
        {
            if (value < 0) return 0;
            if (value > 1) return 1;
            return value;
        }

        public static Pixel operator *(Pixel pixel, double value)
        {
            return new Pixel(
                Pixel.Trim(pixel.R * value),
                Pixel.Trim(pixel.G * value),
                Pixel.Trim(pixel.B * value));
        }

        public static Pixel operator *(double value, Pixel pixel)
        {
            return pixel * value;
        }
    }
}
