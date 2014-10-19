using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Atn;
using CodeSharper.Core.Common;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    class OptionalTestFixture
    {
        [Test(Description = "Optional should be initialized and store value.")]
        public void OptionalShouldBeInitializedAndStoreValueTest()
        {
            // GIVEN
            int value = 20;
            var underTest = new Optional<Int32>(value);

            // WHEN
            var result = underTest.Value;

            // THEN
            Assert.That(result, Is.EqualTo(value));
        }

        [Test(Description = "Optional should be initialized and pass new value.")]
        public void OptionalShouldBeInitalizedAndPassNewValueTest()
        {
            // GIVEN
            var value = "Hello World!";
            var underTest = new Optional<String>();

            // WHEN
            underTest.Value = value;
            var result = underTest.Value;

            // THEN
            Assert.That(result, Is.EqualTo(value));
        }

        [Test(Description = "IsInitialized should return false if Optional is not initialized.")]
        public void IsInitializedShouldReturnFalseIfOptionalIsNotInitializedTest()
        {
            // GIVEN
            var underTest = new Optional<Int32>();

            // WHEN
            var result = underTest.IsInitalized;

            // THEN
            Assert.That(result, Is.False);
        }

        [Test(Description = "IsInitialized should return true if Optional is initiailzed with value.")]
        public void IsInitializedShouldReturnTrueIfOptionalIsInitializedWithValueTest()
        {
            // GIVEN
            var underTest = new Optional<Int32>(100);

            // WHEN
            var result = underTest.IsInitalized;

            // THEN
            Assert.That(result, Is.True);
        }

        [Test(Description = "Optional should convert to Boolean implicitly.")]
        public void OptionalShouldConvertToBooleanImplicitlyTest()
        {
            // GIVEN
            var underTest = new Optional<Int32>(10);

            // WHEN
            Boolean result = underTest == underTest.IsInitalized;

            // THEN
            Assert.That(result, Is.True);
        }

        [Test(Description = "Value should get via implicitly conversion.")]
        public void ValueShouldGetViaImplicitlyConversionTest()
        {
            // GIVEN
            Int32 value = 100;
            var underTest = new Optional<Int32>(value);

            // WHEN
            Int32 result = underTest;

            // THEN
            Assert.That(result, Is.EqualTo(value));
        }

        [Test(Description = "Value should pass via implicitly conversion.")]
        public void ValueShouldPassViaImplicitlyConversionTest()
        {
            // GIVEN
            int value = 100;
            var underTest = new Optional<Int32>();

            // WHEN
            underTest = value;
            var result = underTest.Value;

            // THEN
            Assert.That(result, Is.EqualTo(value));
        }

        [Test(Description = "Every object could be converted to Optional object")]
        public void EveryObjectCouldBeConvertedToOptionalTest()
        {
            // GIVEN
            var underTest = 30.ToOptional();

            // WHEN
            var result = underTest;

            // THEN
            Assert.That(result, Is.TypeOf<Optional<Int32>>());
        }

        [Test]
        public void OptionalObjectShouldBeComparableTest()
        {
            // GIVEN
            var value = "some value";
            var optional = new Optional<String>(value);
            var underTest = new Optional<String>(value);

            // WHEN
            var result = underTest.Equals(optional);

            // THEN
            Assert.That(result, Is.True);
        }

        [Test]
        public void OptionalShouldHaveExplicitNoneValueTest()
        {
            // GIVEN
            var underTest = Optional<Int32>.None;

            // WHEN
            var result = underTest.Equals(new Optional<Int32>());

            // THEN
            Assert.That(result, Is.True);
        }

        [Test]
        public void OptinalShouldHaveExplicitSomeFactoryMethodTest()
        {
            // GIVEN
            var value = 200;
            var underTest = Optional<Int32>.Some(value);

            // WHEN
            var result = underTest.Equals(new Optional<Int32>(value));

            // THEN
            Assert.That(result, Is.True);
        }
    }
}
