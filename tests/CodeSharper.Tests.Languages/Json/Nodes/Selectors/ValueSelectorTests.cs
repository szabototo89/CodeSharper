using CodeSharper.Languages.Json.Nodes.Selectors;
using CodeSharper.Languages.Json.SyntaxTrees.Constants;
using CodeSharper.Languages.Json.SyntaxTrees.Literals;
using NUnit.Framework;

namespace CodeSharper.Tests.Languages.Json.Nodes.Selectors
{
    [TestFixture]
    public class ValueSelectorTests : TestFixtureBase
    {
        [Test(Description = "SelectElement should return ValueDeclarations when no attributes are passed")]
        public void SelectElement_ShouldReturnValueDeclarations_WhenNoAttributesArePassed()
        {
            // Given
            var underTest = new ValueSelector();

            // When

            // Then

        }
    }
}