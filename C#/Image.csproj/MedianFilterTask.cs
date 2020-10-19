using System;
using System.Collections.Generic;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
        public static double[,] MedianFilter(double[,] original)
        {
            var height = original.GetLength(0);
            var width = original.GetLength(1);
            var result = new double[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    List<double> vicinity = new List<double>();

                    var top = Math.Max(i - 1, 0);
                    var bottom = Math.Min(i + 1, height - 1);
                    var left = Math.Max(j - 1, 0);
                    var right = Math.Min(j + 1, width - 1);

                    AddList(top, bottom, left, right, vicinity, original);

                    vicinity.Sort();
                    result[i, j] = СalculateValue(vicinity);
                }
            }
            return result;
        }

        public static void AddList
            (int top, int bottom, int left, int right, List<double> vicinity, double[,] array)
        {
            for (int i = top; i <= bottom; i++)
                for (int j = left; j <= right; j++)
                    vicinity.Add(array[i, j]);
        }

        public static double СalculateValue (List<double> vicinity)
        {
            int index;
            if (vicinity.Count % 2 == 1)
            {
                index = (vicinity.Count - 1) / 2;
                return vicinity[index];
            }
            else
            {
                index = (vicinity.Count) / 2;
                return (vicinity[index] + vicinity[index - 1]) / 2;
            }
        }
    }
}