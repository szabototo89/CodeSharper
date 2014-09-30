using System;
using System.Linq;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextDocumentTestFixture
    {
        private TextDocument UnderTest { get; set; }

        [SetUp]
        public void Setup()
        {
            UnderTest = new TextDocument("Hello World!");
        }

        [Test]
        public void TextDocumentShouldBeRepresentedRawText()
        {
            // Given in setup

            // When
            var result = UnderTest.Text;

            // Then
            Assert.That(result, Is.EqualTo("Hello World!"));
        }

        [Test]
        public void SubStringOfTextShouldCreateTextNodeFromText()
        {
            // Given in setup

            // When
            var result = UnderTest.SubStringOfText(0, 5);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo("Hello"));
            Assert.That(result.Start, Is.EqualTo(0));
            Assert.That(result.Stop, Is.EqualTo(5));
        }

        [Test]
        public void TextNodeOfTextDocumentShouldBeAbleToChangeItsText()
        {
            // Given in setup
            var node = UnderTest.SubStringOfText(0, 5);

            // When
            node.SetText("hello");
            var result = UnderTest.Text;

            // Then
            Assert.That(result, Is.EqualTo("hello World!"));
        }

        [Test]
        public void TextNodeOfTextDocumentShouldBeAbleToRemoveItsText()
        {
            // Given in setup
            var node = UnderTest.SubStringOfText(0, 5);

            // When
            node.SetText("");
            var result = UnderTest.Text;

            // Then
            Assert.That(result, Is.EqualTo(" World!"));
        }

        [Test]
        public void TextNodeOfTextDocumentShouldBeAbleToAddMoreTextToItsText()
        {
            // Given in setup
            var node = UnderTest.SubStringOfText(0, 5);

            // When
            node.SetText("HelloHello");
            var result = UnderTest.Text;

            // Then
            Assert.That(result, Is.EqualTo("HelloHello World!"));
        }

        [Test]
        public void TextNodeOfTextDocumentShouldBeAbleToUpdateOtherNodes()
        {
            // Given in setup
            var first = UnderTest.SubStringOfText(0, 5);
            var last = UnderTest.SubStringOfText(5);

            // When
            first.SetText("HelloHello");
            var result = UnderTest.Text;

            // Then
            Assert.That(result, Is.EqualTo("HelloHello World!"));

            var expectedText = String.Join(String.Empty, UnderTest.Children.Select(child => child.Text));
            Assert.That(result, Is.EqualTo(expectedText));

            Assert.That(last.Start, Is.EqualTo(10));
        }

        [Test]
        public void TextDocumentShouldAbleToConvertToTextNode()
        {
            // Given in setup
            // When
            var result = UnderTest.AsTextNode();

            // Then
            Assert.That(result.Text, Is.EqualTo(UnderTest.Text));
        }

    }
}
