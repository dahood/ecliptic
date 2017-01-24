using System.Data;
using EclipticLib.Generation.Domain;

namespace EclipticLib.Generation.GherkinParsers
{
    public class IgnoreParser : IFeatureParser
    {
        public IgnoreParser(Feature feature, DataTable fixtureTable)
        {
        }

        public void Parse()
        {
            // do nothing
        }
    }
}