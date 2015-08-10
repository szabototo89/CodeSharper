using System;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Interpreter.Bootstrappers;

namespace CodeSharper.Playground.GUI.Modules
{
    public abstract class CompilerModuleBase
    {
        protected Bootstrapper bootstrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextCompilerModule"/> class.
        /// </summary>
        protected CompilerModuleBase(Bootstrapper bootstrapper)
        {
            Assume.NotNull(bootstrapper, nameof(bootstrapper));

            this.bootstrapper = bootstrapper;
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        public abstract DocumentResults? ExecuteQuery(String input, String text);
    }
}