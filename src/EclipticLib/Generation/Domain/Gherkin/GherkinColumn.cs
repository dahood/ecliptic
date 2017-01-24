using System;
using EclipticLib.Extensions;

namespace EclipticLib.Generation.Domain.Gherkin
{
    public class GherkinColumn
    {
        public string Header { get; set; }
        public int Width { get; set; }

        public GherkinColumn(string heading)
        {
            Header = heading;
            AdjustWidth(heading); //starting point for the column width (pad 1 char on both ends).
        }

        private void AdjustWidth(string value)
        {
            Width = Math.Max(Width, value.Length + 2); //keep the maximum width.
        }

        public override string ToString()
        {
            return Header.PadWithMargin(Width);
        }

        protected bool Equals(GherkinColumn other)
        {
            return string.Equals(Header, other.Header);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GherkinColumn) obj);
        }

        public override int GetHashCode()
        {
            return (Header != null ? Header.GetHashCode() : 0);
        }

        public string AddContent(string columnContents)
        {
            AdjustWidth(columnContents);
            return columnContents;
        }
    }
}