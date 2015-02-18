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
    internal class SiblingNodeModifierTests : TestFixtureBase
    {
        [Test(Description = "ModifySelection should return siblings of specified node when passed node has parent")]
        public void ModifySelection_ShouldReturnSiblingsOfSpecifiedNode_WhenPassedNodeHasParent()
        {
            // Given
            var underTest = new SiblingsNodeModifier();
            var parent = new StubNode("A");
            var child = new StubNode("C");
            
            parent.AppendChild(new StubNode("B"));
            parent.AppendChild(child);
            parent.AppendChild(new StubNode("D"));

            // When
            var result = underTest.ModifySelection(child);

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { new StubNode("B"), new StubNode("D") }));
        }
    }
}
