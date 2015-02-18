namespace CodeSharper.Core.Selectors.NodeSelectorOperands
{
    public abstract class BinaryNodeSelectorOperand : NodeSelectorOperandBase
    {
        /// <summary>
        /// Gets or sets the left expression of operand
        /// </summary>
        public NodeSelectorOperandBase Left { get; set; }

        /// <summary>
        /// Gets or sets the right expression of operand
        /// </summary>
        public NodeSelectorOperandBase Right { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryNodeSelectorOperand"/> class.
        /// </summary>
        protected BinaryNodeSelectorOperand(NodeSelectorOperandBase left, NodeSelectorOperandBase right)
        {
            Left = left;
            Right = right;
        }
    }
}