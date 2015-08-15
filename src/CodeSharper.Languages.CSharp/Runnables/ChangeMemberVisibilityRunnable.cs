using System;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Services;
using CodeSharper.Languages.CSharp.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static CodeSharper.Core.Utilities.ConstructsHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CodeSharper.Languages.CSharp.Runnables
{
    [CommandDescriptor("change-member-visibility")]
    [Consumes(typeof (MultiValueConsumer<SyntaxNode>))]
    public class ChangeMemberVisibilityRunnable : RunnableWithContextBase<SyntaxNode, SyntaxNode, DocumentContext>
    {
        private MemberVisibility visibility;

        [Parameter("visibility")]
        public String Visibility
        {
            get { return visibility.ToString().ToLower(); }
            set
            {
                switch (value)
                {
                    case "public":
                        visibility = MemberVisibility.Public;
                        break;
                    case "private":
                        visibility = MemberVisibility.Private;
                        break;
                    case "protected":
                        visibility = MemberVisibility.Protected;
                        break;
                    case "protected internal":
                        visibility = MemberVisibility.ProtectedInternal;
                        break;
                    case "internal":
                        visibility = MemberVisibility.Internal;
                        break;
                    default:
                        throw new NotSupportedException($"Invalid visibility value: {value}.");
                }
            }
        }

        public override SyntaxNode Run(SyntaxNode parameter, DocumentContext context)
        {
            if (parameter == null) return null;
            Assume.IsRequired(context, nameof(context));

            var declaration = parameter as MethodDeclarationSyntax;
            if (declaration == null) return parameter;

            var visibilityModifiers = Array(SyntaxKind.PublicKeyword, SyntaxKind.PrivateKeyword, SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword);

            var modifiers = from modifier in declaration.Modifiers
                            where visibilityModifiers.Any(visibilityModifier => visibilityModifier == modifier.Kind())
                            select modifier;

            var tokenList = RetrieveVisibilityTokenList(visibility);

            var newNode = declaration.WithModifiers(
                TokenList(tokenList.Concat(declaration.Modifiers)
                                     .Except(modifiers)))
                                     .NormalizeWhitespace();

            context.DocumentEditor.ReplaceNode(declaration, newNode);

            return newNode;
        }

        private static SyntaxTokenList RetrieveVisibilityTokenList(MemberVisibility visibility)
        {
            switch (visibility)
            {
                case MemberVisibility.Private:
                    return TokenList(Token(SyntaxKind.PrivateKeyword));
                case MemberVisibility.Public:
                    return TokenList(Token(SyntaxKind.PublicKeyword));
                case MemberVisibility.Protected:
                    return TokenList(Token(SyntaxKind.ProtectedKeyword));
                case MemberVisibility.ProtectedInternal:
                    return TokenList(Token(SyntaxKind.ProtectedKeyword), Token(SyntaxKind.InternalKeyword));
                case MemberVisibility.Internal:
                    return TokenList(Token(SyntaxKind.InternalKeyword));
                default:
                    throw new ArgumentOutOfRangeException(nameof(visibility), visibility, null);
            }
        }

        private enum MemberVisibility
        {
            Private,
            Public,
            Protected,
            ProtectedInternal,
            Internal
        }
    }
}