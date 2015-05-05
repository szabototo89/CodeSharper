using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    [Consumes(typeof(MultiValueConsumer<TextRange>))]
    public class ConvertCaseRunnable : RunnableBase<TextRange, TextRange>
    {
        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override TextRange Run(TextRange parameter)
        {
            if (parameter == null) return null;
            return parameter.ChangeText(parameter.GetText().ToUpper());
        }
    }
}
