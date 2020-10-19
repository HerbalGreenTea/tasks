using Ddd.Taxi.Domain;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ddd.Infrastructure
{
	public class ValueType<T>
	{
        public override bool Equals(object obj)
        {
            return FeatureСomparison(obj, this);
        }

        public bool Equals(PersonName name)
        {
            return FeatureСomparison(name, this);
        }

        private bool FeatureСomparison(object properties1, object properties2)
        {
            if (properties1 == null || properties1.GetType() != properties2.GetType())
                return false;

            var arrProperties1 = properties1
                .GetType()
                .GetProperties()
                .ToArray();

            var arrProperties2 = properties2
                .GetType()
                .GetProperties()
                .ToArray();

            return arrProperties1.Length == arrProperties2.Length &&
                CheckPrpopertires(arrProperties1, arrProperties2, properties1, properties2);
        }

        private bool CheckPrpopertires (PropertyInfo[] arrProperties1, PropertyInfo[] arrProperties2, 
            object properties1, object properties2)
        {
            for (int i = 0; i < arrProperties1.Length; i++)
            {
                var valueProperty1 = arrProperties1[i].GetValue(properties1);
                var valueProperty2 = arrProperties2[i].GetValue(properties2);

                if (valueProperty1 == null && valueProperty2 == null)
                    continue;

                if (valueProperty1 == null && valueProperty2 != null || 
                    valueProperty1 != null && valueProperty2 == null)
                    return false;

                var check = true;
                if (arrProperties1[i].PropertyType.IsSubclassOf(typeof(ValueType<PersonName>)))
                    check = FeatureСomparison(valueProperty1, valueProperty2);

                if (valueProperty1.ToString() != valueProperty2.ToString() || !check)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        { 
            var str = this.ToString();
            var hash = 0;
            int primeNum = 3123423;
            int previosX = 216613678;

            if (str.Length > 0 && hash == 0)
            {
                for (int i = 0; i < str.Length - 1; i++)
                    previosX = unchecked((previosX * primeNum + str[i].GetHashCode()) % (int)Math.Pow(2, 32));
                hash = unchecked((previosX * primeNum + str[str.Length - 1].GetHashCode()) % (int)Math.Pow(2, 32));
            }
            return hash;
        }

        public override string ToString()
        {
            var properties = this
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(property => property.Name)
                .ToArray();

            var builder = new StringBuilder();

            builder.Append(this.GetType().Name + "(");

            for (int i = 0; i < properties.Length; i++)
            {
                builder = AddNameAndValue(this, properties[i], builder);

                if (i != properties.Length - 1)
                    builder.Append("; ");
            }

            builder.Append(")");

            return builder.ToString();
        }

        private StringBuilder AddNameAndValue(object properties, PropertyInfo property, StringBuilder builder)
        {
            builder.Append(property.Name);
            builder.Append(": ");

            if (property.GetValue(properties) != null)
                builder.Append(property.GetValue(properties).ToString());
            else
                builder.Append("");

            return builder;
        }
    }
}