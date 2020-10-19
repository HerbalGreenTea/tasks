using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Randomness
{
    public class FromDistribution : Attribute
    {
        public FromDistribution(Type type, params int[] args)
        {

        }
    }

    public class Generator<TParameter> : IContinousDistribution
    {
        public double Generate(Random rnd)
        {
            var u = rnd.NextDouble();
            var v = rnd.NextDouble();
            var x = Math.Sqrt(-2 * Math.Log(u)) * Math.Cos(2 * Math.PI * v);
            return x * Sigma + Mean;
        }
    }
}
