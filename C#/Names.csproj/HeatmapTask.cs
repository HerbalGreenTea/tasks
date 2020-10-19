using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var pixelsX = new string[30];
            for (var x = 0; x < pixelsX.Length; x++)
                pixelsX[x] = (x + 2).ToString();

            var pixelsY = new string[12];
            for (var y = 0; y < pixelsY.Length; y++)
                pixelsY[y] = (y + 1).ToString();

            var pixels = new double[30, 12];
            foreach (var human in names)
                if (human.BirthDate.Day != 1)
                    pixels[human.BirthDate.Day - 2, human.BirthDate.Month - 1] ++;

            return new HeatmapData("Heatmap", pixels, pixelsX, pixelsY);
        }
    }
}