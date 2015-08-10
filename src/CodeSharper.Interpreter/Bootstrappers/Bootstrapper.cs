using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ControlFlows;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Services;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Compiler;

namespace CodeSharper.Interpreter.Bootstrappers
{
    public class Bootstrapper
    {
        /// <summary>
        /// Gets or sets the runnable factory.
        /// </summary>
        public IRunnableFactory RunnableFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the command call resolver.
        /// </summary>
        public ICommandCallResolver CommandCallResolver { get; protected set; }

        /// <summary>
        /// Gets or sets the command descriptor manager.
        /// </summary>
        public ICommandDescriptorManager CommandDescriptorManager { get; protected set; }

        /// <summary>
        /// Gets or sets the selector factory.
        /// </summary>
        public ISelectorFactory SelectorFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the selector resolver.
        /// </summary>
        public ISelectorResolver SelectorResolver { get; protected set; }

        /// <summary>
        /// Gets or sets the runnable manager.
        /// </summary>
        public IRunnableManager RunnableManager { get; protected set; }

        /// <summary>
        /// Gets or sets the control flow factory.
        /// </summary>
        public IControlFlowFactory<ControlFlowBase> ControlFlowFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the executor.
        /// </summary>
        public IExecutor Executor { get; protected set; }

        /// <summary>
        /// Gets or sets the compiler.
        /// </summary>
        public CodeQueryCompiler Compiler { get; protected set; }

        /// <summary>
        /// Gets or sets the descriptor repository.
        /// </summary>
        public IDescriptorRepository DescriptorRepository { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        public Bootstrapper(IRunnableFactory runnableFactory, IDescriptorRepository descriptorRepository, Func<Bootstrapper, ICommandDescriptorManager> commandDescriptorManager = null, Func<Bootstrapper, ICommandCallResolver> commandResolver = null, Func<Bootstrapper, ISelectorFactory> selectorFactory = null, Func<Bootstrapper, ISelectorResolver> selectorResolver = null, Func<Bootstrapper, IRunnableManager> runnableManager = null, Func<Bootstrapper, IExecutor> executor = null, Func<Bootstrapper, IControlFlowFactory<ControlFlowBase>> controlFlowFactory = null)
        {
            Assume.NotNull(runnableFactory, nameof(runnableFactory));
            Assume.NotNull(descriptorRepository, nameof(descriptorRepository));

            RunnableFactory = runnableFactory;
            DescriptorRepository = descriptorRepository;
            CommandDescriptorManager = commandDescriptorManager.SafeInvoke(this) ?? new DefaultCommandDescriptorManager();
            CommandCallResolver = commandResolver.SafeInvoke(this) ?? new DefaultCommandCallResolver(descriptorRepository, RunnableFactory);
            SelectorFactory = selectorFactory.SafeInvoke(this) ?? new DefaultSelectorFactory();
            SelectorResolver = selectorResolver.SafeInvoke(this) ?? new DefaultSelectorResolver(SelectorFactory, DescriptorRepository);
            RunnableManager = runnableManager.SafeInvoke(this) ?? new DefaultRunnableManager();
            Executor = executor.SafeInvoke(this) ?? new StandardExecutor(RunnableManager);

            ControlFlowFactory = controlFlowFactory.SafeInvoke(this) ?? new DefaultControlFlowFactory(CommandCallResolver, SelectorResolver, Executor);
            Compiler = new CodeQueryCompiler();
        }
    }

}
