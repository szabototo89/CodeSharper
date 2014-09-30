using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class ConstraintsTestFixture
    {
        [Test(Description = "Constraints should check NullReference of a parameter")]
        public void ConstraintsShouldCheckNullReferenceOfAParameter()
        {
            // Given
            Int32? iAmNotNull = 0;
            var underTest = new NotNullConstraint<Int32?>();

            // When
            TestDelegate func = () => underTest.Check(() => iAmNotNull);

            // Then
            Assert.That(func, Throws.Nothing);
        }

        [Test(Description = "Constraints should able to use via constraints static class")]
        public void ConstraintsShouldAbleToUseViaConstraintsStaticClass()
        {
            // Given
            var iAmNotNull = new Object();
            TestDelegate underTest = () => Constraints.NotNull(() => iAmNotNull);

            // Then
            Assert.That(underTest, Throws.Nothing);
        }

        [Test(Description = "Constraints should not contain blank restriction")]
        public void ConstraintsShouldContainNotBlankRestriction()
        {
            // Given
            string value = "Hello World!";
            TestDelegate underTest = () => Constraints.NotBlank(() => value);

            // Then
            Assert.That(underTest, Throws.Nothing);
        }

        [Test(Description = "Constraints should throw NullArgumentException")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstraintsShouldThrowNullArgumentException()
        {
            // Given
            string value = null;
            Constraints.NotNull(() => value);
        }

        [Test(Description = "Constraints should throw ArgumentException")]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstraintsShouldThrowArgumentException()
        {
            // Given
            string value = string.Empty;
            Constraints.NotBlank(() => value);
        }

        [Test(Description = "Constraints should be able to chainable.")]
        public void ConstraintsShouldBeAbleToChainable()
        {
            // Given
            var value = string.Empty;
            var notBlank = "Hello World!";

            // Then
            Constraints.NotNull(() => value)
                .NotNull(() => value)
                .NotBlank(() => notBlank);
        }

        [Test(Description = "Constraints should be able to group by arguments.")]
        public void ConstraintsShouldBeAbleToGroupByArguments()
        {
            // Given
            var value = "Hello World!";

            // Then
            Constraints
                .Argument(() => value)
                    .NotNull()
                    .NotBlank();
        }

        [Test(Description = "Constraints should be able to chain arguments")]
        public void ConstraintsShouldBeAbleToChainArguments()
        {
            // Given
            var value = "Hello World!";
            var notNullValue = "I am not null";

            // Then
            Constraints
                .Argument(() => value)
                    .NotNull()
                    .NotBlank()
                .Argument(() => notNullValue)
                    .NotNull();
        }

        [Test(Description = "Constraints should be able to check multiple arguments at the same time")]
        public void ConstraintsShouldBeAbleToCheckMultipleArgumentAtTheSameTime()
        {
            // Given
            var value = "Hello World!";
            var notNullValue = "I am not null";

            // Then
            Constraints
                .Argument(() => value, () => notNullValue)
                    .NotNull()
                    .NotBlank();
        }

        [Test]
        public void ConstraintsShouldCheckIfAnEnumerableIsEmpty()
        {
            // Given
            var collection = Enumerable.Range(1, 10);

            // Then
            Constraints
                .NotEmpty(() => collection);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstraintsShouldThrowArgumentExceptionWhenAnEnumerableIsEmpty()
        {
            // Given
            var collection = Enumerable.Empty<int>();

            // Then
            Constraints
                .NotEmpty(() => collection);

            Constraints
                .Argument(() => collection)
                    .NotEmpty();
        }

        [Test]
        public void ConstraintsShouldCheckGreaterThanConstraint()
        {
            // Given
            var value = 10;

            // When
            Constraints.IsGreaterThan(() => value, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstraintsShouldThrowArgumentExceptionWhenValueLesserThanExpectedValue()
        {
            // Given
            var value = 10;

            // When
            Constraints.IsGreaterThan(() => value, 100);
        }

        [Test]
        public void GreaterThanConstraintShouldUseWithStyleArgumentToo()
        {
            // Given
            var value = 10;

            // When
            Constraints
                .Argument(() => value)
                    .IsGreaterThan(0);
        }

        [Test]
        public void ConstraintsShouldCheckLesserThanConstraint()
        {
            // Given
            var value = 10;

            // When
            Constraints.IsLesserThan(() => value, 100);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstraintsShouldThrowArgumentExceptionWhenValueGreaterThanExpectedValue()
        {
            // Given
            var value = 10;

            // When
            Constraints.IsLesserThan(() => value, 0);
        }

        [Test]
        public void LesserThanConstraintShouldUseWithStyleArgumentToo()
        {
            // Given
            var value = 10;

            // When
            Constraints
                .Argument(() => value)
                    .IsLesserThan(100);
        }
    }

}
