using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Tests.Interpreter.Stubs
{
    [Consumes(typeof(MultiValueConsumer<Double>))]
    public class IncrementRunnable : RunnableBase<Double, Double>
    {
        /// <summary>
        /// Gets or sets the increment.
        /// </summary>
        [BindTo("value")]
        public Double Increment { get; set; }

        /// <summary>
        /// Runs an algorithm with the specified parameter.
        /// </summary>
        public override Double Run(Double parameter)
        {
            return parameter + Increment;
        }
    }
}