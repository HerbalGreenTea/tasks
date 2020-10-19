using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            if (repetitionCount > 0)
                task.Run();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < repetitionCount; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                task.Run();
            }
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
		}
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        public class BuilderTest : ITask
        {
            public void Run()
            {
                var str = new StringBuilder();
                for (int i = 0; i < 10000; i++)
                    str.Append('a');
                str.ToString();
            }
        }

        public class ConstructTest : ITask
        {
            public void Run()
            {
                var line = new string('a', 10000);
            }
        }

        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var test = new Benchmark();
            var res1 = new BuilderTest();
            var res2 = new ConstructTest();

            Assert.Less(test.MeasureDurationInMs(res2, 300), Math.Round(test.MeasureDurationInMs(res1, 300)));
        } 
    }
}