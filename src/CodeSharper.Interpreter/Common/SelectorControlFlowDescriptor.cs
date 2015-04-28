using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class SelectorControlFlowDescriptor : ControlFlowDescriptorBase
    {
        /// <summary>
        /// Gets or sets the selector.
        /// </summary>
        public BaseSelector Selector { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorControlFlowDescriptor"/> class.
        /// </summary>
        public SelectorControlFlowDescriptor(BaseSelector selector) : base(ControlFlowOperationType.Selector)
        {
            Assume.NotNull(selector, "selector");
            Selector = selector;
        }
    }
}