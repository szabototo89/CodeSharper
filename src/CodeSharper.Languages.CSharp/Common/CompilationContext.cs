using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeSharper.Languages.CSharp.Common
{
    public class CompilationContext
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
        /// Gets semantic model from current document 
        /// </summary>
        public SemanticModel SemanticModel => CurrentDocument?.GetSemanticModelAsync().Result;

        /// <summary>
        /// Gets or sets the document editor.
        /// </summary>
        public DocumentEditor DocumentEditor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilationContext"/> class.
        /// </summary>
        public CompilationContext(Workspace workspace, Document currentDocument)
        {
            Workspace = workspace;
            CurrentDocument = currentDocument;

            DocumentEditor = DocumentEditor.CreateAsync(currentDocument).Result;
        }
    }
}