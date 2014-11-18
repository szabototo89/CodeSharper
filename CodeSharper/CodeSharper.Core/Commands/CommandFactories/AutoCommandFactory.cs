using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands.CommandFactories
{
    public class AutoCommandFactory<TRunnable> : CommandFactoryBase
        where TRunnable : IRunnable
    {
        private TRunnable _runnable;

        public AutoCommandFactory()
        {
            _runnable = Activator.CreateInstance<TRunnable>();
        }

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);

            var type = typeof(TRunnable);
            var properties = type.GetProperties()
                                 .Where(prop => prop.CustomAttributes.Any(attr => attr.AttributeType == typeof(BindToAttribute)))
                                 .Select(prop => new {
                                     PropertyInfo = prop,
                                     Attribute = prop.GetCustomAttribute<BindToAttribute>()
                                 })
                                 .ToArray();

            foreach (var property in properties)
            {
                var name = property.Attribute.PropertyName;
                property.PropertyInfo.SetValue(_runnable, arguments.GetArgumentValue<Object>(name));
            }
        }

        protected override IRunnable CreateRunnable()
        {
            return _runnable;
        }
    }
}
