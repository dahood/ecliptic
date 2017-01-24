using System;
using System.Linq;
using EclipticLib.Generation.Domain;
using EclipticLib.Generation.Domain.Gherkin;

namespace EclipticLib.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Centers the text based on the desired width
        /// if the width is not an even # the excess is padded @ the end.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string PadWithMargin(this string value, int width)
        {
            var margin = value.Length + 1;
            return value.PadLeft(margin).PadRight(width, ' ');
        }

        public static string FormatWith(this string value, params object[] args)
        {
            return String.Format(value, args);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        
        public static string GetFeatureFileFor(this string fileToInclude)
        {
            return fileToInclude.EndsWith(".feature.cs")
                    ? fileToInclude.Substring(0, fileToInclude.Length - 3)
                    : fileToInclude;
        }

        public static string GetRelativepath(this string fullPath)
        {
            var fileName = fullPath.Replace(Environment.CurrentDirectory, string.Empty);
            return fileName.StartsWith(@".\") ? fileName.Substring(2, fileName.Length - 2) : fileName;
        }

        public static bool IsGherkinKeyword(this string value)
        {
            var gherkinKeywords = (GherkinKeyword[])Enum.GetValues(typeof(GherkinKeyword));
            return gherkinKeywords.Any(x => x.ToString() == value);
        }

        public static bool IsEclipticKeyword(this string value)
        {
            var eclipticKeywords = (EclipticKeyword[])Enum.GetValues(typeof(EclipticKeyword));
            return eclipticKeywords.Any(x => x.ToString() == value);
        }

        public static bool IsGherkinKeywordOrEclipticKeyword(this string value)
        {
            return value.IsGherkinKeyword() || value.IsEclipticKeyword();
        }

        public static T EnumParse<T>(this string value) 
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
 