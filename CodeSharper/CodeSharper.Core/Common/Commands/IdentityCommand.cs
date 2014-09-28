using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Commands
{
    public class IdentityCommand : ICommand
    {
        public Argument Execute(Argument parameter)
        {
            return parameter;
        }
    }
}
