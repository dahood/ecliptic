using EclipticLib.Generation.Domain;
using EclipticLib.Generation.GherkinParsers;

namespace EclipticLib.Generation
{
    public interface IMd5HashProcessor
    {
        bool HasChanged(IExcelFeatureParser featureParser, string spreadsheetFile);
        void UpdateHash(string spreadsheetPath, Feature feature);
        void UpdateHashStorageFile();
    }
}