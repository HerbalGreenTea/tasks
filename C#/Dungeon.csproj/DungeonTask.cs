using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
	public class DungeonTask
	{
		public static MoveDirection[] FindShortestPath(Map map)
		{
			MoveDirection[] result = new MoveDirection[0];
			var maxPointMap = map.Dungeon.Length;
			var patchesPlayer = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
			var patchesExit = BfsTask.FindPaths(map, map.Exit, map.Chests);

			var patchNoChests = BfsTask.FindPaths(map, map.InitialPosition, new Point[] { map.Exit }); 

			var patchJoin = patchesPlayer
					.Join(patchesExit, begin => begin.Value, end => end.Value,
					(begin, end) => new { playerPatch = begin, exitPatch = end }).ToList();

			if (patchesPlayer.Count() + patchesExit.Count() > 0)
			{
				foreach (var obj in patchJoin)
				{
					var countPoint = obj.playerPatch.Count() + obj.exitPatch.Count() - 1;

					if (countPoint < maxPointMap)
					{
						maxPointMap = countPoint;
						result = CreatePatch(obj.playerPatch.Reverse().ToList(), obj.exitPatch.Skip(1).ToList());
					}
				}
			}
			else
			{
				foreach(var obj in patchNoChests)
				{
					var countPoint = obj.Count();

					if (countPoint < maxPointMap)
					{
						maxPointMap = countPoint;
						MoveDirection[] patch = new MoveDirection[countPoint - 1];

						var points = obj.Reverse().ToList();
						var previos = points.First();

						for (int i = 1; i < points.Count; i++)
						{
							if (previos.X + 1 == points[i].X)
								patch[i - 1] = MoveDirection.Right;
							else if (previos.X - 1 == points[i].X)
								patch[i - 1] = MoveDirection.Left;
							else if (previos.Y + 1 == points[i].Y)
								patch[i - 1] = MoveDirection.Down;
							else
								patch[i - 1] = MoveDirection.Up;

							previos = points[i];
						}

						result = patch;
					}
				}
			}

			return result;
		}

		public static MoveDirection[] CreatePatch(List<Point> pointsPlayer, List<Point> pointsExit)
		{
			int count = pointsPlayer.Count() + pointsExit.Count() - 1;
			MoveDirection[] patch = new MoveDirection[count];

			var previos = pointsPlayer.First();

			for (int i = 1; i < pointsPlayer.Count; i++)
			{
				if (previos.X + 1 == pointsPlayer[i].X)
					patch[i - 1] = MoveDirection.Right;
				else if (previos.X - 1 == pointsPlayer[i].X)
					patch[i - 1] = MoveDirection.Left;
				else if (previos.Y + 1 == pointsPlayer[i].Y)
					patch[i - 1] = MoveDirection.Down;
				else
					patch[i - 1] = MoveDirection.Up;

				previos = pointsPlayer[i];
			}

			for (int i = pointsPlayer.Count - 1; i < pointsPlayer.Count - 1 + pointsExit.Count; i++)
			{
				if (previos.X + 1 == pointsExit[i - (pointsPlayer.Count - 1)].X)
					patch[i] = MoveDirection.Right;
				else if (previos.X - 1 == pointsExit[i - (pointsPlayer.Count - 1)].X)
					patch[i] = MoveDirection.Left;
				else if (previos.Y + 1 == pointsExit[i - (pointsPlayer.Count - 1)].Y)
					patch[i] = MoveDirection.Down;
				else
					patch[i] = MoveDirection.Up;

				previos = pointsExit[i - (pointsPlayer.Count - 1)];
			}

			return patch;
		}
	}
}
