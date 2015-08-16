using System;
using System.Linq;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Languages.CSharp.Combinators;
using CodeSharper.Languages.CSharp.Selectors.DeclarationSelectors;
using CodeSharper.Languages.CSharp.Selectors.ExpressionSelectors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using static CodeSharper.Core.Utilities.ConstructsHelper;

namespace CodeSharper.Tests.Languages.CSharp.Runnables
{
    [TestFixture]
    public class ComplexSelectorTests : TestFixtureBase
    {
        public static SyntaxNode ParseDeclaration(String sourceText)
        {
            return SyntaxFactory.ParseSyntaxTree(sourceText).GetRoot().ChildNodes().FirstOrDefault();
        }

        public class RelativeSyntaxNodeCombinatorType : TestFixtureBase
        {
            [Test(Description = "should select every classes and every expressions inside the classes")]
            public void ShouldSelectEveryClassesAndEveryExpressionInsideClasses()
            {
                // Arrange
                var sourceText = @"
                    public class Foo {
                        public void FooMethod() {
                            var i = 10 + 5;
                        }
                    }
                ";
                var tree = ParseDeclaration(sourceText);
                var combinator = new CombinatorBuilder()
                                    .Select(new ClassDeclarationSelector())
                                    .And(new ExpressionSelector())
                                    .Build();

                // Act
                var elements = combinator.Calculate(Array(tree));

                // Assert
                var result = elements.Select(element => element.ToString());
                Assert.That(result, Contains.Item("10 + 5"));
            }
        }
    }
}