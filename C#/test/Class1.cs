using System;

public static List<Point> ParsePoints(IEnumerable<string> lines)
{
	return lines
		.Select(line => {
			var arg1 = "";
			var arg2 = "";
			bool flag = false;

			for (int i = 0; i < line.Length; i++)
			{
				if (line[i] == ' ') {
					flag = true;
					continue;
				}

				if (flag) arg2 += line[i];
				else arg1 += line[i];
			}
			return new Point(int.Parse(arg1), int.Parse(arg2));
		})
		.ToList<Point>();
}
