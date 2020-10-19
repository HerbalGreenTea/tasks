using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public static class Test
    {
        public static void Main()
        {
            int[] array = new int[3] { 1, 5, 5 };
            int max = 0;

            if (array != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[max] < array[i])
                        max = i; 
                }
                Console.WriteLine(max);
            }
            else
                Console.WriteLine(-1);


            Console.ReadKey();
        }
    }
}
