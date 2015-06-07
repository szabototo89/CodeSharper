using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(MultiValueConsumer<Node>))]
    public class ConvertToTextRangeRunnable : RunnableBase<Node, TextRange>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override TextRange Run(Node parameter)
        {
            if (parameter == null) return null;
            return parameter.TextRange;
        }
    }
}