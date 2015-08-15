using System;
using System.Linq;
using CodeSharper.Core.Common.Runnables.CollectionRunnables;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables.CollectionOperations
{
    [TestFixture]
    public class TakeRunnableTests : RunnableTestFixtureBase
    {
        public class Initialize
        {
            public class WithDefaultConstructor : RunnableTestFixtureBase
            {
                protected TakeRunnable underTest;

                /// <summary>
                /// Setups this instance.
                /// </summary>
                [SetUp]
                public override void Setup()
                {
                    base.Setup();
                    underTest = new TakeRunnable();
                }
            }

        }
        public class RunMethod : Initialize.WithDefaultConstructor
        {
            [Test(Description = "Run should return empty array when count is zero")]
            public void ShouldReturnEmptyArray_WhenCountIsZero()
            {
                // Given
                underTest.Count = 0;

                // When
                var result = underTest.Run(new Object[] {1, 2, 3, 4});

                // Then
                Assert.That(result, Is.EquivalentTo(Enumerable.Empty<Object>()));
            }

            [Test(Description = "Run should return the first element of enumerable when count is one")]
            public void ShouldReturnFirstElementOfEnumerable_WhenCountIsOne()
            {
                // Given
                underTest.Count = 1;

                // When
                var result = underTest.Run(new Object[] {1, 2, 3, 4});

                // Then
                Assert.That(result, Is.EquivalentTo(new[] {1}));
            }

            [Test(Description = "Run should take N elements from enumerable when count is equal to N")]
            public void ShouldTakeNElementsFromEnumerable_WhenCountIsEqualToN()
            {
                // Given
                underTest.Count = 4;

                // When
                var result = underTest.Run(new Object[] {1, 2, 3, 4, 5});

                // Then
                Assert.That(result, Is.EquivalentTo(new[] {1, 2, 3, 4}));
            }
        }
    }
}