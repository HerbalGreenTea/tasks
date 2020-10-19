using System;
using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var create = new CreateListTime();

            for (int i = 0; i < Constants.FieldCounts.Count; i++)
            {
                if (create.Etr.MoveNext())
                {
                    var taskStruct = new StructArrayCreationTask(create.Etr.Current);
                    AddStructuresTime(taskStruct, repetitionsCount, create);
                  
                    var taskClass = new ClassArrayCreationTask(create.Etr.Current);
                    AddClasssesTime(taskClass, repetitionsCount, create);
                }
            }

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = create.ClassesTimes,
                StructPoints = create.StructuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var create = new CreateListTime();

            for (int i = 0; i < Constants.FieldCounts.Count; i++)
            {
                if (create.Etr.MoveNext())
                {
                    var taskStruct = new MethodCallWithStructArgumentTask(create.Etr.Current);
                    AddStructuresTime(taskStruct, repetitionsCount, create);

                    var taskClass = new MethodCallWithClassArgumentTask(create.Etr.Current);
                    AddClasssesTime(taskClass, repetitionsCount, create);
                }
            }

            return new ChartData
            {
                Title = "Call method with argument", 
                ClassPoints = create.ClassesTimes, 
                StructPoints = create.StructuresTimes,
            };
        }

        public static void AddStructuresTime(ITask task, int repetitCount, CreateListTime structur)
        {
            Benchmark bench = new Benchmark();
            var result = new ExperimentResult(structur.Etr.Current, bench.MeasureDurationInMs(task, repetitCount));
            structur.StructuresTimes.Add(result);
        }

        public static void AddClasssesTime(ITask task, int repetitCount, CreateListTime classes)
        {
            Benchmark bench = new Benchmark();
            var result = new ExperimentResult(classes.Etr.Current, bench.MeasureDurationInMs(task, repetitCount));
            classes.ClassesTimes.Add(result);
        }
    }

    public class CreateListTime
    {
        public List<ExperimentResult> ClassesTimes = new List<ExperimentResult>();
        public List<ExperimentResult> StructuresTimes = new List<ExperimentResult>();
        public IEnumerator<int> Etr = Constants.FieldCounts.GetEnumerator();
    }
}