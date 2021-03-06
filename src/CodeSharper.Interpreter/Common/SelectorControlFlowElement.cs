﻿using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class SelectorControlFlowElement : ControlFlowElementBase
    {
        /// <summary>
        /// Gets or sets the selector element.
        /// </summary>
        public SelectorElementBase SelectorElement { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorControlFlowElement"/> class.
        /// </summary>
        public SelectorControlFlowElement(SelectorElementBase selectorElement) : base(ControlFlowOperationType.Selector)
        {
            Assume.NotNull(selectorElement, nameof(selectorElement));
            SelectorElement = selectorElement;
        }
    }
}