using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Interfaces;

namespace CodeSharper.Core.Nodes
{
    public class SelectorAttribute : IHasName, IHasValue<Object>
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public Object Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorAttribute"/> class.
        /// </summary>
        public SelectorAttribute(String name, Object value)
        {
            Name = name;
            Value = value;
        }
    }
}
