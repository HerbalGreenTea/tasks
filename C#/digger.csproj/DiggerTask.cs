using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    public class Terrain : ICreature
    {
        CreatureCommand ICreature.Act(int x, int y)
        {
            return new CreatureCommand();
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }

        int ICreature.GetDrawingPriority()
        {
            return 1;
        }

        string ICreature.GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand result = new CreatureCommand();

            if (Game.KeyPressed == Keys.Right && x + 1 < Game.MapWidth)
                result.DeltaX = 1;
            else if (Game.KeyPressed == Keys.Left && x - 1 >= 0)
                result.DeltaX = -1;
            else if (Game.KeyPressed == Keys.Up && y - 1 >= 0)
                result.DeltaY = -1;
            else if (Game.KeyPressed == Keys.Down && y + 1 < Game.MapHeight)
                result.DeltaY = 1;

            return (CheckIsSack(x + result.DeltaX, y + result.DeltaY)) ? result : new CreatureCommand();
        }

        public bool CheckIsSack(int coordinateX, int coordinateY)
        {
            return !(Game.Map[coordinateX, coordinateY] is Sack);
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Sack : ICreature
    {
        public int Way = 0;

        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand result = new CreatureCommand();
            var height = Game.MapHeight;
            Object gameMap = null;

            if (y + 1 < height)
                gameMap = Game.Map[x, y + 1];

            if (y + 1 < height && (gameMap == null || ((gameMap is Player || gameMap is Monster) && Way > 0)))
            {
                result.DeltaY = 1;
                if (gameMap == null || gameMap is Monster)
                    Way++;
            }
            else if (Way > 1 && ((y + 1 < height && !(gameMap is null)) || (y == height - 1)))
            {
                result.TransformTo = new Gold();
            }
            return result;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Gold;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                Game.Scores += 10;
            }
            return conflictedObject is Player || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            CreatureCommand result = new CreatureCommand();

            bool checkDigger = false;
            var posPlayer = 0;
            var posMonster = 0;
            FindDigger(ref posPlayer, ref posMonster, ref checkDigger);

            if (checkDigger && x > posPlayer && x - 1 >= 0)
                result.DeltaX = -1;
            else if (checkDigger && x < posPlayer && x + 1 < Game.MapWidth)
                result.DeltaX = 1;
            else if (checkDigger && y > posMonster && y - 1 >= 0)
                result.DeltaY = -1;
            else if (checkDigger && y < posMonster && y + 1 < Game.MapHeight)
                result.DeltaY = 1;

            return CheckIsGoldOrPlayer(result.DeltaX + x, result.DeltaY + y) ? result : new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public void FindDigger(ref int a, ref int b, ref bool diggerFlag)
        {
            for (int i = 0; i < Game.MapHeight; i++)
            {
                for (int j = 0; j < Game.MapWidth; j++)
                {
                    if (Game.Map[j, i] is Player)
                    {
                        a = j;
                        b = i;
                        diggerFlag = true;
                    }
                }
            }
        }

        public bool CheckIsGoldOrPlayer(int coordinateX, int coordinateY)
        {
            return !(Game.Map[coordinateX, coordinateY] is Terrain)
                && !(Game.Map[coordinateX, coordinateY] is Monster)
                && !(Game.Map[coordinateX, coordinateY] is Sack);
        }
    }
}
