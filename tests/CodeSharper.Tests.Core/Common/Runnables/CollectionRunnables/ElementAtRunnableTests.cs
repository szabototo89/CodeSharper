﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables.CollectionRunnables;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables.CollectionRunnables
{
    [TestFixture]
    internal class ElementAtRunnableTests : TestFixtureBase
    {
        private ElementAtRunnable underTest;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        public override void Setup()
        {
            underTest = new ElementAtRunnable();
        }

        [Test(Description = "Run should return the first element of enumerable after setting position property to zero")]
        public void Run_ShouldReturnTheFirstElementOfEnumerable_AfterSettingPositionToZero()
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
        public void Run_ShouldReturnNthElementOfEnumerable_AfterSettingPositionToN()
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
        public void Run_ShouldReturnNthElementStartingFromEndOfEnumerable_AfterSettingPositionToNegativeValue()
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
