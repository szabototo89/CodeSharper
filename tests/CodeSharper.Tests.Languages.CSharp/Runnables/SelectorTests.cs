using System;
using System.Linq;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Languages.CSharp.Nodes.Selectors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

namespace CodeSharper.Tests.Languages.CSharp.Runnables
{
    [TestFixture]
    public class SelectorTests
    {
        public class Initialize<TSelector> : TestFixtureBase
            where TSelector : SelectorBase, new()
        {
            protected TSelector underTest;

            public override void Setup()
            {
                base.Setup();
                underTest = new TSelector();
            }
        }

        private static SyntaxNode ParseSyntaxElement(String sourceText)
        {
            var tree = SyntaxFactory.ParseSyntaxTree(sourceText);
            return tree.GetRoot().ChildNodes().SingleOrDefault();
        }

        public class ClassDeclarationSelectorType : Initialize<ClassDeclarationSelector>
        {
            [Test(Description = "should select Foo class in C# code")]
            public void ShouldSelectFooClassInCSharpCode()
            {
                // Arrange
                var tree = ParseSyntaxElement(@"
                    public class Foo { }
                ");

                // Act
                var result = underTest.SelectElement(tree).SingleOrDefault() as ClassDeclarationSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Identifier.Text, Is.EqualTo("Foo"));
            }
        }

        public class StructDeclarationSelectorType : Initialize<StructDeclarationSelector>
        {
            [Test(Description = "should select Foo struct in specified code")]
            public void ShouldSelectFooStructInSpecifiedCode()
            {
                // Arrange
                var tree = ParseSyntaxElement(@"
                    public struct Foo { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as StructDeclarationSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Identifier.Text, Is.EqualTo("Foo"));
            }
        }

        public class InterfaceDeclarationSelectorType : Initialize<InterfaceDeclarationSelector>
        {
            [Test(Description = "should select Foo interface in specified code")]
            public void ShouldSelectFooStructInSpecifiedCode()
            {
                // Arrange
                var tree = ParseSyntaxElement(@"
                    public interface Foo { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as InterfaceDeclarationSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Identifier.Text, Is.EqualTo("Foo"));
            }
        }

        public class EnumDeclarationSelectorType : Initialize<EnumDeclarationSelector>
        {
            [Test(Description = "should select Foo enum in specified code")]
            public void ShouldSelectFooStructInSpecifiedCode()
            {
                // Arrange
                var tree = ParseSyntaxElement(@"
                    public enum Foo { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as EnumDeclarationSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Identifier.Text, Is.EqualTo("Foo"));
            }
        }
    }
}