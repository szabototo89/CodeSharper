using System.Text.RegularExpressions;

namespace CodeSharper.Core.Services
{
    public interface IServiceFactory
    {
        TService GetService<TService>() where TService : class;
    }
}