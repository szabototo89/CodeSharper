﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeSharper.Languages.CSharp.Selectors.ExpressionSelectors
{
    public abstract class TypeCastingLikeExpressionSelector : ExpressionSelectorBase<BinaryExpressionSyntax>
    {
        private String CASTING_TYPE = "type";

        protected TypeCastingLikeExpressionSelector(SyntaxKind? kind) : base(kind)
        {
        }

        protected String CastingType { get; private set; }

        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);

            if (IsAttributeDefined(CASTING_TYPE))
            {
                CastingType = GetAttributeValue(CASTING_TYPE).ToString();
            }
        }

        public override IEnumerable<Object> SelectElement(Object element)
        {
            var selectedElements = base.SelectElement(element);
            if (!IsAttributeDefined()) return selectedElements;
            return SelectElementByType(selectedElements.OfType<BinaryExpressionSyntax>(), CastingType);
        }

        private IEnumerable<Object> SelectElementByType(IEnumerable<BinaryExpressionSyntax> elements, String typeValue)
        {
            foreach (var element in elements)
            {
                var right = element.Right;
                if (right is IdentifierNameSyntax)
                {
                    var identifier = (IdentifierNameSyntax) right;
                    if (identifier.Identifier.Text == typeValue)
                    {
                        yield return element;
                    }
                }
                else if (right is PredefinedTypeSyntax)
                {
                    var predefinedType = (PredefinedTypeSyntax) right;
                    if (predefinedType.Keyword.Text == typeValue)
                    {
                        yield return element;
                    }
                }
                else if (right is QualifiedNameSyntax)
                {
                    var qualifiedNameNode = (QualifiedNameSyntax) right;
                    var qualifiedName = RetrieveQualifiedName(qualifiedNameNode);

                    if (qualifiedName.EndsWith(typeValue))
                    {
                        yield return element;
                    }
                }
            }
        }

        private static String RetrieveQualifiedName(QualifiedNameSyntax qualifiedNameNode)
        {
            Assume.IsRequired(qualifiedNameNode, nameof(qualifiedNameNode));

            var name = "";
            var current = qualifiedNameNode;

            while (current != null)
            {
                var identifier = current.Right.Identifier;
                name = String.IsNullOrEmpty(name)
                    ? identifier.Text
                    : $"{identifier.Text}.{name}";

                if (current.Left is IdentifierNameSyntax)
                {
                    identifier = ((IdentifierNameSyntax) current.Left).Identifier;
                    name = $"{identifier.Text}.{name}";
                }

                current = current.Left as QualifiedNameSyntax;
            }

            return name;
        }
    }
}