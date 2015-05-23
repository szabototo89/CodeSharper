using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class NthChildNodeModifier : NodeModifierBase
    {
        private enum NthType
        {
            Odd, Even, Specific
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        private NthType Type { get; set; }

        /// <summary>
        /// Gets or sets the children count.
        /// </summary>
        public Int32 Index { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NthChildNodeModifier"/> class.
        /// </summary>
        public NthChildNodeModifier(Object parameter)
        {
            if (parameter.IsNumeric())
            {
                Type = NthType.Specific;
                Index = Convert.ToInt32(parameter);
            }
            else if (parameter is String)
            {
                var type = (String)parameter;
                switch (type)
                {
                    case "odd":
                        Type = NthType.Odd;
                        break;
                    case "even":
                        Type = NthType.Even;
                        break;
                    default:
                        throw new Exception(String.Format("Unrecognized parameter: {0}", type));
                }
            }
        }

        /// <summary>
        /// Modifies the selection of node
        /// </summary>
        public override IEnumerable<Object> ModifySelection(Object value)
        {
            if (!(value is IHasChildren<Object> && value is IHasParent<Object>))
                return Enumerable.Empty<Object>();

            var element = (IHasChildren<Object>)(((IHasParent<Object>)value).Parent);
            var children = element.Children.Where(child => child.GetType() == value.GetType()).ToArray();

            if (Type == NthType.Specific)
            {
                if (Math.Abs(Index) < children.Length)
                {
                    var child = Index >= 0 ? children[Index] : children[children.Length + Index];
                    if (child == value) return new[] { child };
                }
            }
            else
            {
                var childIndex = Array.IndexOf(children, value);

                if (Type == NthType.Odd && childIndex % 2 == 1)
                {
                    return new[] { value };
                }

                if (Type == NthType.Even && childIndex % 2 == 0)
                {
                    return new[] { value };
                }
            }
            return Enumerable.Empty<Object>();
        }
    }
}