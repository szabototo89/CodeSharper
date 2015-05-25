using System;
using System.Linq;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Json.Factories;
using CodeSharper.Languages.Json.SyntaxTrees;
using CodeSharper.Languages.Json.SyntaxTrees.Constants;
using CodeSharper.Languages.Json.SyntaxTrees.Literals;
using CodeSharper.Languages.Utilities;

namespace CodeSharper.Languages.Json.Visitors
{
    public class StandardJsonSyntaxTreeBuilder : JsonBaseVisitor<JsonNode>
    {
        private TextDocument _textDocument;

        /// <summary>
        /// Gets or sets the tree factory.
        /// </summary>
        public IJsonSyntaxTreeFactory TreeFactory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardJsonSyntaxTreeBuilder"/> class.
        /// </summary>
        public StandardJsonSyntaxTreeBuilder(IJsonSyntaxTreeFactory treeFactory)
        {
            Assume.NotNull(treeFactory, "treeFactory");
            TreeFactory = treeFactory;
        }

        /// <summary>
        /// Visits the specified input.
        /// </summary>
        public virtual JsonNode Visit(String input, IParseTree parseTree)
        {
            Assume.NotNull(input, "input");
            Assume.NotNull(parseTree, "parseTree");
            
            _textDocument = new TextDocument(input);
            return base.Visit(parseTree);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="BooleanConstant" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitBooleanConstant(JsonParser.BooleanConstantContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            return TreeFactory.CreateBooleanConstant(textRange);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="NumberConstant" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitNumberConstant(JsonParser.NumberConstantContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            return TreeFactory.CreateNumberConstant(textRange);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="StringConstant" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitStringConstant(JsonParser.StringConstantContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            return TreeFactory.CreateStringConstant(textRange);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="JsonParser.IdentifierKey" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitIdentifierKey(JsonParser.IdentifierKeyContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            var value = context.Key.Text;
            return TreeFactory.CreateKey(value, textRange);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="JsonParser.StringKey" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitStringKey(JsonParser.StringKeyContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            var value = context.Key.Text;
            return TreeFactory.CreateKey(value, textRange);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="JsonParser.literal" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitLiteral(JsonParser.LiteralContext context)
        {
            if (context.ObjectLiteral != null)
            {
                var literal = context.ObjectLiteral.Accept(this);
                return literal;
            }

            if (context.ArrayLiteral != null)
            {
                var arrayLiteral = context.ArrayLiteral.Accept(this);
                return arrayLiteral;
            }

            throw new NotSupportedException("Invalid element of literal.");
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="JsonParser.start" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitStart(JsonParser.StartContext context)
        {
            return context.Literal.Accept(this);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="JsonParser.ConstantValue" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitConstantValue(JsonParser.ConstantValueContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            var value = context.Value.Accept(this) as ConstantSyntax;

            return TreeFactory.CreateValue(value, textRange);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="JsonParser.LiteralValue" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitLiteralValue(JsonParser.LiteralValueContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            var value = context.Value.Accept(this) as LiteralSyntax;

            return TreeFactory.CreateValue(value, textRange);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="JsonParser.keyValuePair" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitKeyValuePair(JsonParser.KeyValuePairContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            var key = context.Key.Accept(this) as KeyDeclaration;
            var value = context.Value.Accept(this) as ValueDeclaration;

            return TreeFactory.CreateKeyValuePair(textRange, key, value);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="JsonParser.arrayLiteral" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitArrayLiteral(JsonParser.ArrayLiteralContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            var values = context.value().AcceptAll(this).OfType<ValueDeclaration>();

            return TreeFactory.CreateArrayLiteral(values, textRange);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="JsonParser.objectLiteral" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override JsonNode VisitObjectLiteral(JsonParser.ObjectLiteralContext context)
        {
            var textRange = context.CreateTextRange(_textDocument);
            var keyValuePairs = context.keyValuePair().AcceptAll(this).OfType<KeyValueDeclaration>();

            return TreeFactory.CreateObjectLiteral(keyValuePairs, textRange);
        }
    }
}