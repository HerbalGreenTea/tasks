using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];

            if (width == 1 && height == 1)
                return VerifyExceptions(sx, g, result);

            AddValue(sx, result, g);
            return result;
        }

        public static void AddValue(double[,] sx, double[,] result, double[,] g)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var halfRowSX = sx.GetLength(0) / 2;
            var halfColumnSX = sx.GetLength(1) / 2;
            var rowSX = sx.GetLength(0);
            var columnSX = sx.GetLength(1);

            for (int x = halfRowSX; x < width - halfRowSX; x++)
                for (int y = halfColumnSX; y < height - halfColumnSX; y++)
                {
                    var gx = 0.0;
                    var gy = 0.0;

                    for (int i = x - halfRowSX, v = 0; (i <= x + halfRowSX && v < rowSX); i++, v++)
                        for (int j = y - halfColumnSX, w = 0; (j <= y + halfColumnSX && w < columnSX); j++, w++)
                        {
                            gx += sx[v, w] * g[i, j];
                            gy += sx[w, v] * g[i, j];
                        }
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
        }

        public static double[,] VerifyExceptions(double[,] sx, double[,] g, double[,] result)
        {
            var gx = sx[0, 0] * g[0, 0];
            var gy = sx[0, 0] * g[0, 0];
            result[0, 0] = Math.Sqrt(gx * gx + gy * gy);
            return result;
        }
    }
}