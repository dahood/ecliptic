namespace EclipticLib.Generation.Domain
{
    public class TranslatedItem
    {
        public string SpreadSheetpath { get; private set; }
        public string FeatureFilePath { get; private set; }

        public TranslatedItem(string spreadSheetpath, string featureFilePath)
        {
            SpreadSheetpath = spreadSheetpath;
            FeatureFilePath = featureFilePath;
        }
    }
}