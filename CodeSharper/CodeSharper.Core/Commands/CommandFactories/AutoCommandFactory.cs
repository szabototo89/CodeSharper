using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands.CommandFactories
{
    public class AutoCommandFactory<TRunnable> : CommandFactoryBase
        where TRunnable : IRunnable
    {
        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            var type = typeof(TRunnable);
            var properties = type.GetProperties()
                                 .Where(prop => prop.CustomAttributes.Any(attr => attr.AttributeType == typeof(BindToAttribute)))
                                 .ToArray();



            base.MapArguments(arguments);
        }

        protected override IRunnable CreateRunnable()
        {
            throw new NotImplementedException();
        }
    }
}
