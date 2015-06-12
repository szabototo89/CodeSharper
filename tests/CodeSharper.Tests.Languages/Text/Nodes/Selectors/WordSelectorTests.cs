using System;
using System.Linq;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Text.Nodes.Selectors;
using NUnit.Framework;

namespace CodeSharper.Tests.Languages.Text.Nodes.Selectors
{
    [TestFixture]
    public class WordSelectorTests : TestFixtureBase
    {
        private TextRange createTextRange(String text)
        {
            var document = new TextDocument(text);
            return document.TextRange;
        }

        [Test(Description = "SelectElement should return separated words of specified text when every attribute has default value")]
        public void SelectElement_ShouldReturnSeparatedWordsOfSpecifiedText_WhenEveryAttributeHasDefaultValue()
        {
            // Given
            var textRange = createTextRange("hello world, how\nare\r\nyou?!Fine (thanks).");
            var underTest = new WordSelector();

            // When
            var result = underTest.SelectElement(textRange).Select(range => range.GetText());

            // Then
            Assert.That(result, Is.EquivalentTo(new[]
            {
                "hello", "world", "how", "are", "you", "Fine", "thanks"
            }));
        }

        [Test(Description = "SelectElement should return only numeric values when IsFilteringNumeric attribute is true and the others are set to false")]
        public void SelectElement_ShouldReturnOnlyNumericValues_WhenIsFilteringNumericIsTrueAndOthersAreSetToFalse()
        {
            // Given
            var textRange = createTextRange("hello 10! how are you? I am 55 years old.");
            var underTest = new WordSelector();
            underTest.ApplyAttributes(new[] {new SelectorAttribute("numeric", true)});

            // When
            var result = underTest.SelectElement(textRange).Select(range => range.GetText());

            // Then
            Assert.That(result, Is.EquivalentTo(new[] {"10", "55"}));
        }
    }
}