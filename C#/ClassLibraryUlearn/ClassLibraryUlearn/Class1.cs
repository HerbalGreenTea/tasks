using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(new Vector() { X = X, Y = Y }, vector);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(new Vector() {X = X, Y = Y }, segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.CalculateLength(new Vector() { X = End.X - Begin.X, Y = End.Y - Begin.Y });
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, new Segment() { Begin = Begin, End = End });
        }
    }

    public class Geometry
    {
        public static double CalculateLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static double GetLength(Vector vector)
        {
            return CalculateLength(vector);
        }

        public static double GetLength(Segment segment)
        {
            return CalculateLength
                (new Vector() { X = segment.End.X - segment.Begin.X, Y = segment.End.Y - segment.Begin.Y });
        }

        public static bool IsVectorInSegment(Vector pixel, Segment segment)
        {
            double k = 0;
            if (Math.Abs(segment.End.X - segment.Begin.X) != 0)
                k = Math.Abs(segment.End.Y - segment.Begin.Y) / Math.Abs(segment.End.X - segment.Begin.X);

            var b = segment.Begin.Y - k * segment.Begin.X;
            var result = pixel.Y >= segment.Begin.Y && pixel.Y <= segment.End.Y
                      && pixel.X >= segment.Begin.X && pixel.X <= segment.End.X;

            if (k * pixel.X != 0) return (pixel.Y == k * pixel.X + b) && result;
            else return result;
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            Vector result = new Vector();
            result.X = vector1.X + vector2.X;
            result.Y = vector1.Y + vector2.Y;

            return result;
        }
    }
}
