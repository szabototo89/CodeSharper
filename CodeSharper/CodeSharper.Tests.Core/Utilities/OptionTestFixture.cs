using System;
using System.CodeDom;
using System.Reflection;
using System.Xml.Schema;
using CodeSharper.Core.Common;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class OptionTestFixture
    {
        [SetUp]
        public void Setup()
        {
            // TODO: (optional) not implemented
        }

        [TearDown]
        public void Teardown()
        {
            // TODO: (optional) not implemented
        }

        [Test]
        public void OptionalShouldBeAbleToContainValue()
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

        [Test]
        public void OptionalShouldBeAbleToContainNoneValues()
        {
            // Given
            Option<String> underTest = Option.None;

            // When
            var result = underTest;

            // Then
            Assert.That(result.HasValue, Is.False);
            Assert.That(underTest == Option.None, Is.True);
        }

        [Test]
        public void OptionalThrowsExceptionIfItHasNoValue()
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

        [Test]
        public void OptionalShouldBeAbleToCompareTheirValues()
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

        [Test]
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

        [Test]
        public void OptionShouldBeAbleToFilterByValue()
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
            Assert.That(result.NegativeValue, Is.EqualTo((Option<Int32>)Option.None));
        }

    }
}