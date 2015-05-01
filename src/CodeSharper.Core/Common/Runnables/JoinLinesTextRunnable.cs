using System;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    public class JoinLinesTextRunnable : RunnableBase<TextRange, TextRange>
    {
        /// <summary>
        /// Gets or sets the join string.
        /// </summary>
        [BindTo("separator")]
        public String Separator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinLinesTextRunnable"/> class.
        /// </summary>
        public JoinLinesTextRunnable()
        {
            Separator = String.Empty;
        }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override TextRange Run(TextRange parameter)
        {
            Assume.NotNull(parameter, "parameter");
            var text = parameter.GetText().Replace(Environment.NewLine, Separator);
            return parameter.ChangeText(text);
        }
    }
}