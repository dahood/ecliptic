using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EclipticLib.Utility.Extensions
{
    public static class StringExtensions
    {
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static byte[] ToByteArray(this string value)
        {
            var bytes = new byte[value.Length*sizeof (char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string JoinWith(this string value, string separator, params string[] values)
        {
            return values.Where(IsNotEmpty).Aggregate(value, (current, newValue) => current + separator + newValue);
        }

        public static string RemoveWhitespace(this string value)
        {
            return (value ?? "").Replace(" ", string.Empty);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !value.IsEmpty();
        }

        public static string AsNullIfWhiteSpace(this string value)
        {
            return value.IsNullOrWhiteSpace() ? null : value;
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format.Replace("\n", Environment.NewLine).Replace("{nl}", Environment.NewLine), args);
        }

        public static string RemoveAllNonAlphanumericCharacters(this string str)
        {
            return Regex.Replace(str, @"[\W]", "");
        }

        public static string TrimTo(this string str, char c)
        {
            var indexOf = str.IndexOf(c);
            return indexOf == -1 ? string.Empty : str.Substring(indexOf + 1);
        }

        public static int AsInt(this string value)
        {
            return !value.IsInteger() ? 0 : int.Parse(value.Trim());
        }

        public static bool IsInteger(this string value)
        {
            return Regex.IsMatch(value, @"^-{0,1}[0-9]{0,1}[0-9]+$"); //needs at least 1 digit
        }

        public static bool IsNumber(this string value)
        {
            return Regex.IsMatch(value, @"^-{0,1}([0-9]+)([.][0-9]+)?$"); //needs at least 1 digit
        }

        public static decimal AsDecimal(this string value)
        {
            return !IsNumber(value) ? 0m : decimal.Parse(value);
        }

        public static decimal AsPositiveOrNegativeDecimal(this string value)
        {
            return
                decimal.Parse(value,
                    NumberStyles.Number | NumberStyles.AllowParentheses | NumberStyles.AllowThousands |
                    NumberStyles.AllowDecimalPoint);
        }

        public static long AsLong(this string value)
        {
            return !value.IsInteger() ? 0 : long.Parse(value.Trim());
        }

        public static List<long> AsLong(this IEnumerable<string> value)
        {
            return value.Where(IsInteger).Select(long.Parse).ToList();
        }

        public static decimal? AsNullableDecimal(this string value)
        {
            if (value.IsNullOrWhiteSpace()) return null;
            return AsDecimal(value);
        }

        public static long? AsNullableLong(this string value)
        {
            if (value.IsNullOrWhiteSpace()) return null;
            return AsLong(value);
        }

        public static bool IsWrappedWith(this string value, string wrapper)
        {
            return value.StartsWith(wrapper) && value.EndsWith(wrapper);
        }

        public static string RemoveWrappingCharacters(this string value, string wrapper)
        {
            return value.IsWrappedWith(wrapper)
                ? value.Substring(wrapper.Length, value.Length - 2*wrapper.Length)
                : value;
        }

        public static bool YesNoToBoolean(this string value)
        {
            if (string.Equals(value, "y", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase))
                return true;
            if (string.Equals(value, "n", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "no", StringComparison.OrdinalIgnoreCase))
                return false;
            throw new Exception($"Cannot convert string [{value}] to true/false.");
        }

        public static bool YesNoOrTrueFalseToBoolean(this string value, bool? defaultValue = null)
        {
            if (string.Equals(value, "y", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase))
                return true;
            if (string.Equals(value, "n", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "no", StringComparison.OrdinalIgnoreCase))
                return false;
            bool outBool;
            if (bool.TryParse(value, out outBool))
                return outBool;
            if (defaultValue == null)
                throw new Exception($"Cannot convert string [{value}] to true/false.");
            return defaultValue.Value;
        }

        public static bool? TrueFalseToNullableBoolean(this string value)
        {
            return value.IsNullOrWhiteSpace() ? default(bool?) : TrueFalseToBoolean(value);
        }

        public static bool TrueFalseToBoolean(this string value, bool defaultValue = false)
        {
            bool outBool;
            return bool.TryParse(value, out outBool) ? outBool : defaultValue;
        }

        public static string TrimEndComma(this string value)
        {
            return value.TrimEnd(',', ' ');
        }

        public static DateTime ToDateTime(this string input, bool throwExceptionIfFailed = false)
        {
            DateTime result;
            var valid = DateTime.TryParse(input, out result);
            if (valid) return result;

            if (throwExceptionIfFailed)
                throw new FormatException($"'{input}' cannot be converted as DateTime");
            return result;
        }

        public static string FirstLetterToUpper(this string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string BuildEscapedCsvLine(this string[] fields)
        {
            for (var i = 0; i < fields.Length; i++)
                if ((fields[i] != null) && fields[i].Contains(","))
                    fields[i] = '"' + fields[i] + '"';
            return string.Join(",", fields);
        }
    }
}
