namespace CodeSharper.Core
{
    /// <summary>
    /// Represents a type of node. 
    /// For example: It may be a compilation unit, a statement, an expression etc.
    /// Use static fields to access types.
    /// </summary>
    public class NodeType
    {
        /// <summary>
        /// The undefined node type
        /// </summary>
        public static readonly NodeType Undefined;

        /// <summary>
        /// The compilation unit node type
        /// </summary>
        public static readonly NodeType CompilationUnit;

        /// <summary>
        /// Initializes the <see cref="NodeType"/> class.
        /// </summary>
        static NodeType()
        {
            Undefined = new NodeType();
            CompilationUnit = new NodeType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeType"/> class.
        /// </summary>
        protected internal NodeType() { }
    }
}