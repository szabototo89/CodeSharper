using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Tests.Core.TestHelpers.Stubs;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Nodes.Modifiers
{
    [TestFixture]
    internal class ChildrenModifierTests : TestFixtureBase
    {
        public class ModifySelectionMethod
        {
            [Test(Description = "ModifySelection should return all children of specified node when passed node has children")]
            public void ShouldReturnAllChildrenOfSpecifiedNode_WhenPassedNodeHasChildren()
            {
                // Given
                var underTest = new ChildrenModifier();
                var node = new StubNode("Hello World");
                node.AppendChild(new StubNode("Hello"));
                node.AppendChild(new StubNode("World"));

                // When
                var result = underTest.ModifySelection(node);

                // Then
                Assert.That(result, Is.EquivalentTo(new[] {new StubNode("Hello"), new StubNode("World")}));
            }
        }
    }
}
