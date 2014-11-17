using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands.CommandFactories
{
    public class IdentityCommandFactory : CommandFactoryBase
    {
        private readonly IdentityRunnable _identityRunnable;

        public IdentityCommandFactory()
        {
            _identityRunnable = new IdentityRunnable();
        }

        protected override IRunnable CreateRunnable()
        {
            return _identityRunnable;
        }
    }
}