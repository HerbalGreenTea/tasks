using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{
			return lines
				.Where(line => 
				{
					SlideType slideType;
					int num;
					return line.Split(';').Length == 3 && Enum.TryParse(line.Split(';')[1], true, out slideType)
					&& int.TryParse(line.Split(';')[0], out num);
				})
				.Select(line => 
				{
					var words = line.Split(';');
					SlideType slideType = (SlideType)Enum.Parse(typeof(SlideType),words[1], true);

					return Tuple.Create(int.Parse(words[0]), new SlideRecord(int.Parse(words[0]), slideType, words[2]));
				})
				.ToDictionary(line => line.Item1, line => line.Item2);
		}

		public static IEnumerable<VisitRecord> ParseVisitRecords(
			IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines
				.Skip(1)
				.Where(line => {
					if (Char.IsDigit(line[0]) && line.Split(';').Length == 4 && line.Split(';')[3] != ""
					&& Char.IsDigit(line.Split(';')[1][0]) && line.Split(';')[1].Length < 6)
						return true;
					else
						throw new FormatException("Wrong line [" + line + "]");
				})
				.Select(line => {
					var words = line.Split(';');
					var userID = int.Parse(words[0]);
					var slideID = int.Parse(words[1]);
					var data = words[2].Split('-');
					var time = words[3].Split(':');

					if (data.Length < 3 || time.Length < 3 || int.Parse(data[1]) > 12 || int.Parse(data[1]) < 1
					|| int.Parse(time[1]) > 59 || data[0].Length > 4)
					{
						throw new FormatException("Wrong line [" + line + "]");
					}

					DateTime dateTime = new DateTime(
						int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]),
						int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));

					return new VisitRecord(userID, slideID, dateTime, slides[slideID].SlideType);
				})
				.ToList();
		}
	}
}