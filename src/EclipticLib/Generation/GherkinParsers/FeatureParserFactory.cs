using System.Data;
using EclipticLib.Generation.Domain;

namespace EclipticLib.Generation.GherkinParsers
{
    public interface IFeatureParserFactory
    {
        IFeatureParser ParserFor(Feature feature, DataTable fixtureTable, EclipticProperties eclipticProperties);
    }

    public class FeatureParserFactory : IFeatureParserFactory
    {
        public IFeatureParser ParserFor(Feature feature, DataTable fixtureTable, EclipticProperties eclipticProperties)
        {
            if (fixtureTable.TableName == "Summary")
            {
                return new SummaryParser(feature, fixtureTable, eclipticProperties);
            }

            if (fixtureTable.TableName == "Background")
            {
                return new BackgroundParser(feature, fixtureTable, eclipticProperties);
            }
            
            if (fixtureTable.TableName.StartsWith("_"))
            {
                return new IgnoreParser(feature, fixtureTable);
            }

            return new BasicParser(feature, fixtureTable, eclipticProperties);
        }
    }
}
