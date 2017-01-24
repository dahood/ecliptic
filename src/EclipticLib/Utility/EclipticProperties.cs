using System;
using System.Collections.Generic;
using System.IO;

namespace Ecliptic.Utility
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
                throw new ArgumentException(file + " does not exist");
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

        public string AcceptanceTestProject
        {
            get { return base["AcceptanceTestProject"]; }
        }

    }
}
