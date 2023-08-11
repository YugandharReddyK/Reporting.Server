using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Reporting.ReportDoc.Interface;
using Sperry.MxS.Reporting.Infrastructure.Interface;
using Sperry.MxS.Reporting.Infrastructure.Extensions;

namespace Sperry.MxS.Reporting.ReportDoc.Extensions
{
    public static class ObjectExtensions
    { 

        #region "Public Methods"
        
        public static bool EqualsAny<T>(this T obj, params T[] values)
        {
            return (Array.IndexOf(values, obj) != -1);
        }

        public static bool EqualsNone<T>(this T obj, params T[] values)
        {
            return (obj.EqualsAny(values) == false);
        }

        public static T ConvertTo<T>(this object value)
        {
            return value.ConvertTo(default(T));
        }

        public static T ConvertToAndIgnoreException<T>(this object value, bool enableLog)
        {
            return value.ConvertToAndIgnoreException(default(T), enableLog);
        }

       
        public static T ConvertToAndIgnoreException<T>(this object value, T defaultValue, bool enableLog)
        {
            return value.ConvertTo(defaultValue, true, enableLog);
        }

        public static T ConvertTo<T>(this object value, T defaultValue)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                if (value.GetType() == targetType)
                {
                    return (T)value;
                }

                var converter = TypeDescriptor.GetConverter(value);

                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                    {
                        return (T)converter.ConvertTo(value, targetType);
                    }
                }

                converter = TypeDescriptor.GetConverter(targetType);

