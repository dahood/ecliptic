using System;
using System.Collections.Generic;
using System.IO;
using Arguments = EclipticLib.Utility.Arguments;

namespace EclipticLib.Generation
{
    public class EclipticProperties : Arguments
    {
        public EclipticProperties() : this("Ecliptic.properties")
        {
        }

        public EclipticProperties(string file) : base(ReadAll(file))
        {
        }

        private static IEnumerable<string> ReadAll(string file)
        {
            if (!File.Exists(file))
                throw new ArgumentException(file + " does not exist.");
            return File.ReadAllLines(file);
        }

        public string SpreadsheetFolder
        {
            get { return base["SpreadsheetFolder"]; }
        }

        public string FeatureFileFolder
        {
            get { return base["FeatureFileFolder"]; }
        }

        public string AcceptanceTestTempProject
        {
            get { return AcceptanceTestProject + ".temp"; }
        }

        public string AcceptanceTestProject
        {
            get { return base["AcceptanceTestProject"]; }
        }

        public string Md5HashFile
        {
            get { return base["Md5HashFile"]; }
        }

        public string DateInputFormats
        {
            get { return base["DateInputFormats"]; }
        }

        public string DateOutputFormat
        {
            get { return base["DateOutputFormat"]; }
        }

        public string DateTimeInputFormats
        {
            get { return base["DateTimeInputFormats"]; }
        }

        public string DateTimeOutputFormat
        {
            get { return base["DateTimeOutputFormat"]; }
        }

        public string YearMonthInputFormats
        {
            get { return base["YearMonthInputFormats"]; }
        }

        public string YearMonthOutputFormat
        {
            get { return base["YearMonthOutputFormat"]; }
        }
    }
}
