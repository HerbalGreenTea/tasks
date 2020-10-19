namespace Mazes
{
	public static class SnakeMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            for (int i = 0; i < (height-2)/4; i++)
            {
                MoveRight(robot, width - 2);
                MoveDownTwo(robot);
                MoveLeft(robot, width - 2);
                MoveDownTwo(robot);
            }
            MoveRight(robot, width - 2);
            MoveDownTwo(robot);
            MoveLeft(robot, width - 2);
        }

        public static void MoveRight(Robot robot, int width)
        {
            for (int i = 1; i < width; i++)
                robot.MoveTo(Direction.Right);
        }

        public static void MoveLeft(Robot robot, int width)
        {
            for (int i = 1; i < width; i++)
                robot.MoveTo(Direction.Left);
        }

        public static void MoveDownTwo(Robot robot)
        {
            robot.MoveTo(Direction.Down);
            robot.MoveTo(Direction.Down);
        }
    }
}