using System;
using System.Collections.Generic;
using System.Diagnostics;
using MyPhotoshop;

namespace Profiler
{
    class Program
    {
        static void Test(Func<double[], LighteningParameters> func, int n)
        {
            var args = new double[] { 0 };
            func(args);

            var watch = new Stopwatch();
            //watch.Start();
            for (int i = 0; i < n; i++)
                func(args);
            //watch.Stop();
            //Console.WriteLine(1000 * (double)watch.ElapsedMilliseconds / n);
        }

        static void Test2 (int n)
        {
            var lt = new List<StaticParametersHandler<LighteningParameters>>();
            var watch = new Stopwatch();
            
            for (int i = 0; i < n; i++)
                lt.Add(new StaticParametersHandler<LighteningParameters>());

            watch.Start();
            foreach(var el in lt)
            {
                Test(values => el.CreateParameters(values), 100);
            }
            watch.Stop();
            Console.WriteLine(1000 * (double)watch.ElapsedMilliseconds / n);
        }

        static void Test3(int n)
        {
            var lt = new List<SimpleParametersHandler<LighteningParameters>>();
            var watch = new Stopwatch();

            for (int i = 0; i < n; i++)
                lt.Add(new SimpleParametersHandler<LighteningParameters>());

            watch.Start();
            foreach (var el in lt)
            {
                Test(values => el.CreateParameters(values), 100);
            }
            watch.Stop();
            Console.WriteLine(1000 * (double)watch.ElapsedMilliseconds / n);
        }

        static void Main()
        {
            //var watch = new Stopwatch();

            //watch.Start();
            //for (int i = 0; i < 10; i++)
            //{
            //    var simpleHandler = new SimpleParametersHandler<LighteningParameters>();
            //    var a = new SimpleParametersHandler<LighteningParameters>();
            //    var b = new SimpleParametersHandler<LighteningParameters>();
            //    Test(values => simpleHandler.CreateParameters(values), 100000);
            //}
            //watch.Stop();
            //Console.WriteLine(1000 * (double)watch.ElapsedMilliseconds / 10);

            //watch.Reset();


            //watch.Start();
            //for (int i = 0; i < 10; i++)
            //{
            //    var staticHandler = new StaticParametersHandler<LighteningParameters>();
            //    var a = new StaticParametersHandler<LighteningParameters>();
            //    var b = new StaticParametersHandler<LighteningParameters>();
            //    Test(values => staticHandler.CreateParameters(values), 100000);
            //}
            //watch.Stop();
            //Console.WriteLine(1000 * (double)watch.ElapsedMilliseconds / 10);

            //var expressionHandler = new ExpressionsParametersHandler<LighteningParameters>();
            //Test(values => expressionHandler.CreateParameters(values), 100000);

            //Test(values => new LighteningParameters { Coefficient = values[0] }, 100000);

            Test2(1000);
            Test3(1000);
        }
    }
}
