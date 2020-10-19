using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application
{
    class Program
    {
        static void Main()
        {
            //char space = ' ';
            string data = Console.ReadLine(); // исходную сумму, процентную ставку (в процентах) и срок вклада в месяцах
            Console.WriteLine(Calculate(data));
            Console.ReadKey();
        }

        public static double Calculate(string userInput)
        {
            string[] dataLine = userInput.Split(' ');
            double sum = double.Parse(dataLine[0]);
            double percent = double.Parse(dataLine[1]);
            int numMonths = int.Parse(dataLine[2]);
            double percentMonths = (percent / 1200) + 1; // + к сумме в месяц 

            sum *= Math.Pow(percentMonths, numMonths);

            return sum;
        }
    }
}
