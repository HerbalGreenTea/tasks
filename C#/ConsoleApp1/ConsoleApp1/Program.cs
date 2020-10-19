using System;
using System.Collections.Generic;
using System.Text;

namespace Contest
{
    public class Prog
    {
        public static void Main()
        {
            var data = Console.ReadLine().Split(' ');

            var count = int.Parse(data[0]);
            var artist = int.Parse(data[1]);

            var time = new int[artist];
            var result = new int[count];

            var str = Console.ReadLine().Split(' ');

            time[0] = int.Parse(str[0]);
            for (int i = 1; i < str.Length; i++)
            {
                time[i] += int.Parse(str[i]) + time[i - 1];
            }

            result[0] = time[artist - 1];

            for (int i = 1; i < count; i++)
            {
                str = Console.ReadLine().Split(' ');
                time[0] += int.Parse(str[0]);
                for (int j = 1; j < str.Length; j++)
                {
                    

                    if (time[j - 1] <= time[j])
                        time[j] += int.Parse(str[j]);
                    else
                        time[j] += (time[j- 1] - time[j]) + int.Parse(str[j]);
                }

                if (str.Length < 2)
                    result[i] = result[i - 1] + int.Parse(str[artist - 1]);
                else
                    result[i] = time[artist - 1];
            }

            for (int i = 0; i < count; i++)
                Console.Write(result[i] + " ");
        }
    }
}
