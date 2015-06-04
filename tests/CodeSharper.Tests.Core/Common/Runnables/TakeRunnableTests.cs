using System;
using System.Linq;
using CodeSharper.Core.Common.Runnables.CollectionRunnables;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables
{
    [TestFixture]
    public class TakeRunnableTests : TestFixtureBase
    {
        private TakeRunnable underTest;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            underTest = new TakeRunnable();
        }

        [Test(Description = "Run should return empty array when count is zero")]
        public void Run_ShouldReturnEmptyArray_WhenCountIsZero()
        {
            // Given
            underTest.Count = 0;

            // When
            var result = underTest.Run(new Object[] {1, 2, 3, 4});

            // Then
            Assert.That(result, Is.EquivalentTo(Enumerable.Empty<Object>()));
        }

        [Test(Description = "Run should return the first element of enumerable when count is one")]
        public void Run_ShouldReturnFirstElementOfEnumerable_WhenCountIsOne()
        {
            // Given
            underTest.Count = 1;

            // When
            var result = underTest.Run(new Object[] {1, 2, 3, 4});

            // Then
            Assert.That(result, Is.EquivalentTo(new[] {1}));
        }

        [Test(Description = "Run should take N elements from enumerable when count is equal to N")]
        public void Run_ShouldTakeNElementsFromEnumerable_WhenCountIsEqualToN()
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