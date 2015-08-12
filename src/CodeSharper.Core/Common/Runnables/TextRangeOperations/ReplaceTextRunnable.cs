using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables.TextRangeOperations
{
    [Consumes(typeof(MultiValueConsumer<IEnumerable<TextRange>>))]
    public class ReplaceTextRunnable : RunnableBase<IEnumerable<Object>, IEnumerable<TextRange>>
    {
        /// <summary>
        /// Gets or sets the replaced text
        /// </summary>
        [Parameter("replacedText")]
        public String ReplacedText { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTextRunnable"/> class.
        /// </summary>
        public ReplaceTextRunnable() : this("$")
        {
        }

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
        public override IEnumerable<TextRange> Run(IEnumerable<Object> parameters)
        {
            Assume.NotNull(parameters, "parameters");

            var textRanges = parameters.OfType<TextRange>().ToArray();
            if (!textRanges.Any()) return Enumerable.Empty<TextRange>();
            var textDocument = textRanges.First().TextDocument;

            textDocument.BeginTransaction();
            foreach (var range in textRanges)
            {
                range.ChangeText(ReplacedText.Replace("$", range.GetText()));
            }
            textDocument.EndTransaction();

            return textRanges;
        }
    }
}