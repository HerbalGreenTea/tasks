namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double[,] ToGrayscale(Pixel[,] original)
		{
            var grayscale = new double[original.GetLength(0), original.GetLength(1)];
            var lengthRow = original.GetLength(0);
            var lengthColumn = original.GetLength(1);

            for (int v = 0; v < lengthRow; v++)
                for (int w = 0; w < lengthColumn; w++)
                    grayscale[v, w] = CalculateBrightness(original[v,w]);

			return grayscale;
		}

        public static double CalculateBrightness (Pixel pixel)
        {
            return (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255;
        }
    }
}