using System.Drawing;
using System;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
            var random = new Random(seed);

            double buf;
            double x = 1;
            double y = 0;
            pixels.SetPixel(x, y);

            for (int i = 0; i < iterationsCount; i++)
            {
                if (random.Next(2) == 0)
                {
                    buf = x;
                    x = (x - y) / 2;
                    y = (buf + y) / 2;
                }
                else
                {
                    buf = x;
                    x = (x * Math.Cos(3 * Math.PI / 4) - y * Math.Sin(3 * Math.PI / 4)) / Math.Sqrt(2) + 1;
                    y = (buf * Math.Sin(3 * Math.PI / 4) + y * Math.Cos(3 * Math.PI / 4)) / Math.Sqrt(2);
                }
                pixels.SetPixel(x, y);
            }
		}
    }
}