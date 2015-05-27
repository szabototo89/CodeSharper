using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;

namespace CodeSharper.Core.Commands
{
    public interface ICommandCall : IHasChildren<ICommandCall>
    {
        
    }
}