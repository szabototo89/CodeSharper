using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Tests.Core.Common.Runnables
{
    public abstract class RunnableTestFixtureBase : TestFixtureBase
    {
        protected IRunnableManager runnableManager;
        protected IExecutor standardExecutor;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        public override void Setup()
        {
            base.Setup();
            runnableManager = new DefaultRunnableManager();
            standardExecutor = new StandardExecutor(runnableManager);
        }
    }

    public abstract class RunnableTestFixtureBase<TRunnable> : RunnableTestFixtureBase
        where TRunnable : IRunnable, new()
    {
        protected TRunnable underTest;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        public override void Setup()
        {
            base.Setup();
            underTest = new TRunnable();
        }
    }
}