using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greedy.Architecture;
using System.Drawing;

namespace Greedy
{
    public class DijkstraData
    {
        public Point Previous { get; set; }
        public int Price { get; set; }
    }

    public class DijkstraPathFinder
    {
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            var targetsDict = new Dictionary<Point, int>();
            var notVisited = new List<Tuple<Point, int>>();
            var track = new Dictionary<Point, DijkstraData>();
            var visited = new HashSet<Point>();

            foreach (var el in targets)
            {
                targetsDict[el] = (Math.Abs(el.X) - Math.Abs(start.X)) + (Math.Abs(el.Y) - Math.Abs(start.Y));
            }

            targetsDict = targetsDict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);


            for (int i = 0; i < state.MapWidth; i++)
                for (int j = 0; j < state.MapHeight; j++)
                {
                    if (new Point(i,j) != start && !state.IsWallAt(i, j) && state.InsideMap(i, j))
                        notVisited.Add(Tuple.Create(new Point(j, i), state.CellCost[i, j]));
                }

            track[start] = new DijkstraData { Previous = new Point(-1, -1), Price = 0 };

            foreach (var el in targetsDict)
            {
                yield return FindPatch(state, start, el.Key, notVisited, track, visited);
            }
        }

        public PathWithCost FindPatch(State state, Point start, Point target, List<Tuple<Point, int>> notVisited, 
            Dictionary<Point, DijkstraData> track, HashSet<Point> visited)
        {
            while (true)
            {
                Point toOpne = new Point(-1, -1);
                var bestPrice = double.PositiveInfinity;
                foreach (var el in notVisited)
                {
                    if (track.ContainsKey(el.Item1) && track[el.Item1].Price < bestPrice)
                    {
                        bestPrice = track[el.Item1].Price;
                        toOpne = el.Item1;
                    }
                }

                if (toOpne == new Point(-1, -1)) return null;
                if (toOpne == target) break;

                for (int dx = -1; dx < 1; dx++)
                    for (int dy = -1; dy < 1; dy++)
                    {
                        if (Math.Abs(dx) + Math.Abs(dy) != 1 && !visited.Contains(new Point(toOpne.X + dx, toOpne.Y + dy)))
                        {
                            var currentPrice = track[toOpne].Price + state.CellCost[toOpne.X + dx, toOpne.Y + dy];
                            var nextPoint = new Point(toOpne.X + dx, toOpne.Y + dy);
                            visited.Add(nextPoint);
                            if (!track.ContainsKey(nextPoint) || track[nextPoint].Price > currentPrice)
                            {
                                track[nextPoint] = new DijkstraData { Previous = toOpne, Price = currentPrice };
                            }
                        }
                    }

                notVisited.Remove(Tuple.Create(toOpne, state.CellCost[toOpne.Y, toOpne.X]));
            }

            var result = new List<Point>();
            var cost = 0;
            while (target != new Point(-1, -1))
            {
                result.Add(target);
                cost += track[target].Price;
                target = track[target].Previous;
            }
            result.Reverse();
            Point[] patch = result.ToArray();

            return new PathWithCost(cost, patch);
        }
    }
}
