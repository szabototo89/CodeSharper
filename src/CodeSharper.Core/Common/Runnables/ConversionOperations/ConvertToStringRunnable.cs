using System;
using System.Collections;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables.ConversionOperations
{
    public class ConvertToStringRunnable : RunnableBase<Object, String>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override String Run(Object parameter)
        {
            if (parameter == null) return "(null)";

            if (parameter is String)
            {
                return (String) parameter;
            }

            if (parameter is IEnumerable)
            {
                var enumerable = parameter.Cast<IEnumerable>()
                                          .OfType<Object>()
                                          .Select((element, index) => Run(element));
                return String.Format("{0}", String.Join(Environment.NewLine, enumerable));
            }

            if (parameter is TextRange)
            {
                var textRange = parameter.Cast<TextRange>();
                return textRange.GetText();
            }

            if (parameter is Node)
            {
                var node = parameter.Cast<Node>();
                return Run(node.TextRange);
            }

            return parameter.ToString();
        }
    }
}
