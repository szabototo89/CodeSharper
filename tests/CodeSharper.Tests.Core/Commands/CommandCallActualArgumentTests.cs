using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Commands
{
    [TestFixture]
    internal class CommandCallActualArgumentTests : TestFixtureBase
    {
        [Test(Description = "NamedCommandCallActualArgument.Equals should return true when same references are compared to eachother")]
        public void NamedCommandCallActualArgumentEquals_ShouldReturnTrue_WhenSameReferencesAreComparedToEachOther()
        {
            // Given
            var underTest = new NamedCommandCallActualArgument("test", "value");

            // When
            var result = underTest.Equals(underTest);

            // Then
            Assert.That(result, Is.True);
        }

        [Test(Description = "NamedCommandCallActualArgument.Equals should return true when same values are compared")]
        public void NamedCommandCallActualArgumentEquals_ShouldReturnTrue_WhenSameValuesAreCompared()
        {
            // Given
            var underTest = new NamedCommandCallActualArgument("test", "value");

            // When
            var result = underTest.Equals(new NamedCommandCallActualArgument("test", "value"));

            // Then
            Assert.That(result, Is.True);
        }

        [TestCase("test", "value-1")]
        [TestCase("test-1", "value")]
        [TestCase("test-1", "value-1")]
        [TestCase("test", null)]
        [Test(Description = "NamedCommandCallActualArgument.Equals should return true when same values are compared")]
        public void NamedCommandCallActualArgumentEquals_ShouldReturnTrue_WhenDifferentValuesAreCompared(String name, String value)
        {
            // Given
            var underTest = new NamedCommandCallActualArgument("test", "value");

            // When
            var result = underTest.Equals(new NamedCommandCallActualArgument(name, value));

            // Then
            Assert.That(result, Is.False);
        }

        [Test(Description = "PositionedCommandCallActualArgument.Equals should return true when same references are compared to eachother")]
        public void PositionedCommandCallActualArgumentEquals_ShouldReturnTrue_WhenSameReferencesAreComparedToEachOther()
        {
            // Given
            var underTest = new PositionedCommandCallActualArgument(1, "value");

            // When
            var result = underTest.Equals(underTest);

            // Then
            Assert.That(result, Is.True);
        }

        [Test(Description = "PositionedCommandCallActualArgument.Equals should return true when same values are compared")]
        public void PositionedCommandCallActualArgumentEquals_ShouldReturnTrue_WhenSameValuesAreCompared()
        {
            // Given
            var underTest = new PositionedCommandCallActualArgument(0, "value");

            // When
            var result = underTest.Equals(new PositionedCommandCallActualArgument(0, "value"));

            // Then
            Assert.That(result, Is.True);
        }

        [TestCase(1, "value-1")]
        [TestCase(2, "value")]
        [TestCase(2, "value-1")]
        [TestCase(1, null)]
        [Test(Description = "PositionedCommandCallActualArgument.Equals should return true when same values are compared")]
        public void PositionedCommandCallActualArgumentEquals_ShouldReturnTrue_WhenDifferentValuesAreCompared(Int32 position, String value)
        {
            // Given
            var underTest = new PositionedCommandCallActualArgument(1, "value");

            // When
            var result = underTest.Equals(new PositionedCommandCallActualArgument(position, value));

            // Then
            Assert.That(result, Is.False);
        }
    }
}