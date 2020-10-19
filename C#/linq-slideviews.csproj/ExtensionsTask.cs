using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		public static double Median(this IEnumerable<double> items)
		{
			var data = items
				.OrderBy(num => num)
				.ToArray();

			if (data.Length == 0)
				throw new  InvalidOperationException();
			else if (data.Length % 2 == 0)
				return (data[data.Length / 2] + data[data.Length / 2 - 1]) / 2;
			else
				return data[data.Length / 2];
		}

		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var flag = false;
			var temp = default(T);
			foreach (var el in items)
			{
				if (flag)
					yield return Tuple.Create(temp, el);
				temp = el;
				flag = true;
			}
		}
	}
}