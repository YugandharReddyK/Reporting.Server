using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Extensions
{
    public static class StringExtensions
    {
        // ReSharper disable once InconsistentNaming
        private static readonly CultureInfo _culture = CultureInfo.CurrentCulture;

        private static readonly Regex WebUriExpression = new Regex(
            @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex EmailAddressExpression =
            new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$",
                      RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex StripHtmlExpression = new Regex("<\\S[^><]*>",
                                                                      RegexOptions.IgnoreCase | RegexOptions.Singleline |
                                                                      RegexOptions.Multiline |
                                                                      RegexOptions.CultureInvariant |
                                                                      RegexOptions.Compiled);

        private static readonly char[] IllegalUrlCharacters = new[] {
            ';', '/', '\\', '?', ':', '@', '&', '=', '+', '$',
            ',', '<', '>', '#', '%', '.', '!', '*', '\'', '"',
            '(', ')', '[', ']', '{', '}', '|', '^', '`', '~',
            '–'
            , '‘', '’', '“', '”', '»', '«'
        };


        public static bool IsValidDouble(this string stringValue)
        {
            double value = 0.0;
            return Double.TryParse(stringValue, out value);
        }

        public static bool IsValidInteger(this string stringValue)
        {
            int value = 0;
            return Int32.TryParse(stringValue, out value);
        }


        public static string ToLowerCurrentCulture(this string thisString)
        {
            return thisString.ToLower(_culture);
        }

        public static bool EqualsIgnoreCase(this string thisString, string otherString)
        {
            return thisString.Equals(otherString, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsEmpty(this string stringValue)
        {
            return String.IsNullOrWhiteSpace(stringValue);
        }


        public static bool IsNotEmpty(this string stringValue)
        {
            return !String.IsNullOrWhiteSpace(stringValue);
        }


        public static TEnum ToEnum<TEnum>(this string text) where TEnum : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) throw new ArgumentException("{0} is not an Enum".ToFormat(enumType.Name));
            return (TEnum)Enum.Parse(enumType, text, true);
        }

        public static string TimeStamp(this string text)
        {
            return String.Format(CultureInfo.CurrentCulture, "{0:hh:mm:ss:ff tt}  {1}", DateTime.Now, text);

        }

        public static string ToFormat(this string stringFormat, params object[] args)
        {
            return String.Format(CultureInfo.CurrentCulture, stringFormat, args);

        }

        public static string Hash(this string target)
        {
            if (String.IsNullOrWhiteSpace(target))
            {
                throw new Exception("Target is empty.");
            }

            using (var md5 = MD5.Create())
            {
                var data = Encoding.Unicode.GetBytes(target);
                var hash = md5.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        public static int GetStableHashCode(this string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

        public static string NormalizeWhitespaceToSingleSpaces(this string s)
        {
            return Regex.Replace(s, @"\s+", " ");
        }


        public static string NullSafe(this string target)
        {
            return (target ?? String.Empty).Trim();
        }

        public static string StripWhitespace(this string s)
        {
            return Regex.Replace(s, @"\s", String.Empty);
        }


        public static string StripHtml(this string target)
        {
            return StripHtmlExpression.Replace(target, String.Empty);
        }

        public static string StripFileExtension(this string target)
        {
            return (target.LastIndexOf('.') >= 0) ? target.Substring(0, target.LastIndexOf('.')) : target;
        }

        public static Guid ToGuid(this string target)
        {
            var result = Guid.Empty;

            if (!String.IsNullOrEmpty(target) && (target.Trim().Length == 22))
            {
                var encoded = String.Concat(target.Trim().Replace("-", "+").Replace("_", "/"), "==");
                var base64 = Convert.FromBase64String(encoded);

                result = new Guid(base64);
            }

            return result;
        }

        public static string ToLegalUrl(this string target)
        {
            if (String.IsNullOrEmpty(target))
            {
                return target;
            }

            target = target.Trim();

            if (target.IndexOfAny(IllegalUrlCharacters) > -1)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var character in IllegalUrlCharacters)
                {
                    target = target.Replace(character.ToString(CultureInfo.CurrentCulture), String.Empty);
                }
            }

            target = target.Replace(" ", "-");

            while (target.Contains("--"))
            {
                target = target.Replace("--", "-");
            }

            return target;
        }

        /// <summary>
        /// 	Attempts to parse a string based on the type of the default value supplied.
        /// </summary>
        /// <remarks>
        /// 	NOTE: this is an expensive method making use of exception handling to deal with enumerations.
        /// </remarks>
        /// <returns> The converted value if conversion is possible, otherwise the default value. </returns>
        public static T TryParseOrDefault<T>(this string s, T defaultValue)
            where T : IConvertible
        {
            if (s == null)
            {
                return defaultValue;
            }

            try
            {
                var isEnum = false;

                try
                {
                    isEnum = Enum.IsDefined(typeof(T), s);
                }
                catch (ArgumentException) { }

                if (isEnum)
                {
                    return (T)Enum.Parse(typeof(T), s);
                }

                return (T)Convert.ChangeType(s, typeof(T));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }



        public static bool IsNullOrWhiteSpace(this string s)
        {
            return String.IsNullOrWhiteSpace(s);
        }

        public static bool IsNotNullOrWhiteSpace(this string s)
        {
            return !String.IsNullOrWhiteSpace(s);
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return String.IsNullOrEmpty(s);
        }

        public static bool IsNotNullOrEmpty(this string s)
        {
            return !String.IsNullOrEmpty(s);
        }



        /// <summary>
        /// 	A utility method for determining the assembly qualified name for a type of a given name for which we do not know the containing assembly at run time. See <a
        ///  	href="http://msdn.microsoft.com/en-us/library/system.type.assemblyqualifiedname.aspx">MSDN</a> .
        /// </summary>
        /// <param name="typeName"> The name of the type to find the assembly qualified name for. This must include the full namespace. </param>
        /// <returns> The assembly qualified name of the type. </returns>
        public static string GetAssemblyQualifiedName(this string typeName)
        {
            if (typeName.IsNotNullOrWhiteSpace())
            {
                throw new Exception("Type name is empty.");
            }

            foreach (var currentassembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var t = currentassembly.GetType(typeName, false, true);
                if (t != null) return t.AssemblyQualifiedName;
            }

            throw new ArgumentException("Unable to find supplied type name: " + typeName);
        }

        public static string WithCapitalizedFirstLetter(this string s)
        {
            if (s.IsNotNullOrWhiteSpace())
            {
                throw new Exception("String to capitalize is empty.");
            }

            return Char.ToUpper(s[0]) + s.Substring(1);
        }

        public static byte[] ToUtf8Bytes(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }



        public static double? ToNullableDouble(this string source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return null;
            }
            double returnValue;
            if (Double.TryParse(source, out returnValue))
            {
                return returnValue;
            }
            return null;
        }

        public static bool IsNumeric(this string expression)
        {
            double retNum;
            return Double.TryParse(expression, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out retNum);
        }


        public static DateTime ConvertStringToDateTime(this string strDateTime, string strFormat)
        {
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = strFormat;
            DateTime result = DateTime.MinValue;
            DateTime.TryParse(strDateTime, dtFormat, DateTimeStyles.None, out result);
            return result;
        }

    }
}
