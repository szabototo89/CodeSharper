using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Selectors
{
    public abstract class SelectorBase
    {
        private Dictionary<String, Object> attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorBase"/> class.
        /// </summary>
        protected SelectorBase()
        {
            attributes = new Dictionary<String, Object>();
        }

        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public abstract IEnumerable<Object> SelectElement(Object element);

        /// <summary>
        /// Determines whether [is attribute defined] [the specified name].
        /// </summary>
        protected virtual Boolean IsAttributeDefined(String attributeName)
        {
            return attributes.ContainsKey(attributeName);
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        protected Object GetAttributeValue(String attributeName)
        {
            Option<Object> result = attributes.TryGetValue(attributeName);

            if (result == Option.None) return null;
            return result.Value;
        }

        /// <summary>
        /// Gets the attribute boolean value.
        /// </summary>
        protected Boolean GetAttributeBooleanValue(String attributeName)
        {
            if (!IsAttributeDefined(attributeName)) return false;
            var value = GetAttributeValue(attributeName);
            return ReferenceEquals(value, null) || Equals(value, true);
        }

        /// <summary>
        /// Applys the attributes.
        /// </summary>
        public virtual void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            this.attributes = attributes.ToDictionary(key => key.Name, value => value.Value);
        }
    }
}