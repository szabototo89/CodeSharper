using System.Linq;
using System.Runtime.InteropServices;
using CodeSharper.Core.Common.Values;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class ArgumentsTestFixture : TestFixtureBase
    {
        [Test(Description = "ArgumentValue should be initialized by value")]
        public void ArgumentValueShouldBeInitializedByValue()
        {
            // Given
            var underTest = new ValueArgument<int>(4);

            // When
            var result = underTest.Value;

            // Then
            var expected = 4;
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test(Description = "ArgumentValue should be equalitiable")]
        public void ArgumentValueShouldBeEqualitiable()
        {
            // Given
            var underTest = new ValueArgument<int>(8);

            // When
            var result = underTest;

            // Then
            var expected = new ValueArgument<int>(8);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test(Description = "ArgumentValue should extend argument base class")]
        public void ArgumentValueShouldExtendArgumentBaseClass()
        {
            // Given
            var underTest = new ValueArgument<int>(8);

            // When
            // Then
            Assert.That(underTest, Is.AssignableTo<Argument>());
        }


        [Test(Description = "ArgumentValue should be initialized via static factory")]
        public void ArgumentValueShouldBeInitializedViaStaticFactory()
        {
            // Given
            var underTest = Arguments.Value(8);

            // When
            var result = underTest is ValueArgument<int>;

            // Then
            Assert.That(result, Is.True);
        }

        [Test(Description = "Arguments should have error argument")]
        public void ArgumentsShouldHaveErrorArgument()
        {
            // Given
            var message = "An error has occured!";
            var underTest = new ErrorArgument(message);

            // When
            var result = underTest.Value;

            // Then
            var expected = message;
            Assert.That(underTest, Is.AssignableTo<Argument>());
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test(Description = "ErrorValue should be initialized via static factory")]
        public void ErrorValueShouldBeInitializedViaStaticFactory()
        {
            // Given
            var underTest = Arguments.Error("An error has occured!");

            // When
            var result = underTest is ErrorArgument;

            // Then
            Assert.That(result, Is.True);
        }

        [Test(Description = "Values should be able to initialize")]
        public void ValuesShouldBeAbleToInitialize()
        {
            // Given
            var underTest = Enumerable.Range(1, 10);

            // When
            var result = Arguments.Values(underTest);

            // Then
            Assert.That(result, Is.EqualTo(Enumerable.Range(1, 10).Select(Arguments.Value)));
        }


    }
}