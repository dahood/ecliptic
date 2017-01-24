using System;
using System.Data;
using System.Globalization;
using EclipticLib.Generation;

namespace EclipticLib.Extensions
{
    public static class DataTableExtensions
    {
        public static string DataRowValue(this DataTable table, int row, int col, EclipticProperties eclipticProperties = null)
        {
            var cell = table.Rows[row][col];
            
            if (eclipticProperties == null)
                return cell.ToString().Trim();

            var dateTimeString = DataRowValueForDateTimeFormats(cell.ToString(), eclipticProperties);
            return dateTimeString ?? cell.ToString().Trim();
        }

        private static string DataRowValueForDateTimeFormats(this string cellValue,
            EclipticProperties eclipticProperties)
        {
            var yearMonthValue = FormatToOverride(cellValue, eclipticProperties.YearMonthInputFormats,
                eclipticProperties.YearMonthOutputFormat);
            if (yearMonthValue.IsNotEmpty())
                return yearMonthValue;

            var dateTimeValue = FormatToOverride(cellValue, eclipticProperties.DateTimeInputFormats,
                eclipticProperties.DateTimeOutputFormat);
            if (dateTimeValue.IsNotEmpty())
                return dateTimeValue;

            var dateValue = FormatToOverride(cellValue, eclipticProperties.DateInputFormats,
                eclipticProperties.DateOutputFormat);
            if (dateValue.IsNotEmpty())
                return dateTimeValue;

            return null;
        }

        private static string FormatToOverride(string cellValue, string inputFormats, string outputFormat)
        {
            if (inputFormats.IsNotEmpty() && outputFormat.IsNotEmpty())
            {
                var formats = inputFormats.Split(',');
                DateTime parsed;
                if (DateTime.TryParseExact(cellValue, formats, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces, out parsed))
                {
                    return parsed.ToString(outputFormat);
                }
            }
            return string.Empty;
        }
    }
}