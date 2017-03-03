using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EclipticLib.Extensions;
using EclipticLib.Generation.Domain;
using EclipticLib.Generation.GherkinParsers;
using EclipticLib.Utility.Logging;

namespace EclipticLib.Generation
{
    public sealed class EclipticControl
    {
        private readonly EclipticProperties eclipticProperties;
        private readonly IExcelFeatureParser featureParser;
        private readonly EclipticPostProcessor postProcessor;
        private readonly IMd5HashProcessor md5HashProcessor;

        public EclipticControl(EclipticProperties properties) : this(properties, ExcelFeatureParser.Create(), new EclipticPostProcessor(properties), Md5HashProcessor.Create(properties))
        {
        }

        private EclipticControl(EclipticProperties eclipticProperties, IExcelFeatureParser featureParser, EclipticPostProcessor postProcessor, IMd5HashProcessor md5HashProcessor)
        {
            this.eclipticProperties = eclipticProperties;
            this.featureParser = featureParser;
            this.postProcessor = postProcessor;
            this.md5HashProcessor = md5HashProcessor;
        }

        public void Run()
        {
            var featureFilesGenerated = new List<TranslatedItem>();
            LocateAllSpreadsheets(eclipticProperties.SpreadsheetFolder).ForEach(each =>
            {
                var directory = Path.GetDirectoryName(each);
                Log.Debug(x => x($"working in directory: {directory}"));

                var feature = featureParser.Parse(each);
                md5HashProcessor.UpdateHash(each, feature);
                var saveToDirectory = directory.Replace(eclipticProperties.SpreadsheetFolder, eclipticProperties.FeatureFileFolder);

                Log.Debug(x => x($"saving to directory: {saveToDirectory}"));
                featureFilesGenerated.Add(new TranslatedItem(each, SaveTo(feature, saveToDirectory)));
            });

            md5HashProcessor.UpdateHashStorageFile();
            postProcessor.Process(featureFilesGenerated);
        }

        private IEnumerable<string> LocateAllSpreadsheets(string spreadsheetFolder)
        {
            if (!Directory.Exists(spreadsheetFolder))
                throw new ArgumentException("the spreadsheet folder " + spreadsheetFolder + " does not exist");

            IEnumerable<string> allSpreadsheets = Directory.EnumerateFiles(spreadsheetFolder, SearchPattern,
                SearchOption.AllDirectories);

            IEnumerable<string> newOrChangedSpreadsheets = allSpreadsheets.Where(s => md5HashProcessor.HasChanged(featureParser, s));
            return newOrChangedSpreadsheets;
        }

        private static string SearchPattern { get { return "*.xlsx"; } }


        private string SaveTo(Feature feature, string directoryToSaveTo)
        {
            return feature.SaveToFile(directoryToSaveTo);
        }
    }
}
