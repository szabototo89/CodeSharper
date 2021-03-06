﻿using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Tests.Core.TestHelpers.Stubs;
using CodeSharper.Tests.TestAttributes;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Nodes.Modifiers
{
    [TestFixture]
    internal class SiblingModifierTests : TestFixtureBase
    {
        public class ModifySelectionMethod
        {
            [MethodTest(nameof(ModifySelectionMethod),
                        Description = "should return siblings of specified node when passed node has parent")]
            public void ShouldReturnSiblingsOfSpecifiedNode_WhenPassedNodeHasParent()
            {
                // Given
                var underTest = new SiblingsModifier();
                var parent = new StubNode("A");
                var child = new StubNode("C");

                parent.AppendChild(new StubNode("B"));
                parent.AppendChild(child);
                parent.AppendChild(new StubNode("D"));

                // When
                var result = underTest.ModifySelection(child);

                // Then
                Assert.That(result, Is.EquivalentTo(new[] {new StubNode("B"), new StubNode("D")}));
            }
        }
    }
}