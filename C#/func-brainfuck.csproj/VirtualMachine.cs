using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		Dictionary<char, Action<IVirtualMachine>> setCommand;

		public VirtualMachine(string program, int memorySize)
		{
			setCommand = new Dictionary<char, Action<IVirtualMachine>>();

			Instructions = program;
			Memory = new byte[memorySize];
			MemoryPointer = 0;
			InstructionPointer = 0;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			setCommand.Add(symbol, execute);
		}

		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		public void Run()
		{
			while (InstructionPointer < Instructions.Length)
			{
				if (setCommand.ContainsKey(Instructions[InstructionPointer]))
					setCommand[Instructions[InstructionPointer]](this);

				InstructionPointer++;
			}
		}
	}
}