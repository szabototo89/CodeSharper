using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Interpreter.Common;

namespace CodeSharper.Interpreter.Visitors
{
    public interface INodeSelectorFactory
    {
        /// <summary>
        /// Creates the unary selector.
        /// </summary>
        UnarySelector CreateUnarySelector(SelectableElement element);

        /// <summary>
        /// Creates the binary selector.
        /// </summary>
        BinarySelector CreateBinarySelector(BaseSelector left, BaseSelector right, BaseSelectorOperator @operator);

        /// <summary>
        /// Creates the selector operator.
        /// </summary>
        BaseSelectorOperator CreateSelectorOperator(String @operator);

        /// <summary>
        /// Creates a selector element
        /// </summary>
        SelectorElementAttribute CreateSelectorElementAttribute(String name, Constant value);

        /// <summary>
        /// Creates a pseudo selector
        /// </summary>
        PseudoSelector CreatePseudoSelector(String name, Constant value);


        /// <summary>
        /// Creates a selectable element
        /// </summary>
        SelectableElement CreateSelectableElement(String name, IEnumerable<SelectorElementAttribute> attributes, IEnumerable<PseudoSelector> pseudoSelectors);
    }

    public class DefaultNodeSelectorFactory : INodeSelectorFactory
    {
        /// <summary>
        /// Creates the unary selector.
        /// </summary>
        public UnarySelector CreateUnarySelector(SelectableElement element)
        {
            return new UnarySelector(element);
        }

        /// <summary>
        /// Creates the binary selector.
        /// </summary>
        public BinarySelector CreateBinarySelector(BaseSelector left, BaseSelector right, BaseSelectorOperator selectorOperator)
        {
            return new BinarySelector(left, right, selectorOperator);
        }

        /// <summary>
        /// Creates the selector operator.
        /// </summary>
        public BaseSelectorOperator CreateSelectorOperator(String @operator)
        {
            Assume.NotNull(@operator, "operator");

            switch (@operator)
            {
                case ">":
                    return new DirectChildSelectorOperator();
                case "":
                    return new RelativeChildSelectorOperator();
                default:
                    throw new NotSupportedException(String.Format("Not supported child selector: {0}.", @operator));
            }
        }

        /// <summary>
        /// Creates a selector element
        /// </summary>
        public SelectorElementAttribute CreateSelectorElementAttribute(String name, Constant value)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(value, "value");

            return new SelectorElementAttribute {
                Name = name,
                Value = value
            };
        }

        /// <summary>
        /// Creates a pseudo selector
        /// </summary>
        public PseudoSelector CreatePseudoSelector(String name, Constant value)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(value, "value");

            return new PseudoSelector {
                Name = name,
                Value = value
            };
        }

        /// <summary>
        /// Creates a selectable element
        /// </summary>
        public SelectableElement CreateSelectableElement(String name, IEnumerable<SelectorElementAttribute> attributes, IEnumerable<PseudoSelector> pseudoSelectors)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(attributes, "attributes");
            Assume.NotNull(pseudoSelectors, "pseudoSelectors");

            return new SelectableElement(name, attributes, pseudoSelectors);
        }
    }
}