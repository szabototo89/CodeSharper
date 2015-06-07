using System;
using CodeSharper.Core.Common.Runnables.ValueConverters;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables.ValueConverters
{
    [TestFixture]
    public class EmptyValueConverterTests : TestFixtureBase
    {
        [Test(Description = "CanConvert should return false always")]
        public void CanConvert_ShouldReturnFalseAlways()
        {
            // Given
            var value = 10;
            var underTest = new EmptyValueConverter();

            // When
            var result = underTest.CanConvert(value, typeof(Object));

            // Then
            Assert.That(result, Is.False);
        }

        [Test(Description = "Convert should throw NotSupportedException")]
        public void Convert_ShouldThrowNotSupportedException()
        {
            // Given
            var value = 10;
            var underTest = new EmptyValueConverter();

            // When
            TestDelegate conversion = () => underTest.Convert(value);

            // Then
            Assert.That(conversion, Throws.TypeOf<NotSupportedException>());
        }
    }
}