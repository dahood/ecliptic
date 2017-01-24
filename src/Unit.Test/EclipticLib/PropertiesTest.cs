using EclipticLib.Generation;
using FluentAssertions;
using NUnit.Framework;

namespace Ecliptic.Unit.Test.EclipticLib
{
    [TestFixture]
    public class PropertiesTest
    {
        private EclipticProperties eclipticProperties;

        [SetUp]
        public void SetUp()
        {
            eclipticProperties = new EclipticProperties();
        }

        [Test]
        public void CanReadSpreadsheetFolderFromPropertiesFile()
        {
            eclipticProperties.SpreadsheetFolder.Should().Be(@".\Stories");
        }

        [Test]
        public void CanReadFeatureFileFolderFromPropertiesFile()
        {
            eclipticProperties.FeatureFileFolder.Should().Be(@".\Gherkin");
        }

        [Test]
        public void CanReadAcceptanceTestProjectrFromPropertiesFile()
        {
            eclipticProperties.AcceptanceTestProject.Should().Be(@"Unit.Test.csproj");
        }
    }
}