                if (converter != null)
                {
                    if (converter.CanConvertFrom(value.GetType()))
                    {
                        return (T)converter.ConvertFrom(value);
                    }
                }
            }

            return defaultValue;
        }

       
        public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException, bool enableLog)
        {
            if (ignoreException)
            {
                try
                {
                    return value.ConvertTo<T>();
                }
                catch (Exception exception)
                {
                    //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                    return defaultValue;
                }
            }

            return value.ConvertTo<T>();
        }

      
        public static bool CanConvertTo<T>(this object value)
        {
            if (value != null)
            {
                var targetType = typeof(T);

                var converter = TypeDescriptor.GetConverter(value);

                if (converter != null)
                {
                    if (converter.CanConvertTo(targetType))
                    {
                        return true;
                    }
                }

                converter = TypeDescriptor.GetConverter(targetType);

                if (converter != null)
                {
                    if (converter.CanConvertFrom(value.GetType()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static IMxSConverter<T> ConvertTo<T>(this T value)
        {
            return new Converter<T>(value);
        }


        public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
        {
            return InvokeMethod<object>(obj, methodName, parameters);
        }

       
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var method = type.GetMethod(methodName, parameters.Select(o => o.GetType()).ToArray());

            if (method == null)
            {
                throw new ArgumentException(string.Format("Method '{0}' not found.", methodName), methodName);
            }

            var value = method.Invoke(obj, parameters);

            return (value is T ? (T)value : default(T));
        }

       
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return GetPropertyValue<object>(obj, propertyName, null);
        }

      
        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            return GetPropertyValue(obj, propertyName, default(T));
        }

        
        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);
            }

            var value = property.GetValue(obj, null);

            return (value is T ? (T)value : defaultValue);
        }

      
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);
            }

            if (!property.CanWrite)
            {
                throw new ArgumentException(string.Format("Property '{0}' does not allow writes.", propertyName), propertyName);
            }

            property.SetValue(obj, value, null);
        }

        public static T GetAttribute<T>(this object obj) where T : Attribute
        {
            return GetAttribute<T>(obj, true);
        }

        public static T GetAttribute<T>(this object obj, bool includeInherited) where T : Attribute
        {
            var type = (obj as Type ?? obj.GetType());
            var attributes = type.GetCustomAttributes(typeof(T), includeInherited);

            return attributes.FirstOrDefault() as T;
        }
      
        public static IEnumerable<T> GetAttributes<T>(this object obj) where T : Attribute
        {
            return GetAttributes<T>(obj, false);
        }

        public static IEnumerable<T> GetAttributes<T>(this object obj, bool includeInherited) where T : Attribute
        {
            return (obj as Type ?? obj.GetType()).GetCustomAttributes(typeof(T), includeInherited).OfType<T>().Select(attribute => attribute);
        }

        
        public static bool IsOfType<T>(this object obj)
        {
            return obj.IsOfType(typeof(T));
        }

       
        public static bool IsOfType(this object obj, Type type)
        {
            return (obj.GetType().Equals(type));
        }

        public static bool IsOfTypeOrInherits<T>(this object obj)
        {
            return obj.IsOfTypeOrInherits(typeof(T));
        }

        public static bool IsOfTypeOrInherits(this object obj, Type type)
        {
            var objectType = obj.GetType();

            do
            {
                if (objectType.Equals(type))
                {
                    return true;
                }

                if ((objectType == objectType.BaseType) || (objectType.BaseType == null))
                {
                    return false;
                }

                objectType = objectType.BaseType;

            } while (true);
        }

       
        public static bool IsAssignableTo<T>(this object obj)
        {
            return obj.IsAssignableTo(typeof(T));
        }

      
        public static bool IsAssignableTo(this object obj, Type type)
        {
            var objectType = obj.GetType();
            return type.IsAssignableFrom(objectType);
        }

       
        public static T GetTypeDefaultValue<T>(this T value)
        {
            return default(T);
        }

      
        public static object ToDatabaseValue<T>(this T value)
        {
            return (value.Equals(value.GetTypeDefaultValue()) ? DBNull.Value : (object)value);
        }

       
        public static T CastTo<T>(this object value)
        {
            if (value == null || !(value is T))
                return default(T);

            return (T)value;
        }

        
        public static bool IsNull(this object target)
        {
            var ret = IsNull<object>(target);

            return ret;
        }

        
        public static bool IsNull<T>(this T target)
        {
            var result = ReferenceEquals(target, null);

            return result;
        }

        
        public static bool IsNotNull(this object target)
        {
            var ret = IsNotNull<object>(target);

            return ret;
        }

        public static bool IsNotNull<T>(this T target)
        {
            var result = !ReferenceEquals(target, null);
            return result;
        }

        
        public static string AsString(this object target)
        {
            return ReferenceEquals(target, null) ? null : string.Format("{0}", target);
        }

        
        public static string AsString(this object target, IFormatProvider formatProvider)
        {
            var result = string.Format(formatProvider, "{0}", target);

            return result;
        }

        public static string AsInvariantString(this object target)
        {
            var result = string.Format(CultureInfo.InvariantCulture, "{0}", target);

            return result;
        }

        
        public static T NotNull<T>(this T target, T notNullValue)
        {
            return ReferenceEquals(target, null) ? notNullValue : target;
        }

        public static T NotNull<T>(this T target, Func<T> notNullValueProvider)
        {
            return ReferenceEquals(target, null) ? notNullValueProvider() : target;
        }


        public static object GetValue<T>(this T o, PropertyInfo propertyInfo, bool enableLog)
        {
            object value;
            try
            {
                value = propertyInfo.GetValue(o, null);
            }
            catch
            {
                try
                {
                    value = propertyInfo.GetValue(o, new object[] { 0 });
                }
                catch (Exception exception)
                {
                    //LoggingSingleton.Instance.LogMessage(exception, enableLog);

                    value = null;
                }
            }
            return value;
        }

       
        public static object DynamicCast(this object obj, Type targetType)
        {
            // Check for the simple stuff first
            if (targetType.IsAssignableFrom(obj.GetType()))
            {
                return obj;
            }

            // The Cast operator may be explicit or implicit and may be included in either of the two types...
            const BindingFlags pubStatBinding = BindingFlags.Public | BindingFlags.Static;

            var originType = obj.GetType();
            String[] names = { "op_Implicit", "op_Explicit" };

            var castMethod = targetType.GetMethods(pubStatBinding).Union(originType.GetMethods(pubStatBinding)).FirstOrDefault(
                                    itm => itm.ReturnType.Equals(targetType) && itm.GetParameters().Length == 1 && itm.GetParameters()[0].ParameterType.IsAssignableFrom(originType) && names.Contains(itm.Name));

            if (null != castMethod)
            {
                return castMethod.Invoke(null, new[] { obj });
            }

            throw new InvalidOperationException($"No matching cast operator found from {originType.Name} to {targetType.Name}.");
        }

       
        public static T CastAs<T>(this object obj) where T : class, new()
        {
            return obj as T;
        }

       
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);

                return (T)formatter.Deserialize(stream);
            }
        }

      
        public static T Cast<T>(this object o)
        {
            if (o == null)
            {
                throw new NullReferenceException();
            }

            return (T)Convert.ChangeType(o, typeof(T));
        }

     
        public static T Cast<T>(this object o, T defaultValue)
        {
            if (o == null)
            {
                return defaultValue;
            }

            return (T)Convert.ChangeType(o, typeof(T));
        }

        public static void CopyPropertiesFrom(this object target, object source)
        {
            CopyPropertiesFrom(target, source, string.Empty);
        }

       
        public static void CopyPropertiesFrom(this object target, object source, string ignoreProperty)
        {
            CopyPropertiesFrom(target, source, new[] { ignoreProperty });
        }

        
        public static void CopyPropertiesFrom(this object target, object source, string[] ignoreProperties)
        {
            // Get and check the object types
            Type type = source.GetType();

            if (target.GetType() != type)
            {
                throw new ArgumentException("The source type must be the same as the target");
            }

            // Build a clean list of property names to ignore
            var ignoreList = new List<string>();

            foreach (string item in ignoreProperties)
            {
                if (!string.IsNullOrEmpty(item) && !ignoreList.Contains(item))
                {
                    ignoreList.Add(item);
                }
            }

            // Copy the properties
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.CanWrite && property.CanRead && !ignoreList.Contains(property.Name))
                {
                    object val = property.GetValue(source, null);
                    property.SetValue(target, val, null);
                }
            }
        }

       
        public static string ToPropertiesString(this object source)
        {
            return ToPropertiesString(source, Environment.NewLine);
        }

        public static string ToPropertiesString(this object source, string delimiter)
        {
            if (source == null)
            {
                return string.Empty;
            }

            Type type = source.GetType();

            var sb = new StringBuilder(type.Name);
            sb.Append(delimiter);

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.CanWrite && property.CanRead)
                {
                    object val = property.GetValue(source, null);
                    sb.Append(property.Name);
                    sb.Append(": ");
                    sb.Append(val == null ? "[NULL]" : val.ToString());
                    sb.Append(delimiter);
                }
            }

            return sb.ToString();
        }
        #endregion
    }
}
