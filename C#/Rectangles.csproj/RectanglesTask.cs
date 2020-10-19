using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            return CalculateLength(r1.Width + r2.Width, r1.Left - r2.Left, r1.Right - r2.Right)
                && CalculateLength(r1.Height + r2.Height, r1.Top - r2.Top, r1.Bottom - r2.Bottom);
        }

        public static bool CalculateLength(double l, double lineOne, double lineTwo)
        {
            return l - Math.Abs(lineOne) - Math.Abs(lineTwo) >= 0;
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            var lineWidth = LengthSearch(r1.Left, r1.Right, r2.Left, r2.Right);
            var lineHeight = LengthSearch(r1.Top, r1.Bottom, r2.Top, r2.Bottom);

            return lineHeight * lineWidth;
        }

        public static int LengthSearch(int oneLeft, int oneRight, int twoLeft, int twoRight)
        {
            var one = Math.Max(oneLeft, twoLeft);
            var two = Math.Min(oneRight, twoRight);

            return Math.Max(two - one, 0);
        }

        public static void CheckAngle(double x1, double y1, double x2, double y2)
        {
            double buf;
            if (x1 > x2) { buf = x1; x1 = x2; x2 = buf; }
            if (y1 > y2) { buf = y1; y1 = y2; y2 = buf; }
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            CheckAngle(r1.Left, r1.Top, r1.Right, r1.Bottom);
            CheckAngle(r2.Left, r2.Top, r2.Right, r2.Bottom);

            if (ChooseRectangle(r1,r2))
                return 0;
            else if (ChooseRectangle(r2,r1))
                return 1;
            else
                return -1;
        }

        public static bool ChooseRectangle(Rectangle r1, Rectangle r2)
        {
            return r2.Left <= r1.Left && r1.Right <= r2.Right && r2.Bottom >= r1.Bottom && r1.Top >= r2.Top;
        }
    }
}