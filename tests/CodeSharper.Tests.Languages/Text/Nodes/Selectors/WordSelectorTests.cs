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
        public static TextRange CreateTextRange(String text)
        {
            var document = new TextDocument(text);
            return document.TextRange;
        }

        public class SelectElementMethod
        {
            [Test(Description = "SelectElement should return separated words of specified text when every attribute has default value")]
            public void ShouldReturnSeparatedWordsOfSpecifiedText_WhenEveryAttributeHasDefaultValue()
            {
                // Given
                var textRange = CreateTextRange("hello world, how\nare\r\nyou?!Fine (thanks).");
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
            public void ShouldReturnOnlyNumericValues_WhenIsFilteringNumericIsTrueAndOthersAreSetToFalse()
            {
                // Given
                var textRange = CreateTextRange("hello 10! how are you? I am 55 years old.");
                var underTest = new WordSelector();
                underTest.ApplyAttributes(new[] {new SelectorAttribute("numeric", true)});

                // When
                var result = underTest.SelectElement(textRange).Select(range => range.GetText());

                // Then
                Assert.That(result, Is.EquivalentTo(new[] {"10", "55"}));
            }

            [Test(Description = "SelectElement should return only alphabetic values when IsFilteringAlphabetic attribute is true and the others are set to false")]
            public void ShouldReturnOnlyAlphabeticValues_WhenIsFilteringAlphabeticIsTrueAndOthersAreSetToFalse()
            {
                // Given
                var textRange = CreateTextRange("hello 10! how are you? I am 55 years old.");
                var underTest = new WordSelector();
                underTest.ApplyAttributes(new[] { new SelectorAttribute("alphabetic", true) });

                // When
                var result = underTest.SelectElement(textRange).Select(range => range.GetText());

                // Then
                Assert.That(result, Is.EquivalentTo(new[] { "hello", "how", "are", "you", "I", "am", "years", "old" }));
            }
        }

    }
}