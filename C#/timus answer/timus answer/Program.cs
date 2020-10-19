using System;

namespace Timus
{
    public class Pro
    {
        public static void Main()
        {
            Program<Test>.Result();
            Console.ReadKey();
        }
    }

    public class Program<T>
    {
        public T Name { get; set; }
        public int Num { get; set; }

        public static void Result ()
        {
            var test = new Program<Test> {Num = 5};
            test.Name.Print();
        }
    }

    public class Test
    {
        public readonly int result;
        public Test (int a)
        {
            result = a;
        }

        public void Print ()
        {
            Console.WriteLine(result);
        }
    }
}

/*
 * public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
        PUBLIC T TASK = DEFALT();



            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            for (int i = 0; i < Constants.FieldCounts.Count; i++)
            {
                if (etr.MoveNext())
                {
                    var taskStruct = new MethodCallWithStructArgumentTask(etr.Current); // AddTime (Meth, str)
                    var resultStruct = new ExperimentResult(etr.Current, benchmark.MeasureDurationInMs(taskStruct, repetitionsCount));
                    structuresTimes.Add(resultStruct);

                    var taskClass = new MethodCallWithClassArgumentTask(etr.Current); // AddTime(Call, cl)
                    var resultClass = new ExperimentResult(etr.Current, benchmark.MeasureDurationInMs(taskClass, repetitionsCount));
                    classesTimes.Add(resultClass);
                }
            }

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public void AddTime(Type method, Type result, int repetitionsCount)
        {
            Benchmark benchmark = new Benchmark();

            var task = new charData<>
        }
 */



