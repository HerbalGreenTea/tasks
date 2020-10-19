using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
	public class BfsTask
	{
	    public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
			HashSet<Point> pointsChests = new HashSet<Point>();
			HashSet<Point> visits = new HashSet<Point>();

			foreach (var el in chests)
				pointsChests.Add(el);

			Queue<SinglyLinkedList<Point>> points = new Queue<SinglyLinkedList<Point>>();
			points.Enqueue(new SinglyLinkedList<Point>(start, null));

			while(points.Count != 0)
			{
				var listPoints = points.Dequeue();
				if (listPoints.Value.X < 0 || listPoints.Value.X >= map.Dungeon.GetLength(0)
				 || listPoints.Value.Y < 0 || listPoints.Value.Y >= map.Dungeon.GetLength(1))
					continue;

				if (map.Dungeon[listPoints.Value.X, listPoints.Value.Y] != MapCell.Empty 
					|| visits.Contains(new Point(listPoints.Value.X, listPoints.Value.Y))) continue;

				visits.Add(new Point(listPoints.Value.X, listPoints.Value.Y));

				if (pointsChests.Contains(listPoints.Value))
					yield return listPoints;

				for (int dy = -1; dy <= 1; dy++)
				{
					for (int dx = -1; dx <= 1; dx++)
					{
						if (Math.Abs(dy) + Math.Abs(dx) != 1)
							continue;
						else
							points.Enqueue(new SinglyLinkedList<Point>
								(new Point(listPoints.Value.X + dx, listPoints.Value.Y + dy), listPoints));
					}
				}
			}
		}
	}
}