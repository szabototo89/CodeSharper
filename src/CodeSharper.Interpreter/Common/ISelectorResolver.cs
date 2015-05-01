using CodeSharper.Core.Nodes.Combinators;

namespace CodeSharper.Interpreter.Common
{
    public interface ISelectorResolver
    {
        /// <summary>
        /// Creates the specified selectorElement.
        /// </summary>
        CombinatorBase Create(SelectorElementBase selectorElement);
    }
}
