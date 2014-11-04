using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands
{
    public class IdentityCommand : CommandBase
    {
        private readonly IdentityRunnable _identityRunnable;

        public IdentityCommand(CommandDescriptor descriptor) : base(descriptor)
        {
            _identityRunnable = new IdentityRunnable();
        }

        public override IRunnable GetRunnable()
        {
            return _identityRunnable;
        }
    }
}