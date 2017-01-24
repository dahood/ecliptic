using EclipticLib.Generation.Domain;

namespace EclipticLib.Generation.GherkinParsers
{
    public interface IExcelFeatureParser
    {
        Feature Parse(string filePath);
    }

    public interface ISetupFeatureParser : IExcelFeatureParser
    {
    }
}