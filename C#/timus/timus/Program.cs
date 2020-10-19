using System;
using System.Collections.Generic;
using System.Text;

namespace timus
{
    public class Timus
    {
        public static void Main()
        {
            long value = long.Parse(Console.ReadLine());
            List<long> result = new List<long>();
            StringBuilder line = new StringBuilder();
            bool flag = true;

            if (value > 9 || value == 0)
            {
                while (value / 10 != 0)
                {
                    if ((CheckValue(value) && value > 9) || value == 0)
                    {
                        Console.WriteLine(-1);
                        return;
                    }

                    for (int i = 9; i > 1; i--)
                    {
                        if (value % i == 0)
                        {
                            result.Insert(0, i);
                            value /= i;
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        Console.WriteLine(-1);
                        return;
                    }
                }
                result.Insert(0, value);
                for (int i = 0; i < result.Count; i++)
                    line.Append(result[i]);
            }
            else
            {
                line.Append(1);
                line.Append(value);
            }

            Console.WriteLine(line);
            Console.ReadKey();
        }

        public static bool CheckValue (long value)
        {
            bool simple = true;

            for (int i = 2; i <= value / 2; i++)
            {
                if (value % i == 0)
                {
                    simple = false;
                    break;
                }
            }
            return simple;
        }
    }
}
