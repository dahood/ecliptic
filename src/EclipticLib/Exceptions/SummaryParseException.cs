using System;

namespace EclipticLib.Exceptions
{
    public class SummaryParseException : Exception
    {
        public SummaryParseException(string errorMessage) : base(errorMessage)
        {
        }
    }
}