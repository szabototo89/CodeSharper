using System;
using System.CodeDom;
using System.Reflection;
using System.Xml.Schema;
using CodeSharper.Core.Common;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class OptionTestFixture : TestFixtureBase
    {
        [Test(Description = "Option should be able to contain value")]
        public void OptionShouldBeAbleToContainValue()
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

        [Test(Description = "Option should be able to contain none values")]
        public void OptionShouldBeAbleToContainNoneValues()
        {
            // Given
            Option<String> underTest = Option.None;

            // When
            var result = underTest;

            // Then
            Assert.That(result.HasValue, Is.False);
            Assert.That(underTest == Option.None, Is.True);
        }

        [Test(Description = "Option throws exception if it has no value")]
        public void OptionThrowsExceptionIfItHasNoValue()
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
        public void OptionShouldBeAbleToCompareTheirValues()
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

        [Test(Description = "Option should be able to transfrom its value")]
        public void OptionShouldBeAbleToTransfromItsValue()
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

        [Test(Description = "Option should be filtered by value")]
        public void OptionShouldBeFilteredByValue()
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