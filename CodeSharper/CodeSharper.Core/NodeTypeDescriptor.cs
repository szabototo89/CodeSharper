namespace CodeSharper.Core
{
    /// <summary>
    /// Represents a descriptor object of node type
    /// It contains language and node type
    /// </summary>
    public class NodeTypeDescriptor
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public virtual LanguageDescriptor Language { get; protected internal set; }

        /// <summary>
        /// Gets or sets the node type.
        /// </summary>
        public virtual NodeType Type { get; protected internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeTypeDescriptor"/> class.
        /// </summary>
        public NodeTypeDescriptor()
        {
            Language = LanguageDescriptor.Any;
            Type = NodeType.Undefined;
        }
    }
}