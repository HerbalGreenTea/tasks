using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MyPhotoshop
{
    public class SimpleParametersHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        PropertyInfo[] properties;
        TParameters parameters;

        public SimpleParametersHandler()
        {
            parameters = new TParameters();

            properties = parameters
                .GetType()
                .GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();
        }

        public TParameters CreateParameters(double[] values)
        {
            //var parameters = new TParameters();

            //var properties = parameters
            //    .GetType()
            //    .GetProperties()
            //    .Where(prop => prop.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
            //    .ToArray();

            for (int i = 0; i < values.Length; i++)
                properties[i].SetValue(parameters, values[i], new object[0]);

            return parameters;
        }

        public ParameterInfo[] GetDescription()
        {
            return typeof(TParameters)
                .GetProperties()
                .Select(prop => prop.GetCustomAttributes(typeof(ParameterInfo), false))
                .Where(prop => prop.Length > 0)
                .Select(prop => prop[0])
                .Cast<ParameterInfo>()
                .ToArray();
        }

        public System.Reflection.PropertyInfo[] GetProperties (TParameters parameters)
        {
            return parameters
                .GetType()
                .GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();
        }
    }
}
