namespace CodeSharper.Interpreter.Common
{
    public interface INodeSelectorResolver
    {
        /// <summary>
        /// Creates the specified selector.
        /// </summary>
        Core.Nodes.Combinators.CombinatorBase Create(BaseSelector selector);
    }
}
