using System;
using System.Drawing;
using System.Linq;

namespace func_rocket
{
	public class ForcesTask
	{
		public static RocketForce GetThrustForce(double forceValue)
		{
			return rocket => new Vector((rocket.Location.X + Math.Cos(rocket.Direction) * forceValue) - rocket.Location.X, 
								        (rocket.Location.Y + Math.Sin(rocket.Direction) * forceValue) - rocket.Location.Y);
		}

		public static RocketForce ConvertGravityToForce(Gravity gravity, Size spaceSize)
		{
			return rocket => gravity(spaceSize, rocket.Location);
		}

		public static RocketForce Sum(params RocketForce[] forces)
		{
			return rocket => {
				Vector result = Vector.Zero;
				foreach (var el in forces)
				{
					result += el(rocket);
				}
				return result;
			};
		}
	}
}