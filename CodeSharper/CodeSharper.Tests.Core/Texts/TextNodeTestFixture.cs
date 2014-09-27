using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextNodeTestFixture
    {
        [Test(Description = "TextNode should have Text and Position property")]
        public void TextNodeShouldHaveTextAndPositionProperty()
        {
            // Given
            var underTest = new TextNode("Hello World!");

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
            var result = underTest.GetSubText(0, 4);

            // Then
            Assert.That(result, Is.TypeOf<TextNode>());
        }
    }
}