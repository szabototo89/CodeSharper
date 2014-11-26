using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class ObjectHelperTestFixture
    {
        [Test]
        public void EveryObjectShouldAbleToConvertToArray()
        {
            // Given
            var underTest = 5;

            // When
            var result = underTest.AsArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { 5 }));
        }

        [Test]
        public void AsArrayShouldAbleToHandleStrings()
        {
            // Given
            var underTest = "hello world";

            // When
            var result = underTest.AsArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { "hello world" }));
        }

        [Test]
        public void AsArrayShouldAbleToHandleEnumerable()
        {
            // Given
            var underTest = new[] { "hello world" };

            // When
            var result = underTest.AsArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { new[] { "hello world" } }));
        }

        [Test]
        public void AsListShouldAbleToConvertEveryObjectToList()
        {
            // Given
            var underTest = 5;

            // When
            var result = underTest.AsList();

            // Then
            Assert.That(result, Is.InstanceOf<List<Int32>>());
            Assert.That(result, Is.EquivalentTo(new[] { 5 }));
        }

    }
}
