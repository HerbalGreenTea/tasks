using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delegates.Reports
{
	public class CalculateMedian : ICalculate
    {
		public object MakeStatistics(IEnumerable<double> measurements)
		{
			var list = measurements.OrderBy(z => z).ToList();
			if (list.Count % 2 == 0)
				return (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2;

			return list[list.Count / 2];
		}
	}

    public class CalculateMeanAndStd : ICalculate
    {
        public object MakeStatistics(IEnumerable<double> measurements)
        {
			var data = measurements.ToList();
            var mean = data.Average();
            var std = Math.Sqrt(data.Select(z => Math.Pow(z - mean, 2)).Sum() / (data.Count - 1));

            return new MeanAndStd
            {
                Mean = mean,
                Std = std
            };
        }
    }

    public class CreateTemplateMarkdown : ITemplate
    {
		public string Name { get; private set; }

		public CreateTemplateMarkdown(string name)
        {
			Name = name;
        }

		public string Caption => Name;

		public string BeginList() => "";

		public string EndList() => "";

		public string MakeCaption(string caption) => $"## {caption}\n\n";

		public string MakeItem(string valueType, string entry) => $" * **{valueType}**: {entry}\n\n";
    }

	public class CreateTemplateHtml : ITemplate
    {
		public string Name { get; private set; }

		public CreateTemplateHtml(string name)
		{
			Name = name;
		}

		public string Caption => Name;

		public string MakeCaption(string caption) => $"<h1>{caption}</h1>";

		public string BeginList() => "<ul>";

		public string EndList() => "</ul>";

		public string MakeItem(string valueType, string entry) => $"<li><b>{valueType}</b>: {entry}";
	}

	public class Statistic<TParameters1, TParameters2>
		where TParameters1 : ITemplate
		where TParameters2 : ICalculate
    {
		public readonly ITemplate Template;
		public readonly ICalculate Calculate;

		public Statistic(TParameters1 template, TParameters2 calculate)
        {
			Template = template;
			Calculate = calculate;
        }

		public string MakeReport(IEnumerable<Measurement> measurements)
		{
			var data = measurements.ToList();
			var result = new StringBuilder();
			result.Append(Template.MakeCaption(Template.Caption));
			result.Append(Template.BeginList());
			result.Append(Template.MakeItem
				("Temperature", Calculate.MakeStatistics(data.Select(z => z.Temperature)).ToString()));
			result.Append(Template.MakeItem
				("Humidity", Calculate.MakeStatistics(data.Select(z => z.Humidity)).ToString()));
			result.Append(Template.EndList());
			return result.ToString();
		}
	}

	public interface ICalculate
    {
		object MakeStatistics(IEnumerable<double> data);
	}

	public interface ITemplate
    {
	    string Name { get; } 
		string MakeCaption(string name);
		string BeginList();
		string MakeItem(string valueType, string entry);
		string EndList();
		string Caption { get; }
	}

	public static class ReportMakerHelper
	{
		public static string MeanAndStdHtmlReport(IEnumerable<Measurement> data)
        {
			return new Statistic<CreateTemplateHtml, CalculateMeanAndStd>
				(new CreateTemplateHtml("Mean and Std"), new CalculateMeanAndStd()).MakeReport(data);
        }

		public static string MedianMarkdownReport(IEnumerable<Measurement> data)
        {
			return new Statistic<CreateTemplateMarkdown, CalculateMedian>
				(new CreateTemplateMarkdown("Median"), new CalculateMedian()).MakeReport(data);
        }

		public static string MeanAndStdMarkdownReport(IEnumerable<Measurement> measurements)
		{
			return new Statistic<CreateTemplateMarkdown, CalculateMeanAndStd>
				(new CreateTemplateMarkdown("Mean and Std"), new CalculateMeanAndStd()).MakeReport(measurements);
		}

		public static string MedianHtmlReport(IEnumerable<Measurement> measurements)
		{
			return new Statistic<CreateTemplateHtml, CalculateMedian>
				(new CreateTemplateHtml("Median"), new CalculateMedian()).MakeReport(measurements);
		}
	}
}
