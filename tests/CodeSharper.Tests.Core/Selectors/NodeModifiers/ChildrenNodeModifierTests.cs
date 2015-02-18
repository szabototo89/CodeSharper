using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Selectors.NodeModifiers;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Tests.Core.TestHelpers.Stubs;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Selectors.NodeModifiers
{
    [TestFixture]
    internal class ChildrenNodeModifierTests : TestFixtureBase
    {
        [Test(Description = "ModifySelection should return all children of specified node when passed node has children")]
        public void ModifySelection_ShouldReturnsAllChildrenOfSpecifiedNode_WhenPassedNodeHasChildren()
        {
            // Given
            var underTest = new ChildrenNodeModifier();
            var node = new StubNode("Hello World");
            node.AppendChild(new StubNode("Hello"));
            node.AppendChild(new StubNode("World"));

            // When
            var result = underTest.ModifySelection(node);

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { new StubNode("Hello"), new StubNode("World") }));
        }
    }
}
