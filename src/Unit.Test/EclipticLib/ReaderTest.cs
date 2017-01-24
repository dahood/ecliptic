using System.IO;
using EclipticLib.Generation.GherkinParsers;
using FluentAssertions;
using NUnit.Framework;

namespace Ecliptic.Unit.Test.EclipticLib
{
    [TestFixture]
    public class ReaderTest
    {
        private  string gherkinDir = "../../Gherkin/";
        private const string setupDir = "../../Gherkin/Setup/";

        [SetUp]
        public void SetUp()
        {
            Directory.CreateDirectory(gherkinDir);
            Directory.CreateDirectory(setupDir);
        }

        [Test]
        public void ShouldReadGivenStatement()
        {
            var reader = new ExcelFeatureParser();
            var featureset = reader.Parse(TestFiles.AnnularVelocity);
            featureset.SaveToFile(gherkinDir);
        }

        [Test]
        public void ReadEnterpriseTest()
        {
            var reader = new ExcelFeatureParser();
            var featureset = reader.Parse(@".\Stories\RetrieveEnterpriseEffectiveInFuture.xlsx");
            featureset.Should().NotBeNull();
            featureset.SaveToFile(gherkinDir);
        }

        [Test]
        public void ShouldReadSetupStatement()
        {
            var reader = new ExcelFeatureParser();
            var featureset = reader.Parse(TestFiles.CustomerSetup);
            featureset.SaveToFile(setupDir);
        }
    }
}
