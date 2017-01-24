using EclipticLib.Generation.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Ecliptic.Unit.Test.EclipticLib.Domain
{
    [TestFixture]
    public class BasicFeatureTranslation : FeatureToSpecflowFeatureTest
    {
        [Test]
        public void TranslateBasicFeature()
        {
            translation.Should().Be(FeatureSpecflowTranslation.BasicFeature);
        }

        protected override Feature CreateFeature()
        {
            return DomainFactory.BasicFeature();
        }
    }

    [TestFixture]
    public class FeatureWithBackground : FeatureToSpecflowFeatureTest
    {
        [Test]
        public void TranslateToFeatureWithBackground()
        {
            translation.Should().Be(FeatureSpecflowTranslation.FeatureWithBackground);
        }

        protected override Feature CreateFeature()
        {
            return DomainFactory.FeatureWithBackground();
        }
    }

    [TestFixture]
    public class FeatureWithSetup : FeatureToSpecflowFeatureTest
    {
        [Test]
        public void TranslateToFeatureWithSetup()
        {
            translation.Should().Be(FeatureSpecflowTranslation.FeatureWithSetup);
        }

        protected override Feature CreateFeature()
        {
            return DomainFactory.FeatureWithSetup();
        }
    }

    public abstract class FeatureToSpecflowFeatureTest
    {
        
        [SetUp]
        public void SetUp()
        {
            translation = CreateFeature().ToString();
        }

        protected abstract Feature CreateFeature();
        protected string translation;
    }
}
