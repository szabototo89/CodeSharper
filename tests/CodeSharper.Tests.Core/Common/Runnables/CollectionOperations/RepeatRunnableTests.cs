using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables.CollectionOperations;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables.CollectionOperations
{
    [TestFixture]
    public class RepeatRunnableTests : TestFixtureBase
    {
        public class RunMethod
        {
            [Test(Description = "Run should return an empty array when count is zero")]
            public void ShouldReturnEmptyArray_WhenCountIsZero()
            {
                // Given
                var underTest = new RepeatRunnable() {Count = 0};

                // When
                var result = underTest.Run("hello");

                // Then
                Assert.That(result, Is.Empty);
            }

            [Test(Description = "Run should return an array within one element of passed argument when count is zero")]
            public void ShouldReturnArrayWithinOneElementOfPassedArgument_WhenCountIsOne()
            {
                // Given
                var underTest = new RepeatRunnable() {Count = 1};

                // When
                var result = underTest.Run("hello");

                // Then
                Assert.That(result, Is.EqualTo(new[] {"hello"}));
            }

            [Test(Description = "Run should return an array within several duplicated elementes when count is greater than one.")]
            public void ShouldArrayWithinSeveralDuplicatedElements_WhenCountIsGreaterThanOne()
            {
                // Given
                var underTest = new RepeatRunnable()
                {
                    Count = 3
                };

                // When
                var result = underTest.Run("hello");

                // Then
                Assert.That(result, Is.EqualTo(new[] {"hello", "hello", "hello"}));
            }
        }
    }
}