using System;

namespace SRP.ControlDigit
{
	public static class ControlDigitAlgo
	{
		public static int Upc(long number)
		{
			int sum = 0;
			int factor = 3;
			do
			{
				int digit = (int)(number % 10);
				sum += factor * digit;
				factor = 4 - factor;
				number /= 10;

			}
			while (number > 0);

			int result = sum % 10;
			if (result != 0)
				result = 10 - result;
			return result;
		}

		public static char Isbn10(long number)
		{
			var count = 10;
			var data = number.ToString();
			var sum = 0;

			for (int i = 0; i < data.Length; i++)
            {
				sum += int.Parse(data[i].ToString()) * count;
				count--;
            }

			var temp = 11 - (sum % 11);
			var result = ' ';
			if (temp == 11)
				result = 0.ToString()[0];
			else
				result = result.ToString()[0];

			return result;
		}

		public static int Isbn13(long number)
		{
			int sum = 0;
			int factor = 1;
			do
			{
				int digit = (int)(number % 10);
				sum += factor * digit;
				factor = 4 - factor;
				number /= 10;

			}
			while (number > 0);

			int result = sum % 10;
			if (result != 0)
				result = 10 - result;
			return result;
		}
	}
}
