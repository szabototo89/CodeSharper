using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Tests.Core.TestHelpers.Stubs;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Nodes.Modifiers
{
    [TestFixture]
    internal class ParentModifierTests : TestFixtureBase
    {
        public class ModifySelectionMethod
        {
            [Test(Description = "ModifySelection should return parent of specified node when passed node has parent")]
            public void ShouldReturnParentOfSpecifiedNode_WhenPassedNodeHasParent()
            {
                // Given
                var underTest = new ParentModifier();
                var parent = new StubNode("Hello World");
                var child = new StubNode("Hello");
                parent.AppendChild(child);

                // When
                var result = underTest.ModifySelection(child);

                // Then
                Assert.That(result, Is.EquivalentTo(new[] {parent}));
            }
        }
    }
}
