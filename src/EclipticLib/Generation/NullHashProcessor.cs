using EclipticLib.Generation.Domain;
using EclipticLib.Generation.GherkinParsers;

namespace EclipticLib.Generation
{
    public class NullHashProcessor : IMd5HashProcessor
    {
        public bool HasChanged(IExcelFeatureParser featureParser, string spreadsheetFile)
        {
            return true;
        }

        public void UpdateHash(string spreadsheetPath, Feature feature)
        {
        }

        public void UpdateHashStorageFile()
        {
        }
    }
}