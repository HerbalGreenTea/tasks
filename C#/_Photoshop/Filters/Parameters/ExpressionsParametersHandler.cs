using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace MyPhotoshop
{
    public class ExpressionsParametersHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        static ParameterInfo[] description;
        static Func<double[], TParameters> parser;

        static ExpressionsParametersHandler()
        {
            description = typeof(TParameters)
                .GetProperties()
                .Select(prop => prop.GetCustomAttributes(typeof(ParameterInfo), false))
                .Where(prop => prop.Length > 0)
                .Select(prop => prop[0])
                .Cast<ParameterInfo>()
                .ToArray();

            var properties = typeof(TParameters)
                .GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();

            var arg = Expression.Parameter(typeof(double[]), "values");
            var bindings = new List<MemberBinding>();

            for (int i = 0; i < properties.Length; i++)
            {
                var binding = Expression.Bind(
                    properties[i],
                    Expression.ArrayIndex(arg, Expression.Constant(i)));

                bindings.Add(binding);
            }

            var body = Expression.MemberInit(
                Expression.New(typeof(TParameters).GetConstructor(new Type[0])),
                bindings);

            var lambda = Expression.Lambda<Func<double[], TParameters>>(body, arg);

            parser = lambda.Compile();
        }

        public TParameters CreateParameters(double[] values) => parser(values);

        public ParameterInfo[] GetDescription() => description;
    }
}
