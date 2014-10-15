using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common.Commands
{
    public class IdentityCommand : CommandBase
    {
        public override Argument Execute(Argument parameter)
        {
            return parameter;
        }
    }
}
