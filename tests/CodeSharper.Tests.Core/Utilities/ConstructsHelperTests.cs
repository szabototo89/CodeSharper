using System;
using System.Linq;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    public class ConstructsHelperTests : TestFixtureBase
    {
        [Test(Description = "Array should create a new array with strings when passing strings")]
        public void Array_ShouldCreateANewArrayWithString_WhenPassingStrings()
        {
            // Act

            // Arrange
            var result = ConstructsHelper.Array("hello", "world");

            // Assert
            Assert.That(result.SequenceEqual(new[] {"hello", "world"}), Is.True);
        }

        [Test(Description = "Array should return an empty array when calling without arguments")]
        public void Array_ShouldReturnAnEmptyArray_WhenCallingWithoutArguments()
        {
            // Act

            // Arrange
            var result = ConstructsHelper.Array<Int32>();

            // Assert
            Assert.That(result, Is.SameAs(Enumerable.Empty<Int32>()));
        }

        [Test(Description = "Array should create a new array when passing elements")]
        public void Array_ShouldReturnNewArray_WhenPassingElements()
        {
            // Act

            // Arrange
            var result = ConstructsHelper.Array(1, 2, 3);

            // Assert
            Assert.That(result.SequenceEqual(new[] {1, 2, 3}), Is.True);
        }
    }
}