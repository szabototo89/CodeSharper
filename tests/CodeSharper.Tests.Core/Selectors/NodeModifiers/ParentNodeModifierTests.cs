using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Selectors.NodeModifiers;
using CodeSharper.Tests.Core.TestHelpers.Stubs;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Selectors.NodeModifiers
{
    [TestFixture]
    internal class ParentNodeModifierTests : TestFixtureBase
    {
        [Test(Description = "ModifySelection should return parent of specified node when passed node has parent")]
        public void ModifySelection_ShouldReturnsParentOfSpecifiedNode_WhenPassedNodeHasParent()
        {
            // Given
            var underTest = new ParentNodeModifier();
            var parent = new StubNode("Hello World");
            var child = new StubNode("Hello");
            parent.AppendChild(child);

            // When
            var result = underTest.ModifySelection(child);

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { parent }));
        }

    }
}
