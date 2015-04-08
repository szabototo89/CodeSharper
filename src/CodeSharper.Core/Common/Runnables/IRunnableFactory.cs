using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    public interface IRunnableFactory
    {
        /// <summary>
        /// Creates a runnable with the specified name and actual arguments
        /// </summary>
        IRunnable Create(String runnableName, IEnumerable<KeyValuePair<String, Object>> actualArguments);
    }

    public class DefaultRunnableFactory : IRunnableFactory
    {
        /// <summary>
        /// Gets or sets the available runnables.
        /// </summary>
        public IEnumerable<Type> AvailableRunnables { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRunnableFactory"/> class.
        /// </summary>
        public DefaultRunnableFactory(IEnumerable<Type> availableRunnables)
        {
            AvailableRunnables = availableRunnables ?? Enumerable.Empty<Type>();
        }

        /// <summary>
        /// Creates a runnable with the specified name and actual arguments
        /// </summary>
        public IRunnable Create(String runnableName, IEnumerable<KeyValuePair<String, Object>> actualArguments)
        {
            Assume.NotNull(runnableName, "runnableName");
            Assume.NotNull(actualArguments, "actualArguments");

            // instantiate runnable
            var runnableType = AvailableRunnables.FirstOrDefault(type => type.Name == runnableName);
            IRunnable runnable = null;

            var constructor = runnableType.GetConstructor(null);
            if (constructor != null)
            {
                runnable = constructor.Invoke(Enumerable.Empty<Object>().ToArray()) as IRunnable;
            }

            // update properties via BindTo attribute
            var properties = runnableType
                                .GetProperties()
                                .Select(prop => new {
                                    Property = prop,
                                    BindTo = prop.GetCustomAttributes(typeof(BindToAttribute), true).SingleOrDefault() as BindToAttribute
                                })
                                .Where(prop => prop.BindTo != null);

            foreach (var property in properties)
            {
                var argument = actualArguments.FirstOrDefault(arg => arg.Key == property.BindTo.PropertyName);
                property.Property.SetValue(runnable, argument.Value);
            }

            return null;
        }
    }


    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class BindToAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public String PropertyName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindToAttribute"/> class.
        /// </summary>
        public BindToAttribute(String propertyName)
        {
            PropertyName = propertyName;
        }
    }
}