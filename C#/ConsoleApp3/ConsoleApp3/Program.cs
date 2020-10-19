using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StyleErrors
{
    class Program
    {
        static void Main()
        {
            string[] name = new string[3];
            int[] math = new int[3];
            int[] inf = new int[3];

            int[] prog = new int[3];

            StudentData Student = new StudentData();

            Console.WriteLine();
            int switchOn = int.Parse(Console.ReadLine());

            switch (switchOn)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
    }

    class StudentData
    {
        static void Lesson()
        {
            int[] math = new int[3];
            int[] inf = new int[3];
            int[] prog = new int[3];

            GetLessonData();

            Console.WriteLine("Заполните лист успеваемости");
            Console.WriteLine("математика: ");
        }
        static void GetLessonData()
        {
                math[i] = int.Parse(Console.ReadLine());
                Console.WriteLine();
                inf[i] = int.Parse(Console.ReadLine());
                Console.WriteLine();
                prog[i] = int.Parse(Console.ReadLine());
        }

        static void LastName()
        {

        }
    }

}
