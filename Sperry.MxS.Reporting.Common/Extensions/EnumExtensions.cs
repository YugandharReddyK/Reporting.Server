using System.Reflection;
using System.ComponentModel;
using Sperry.MxS.Core.Common.Enums;
using System.Collections.Generic;
using System;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class EnumExtensions
    {
        private static readonly Dictionary<Guid, Dictionary<string, string>> _cache = new Dictionary<Guid, Dictionary<string, string>>();

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            string text = string.Empty;
            if (name != null)
            {
                text = GetExistingDescription(type.GUID, name);
                if (!string.IsNullOrEmpty(text))
                {
                    return text;
                }

                text = name;
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute descriptionAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (descriptionAttribute != null)
                    {
                        text = descriptionAttribute.Description;
                    }

                    return text;
                }

                AddEnumDescription(type.GUID, name, text);
            }

            return text;
        }

        private static string GetExistingDescription(Guid enumId, string enumName)
        {
            string value = string.Empty;
            lock (_cache)
            {
                if (_cache.TryGetValue(enumId, out var value2) && value2.TryGetValue(enumName, out value))
                {
                    return value;
                }
            }

            return value;
        }

        private static void AddEnumDescription(Guid enumId, string enumName, string enumDescription)
        {
            lock (_cache)
            {
                if (!_cache.TryGetValue(enumId, out var value))
                {
                    value = new Dictionary<string, string>();
                    _cache.Add(enumId, value);
                }

                if (!value.TryGetValue(enumName, out var _))
                {
                    value.Add(enumName, enumDescription);
                }
            }
        }

        public static bool IsIncludeDynamic(this MxSQCType qcType)
        {
            return qcType >= MxSQCType.Dynamic;
        }

        public static void Each<T>(this T value, Action<T> eachAction)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException();
            }

            foreach (T value2 in Enum.GetValues(typeof(T)))
            {
                eachAction(value2);
            }
        }

        public static TEnum ParseEnum<TEnum>(string value) where TEnum : struct
        {
            Enum.TryParse<TEnum>(value, ignoreCase: false, out var result);
            return result;
        }

        public static T ParseEnum<T>(this T enumType, string value)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException();
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }

        public static string GetName(this Enum value)
        {
            Type type = value.GetType();
            return Enum.GetName(type, value);
        }
        public static bool IsNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }

}
