using System;
using System.Collections;
using System.Collections.Generic;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    public class EnumerableExtensionsTests : TestFixtureBase
    {
        [Test(Description = "WhereNotNull should return null when null is passed")]
        public void WhereNotNull_ShouldReturnNull_WhenNullIsPassed()
        {
            // Given
            IEnumerable<Object> underTest = null;

            // When
            var result = underTest.WhereNotNull();

            // Then
            Assert.That(result, Is.Null);
        }

        [Test(Description = "WhereNotNull should filter not null elements when enumerable is passed")]
        public void WhereNotNull_ShouldFilterNotNullElements_WhenEnumerableIsPassed()
        {
            // Given
            var underTest = new[] {"1", "2", "3", null};

            // When
            var result = underTest.WhereNotNull();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] {"1", "2", "3"}));
        }
    }
}