using System;
using System.Linq;
using CodeSharper.Core.Common.Runnables.CollectionRunnables;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables.CollectionOperations
{
    [TestFixture]
    internal class LengthRunnableTests : RunnableTestFixtureBase
    {
        #region Test initialization

        private LengthRunnable underTest;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        public override void Setup()
        {
            underTest = new LengthRunnable();
        }

        #endregion

        #region Tests

        [Test(Description = "Run should return length of text when string is passed")]
        public void Run_ShouldReturnLengthOfText_WhenStringIsPassed()
        {
            // Given in setup
            var parameter = "John Doe";

            // When
            var result = underTest.Run(parameter);

            // Then
            Assert.That(result, Is.EquivalentTo(new[] {8}));
        }

        [Test(Description = "Run should return enumerable length when not empty enumerable is passed")]
        public void Run_ShouldReturnEnumerableLength_WhenNotEmptyEnumerableIsPassed()
        {
            // Given in setup
            var parameter = new[] {1, 2, 3};

            // When
            var result = underTest.Run(parameter);

            // Then
            Assert.That(result, Is.EquivalentTo(new[] {3}));
        }

        [Test(Description = "Run should return an empty array when null is passed")]
        public void Run_ShouldReturnAnEmptyArray_WhenNullIsPassed()
        {
            // Given in setup

            // When
            var result = underTest.Run(null);

            // Then
            Assert.That(result, Is.EquivalentTo(Enumerable.Empty<Object>()));
        }

        [Test(Description = "Run should return TextRange length when not empty TextRange is passed")]
        public void Run_ShouldReturnTextRangeLength_WhenNotEmptyTextRangeIsPassed()
        {
            // Given in setup
            var textDocument = new TextDocument("hello");
            var textRange = textDocument.TextRange;

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result, Is.EquivalentTo(new[] {5}));
        }

        #endregion
    }
}