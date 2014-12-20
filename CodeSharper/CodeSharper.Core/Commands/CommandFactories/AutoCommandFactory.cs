using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;

namespace CodeSharper.Core.Commands.CommandFactories
{
    public class AutoCommandFactory<TRunnable> : CommandFactoryBase
        where TRunnable : IRunnable
    {
        private struct PropertyInformation
        {
            public PropertyInfo PropertyInfo { get; set; }

            public BindToAttribute Attribute { get; set; }
        }

        private readonly TRunnable _runnable;
        private readonly PropertyInformation[] _properties;

        public AutoCommandFactory(Func<TRunnable> instantiation)
        {
            Constraints.NotNull(() => instantiation);

            _runnable = instantiation.Invoke();

            var type = typeof(TRunnable);
            _properties = type
                .GetProperties()
                .Where(prop => prop.CustomAttributes.Any(attr => attr.AttributeType == typeof(BindToAttribute)))
                .Select(prop => new PropertyInformation() {
                    PropertyInfo = prop,
                    Attribute = prop.GetCustomAttribute<BindToAttribute>()
                })
                .ToArray();
        }

        public AutoCommandFactory() : this(Activator.CreateInstance<TRunnable>)
        {
        }

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);

            foreach (var property in _properties)
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
