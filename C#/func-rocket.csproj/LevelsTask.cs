using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();
		private static Vector whiteHole = Vector.Zero;
		private static Vector blackHole = Vector.Zero;
		private static Rocket rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
		private static Vector target = new Vector(600, 200);

		public static IEnumerable<Level> CreateLevels()
		{
			yield return new Level("Zero", rocket, target, (size, v) => Vector.Zero, standardPhysics);

			yield return new Level("Heavy", rocket, target, (size, v) => new Vector(0,0.9), standardPhysics);

			yield return new Level("Up", rocket, new Vector(700, 500),
				(size, v) => new Vector(0, -1) * (300 / (size.Height - v.Y + 300)), standardPhysics);

			yield return new Level("WhiteHole", rocket, target,
				(size, v) => {
					Vector vector = v - target;
					whiteHole = vector.Normalize() * (140 * vector.Length / (vector.Length * vector.Length + 1));
					return whiteHole; }, standardPhysics);

			yield return new Level("BlackHole", rocket, target,
				(size, v) => {
					Vector anomaly = (rocket.Location + target) / 2;
					var lenAnomaly = (v - anomaly).Length;
					blackHole = (anomaly - v).Normalize() * (300 * lenAnomaly / (lenAnomaly * lenAnomaly + 1));
					return blackHole; }, standardPhysics);

			yield return new Level("BlackAndWhite", rocket, target, (size, v) => (whiteHole + blackHole) / 2, standardPhysics);
		}
	}
}