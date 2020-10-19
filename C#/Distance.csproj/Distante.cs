using System;

namespace DistanceTask
{
    public static class Distance
    {
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, 
                                                  double x, double y)
        {
            var ab = FindingCutLength(bx - ax, by - ay); 
            var bc = FindingCutLength(x - bx, y - by); 
            var ca = FindingCutLength(ax - x, ay - y);
            var minNum = 0.00001;

            if (ab < minNum)
                return ca;
            else if (ZoneCheck(ab, ca, bc) || ZoneCheck(ab, bc, ca))
                return Math.Min(ca, bc);
            else if (Math.Abs((by - ay) * (x - ax) - (y - ay) * (bx - ax)) < minNum)
                return 0.0;
            else
                return FindingPerpendicularLength(ax, ay, bx, by, x, y);
        }

        public static bool ZoneCheck(double a, double b, double c)
        {
            return c * c > a * a + b * b;
        }

        public static double FindingCutLength(double a, double b)
        {
            return Math.Sqrt(a * a + b * b); 
        }

        public static double FindingPerpendicularLength(double a1, double a2, double b1, double b2, 
                                                        double cx, double cy)
        {
            var c = a1 * b2 - a2 * b1;
            var a = a2 - b2;
            var b = b1 - a1;

            return Math.Abs(a * cx + b * cy + c) / Math.Sqrt(a * a + b * b);
        }
    }
}
