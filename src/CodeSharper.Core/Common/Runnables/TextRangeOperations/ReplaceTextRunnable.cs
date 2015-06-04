using System;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables.TextRangeOperations
{
    [Consumes(typeof(GreadyMultiValueConsumer<TextRange>))]
    public class ReplaceTextRunnable : RunnableBase<TextRange, TextRange>
    {
        /// <summary>
        /// Gets or sets the replaced text
        /// </summary>
        [Parameter("replacedText")]
        public String ReplacedText { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTextRunnable"/> class.
        /// </summary>
        public ReplaceTextRunnable() : this("$") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTextRunnable"/> class.
        /// </summary>
        public ReplaceTextRunnable(String replacedText)
        {
            Assume.NotNull(replacedText, "replacedText");
            ReplacedText = replacedText;
        }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override TextRange Run(TextRange parameter)
        {
            Assume.NotNull(parameter, "parameter");

            parameter.ChangeText(ReplacedText.Replace("$", parameter.GetText()));
            return parameter;
        }
    }
}