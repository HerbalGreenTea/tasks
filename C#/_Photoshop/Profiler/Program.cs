using System;
using System.Diagnostics;
using MyPhotoshop;

namespace Profiler
{
    class Program
    {
        static void Test(Action<double[], LighteningParameters> action, int n)
        {
            var args = new double[] { 0 };
            var obj = new LighteningParameters();
            action(args, obj);

            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < n; i++)
                action(args, obj);
            watch.Stop();
            Console.WriteLine(1000 * (double)watch.ElapsedMilliseconds / n);
        }

        static void Main(string[] args)
        {
            Test((values, pars) => pars.SetValues(values), 100000);

            Test((values, pars) => pars.Coefficient = values[0], 100000);
        }
    }
}
