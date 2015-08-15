using CodeSharper.Core.Common.Runnables.CollectionRunnables;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables.CollectionOperations
{
    [TestFixture]
    internal class ElementAtRunnableTests : RunnableTestFixtureBase
    {
        internal class InitializeRunnableWithDefaultConstructor : RunnableTestFixtureBase
        {
            protected ElementAtRunnable underTest;

            /// <summary>
            /// Setups this instance.
            /// </summary>
            public override void Setup()
            {
                underTest = new ElementAtRunnable();
            }
        }

        public class RunMethod : InitializeRunnableWithDefaultConstructor
        {
            [Test(Description = "Run should return the first element of enumerable after setting position property to zero")]
            public void ShouldReturnTheFirstElementOfEnumerable_AfterSettingPositionToZero()
            {
                // Given in setup
                underTest.Position = 0;
                var parameter = new[] { "a", "b", "c" };

                // When
                var result = underTest.Run(parameter);

                // Then
                Assert.That(result, Is.EquivalentTo(new[] { "a" }));
            }

            [Test(Description = "Run should return (positive) nth element of enumerable after setting position to n")]
            public void ShouldReturnNthElementOfEnumerable_AfterSettingPositionToN()
            {
                // Given in setup
                underTest.Position = 2;
                var parameter = new[] { "a", "b", "c" };

                // When
                var result = underTest.Run(parameter);
            
                // Then
                Assert.That(result, Is.EquivalentTo(new[] { "c" }));
            }

            [Test(Description = "Run should return nth element starting from end of enumerable after setting position to negative value")]
            public void ShouldReturnNthElementStartingFromEndOfEnumerable_AfterSettingPositionToNegativeValue()
            {
                // Given
                underTest.Position = -2;
                var parameter = new[] {"a", "b", "c", "d"};

                // When
                var result = underTest.Run(parameter);

                // Then
                Assert.That(result, Is.EquivalentTo(new[] { "c" }));
            }
        }
    }
}
