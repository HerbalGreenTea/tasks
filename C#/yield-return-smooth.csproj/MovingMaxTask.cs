using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			LinkedList<double> listMax = new LinkedList<double>();
			Queue<double> bufPixels = new Queue<double>();

			foreach (var el in data)
			{
				if (bufPixels.Count >= windowWidth && 
				(listMax.First.Value == bufPixels.Dequeue() || listMax.Count >= windowWidth))
					listMax.RemoveFirst();

				bufPixels.Enqueue(el.OriginalY);

				while (listMax.Count != 0 && listMax.Last.Value < el.OriginalY)
			    {
					listMax.RemoveLast();
			    }
				listMax.AddLast(el.OriginalY);

				el.MaxY = listMax.First.Value;
				yield return el;
			}
		}
	}
}