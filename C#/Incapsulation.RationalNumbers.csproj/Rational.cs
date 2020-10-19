using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        public bool IsNan = false;
        public int Numerator { get; set; }
        private int denominator;
        public int Denominator
        {
            set 
            {
                if (value == 0)
                    IsNan = true;
                denominator = value;
            }
            get { return denominator; }
        }

        public Rational(int numerator, int denominator = 1)
        {
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            var rationalParts = ReduceFraction(numerator, denominator);

            Numerator = rationalParts[0];
            Denominator = rationalParts[1];
        }

        public static Rational operator + (Rational arg1, Rational arg2)
        {
            Rational temp = FindСommonDenominator(arg1, arg2, (num1, num2) => num1 + num2);
            return ReduceFraction(temp);
        }

        public static Rational operator - (Rational arg1, Rational arg2)
        {
            Rational temp = FindСommonDenominator(arg1, arg2, (num1, num2) => num1 - num2);
            return ReduceFraction(temp);
        }

        public static Rational operator * (Rational arg1, Rational arg2)
        {
            Rational temp = new Rational(arg1.Numerator * arg2.Numerator, arg1.Denominator * arg2.Denominator);
            return ReduceFraction(temp);
        }

        public static Rational operator / (Rational arg1, Rational arg2)
        {
            Rational temp = new Rational(arg1.Numerator * arg2.Denominator, arg1.Denominator * arg2.Numerator);
            return ReduceFraction(temp);
        }

        public static implicit operator double (Rational rational)
        {
            if (rational.Denominator == 0)
                return double.NaN;

            return (double)rational.Numerator / (double)rational.Denominator;
        }

        public static implicit operator Rational (int value)
        {
            return new Rational(value, 1);
        }

        public static explicit operator int(Rational rational)
        {
            if ((rational.Numerator < rational.Denominator || rational.Numerator % rational.Denominator != 0) 
                && rational.Numerator != 0)
                throw new Exception();
            return (int)(rational.Numerator / rational.Denominator);
        }

        private static Rational FindСommonDenominator(Rational arg1, Rational arg2, Func<int, int, int> operation)
        {
            if (arg1.Denominator != arg2.Denominator)
            {
                var newDenominator = arg1.Denominator * arg2.Denominator;
                var newNumeratorArg1 = arg1.Numerator * arg2.Denominator;
                var newNumeratorArg2 = arg2.Numerator * arg1.Denominator;

                return new Rational(operation(newNumeratorArg1, newNumeratorArg2), newDenominator);
            }
            else
            {
                return new Rational(operation(arg1.Numerator, arg2.Numerator), arg1.Denominator);
            }
        }

        private static Rational ReduceFraction(Rational rational)
        {
            var maxDivider = Math.Abs(rational.Numerator) < Math.Abs(rational.Denominator)
                ? Math.Abs(rational.Numerator) : Math.Abs(rational.Denominator);
            var divider = 1;

            for (int i = 1; i <= maxDivider; i++)
            {
                if (rational.Numerator % i == 0 && rational.Denominator % i == 0 && i > divider)
                {
                    divider = i;
                }
            }
            var temp = new Rational(rational.Numerator / divider, rational.Denominator / divider);
            if (temp.Numerator == 0 || temp.Denominator == 0)
                temp.IsNan = true;

            return temp;
        }

        public int[] ReduceFraction(int numerator, int denominator)
        {
            var maxDivider = Math.Abs(numerator) < Math.Abs(denominator)
                ? Math.Abs(numerator) : Math.Abs(denominator);
            var divider = 1;

            for (int i = 1; i <= maxDivider; i++)
            {
                if (numerator % i == 0 && denominator % i == 0 && i > divider)
                {
                    divider = i;
                }
            }

            return new int[] { numerator / divider, denominator / divider };
        }
    }
}
