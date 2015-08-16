using System;
using System.Linq;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Languages.CSharp.Selectors.DeclarationSelectors;
using CodeSharper.Languages.CSharp.Selectors.ExpressionSelectors;
using CodeSharper.Languages.CSharp.Selectors.StatementSelectors;
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

        private static SyntaxNode ParseDeclarationElement(String sourceText)
        {
            var tree = SyntaxFactory.ParseSyntaxTree(sourceText);
            return tree.GetRoot().ChildNodes().SingleOrDefault();
        }

        private static SyntaxNode ParseExpression(String sourceText)
        {
            var tree = SyntaxFactory.ParseExpression(sourceText);
            return tree;
        }

        private static SyntaxNode ParseStatement(String sourceText)
        {
            var tree = SyntaxFactory.ParseStatement(sourceText);
            return tree;
        }

        public class ClassDeclarationSelectorType : Initialize<ClassDeclarationSelector>
        {
            [Test(Description = "should select Foo class in C# code")]
            public void ShouldSelectFooClassInCSharpCode()
            {
                // Arrange
                var tree = ParseDeclarationElement(@"
                    public class Foo { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as ClassDeclarationSyntax;

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
                var tree = ParseDeclarationElement(@"
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
            public void ShouldSelectFooInterfaceInSpecifiedCode()
            {
                // Arrange
                var tree = ParseDeclarationElement(@"
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
            public void ShouldSelectFooEnumInSpecifiedCode()
            {
                // Arrange
                var tree = ParseDeclarationElement(@"
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

        public class PropertyDeclarationSelectorType : Initialize<PropertyDeclarationSelector>
        {
            [Test(Description = "should select Foo property in specified code")]
            public void ShouldSelectFooPropertyInSpecifiedCode()
            {
                // Arrange
                var tree = ParseDeclarationElement(@"
                    public string Foo { get; set; }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as PropertyDeclarationSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Identifier.Text, Is.EqualTo("Foo"));
            }
        }

        public class MethodDeclarationSelectorType : Initialize<MethodDeclarationSelector>
        {
            [Test(Description = "should select Foo method in specified code")]
            public void ShouldSelectFooMethodInSpecifiedCode()
            {
                // Arrange
                var tree = ParseDeclarationElement(@"
                    public void Foo() { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as MethodDeclarationSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Identifier.Text, Is.EqualTo("Foo"));
            }
        }

        public class VariableDeclarationSelectorType : Initialize<LocalVariableDeclarationSelector>
        {
            [Test(Description = "should select foo variable in specified code")]
            public void ShouldSelectFooVariableInSpecifiedCode()
            {
                // Arrange
                var tree = ParseStatement(@"
                    int foo = 10;
                ");
                Assume.That(tree, Is.Not.Null);

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as LocalDeclarationStatementSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Declaration.Variables.FirstOrDefault()?.Identifier.Text, Is.EqualTo("foo"));
            }
        }

        public class ForeachStatementSelectorType : Initialize<ForeachStatementSelector>
        {
            [Test(Description = "should select foreach statement in specified code")]
            public void ShouldSelectForeachStatementInSpecifiedCode()
            {
                // Arrange
                var tree = ParseStatement(@"
                    foreach (var i in new[] { 1, 2, 3 }) { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as ForEachStatementSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }

        public class ForStatementSelectorType : Initialize<ForStatementSelector>
        {
            [Test(Description = "should select for statement in specified code")]
            public void ShouldSelectForStatementInSpecifiedCode()
            {
                // Arrange
                var tree = ParseStatement(@"
                    for (int i = 0; i < 10; i++) { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as ForStatementSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }

        public class WhileStatementSelectorType : Initialize<WhileStatementSelector>
        {
            [Test(Description = "should select while statement in specified code")]
            public void ShouldSelectWhileStatementInSpecifiedCode()
            {
                // Arrange
                var tree = ParseStatement(@"
                    while (true) { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as WhileStatementSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }

        public class IfStatementSelectorType : Initialize<IfStatementSelector>
        {
            [Test(Description = "should select if statement in specified code")]
            public void ShouldSelectIfStatementInSpecifiedCode()
            {
                // Arrange
                var tree = ParseStatement(@"
                    if (true) { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as IfStatementSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }

        public class UsingStatementSelectorType : Initialize<UsingStatementSelector>
        {
            [Test(Description = "should select using statement in specified code")]
            public void ShouldSelectUsingStatementInSpecifiedCode()
            {
                // Arrange
                var tree = ParseStatement(@"
                    using (null) { }
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as UsingStatementSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }

        public class ExpressionStatementSelectorType : Initialize<ExpressionStatementSelector>
        {
            [Test(Description = "should select any kind of expression statement in specified code")]
            public void ShouldSelectAnyKindOfExpressionStatementInSpecifiedCode()
            {
                // Arrange
                var tree = ParseStatement(@"
                    (5 + 5).ToString();
                ");

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as ExpressionStatementSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }

        public class ExpressionSelectorType : Initialize<ExpressionSelector>
        {
            [TestCase("5 + 5")]
            [TestCase("false")]
            [TestCase("(5 + 5).ToString().ToLowerCase()")]
            [Test(Description = "should select any kind of expression in specified code")]
            public void ShouldSelectAnyKindOfExpressionInSpecifiedCode(String expression)
            {
                // Arrange
                var tree = ParseExpression(expression);

                // Act
                var result = underTest.SelectElement(tree)
                                      .SingleOrDefault() as ExpressionSyntax;

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }
    }
}