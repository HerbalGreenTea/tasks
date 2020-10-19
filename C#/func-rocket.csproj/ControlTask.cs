using System;

namespace func_rocket
{
	public class ControlTask
	{
		public static Turn ControlRocket(Rocket rocket, Vector target)
		{
			Vector directionTarget = target - rocket.Location;
			Vector directionRocket = new Vector(1, 1).Rotate(rocket.Direction) + rocket.Velocity;
			var angleTarget = directionTarget.Angle;

			if (directionRocket.Angle > angleTarget)
				return Turn.Left;
			else if (directionRocket.Angle < angleTarget)
				return Turn.Right;
			else
				return Turn.None;
		}
	}
}