using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Selectors
{
    public abstract class SelectorBase
    {
        private Dictionary<String, Object> attributes;
        private IEnumerable<Regex> classSelectors;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorBase"/> class.
        /// </summary>
        protected SelectorBase()
        {
            attributes = new Dictionary<String, Object>();
            classSelectors = Enumerable.Empty<Regex>();
        }

        #region Class Selectors

        /// <summary>
        /// Gets the class selectors.
        /// </summary>
        protected IEnumerable<Regex> ClassSelectors
        {
            get { return classSelectors; }
        }

        /// <summary>
        /// Adds the class selectors.
        /// </summary>
        public virtual void AddClassSelectors(IEnumerable<Regex> classSelectors)
        {
            this.classSelectors = classSelectors ?? Enumerable.Empty<Regex>();
        }

        /// <summary>
        /// Clears the class selectors.
        /// </summary>
        public virtual void ClearClassSelectors()
        {
            classSelectors = Enumerable.Empty<Regex>();
        }

        #endregion


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
        /// Gets the attribute value or default.
        /// </summary>
        protected TResult GetAttributeValueOrDefault<TResult>(String attributeName, TResult defaultValue)
        {
            Option<Object> result = attributes.TryGetValue(attributeName);
            if (result == Option.None) return defaultValue;
            return (TResult) result.Value;
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
        /// Applys the attributes to specified selector
        /// </summary>
        public virtual void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            this.attributes = attributes.GetOrEmpty().ToDictionary(key => key.Name, value => value.Value);
        }
    }
}