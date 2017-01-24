using System.Data;
using EclipticLib.Generation.Domain;

namespace EclipticLib.Generation.GherkinParsers
{
    public class BackgroundParser : FeatureParser
    {
        public BackgroundParser(Feature feature, DataTable fixtureTable, EclipticProperties eclipticProperties)
            : base(feature, fixtureTable, eclipticProperties)
        {
        }

        public override void Parse()
        {
            var backgroundForStory = ReadScenarioFromWorksheet(FixtureTable);
            backgroundForStory.IsBackground = true;
            Feature.Background = backgroundForStory;        
        }
    }
}