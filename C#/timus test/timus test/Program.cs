using System;
using System.Collections.Generic;
using System.Text;

namespace timus
{
    public class Program
    {
        public static void Main()
        {
            var count = int.Parse(Console.ReadLine());
            int[,] arr = new int[count, count];
            var value = 2;
            var i = 0;
            var j = count - 1;

            for (int c = 2; c <= count; c++)
            {
                var a = i - 1;
                var b = j - (c - 1) - 1;

                for (int q = 0; q < c; q++)
                {
                    a++;
                    b++;

                    arr[a, b] = value;
                    value++;
                }
            }

            for (int c = count - 1; c >= 2; c-- )
            {
                var a = i + (count - c) - 1;
                var b = j - (count - 1) - 1;

                for (int q = 0; q < c; q++)
                {
                    a++;
                    b++;

                    arr[a, b] = value;
                    value++;
                }
            }

            arr[count - 1, 0] = value;
            arr[i, j] = 1;
            
            for (int v = 0; v < count; v++)
            {
                for (int w = 0; w < count; w++)
                    Console.Write(arr[v, w] + " ");

                Console.WriteLine(" ");
            }

            Console.ReadKey();
        }
    }
}


