using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class Element // можно было Tuple но так думаю код более понятен
	{
		public int Index = 0;
		public int Count = 0;
	}

	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			var begin = new Dictionary<int, int>();
			var end = new Dictionary<int, Element>();

			FindPairsBrackets(vm, begin, end);

			vm.RegisterCommand('[', b => {
				end[begin[b.InstructionPointer]].Count = vm.Memory[vm.MemoryPointer];
				if (end[begin[b.InstructionPointer]].Count <= 0)
					b.InstructionPointer = begin[b.InstructionPointer];
			});

			vm.RegisterCommand(']', b => {
				end[b.InstructionPointer].Count--;
				if (end[b.InstructionPointer].Count >= 1)
					b.InstructionPointer = end[b.InstructionPointer].Index;
			});
		}

		static void FindPairsBrackets(IVirtualMachine vm, Dictionary<int, int> begin, Dictionary<int,Element> end)
		{
			var indexes = new Stack<int>();

			for (int i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[')
				{
					begin.Add(i,0);
					indexes.Push(i);
				}
				else if (vm.Instructions[i] == ']')
				{
					begin[indexes.Peek()] = i;
					end.Add(i, new Element { Index = indexes.Pop() });
				}
			}
		}
	}
}