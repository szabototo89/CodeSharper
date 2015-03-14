using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class OptionTestFixture : TestFixtureBase
    {
        [Test(Description = "Option.Some should contain value when it is called")]
        public void Some_ShouldContainValue_WhenItIsCalled()
        {
            // Given
            Option<Int32> underTest = Option.Some(10);

            // When
            var result = new {
                HasValue = underTest.HasValue,
                Value = underTest.Value
            };

            // Then
            Assert.That(result, Is.EqualTo(new {
                HasValue = true,
                Value = 10
            }));
        }

        [Test(Description = "Option.None should represent null value of option type when it is used")]
        public void None_ShouldRepresentNullValueOfOptionType_WhenItIsUsed()
        {
            // Given
            Option<String> underTest = Option.None;

            // When
            var result = underTest;

            // Then
            Assert.That(result.HasValue, Is.False);
            Assert.That(underTest == Option.None, Is.True);
        }

        [Test(Description = "Option should throw InvalidOperationException when it has no value")]
        public void Option_ShouldThrowInvalidOperationException_WhenItHasNoValue()
        {
            // Given
            Option<Int32> underTest = Option.None;

            // When
            TestDelegate result = () => {
                var value = underTest.Value;
            };

            // Then
            Assert.That(result, Throws.InstanceOf<InvalidOperationException>());
        }

        [Test(Description = "Option should be able to compare their values")]
        public void Option_ShouldBeAbleToCompareTheirValues()
        {
            // Given
            var underTest = Option.Some(Option.Some("Hello World!"));

            // When
            var result = new {
                IsEqual = underTest == Option.Some(Option.Some("Hello World!")),
                IsNotEqual = underTest != Option.Some(Option.Some("Hello World!"))
            };

            // Then
            Assert.That(result.IsEqual, Is.True);
            Assert.That(result.IsNotEqual, Is.False);
        }

        [Test(Description = "Map should transform specified value when it contains value")]
        public void Map_ShouldTransformSpecifiedValue_WhenItContainsValue()
        {
            // Given
            var underTest = Option.Some(15)
                                  .Map(value => value + 5)
                                  .Map(value => 2 * value);

            // When
            var result = underTest.Value;

            // Then
            Assert.That(result, Is.EqualTo(40));
        }

        [Test(Description = "Filter should return filtered values when it is called")]
        public void Filter_ShouldReturnFilteredValues_WhenItIsCalled()
        {
            // Given
            var underTest = Option.Some(15);

            // When
            var result = new {
                PositiveValue = underTest.Filter(value => value > 0),
                NegativeValue = underTest.Filter(value => value < 0)
            };

            // Then
            Assert.That(result.PositiveValue, Is.EqualTo(Option.Some(15)));
            Assert.That(result.NegativeValue == Option.None, Is.True);
        }

    }
}
