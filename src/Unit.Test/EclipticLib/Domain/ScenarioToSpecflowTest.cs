using System.Text;
using EclipticLib.Generation.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Ecliptic.Unit.Test.EclipticLib.Domain
{
    [TestFixture]
    public class BasicScenarioTranslation : ScenarioToSpecflowTest
    {
        [Test]
        public void CanTranslate()
        {
            translation.Should().Be(FeatureSpecflowTranslation.BasicScenario);
        }

        protected override Scenario CreateScenario()
        {
            return DomainFactory.CreateScenario();
        }
    }

    [TestFixture]
    public class SetupScenarioTranslation : ScenarioToSpecflowTest
    {
        [Test]
        public void CanTranslate()
        {
            translation.Should().Be(FeatureSpecflowTranslation.SetupScenario);
        }

        protected override Scenario CreateScenario()
        {
            return DomainFactory.CreateSetupScenario();
        }
    }

    [TestFixture]
    public class BackgroundScenarioTranslation : ScenarioToSpecflowTest
    {
        [Test]
        public void CanTranslate()
        {
            translation.Should().Be(FeatureSpecflowTranslation.BackgroundScenario);
        }

        protected override Scenario CreateScenario()
        {
            return DomainFactory.CreateBackgroundScenario();
        }
    }

    public abstract class ScenarioToSpecflowTest
    {
        [SetUp]
        public void SetUp()
        {
            var builder = new StringBuilder();
            CreateScenario().WriteOn(builder);
            translation = builder.ToString();
        }

        protected abstract Scenario CreateScenario();

        protected string translation;
    }
}
