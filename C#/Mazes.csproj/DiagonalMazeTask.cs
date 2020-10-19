namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            if (width > height)
            {
                GoMazeRight(robot, width, height);
            }
            else 
            {
                GoMazeDown(robot, width, height);
            }

		}

        public static void GoMazeRight(Robot robot, int width, int height)
        {
            for (int i = 0; i < height - 3; i++)
            {
                MoveRight(robot, width, height);
                robot.MoveTo(Direction.Down);
            }
            MoveRight(robot, width, height);
        }

        public static void GoMazeDown(Robot robot, int width, int height)
        {
            for (int i = 0; i <= width - 4; i++)
            {
                MoveDown(robot, width, height);
                robot.MoveTo(Direction.Right);
            }
            MoveDown(robot, width, height);
        }

        public static void MoveRight(Robot robot, int width, int height)
        {
            for (int i = 0; i <= width/height ; i++)
                robot.MoveTo(Direction.Right);
        }

        public static void MoveDown(Robot robot, int width, int height)
        {
            for (int i = 0; i < (height-2)/(width-2); i++)
                robot.MoveTo(Direction.Down);
        }
    }
}