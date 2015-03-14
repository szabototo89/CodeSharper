using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Commands
{
    [TestFixture]
    public class CommandCallDescriptorTests : TestFixtureBase
    {
        [Test(Description = "Equals should return true when same references are compared")]
        public void Equals_ShouldReturnTrue_WhenSameReferencesAreCompared()
        {
            // Given
            var underTest = new CommandCallDescriptor("test-call", Enumerable.Empty<ICommandParameter>());

            // When
            var result = underTest.Equals(underTest);

            // Then
            Assert.That(result, Is.True);
        }

        [Test(Description = "Equals should return false when it is compared with null value")]
        public void Equals_ShouldReturnFalse_WhenItIsComparedWithNullValue()
        {
            // Given
            var underTest = new CommandCallDescriptor("test-call", Enumerable.Empty<ICommandParameter>);

            // When
            var result = underTest.Equals(null);

            // Then
            Assert.That(result, Is.False);
        }
    }
}
