using CodeSharper.Core.Texts;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextNodeTestFixture
    {
        private TextDocument TextDocument { get; set; }

        [SetUp]
        public void Setup()
        {
            TextDocument = new TextDocument("Hello World!");
        }

        [Test(Description = "TextNode should have Text and Position property")]
        public void TextNodeShouldHaveTextAndPositionProperty()
        {
            // Given
            var underTest = new TextNode("Hello World!", TextDocument);

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

        [Test(Description = "TextNode should be able to detach to another node.")]
        public void TextNodeShouldBeAbleToDetachToAnotherNode()
        {
            // Given
            var parent = TextDocument;
            var underTest = new TextNode("Hello", parent);

            // When
            var result = underTest.Detach();

            // Then
            Assert.That(result.Parent, Is.Null);
            Assert.That(parent.Children, Is.Not.Contains(underTest));
        }

        [Test(Description = "TextNode should be able to attach to another node.")]
        public void TextNodeShouldBeAbleToAttachToAnotherNode()
        {
            // Given
            var parent = TextDocument;
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