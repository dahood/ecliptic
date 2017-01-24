using System;
using System.Data;
using EclipticLib.Exceptions;
using EclipticLib.Generation.Domain;

namespace EclipticLib.Generation.GherkinParsers
{
    public class SummaryParser : FeatureParser
    {
        public SummaryParser(Feature feature, DataTable fixtureTable, EclipticProperties eclipticProperties)
            : base(feature, fixtureTable, eclipticProperties)
        {
        }

        public override void Parse()
        {
            Feature.Title = SafeGetValue(0, 1, "Summary Sheet - Title must be on Row 1, Column 1");
            Feature.Description = SafeGetValue(1,1, "Summary Sheet - Feature description must be on Row 2, Column 1");
            Feature.Tags.AddRange(SafeGetValue(2, 1, "Summary Sheet - Tabs must be on Row 3, Column 1").Split(' '));
        }

        private string SafeGetValue(int row, int col, string errorMessage)
        {
            try
            {
                return (string) FixtureTable.Rows[row][col];
            }
            catch (Exception e)
            {
                throw new SummaryParseException(errorMessage);
            }
        }
    }
}