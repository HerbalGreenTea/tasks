using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.MapObjects
{
    public class Dwelling : IOwner
    {
        public int Owner { get; set; }
    }

    public class Mine : IOwner, IArmy, ITreasure
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Creeps : IArmy, ITreasure
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Wolfs : IArmy
    {
        public Army Army { get; set; }
    }

    public class ResourcePile : ITreasure
    {
        public Treasure Treasure { get; set; }
    }

    public static class Interaction
    {
        public static void Make(Player player, object mapObject)
        {
            if (mapObject as IArmy != null && !player.CanBeat(((IArmy)mapObject).Army))
            {
                player.Die();
                return;
            }
            
            if (mapObject as ITreasure != null)
                player.Consume(((ITreasure)mapObject).Treasure);

            if (mapObject as IOwner != null)
                ((IOwner)mapObject).Owner = player.Id;
        }
    }

    public interface IOwner
    {
        int Owner { get; set; }
    }

    public interface IArmy
    {
        Army Army { get; set; }
    }

    public interface ITreasure
    {
        Treasure Treasure { get; set; }
    }
}
