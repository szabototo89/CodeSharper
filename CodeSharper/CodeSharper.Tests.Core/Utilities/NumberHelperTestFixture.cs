using System.Linq;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class NumberHelperTestFixture : TestFixtureBase
    {
        [TestCase(124)]
        [TestCase(1000)]
        [Test(Description = "Times should iterate the given number of times test")]
        public void TimesShouldIterateTheGivenNumberOfTimesTest(int times)
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
        [Test(Description = "Times should iterate the given number of times without parameter test")]
        public void TimesShouldIterateTheGivenNumberOfTimesWithoutParameterTest(int times)
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
        [Test(Description = "Times should return the given number test")]
        public void TimesShouldReturnTheGivenNumberTest(int times)
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
        [Test(Description = "To should return range of elements test")]
        public void ToShouldReturnRangeOfElementsTest(int start, int stop)
        {
            // GIVEN
            var underTest = start;

            // WHEN
            var result = underTest.To(stop);

            // THEN
            Assert.That(result, Is.EquivalentTo(Enumerable.Range(start, stop - start)));
        }
    }
}
