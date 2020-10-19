using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var height = original.GetLength(0);
            var width = original.GetLength(1);
            var countWhite = (int)(whitePixelsFraction * height * width);
            List<double> arrSort = new List<double>();

            if ((width * height <= countWhite) || countWhite == 0)
                return VerifyExceptions(height, width, countWhite, original);
            
            AddList(height, width, arrSort, original);

            arrSort.Sort();
            var pixel = arrSort[arrSort.Count - countWhite];

            return ApplyFilter(height, width, original, pixel);
        }

        public static void AddList(int countRow, int countColumn, List<double> list, double[,] array)
        {
            for (int i = 0; i < countRow; i++)
                for (int j = 0; j < countColumn; j++)
                    list.Add(array[i, j]);
        }

        public static double[,] VerifyExceptions(int countRow, int countColumn, double countWhite, double[,] array)
        {
            var value = 0.0;
            if (countColumn * countRow <= countWhite || countWhite == 0)
            {
                if (countColumn * countRow <= countWhite) value = 1.0;

                for (int i = 0; i < countRow; i++)
                    for (int j = 0; j < countColumn; j++)
                        array[i, j] = value;
                return array;
            }
            return array;
        }

        public static double[,] ApplyFilter(int countRow, int countColumn, double[,] array, double value)
        {
            for (int i = 0; i < countRow; i++)
                for (int j = 0; j < countColumn; j++)
                    array[i, j] = array[i, j] >= value ? 1.0 : 0.0;
            
            return array;
        }
    }
}
