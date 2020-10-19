using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Management;

namespace MyPhotoshop
{
    public class StaticParametersHandler<TParameters> /*: IParametersHandler<TParameters>*/
        where TParameters : IParameters, new()
    {
        static PropertyInfo[] properties;
        //static ParameterInfo[] description;

        static StaticParametersHandler()
        {
            properties = typeof(TParameters)
                .GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(ParameterInfo), false).Length > 0)
                .ToArray();

            //description = typeof(TParameters)
            //    .GetProperties()
            //    .Select(prop => prop.GetCustomAttributes(typeof(ParameterInfo), false))
            //    .Where(prop => prop.Length > 0)
            //    .Select(prop => prop[0])
            //    .Cast<ParameterInfo>()
            //    .ToArray();
        }

        public TParameters CreateParameters(double[] values)
        {
            var parameters = new TParameters();

            if (properties.Length != values.Length)
                throw new ArgumentException();

            for (int i = 0; i < values.Length; i++)
                properties[i].SetValue(parameters, values[i], new object[0]);

            return parameters;
        }

        //public ParameterInfo[] GetDescription()
        //{
        //    return description;
        //}
    }
}
