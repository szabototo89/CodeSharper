using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class SelectorControlFlowDescriptor : ControlFlowDescriptorBase
    {
        /// <summary>
        /// Gets or sets the selectorElement.
        /// </summary>
        public SelectorElementBase SelectorElement { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorControlFlowDescriptor"/> class.
        /// </summary>
        public SelectorControlFlowDescriptor(SelectorElementBase selectorElement) : base(ControlFlowOperationType.Selector)
        {
            Assume.NotNull(selectorElement, "selectorElement");
            SelectorElement = selectorElement;
        }
    }
}