using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hal.Core.StringExtensions
{
	/// <summary>
	/// StringExtension Class - String Extension
	/// </summary>
	public static class StringExtensions
	{
		// ************************************* STRUCTURES **************************************
		#region "Structures"
		#endregion

		// *************************************** ENUMS *****************************************
		#region "Enums"
		#endregion

		// ************************************ DLL IMPORTS **************************************
		#region "DLL Imports"
		#endregion

		// ********************************** PRIVATE VARIABLES **********************************
		#region "Private Variables"
		#endregion

		// ********************************* PROTECTED VARIABLES *********************************
		#region "Protected Variables"
		#endregion

		// ************************************** EVENTS *****************************************
		#region "Events"
		#endregion

		// ************************************* DELEGATES ***************************************
		#region "Delegates"
		#endregion

		// ************************************* PROPERTIES **************************************
		#region "Properties"
		#endregion

		// ************************************ CONSTRUCTORS *************************************
		#region "Constructors"
		#endregion

		// *********************************** PUBLIC METHODS ************************************
		#region "Public Methods"
		/// <summary>
		/// IsEmpty Method - Determines whether the specified string is null or empty.
		/// </summary>
		/// <param name = "value">The string value to check.</param>
		public static bool IsEmpty(this string value)
		{
			return ((value == null) || (value.Length == 0));
		}

		/// <summary>
		/// IsNotEmpty Method - Determines whether the specified string is not null or empty.
		/// </summary>
		/// <param name = "value">The string value to check.</param>
		public static bool IsNotEmpty(this string value)
		{
			return (value.IsEmpty() == false);
		}

		/// <summary>
		/// FormatWith Method - Formats the value with the parameters using string.Format.
		/// <example>"Selected index is {0}".FormatWith(9)</example>
		/// </summary>
		/// <param name = "value">The input string.</param>
		/// <param name = "parameters">The parameters.</param>
		/// <returns></returns>
		public static string FormatWith(this string value, params object[] parameters)
		{
			return string.Format(value, parameters);
		}

		/// <summary>
		/// CenterString Method - Centers the string by padding each end wit equal charactors
		/// </summary>
		/// <param name="value">String - The string to pad</param>
		/// <param name="width">Int - The total with of characters to pad to</param>
		/// <param name="padChar">Char - The character to use for padding</param>
		/// <param name="truncate">Bool - If set to <c>true</c> [truncate].</param>
		/// <returns>String - Returns the newly padded (Centered) String</returns>
		public static string CenterString(this string value, int width, char padChar, bool truncate = false)
		{
			int diff = width - value.Length;

			if (diff == 0 || diff < 0 && !(truncate))
			{
				return value;
			}
			else if (diff < 0)
			{
				return value.Substring(0, width);
			}
			else
			{
				return value.PadLeft(width - diff / 2, padChar).PadRight(width, padChar);
			}
		}

		/// <summary>
		/// Reverse Method - Reverses the specified value.
		/// </summary>
		/// <param name="value">String - The String to reverse</param>
		/// <returns>String - Returns the newly reversed string</returns>
		public static string Reverse(this string value)
		{
			if (value.IsEmpty() || (value.Length == 1))
			{
				return value;
			}

			var chars = value.ToCharArray();
			Array.Reverse(chars);

			return new string(chars);
		}

		/// <summary>
		/// Repeat Method - Repeats the specified value.
		/// </summary>
		/// <param name="value">String - The string to repeat</param>
		/// <param name="repeatCount">Int - The number of times to repeat</param>
		/// <returns>String - Returns the string filled with the repeated value</returns>
		public static string Repeat(this string value, int repeatCount)
		{
			if (value.Length == 1)
			{
				return new string(value[0], repeatCount);
			}

			var sb = new StringBuilder(repeatCount * value.Length);

			while (repeatCount-- > 0)
			{
				sb.Append(value);
			}

			return sb.ToString();
		}

		/// <summary>
		/// IsNumeric Method - Determines whether the specified value is numeric.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///   <c>true</c> if the specified value is numeric; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsNumeric(this string value)
		{
			float output;
			return float.TryParse(value, out output);
		}

		/// <summary>
        /// IsEmptyOrWhiteSpace - Is Value Empty Or White Space
		/// </summary>
		/// <param name="value">Value</param>
		/// <returns></returns>
		public static bool IsEmptyOrWhiteSpace(this string value)
		{
			return (value.IsEmpty() || value.All(t => char.IsWhiteSpace(t)));
		}

		/// <summary>
        /// ToUpperFirstLetter - Converts first letter to upper
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToUpperFirstLetter(this string value)
		{
			if (value.IsEmptyOrWhiteSpace())
			{
				return string.Empty;
			}

			char[] valueChars = value.ToCharArray();
			valueChars[0] = char.ToUpper(valueChars[0]);

			return new string(valueChars);
		}

		/// <summary>
		/// Returns the left part of the string.
		/// </summary>
		/// <param name="value">The original string.</param>
		/// <param name="characterCount">The character count to be returned.</param>
		/// <returns>The left part</returns>
		public static string Left(this string value, int characterCount)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (characterCount >= value.Length)
			{
				return value;
			}

			return value.Substring(0, characterCount);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value">Initial Value</param>
		/// <param name="findChar">Character to Find</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static string Left(this string value, string findChar)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (value.IndexOf(findChar, System.StringComparison.Ordinal) == -1)
			{
				return value;
			}

			return value.Left(value.IndexOf(findChar, System.StringComparison.Ordinal));
		}

		/// <summary>
		/// Returns the Right part of the string.
		/// </summary>
		/// <param name="value">The original string.</param>
		/// <param name="characterCount">The character count to be returned.</param>
		/// <returns>The right part</returns>
		public static string Right(this string value, int characterCount)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (characterCount >= value.Length)
			{
				return value;
			}

			return value.Substring(value.Length - characterCount);
		}

		/// <summary>
        /// Right - String Extension Right
		/// </summary>
		/// <param name="value">Initial Value</param>
		/// <param name="findChar">Character to find</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static string Right(this string value, string findChar)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			if (value.IndexOf(findChar) == -1)
			{
				return value;
			}

			return value.Substring(value.IndexOf(findChar) + 1);
		}

		/// <summary>
        /// ToBytes - string To Byte[]
		/// </summary>
		/// <param name="data">string data</param>
		/// <returns></returns>
		public static byte[] ToBytes(this string data)
		{
			return Encoding.Default.GetBytes(data);
		}

		/// <summary>
        /// ToBytes - String To Byte[] with encoding
		/// </summary>
		/// <param name="data">string data</param>
		/// <param name="encoding">Encoding</param>
		/// <returns></returns>
		public static byte[] ToBytes(this string data, Encoding encoding)
		{
			return encoding.GetBytes(data);
		}

		/// <summary>
		/// ToTitleCase Method - Converts the String to Title Case
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string ToTitleCase(this string value)
		{
			return ToTitleCase(value, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// ToTitleCase Method - Converts the String to Title Case using the provided CultureInfo
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public static string ToTitleCase(this string value, CultureInfo culture)
		{
			return culture.TextInfo.ToTitleCase(value);
		}

		/// <summary>
		/// ParseCount Method - Determines the number of items in a string which are seperated by the specified delimeter
		/// </summary>
		/// <param name="value">String - The string to process</param>
		/// <param name="delimeter">String - The delimeter to look for</param>
		/// <param name="splitOptions">StringSplitOptions - Indicates which options to use when splitting the string</param>
		/// <returns></returns>
		public static int ParseCount(this string value, string delimeter, StringSplitOptions splitOptions = StringSplitOptions.None)
		{
			if (String.IsNullOrEmpty(value))
			{
				return 0;
			}

			string[] s = value.Split(new string[] { delimeter }, splitOptions);

			return s.GetUpperBound(0) + 1;
		}

	    /// <summary>
		/// Parse Method
		/// </summary>
		/// <param name="value">String - Line</param>
		/// <param name="length">Int - Length</param>
		/// <param name="trim">Bool - Trim</param>
		/// <returns>String - Returns item parsed from line</returns>
		public static string Parse(this string value, int length, bool trim = true)
		{
			if (String.IsNullOrEmpty(value))
			{
				return null;
			}

			string s = null;

			if (length > value.Length)
			{
				s = value;
				value = "";
			}
			else
			{
				s = value.Substring(0, length);
				value = value.Substring(length);
			}

			return (trim) ? s.Trim() : s;
		}

	    /// <summary>
        /// Parse - Parse string 
	    /// </summary>
	    /// <param name="value">Initial Value</param>
        /// <param name="delimeters">Delimeters</param>
	    /// <param name="trim">Trim Return Value</param>
	    /// <returns></returns>
	    public static string Parse(this string value, char[] delimeters, bool trim = false)
		{
			int min = value.Length;
			int index = 0;
			int cindex = 0;

			foreach (char c in delimeters)
			{
				int pos = value.IndexOf(c);

				if (min > pos)
				{
					min = pos;
					index = cindex;
				}

				cindex++;
			}

			return Parse(value, delimeters[index].ToString(CultureInfo.InvariantCulture), trim, false);
		}

		/// <summary>
		/// Parse Method
		/// </summary>
        /// <param name="value">String - value</param>
		/// <param name="delimeter">String - Delimeter</param>
		/// <returns>String - Returns the parsed item</returns>
		public static string Parse(this string value, string delimeter)
		{
			return Parse(value, delimeter, true, false);
		}


	    /// <summary>
	    /// Parse Method
	    /// </summary>
	    /// <param name="value">String - value</param>
	    /// <param name="delimeter">String - Delimeter</param>
	    /// <param name="trim">Trim Return Value</param>
	    /// <returns>String - Returns the parsed item</returns>
	    public static string Parse(this string value, string delimeter, bool trim)
		{
			return Parse(value, delimeter, trim, false);
		}

	    /// <summary>
	    /// Parse Method
	    /// </summary>
	    /// <param name="value">String - Line</param>
	    /// <param name="delimeter">String - Delimeter</param>
	    /// <param name="trim">Bool - Trim</param>
        /// <param name="linereturn">Return Line</param>
	    /// <returns>String - Returns the parsed item</returns>
	    public static string Parse(this string value, string delimeter, bool trim, bool linereturn)
		{
			if (String.IsNullOrEmpty(value))
			{
				return "";
			}

			int pos = 0;

			if (value.Contains(delimeter))
			{
				pos = value.IndexOf(delimeter, System.StringComparison.Ordinal);

				string token = value.Substring(0, pos);
				value = value.Substring(pos + 1);

				if (trim)
				{
					value = value.Trim(new char[] { ' ', '\n', '\r' });
				}

				return (trim) ? token.Trim(new char[] { ' ', '\n', '\r' }) : token;
			}
			else
			{
				string token = null;

				if (linereturn)
				{
					token = value;
					value = "";
					return token;
				}
				token = String.Empty;
				
				return token;
			}
		}

		/// <summary>
        /// Is Pure Ascii
		/// </summary>
		/// <param name="value">Initail Value</param>
		/// <returns></returns>
		public static bool IsPureAscii(this string value)
		{
			Regex re = new Regex("^[-',:a-zA-Z0-9]*$");

			if (re.IsMatch(value))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// EncodeBase64 Method - Encodes the input value to a Base64 string using the default encoding.
		/// </summary>
		/// <param name = "value">The input value.</param>
		/// <returns>The Base 64 encoded string</returns>
		public static string EncodeBase64(this string value)
		{
			return value.EncodeBase64(null);
		}

		/// <summary>
		/// EncodeBase64 Method - Encodes the input value to a Base64 string using the supplied encoding.
		/// </summary>
		/// <param name = "value">The input value.</param>
		/// <param name = "encoding">The encoding.</param>
		/// <returns>The Base 64 encoded string</returns>
		public static string EncodeBase64(this string value, Encoding encoding)
		{
			encoding = (encoding ?? Encoding.UTF8);
			var bytes = encoding.GetBytes(value);

			return Convert.ToBase64String(bytes);
		}

		/// <summary>
		/// DecodeBase64 Method - Decodes a Base 64 encoded value to a string using the default encoding.
		/// </summary>
		/// <param name = "encodedValue">The Base 64 encoded value.</param>
		/// <returns>The decoded string</returns>
		public static string DecodeBase64(this string encodedValue)
		{
			return encodedValue.DecodeBase64(null);
		}

		/// <summary>
		/// DecodeBase64 Method - Decodes a Base 64 encoded value to a string using the supplied encoding.
		/// </summary>
		/// <param name = "encodedValue">The Base 64 encoded value.</param>
		/// <param name = "encoding">The encoding.</param>
		/// <returns>The decoded string</returns>
		public static string DecodeBase64(this string encodedValue, Encoding encoding)
		{
			encoding = (encoding ?? Encoding.UTF8);
			var bytes = Convert.FromBase64String(encodedValue);

			return encoding.GetString(bytes);
		}

		/// <summary>
		/// ToEnum Method - Parse a string to a enum item if that string exists in the enum otherwise return the default enum item.
		/// </summary>
		/// <typeparam name="TEnum">The Enum type.</typeparam>
		/// <param name="dataToMatch">The data will use to convert into give enum</param>
		/// <param name="ignorecase">Whether the enum parser will ignore the given data's case or not.</param>
		/// <returns>Converted enum.</returns>
		public static TEnum ToEnum<TEnum>(this string dataToMatch, bool ignorecase = default(bool))  where TEnum : struct
		{
			return dataToMatch.IsInEnum<TEnum>()() ? default(TEnum) : (TEnum)Enum.Parse(typeof(TEnum), dataToMatch, default(bool));
		}

		/// <summary>
		/// IsInEnum Method - To check whether the data is defined in the given enum.
		/// </summary>
		/// <typeparam name="TEnum">The enum will use to check, the data defined.</typeparam>
		/// <param name="dataToCheck">To match against enum.</param>
		/// <returns>Anonoymous method for the condition.</returns>
		public static Func<bool> IsInEnum<TEnum>(this string dataToCheck) where TEnum : struct
		{
			return () => { return string.IsNullOrEmpty(dataToCheck) || !Enum.IsDefined(typeof(TEnum), dataToCheck); };
		}

		/// <summary>
		/// ToInt16 Method - To the int16.
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <returns></returns>
		public static Int16 ToInt16(this string str)
		{
			if (!str.IsNumeric())
			{
				return 0;
			}

			return Convert.ToInt16(str);
		}

		/// <summary>
		/// ToInt32 Method - To the int32.
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <returns></returns>
		public static Int32 ToInt32(this string str)
		{
			if (!str.IsNumeric())
			{
				return 0;
			}

			return Convert.ToInt32(str);
		}

		/// <summary>
		/// ToInt64 Method - To the int64.
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <returns></returns>
		public static Int64 ToInt64(this string str)
		{
			if (!str.IsNumeric())
			{
				return 0;
			}

			return Convert.ToInt64(str);
		}

		/// <summary>
		/// ToDecimal Method - To the decimal.
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <returns></returns>
		public static Decimal ToDecimal(this string str)
		{
			if (!str.IsNumeric())
			{
				return 0;
			}

			return Convert.ToDecimal(str);
		}

		/// <summary>
		/// ToDouble Method - To the double.
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <returns></returns>
		public static double ToDouble(this string str)
		{
			if (!str.IsNumeric())
			{
				return 0;
			}

			return Convert.ToDouble(str);
		}

		/// <summary>
		/// ToFloat Method - To the float.
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <returns></returns>
		public static float ToFloat(this string str)
		{
			if (!str.IsNumeric())
			{
				return 0;
			}

			return Convert.ToSingle(str);
		}

		/// <summary>
		/// ToBool Method - Convert a string value to a bool
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool ToBool(this string str)
		{
			if (String.IsNullOrEmpty(str))
			{
				str = Boolean.FalseString;
			}
			else if (str.Equals("1"))
			{
				str = Boolean.TrueString;
			}
			else if (str.Equals("0"))
			{
				str = Boolean.FalseString;
			}
			else if (String.Compare(str, "True", true) == 0)
			{
				str = Boolean.TrueString;
			}
			else if (String.Compare(str, "False", true) == 0)
			{
				str = Boolean.FalseString;
			}
			else if (String.Compare(str, "Yes", true) == 0)
			{
				str = Boolean.TrueString;
			}
			else if (String.Compare(str, "No", true) == 0)
			{
				str = Boolean.FalseString;
			}

			return Convert.ToBoolean(str);
		}

		/// <summary>
		/// EnsureStartsWith Method - Ensures that a string starts with a given prefix.
		/// </summary>
		/// <param name = "value">The string value to check.</param>
		/// <param name = "prefix">The prefix value to check for.</param>
		/// <returns>The string value including the prefix</returns>
		/// <example>
		/// 	<code>
		/// 		var extension = "txt";
		/// 		var fileName = string.Concat(file.Name, extension.EnsureStartsWith("."));
		/// 	</code>
		/// </example>
		public static string EnsureStartsWith(this string value, string prefix)
		{
			return value.StartsWith(prefix) ? value : string.Concat(prefix, value);
		}

		/// <summary>
		/// EnsureEndsWith Method - Ensures that a string ends with a given suffix.
		/// </summary>
		/// <param name = "value">The string value to check.</param>
		/// <param name = "suffix">The suffix value to check for.</param>
		/// <returns>The string value including the suffix</returns>
		/// <example>
		/// 	<code>
		/// 		var url = "http://www.microsoft.com";
		/// 		url = url.EnsureEndsWith("/"));
		/// 	</code>
		/// </example>
		public static string EnsureEndsWith(this string value, string suffix)
		{
			return value.EndsWith(suffix) ? value : string.Concat(value, suffix);
		}
		#endregion

		// *********************************** PRIVATE METHODS ***********************************
		#region "Private Methods"
		#endregion

		// ********************************** PROTECTED METHODS **********************************
		#region "Protected Methods"
		#endregion

		// ********************************** INTERNAL METHODS ***********************************
		#region "Internal Methods"
		#endregion

		// ********************************** INTERNAL CLASSES ***********************************
		#region "Internal Classes"
		#endregion

	}
}
