using System;
using System.Collections.Generic;

namespace Clones
{
	public class CloneVersionSystem : ICloneVersionSystem
	{
		Dictionary<int, Clone> allClone = new Dictionary<int, Clone>();
		private int count = 1;
		public CloneVersionSystem()
		{
			allClone.Add(1, new Clone());
		}

		public string Execute(string query)
		{
			var command = query.Split(' ')[0];
			var ci = 0;
			var pi = 0;
			switch(command)
			{
				case "learn":
					ci = int.Parse(query.Split(' ')[1]);
					pi = int.Parse(query.Split(' ')[2]);
					allClone[ci].setProgram.Push(pi);
					return null;
				case "rollback":
					ci = int.Parse(query.Split(' ')[1]);
					allClone[ci].setUndo.Push(allClone[ci].setProgram.Pop());
					return null;
				case "relearn":
					ci = int.Parse(query.Split(' ')[1]);
					allClone[ci].setProgram.Push(allClone[ci].setUndo.Pop());
					return null;
				case "clone":
					ci = int.Parse(query.Split(' ')[1]);
					count++;
					allClone.Add(count, allClone[ci]);
					return null;
				case "check":
					ci = int.Parse(query.Split(' ')[1]);
					if (allClone[ci].setProgram.Count > 0)
						return allClone[ci].setProgram.Peek().ToString();
					else
						return "basic";
			}
			return null;
		}
	}

	public class Clone
	{
		public Stack<int> setProgram = new Stack<int>();
		//public Stack<string> setNamesPrograms = new Stack<string>();
		public Stack<int> setUndo = new Stack<int>();
	}
}
