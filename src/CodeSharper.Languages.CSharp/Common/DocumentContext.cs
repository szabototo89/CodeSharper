using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeSharper.Languages.CSharp.Common
{
    public class DocumentContext
    {
        /// <summary>
        /// Gets or sets the workspace.
        /// </summary>
        public Workspace Workspace { get; }

        /// <summary>
        /// Gets or sets the current document.
        /// </summary>
        public Document CurrentDocument { get; }

        /// <summary>
        /// Gets or sets the document editor.
        /// </summary>
        public DocumentEditor DocumentEditor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentContext"/> class.
        /// </summary>
        public DocumentContext(Workspace workspace, Document currentDocument)
        {
            Workspace = workspace;
            CurrentDocument = currentDocument;

            DocumentEditor = DocumentEditor.CreateAsync(currentDocument).Result;
        }
    }
}