namespace CodeSharper.Interpreter.Common
{
    public class DirectChildSelectorOperator : BaseSelectorOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectChildSelectorOperator"/> class.
        /// </summary>
        public DirectChildSelectorOperator() : base(">") { }
    }
}