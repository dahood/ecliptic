using System.Data;
using EclipticLib.Excel;
using EclipticLib.Generation;
using EclipticLib.Generation.Domain;
using EclipticLib.Generation.GherkinParsers;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Ecliptic.Unit.Test.EclipticLib
{
    public class ExcelParserTest
    {
        [TestFixture]
        public class ParsingTheAnnularVelocityFeature
        {
            ExcelFeatureParser parser;
            IFeatureParser basicScenarioParser;

            IFeatureParserFactory parserFactory;
            Feature featureset;
            private IExcelReader excelReader;
            private DataSet dataset;

            [Test]
            public void CreatedTheFeatureScenarios()
            {
                featureset.Title.Should().Be("AnnularVelocity");
                ReceivedCallFor("Summary");
                ReceivedCallFor("Background");
                ReceivedCallFor("Scenario 1");
                ReceivedCallFor("Scenario 2");
            }

            [SetUp]
            public void SetUp()
            {
                basicScenarioParser = Substitute.For<IFeatureParser>();
                parserFactory = Substitute.For<IFeatureParserFactory>();
                excelReader = Substitute.For<IExcelReader>();
                
                dataset = CreateTestDataSet();
                excelReader.ReadToDataSet(TestFiles.AnnularVelocity).Returns(dataset);

                ExpectCallForParser("Summary");
                ExpectCallForParser("Background");
                ExpectCallForParser("Scenario 1");
                ExpectCallForParser("Scenario 2");

                parser = new ExcelFeatureParser(parserFactory, excelReader, new EclipticProperties());
                featureset = parser.Parse(TestFiles.AnnularVelocity);
            }

            private static DataSet CreateTestDataSet()
            {
                var ds = new DataSet();
                ds.Tables.Add("Summary");
                ds.Tables.Add("Background");
                ds.Tables.Add("Scenario 1");
                ds.Tables.Add("Scenario 2");
                return ds;
            }

            private void ExpectCallForParser(string tableName)
            {
                parserFactory.ParserFor(Arg.Any<Feature>(), Arg.Is<DataTable>(x => x.TableName == tableName), Arg.Any<EclipticProperties>()).Returns(basicScenarioParser);
                basicScenarioParser.Parse();
            }

            private void ReceivedCallFor(string tableName)
            {
                parserFactory.Received().ParserFor(Arg.Any<Feature>(), Arg.Is<DataTable>(x => x.TableName == tableName), Arg.Any<EclipticProperties>());
                basicScenarioParser.Received().Parse();
            }
        }
    }
}
