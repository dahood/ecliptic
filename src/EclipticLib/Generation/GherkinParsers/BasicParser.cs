using System.Data;
using EclipticLib.Generation.Domain;

namespace EclipticLib.Generation.GherkinParsers
{
    public class BasicParser : FeatureParser
    {
        public BasicParser(Feature feature, DataTable fixtureTable, EclipticProperties eclipticProperties)
            : base(feature, fixtureTable, eclipticProperties)
        {
        }

        public override void Parse()
        {
            Feature.Scenarios.Add(ReadScenarioFromWorksheet(FixtureTable));
        }
    }
}