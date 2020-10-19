using System;
using System.Collections.Generic;

namespace yield
{
    public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			Queue<double> bufNum = new Queue<double>();
			double sum = 0;


			foreach (var el in data)
			{
				if (bufNum.Count >= windowWidth)
					sum -= bufNum.Dequeue();
				bufNum.Enqueue(el.OriginalY);
				sum += el.OriginalY;

				el.AvgSmoothedY = sum / bufNum.Count;
				yield return el;
			}
		}
	}
}