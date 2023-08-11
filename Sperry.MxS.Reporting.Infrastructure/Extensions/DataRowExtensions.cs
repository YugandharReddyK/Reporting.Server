using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Infrastructure.Extensions
{
    public static class DataRowExtensions
    {
        public static T Get<T>(this DataRow row, string field, bool enableLog)
        {

            return row.Get(field, default(T), enableLog);

        }

        public static T Get<T>(this DataRow row, string field, T defaultValue, bool enableLog)
        {
            try
            {
                object value = row[field];
                if (value == DBNull.Value)
                {
                    return defaultValue;
                }

                Type nullableType = typeof(T);
                if (nullableType.IsGenericType && nullableType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    if (value == null)
                    {
                        return default(T);
                    }

                    nullableType = Nullable.GetUnderlyingType(nullableType);
                }

                //Decimal numbers with exponent, like 1E-05, fail to pass Convert.ChangeType. Changing it to a decimal before hand
                if (nullableType == typeof(System.Decimal))
                {
                    value = Decimal.Parse(value.ToString(), NumberStyles.Float);
                }

                return (T)Convert.ChangeType(value, nullableType);
            }
            catch (FormatException exp)
            {
                // Commented by Suhail
                //LoggingSingleton.Instance.LogMessage(exp, enableLog);
                return defaultValue;
            }

        }
    }
}
