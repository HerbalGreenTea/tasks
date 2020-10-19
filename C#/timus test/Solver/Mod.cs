using System;
using System.Collections.Generic;
using System.Text;

namespace Solver
{
    public class SolveRing
    {
        public static int ModRing(int countRing, int valueOne, int valueTwo)
        {
            return (valueOne * valueTwo) % countRing;
        }
    }
}
