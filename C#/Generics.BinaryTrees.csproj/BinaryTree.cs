using System;
using System.Collections;
using System.Collections.Generic;

namespace Generics.BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        public T Value { get; set; }
        public Left<T> Left { get; set; }
        public Right<T> Right { get; set; }
        public List<T> DotList { get; set; }

        public BinaryTree()
        {
            DotList = new List<T>();
            Left = new Left<T>();
            Right = new Right<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var el in DotList)
                yield return el;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T nodeValue)
        {
            DotList.Add(nodeValue);
            if (DotList.Count == 1)
                Value = nodeValue;
            else if (nodeValue.CompareTo(Value) < 1)
                Left.Value = nodeValue;
            else
                Right.Value = nodeValue;
        }
    }

    public class BinaryTree
    {
        public static int[] Create(params int[] data)
        {
            Array.Sort(data);
            return data;
        }
    }

    public class Left<T>
    {
        public T Value { get; set; }
    }

    public class Right<T>
    {
        public T Value { get; set; }
    }
}