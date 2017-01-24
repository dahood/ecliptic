using EclipticLib.Extensions;
using NUnit.Framework;

namespace Ecliptic.Unit.Test.EclipticLib.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [TestCase("Given", Result = true)]
        [TestCase("When", Result = true)]
        [TestCase("Then", Result = true)]
        [TestCase("And", Result = true)]
        [TestCase("Setup", Result = true)]
        [TestCase("", Result = false)]
        [TestCase("something that looks like Give Then When", Result = false)]
        public bool ShouldIdentifyGherkinKeywordsOrEclipticKeywords(string text)
        {
            return text.IsGherkinKeywordOrEclipticKeyword();
        }

        [TestCase("Given", Result = true)]
        [TestCase("When", Result = true)]
        [TestCase("Then", Result = true)]
        [TestCase("And", Result = true)]
        [TestCase("", Result = false)]
        [TestCase("Setup", Result = false)]
        [TestCase("Scenario", Result = false)]
        [TestCase("GivenWhenThen", Result = false)]
        public bool ShouldRecognizeGherkinKeywords(string text)
        {
            return text.IsGherkinKeyword();
        }

        [TestCase("Given", Result = false)]
        [TestCase("When", Result = false)]
        [TestCase("Then", Result = false)]
        [TestCase("And", Result = false)]
        [TestCase("", Result = false)]
        [TestCase("Setup", Result = true)]
        [TestCase("Scenario", Result = true)]
        [TestCase("GivenWhenThen", Result = false)]
        public bool ShouldRecognizeEclipticKeywords(string text)
        {
            return text.IsEclipticKeyword();
        }

        [TestCase("test", 6, Result = " test ")]
        [TestCase("test", 8, Result = " test   ")]
        [TestCase("test", 5, Result = " test")]
        [TestCase("test", 9, Result = " test    ")]
        public string ShouldCenterTextUsingWidth(string text, int width)
        {
            return text.PadWithMargin(width);
        }

    }
}