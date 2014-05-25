using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    class NumberHelperTestFixture
    {
        [TestCase(124)]
        [TestCase(1000)]
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
        public void TimeShouldReturnTheGivenNumberTest(int times)
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
