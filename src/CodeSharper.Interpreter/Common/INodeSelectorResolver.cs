namespace CodeSharper.Interpreter.Common
{
    public interface INodeSelectorResolver
    {
        /// <summary>
        /// Creates the specified selectorElement.
        /// </summary>
        Core.Nodes.Combinators.CombinatorBase Create(SelectorElementBase selectorElement);
    }
}
