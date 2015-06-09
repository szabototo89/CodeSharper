using System;

namespace CodeSharper.Languages.Text.Nodes.Selectors
{
    public class ParagraphSelector : TextSeparatorSelector
    {
        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected override String Pattern
        {
            get { return "(\r?\n){2,}"; }
        }
    }
}