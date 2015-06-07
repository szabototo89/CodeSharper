using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    public class StringExtensionsTests : TestFixtureBase
    {
        [Test(Description = "FormatString should call String.Format method")]
        public void FormatString_ShouldCallStringFormatMethod()
        {
            // Given
            var pattern = "{0} {1}!";

            // When
            var result = pattern.FormatString("hello", "world");

            // Then
            Assert.That(result, Is.EqualTo("hello world!"));
        }
    }
}