using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			double previousSmoothValue = 0.0;
			bool startSmoothValue = true;

			foreach (var element in data)
			{
				if (startSmoothValue)
				{
					previousSmoothValue = element.OriginalY;
					startSmoothValue = false;
				}
				else
					previousSmoothValue = CalculateExpSmoothing(element.OriginalY, alpha, previousSmoothValue);

				element.ExpSmoothedY = previousSmoothValue;
				yield return element;
			}
		}

		public static double CalculateExpSmoothing(double x, double alpha, double previousValue)
		{
			return alpha * x + (1.0 - alpha) * previousValue;
		}
	}
}