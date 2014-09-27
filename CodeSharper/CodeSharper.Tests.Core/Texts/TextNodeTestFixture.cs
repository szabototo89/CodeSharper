using CodeSharper.Core.Texts;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextNodeTestFixture
    {
        [Test(Description = "TextNode should have Text and Position property")]
        public void TextNodeShouldHaveTextAndPositionProperty()
        {
            // Given
            var underTest = new TextNode( "Hello World!");

            // When
            var result = new
            {
                underTest.Text,
                underTest.TextSpan
            };

            // Then
            var expected = new
            {
                Text = "Hello World!",
                TextSpan = new TextSpan(0, "Hello World!")
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test(Description = "This text node don't have parent because it is root node.")]
        public void ThisTextNodeDontHaveParentBecauseItIsRootNode()
        {
            // Given
            var underTest = new TextNode("Hello World!");

            // When
            var result = new
            {
                Parent = underTest.Parent, 
                IsRoot = underTest.IsRoot()
            };

            // Then
            Assert.That(result.Parent, Is.Null);
            Assert.That(result.IsRoot, Is.True);
        }

        [Test]
        public void TextNodeShouldGetSubText()
        {
            // Given
            var underTest = new TextNode("Hello World!");

            // When
            var result = underTest.GetSubText(0, 5);

            // Then
            Assert.That(result, Is.TypeOf<TextNode>());
            Assert.That(result.Parent, Is.EqualTo(underTest));
            Assert.That(underTest.Children, Contains.Item(result));
            Assert.That(result.Text, Is.EqualTo("Hello"));
            Assert.That(result.TextSpan, Is.EqualTo(new TextSpan(0, "Hello")));
        }

        [Test]
        public void TextNodeShouldBeAbleToDetachToAnotherNode()
        {
            // Given
            var parent = new TextNode("Hello World!");
            var underTest = new TextNode("Hello", parent);

            // When
            var result = underTest.Detach();

            // Then
            Assert.That(result.Parent, Is.Null);
            Assert.That(parent.Children, Is.Not.Contains(underTest));
        }

        [Test]
        public void TextNodeShouldBeAbleToAnotherToAnotherNode()
        {
            // Given
            var parent = new TextNode("Hello World!");
            var underTest = new TextNode("Hello");

            // When
            var result = underTest.AttachTo(parent);

            // Then
            Assert.That(result.Parent, Is.Not.Null);
            Assert.That(result.Parent, Is.EqualTo(parent));
            Assert.That(parent.Children, Contains.Item(underTest));
        }
    }
}