using System.Data;
using System.IO;
using EclipticLib.Excel;
using EclipticLib.Generation.Domain;

namespace EclipticLib.Generation.GherkinParsers
{
    public class ExcelFeatureParser : IExcelFeatureParser
    {
        private readonly IFeatureParserFactory parserFactory;
        private readonly IExcelReader excelReader;
        private readonly EclipticProperties eclipticProperties;

        public ExcelFeatureParser() : this(new FeatureParserFactory(), new EpplusExcelReader(), new EclipticProperties())
        {
        }

        public ExcelFeatureParser(IFeatureParserFactory parserFactory, IExcelReader excelReader, EclipticProperties eclipticProperties)
        {
            this.parserFactory = parserFactory;
            this.excelReader = excelReader;
            this.eclipticProperties = eclipticProperties;
        }

        public Feature Parse(string filePath)
        {
            var result = excelReader.ReadToDataSet(filePath);
            var feature = new Feature();

            feature.Title = Path.GetFileNameWithoutExtension(filePath); //in case there is no summary sheet

            foreach (DataTable table in result.Tables)
            {
                parserFactory.ParserFor(feature, table, eclipticProperties).Parse();
            }
            return feature;
        }

        public static IExcelFeatureParser Create()
        {
            return new ExcelFeatureParser();
        }
    }
}