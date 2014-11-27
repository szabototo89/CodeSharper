using System;
using System.Linq;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class NumberHelperTestFixture
    {
        [TestCase(124)]
        [TestCase(1000)]
        public void TimesShouldIterateTheGivenNumberOfTimesTest(Int32 times)
        {
            // GIVEN
            var underTest = times;

            // WHEN
            var result = 0;
            underTest.Times(value => result++);

            // THEN
            var expected = times;
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(100)]
        public void TimesShouldIterateTheGivenNumberOfTimesWithoutParameterTest(Int32 times)
        {
            // GIVEN
            var underTest = times;

            // WHEN
            var result = 0;
            underTest.Times(() => result++);

            // THEN
            var expected = times;
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(24)]
        [TestCase(100)]
        public void TimeShouldReturnTheGivenNumberTest(Int32 times)
        {
            // GIVEN
            var underTest = times;

            // WHEN
            var result = underTest.Times(i => { });

            // THEN
            var expected = times;
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(1, 10)]
        [TestCase(99, 100)]
        [TestCase(100, 100)]
        public void ToShouldReturnRangeOfElementsTest(Int32 start, Int32 stop)
        {
            // GIVEN
            var underTest = start;

            // WHEN
            var result = underTest.To(stop);

            // THEN
            Assert.That(result, Is.EquivalentTo(Enumerable.Range(start, stop - start)));
        }

        [TestCase(1, 20)]
        [TestCase(99, 100)]
        [Test(Description = "Until should return range of elements")]
        public void UntilShouldReturnRangeOfElements(Int32 start, Int32 stop)
        {
            // Given
            var underTest = start;

            // When
            var result = underTest.Until(stop);

            // Then
            Assert.That(result, Is.EquivalentTo(Enumerable.Range(start, stop - start - 1)));
        }
    }
}
