using CodeSharper.Core;
using NUnit.Framework;

namespace CodeSharper.Tests.Core
{
    [TestFixture]
    class TextInformationTestFixture
    {
        private MutableNode Node;

        [SetUp]
        public void Setup()
        {
            Node = new MutableNode();
        }

        [Test(Description = "TextInformation should contain value of FullText")]
        public void TextInformationShouldContainFullTextValueTest()
        {
            // GIVEN
            var underTest = Node.GetTextInformation();

            // WHEN
            var result = underTest.FullText;

            // THEN
            Assert.That(result, Is.InstanceOf<string>());
            Assert.That(result, Is.Empty);
        }

        [Test(Description = "TextInformation should contain TextSpan")]
        public void TextInformationShouldContainTextSpanTest()
        {
            // GIVEN
            var underTest = Node.GetTextInformation();

            // WHEN
            var result = underTest.TextSpan;

            // THEN
            var expected = new TextSpan();
            Assert.That(result, Is.InstanceOf<TextSpan>());
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test(Description = "TextInformation should return its node")]
        public void TextInformationShouldReturnItsNodeTest()
        {
            // GIVEN
            var underTest = Node.GetTextInformation();

            // WHEN
            var result = underTest.GetNode();

            // THEN
            var expected = Node;
            Assert.That(result, Is.InstanceOf<MutableNode>());
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}