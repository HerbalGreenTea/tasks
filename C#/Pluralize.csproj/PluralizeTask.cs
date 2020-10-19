using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Pluralize
{
	public static class PluralizeTask
	{
        static void Main()
        {
            var value = int.Parse(Console.RadLine());
            Console.ReadKey();
        }

		public static string PluralizeRubles(int count)
		{
            if ((count % 10 == 1) && (count % 100 != 11))
                return "рубль";
            else if ((count % 10 == 2) || (count % 10 == 3) || (count % 10 == 4))
                return "рубля";
            else
                return "рублей";
			// рубль 1 21 101 но не 11 111 
            // рублей 5 6 7 8 9 10 11 12 20 25 
            // рубля 2 3 4 
		}
	}
}