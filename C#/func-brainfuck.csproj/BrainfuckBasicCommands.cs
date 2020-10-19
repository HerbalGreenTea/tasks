using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

			foreach (var el in symbols)
			{
				vm.RegisterCommand(el, b => { b.Memory[b.MemoryPointer] = (byte)el; });
			}

			RegisterInputAndOutput(vm, read, write);
			RegisterIncreaseAndDecrease(vm);
			RegisterPointerMovement(vm);
		}

		static void RegisterInputAndOutput(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });

			vm.RegisterCommand(',', b => {
				var value = read();
				if (value != -1)
					b.Memory[b.MemoryPointer] = (byte)value;
			});
		}

		static void RegisterIncreaseAndDecrease(IVirtualMachine vm)
		{
			vm.RegisterCommand('+', b => {
				if (b.Memory[b.MemoryPointer] < 255)
					b.Memory[b.MemoryPointer]++;
				else
					b.Memory[b.MemoryPointer] = 0;
			});

			vm.RegisterCommand('-', b => {
				if (b.Memory[b.MemoryPointer] > 0)
					b.Memory[b.MemoryPointer]--;
				else
					b.Memory[b.MemoryPointer] = 255;
			});
		}

		static void RegisterPointerMovement(IVirtualMachine vm)
		{
			vm.RegisterCommand('<', b => {
				if (b.MemoryPointer > 0)
					b.MemoryPointer--;
				else
					b.MemoryPointer = b.Memory.Length - 1;
			});

			vm.RegisterCommand('>', b => {
				if (b.MemoryPointer < b.Memory.Length - 1)
					b.MemoryPointer++;
				else
					b.MemoryPointer = 0;
			});
		}
	}
}