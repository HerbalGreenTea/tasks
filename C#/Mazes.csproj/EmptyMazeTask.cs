namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            MoveRight(robot, width-2);
            MoveDown(robot, height-2);
		}

        public static void MoveRight(Robot robot, int width)
        {
            for (int i = 1; i < width; i++)
                robot.MoveTo(Direction.Right);
        }

        public static void MoveDown(Robot robot, int height)
        {
            for (int i = 1; i < height; i++)
                robot.MoveTo(Direction.Down);
        }
	}
}