using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Weights
{
	public class Indexer
	{
		public readonly double[] OldArr;
		public readonly double[] NeuronWeights;
		private int startIndex;
		public int StartIndex
		{
			set
			{
				Check(value, 0, OldArr.Length, () => throw new ArgumentException());
				startIndex = value;
			}
			get { return startIndex; } 
		}
		private int length;
		public int Length
		{
			set
			{
				Check(value, 0, OldArr.Length - StartIndex, () => throw new ArgumentException());
				length = value;
			}
			get { return length; }
		}

		public Indexer(double[] arr, int start, int length)
		{
			OldArr = arr;
			StartIndex = start;
			Length = length;
			NeuronWeights = OldArr.Skip(start).Take(length).ToArray();
		}

		public double this[int index]
		{
			get 
			{
				Check(index, 0, NeuronWeights.Length - 1, () => throw new IndexOutOfRangeException());
				return OldArr[index + StartIndex]; 
			}
			set 
			{
				Check(index, 0, NeuronWeights.Length - 1, () => throw new IndexOutOfRangeException());
				OldArr[index + StartIndex] = value; 
			}
		}

		public void Check(int value, int arg1, int arg2, Action action)
		{
			if (value < arg1 || value > arg2)
				action();
		}
	}
}
