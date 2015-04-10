using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common.Runnables
{
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
        public virtual IRunnable Create(String runnableName, IEnumerable<KeyValuePair<String, Object>> actualArguments)
        {
            Assume.NotNull(runnableName, "runnableName");
            Assume.NotNull(actualArguments, "actualArguments");

            // instantiate runnable
            var runnableType = AvailableRunnables.FirstOrDefault(type => type.Name == runnableName);
            IRunnable runnable = null;

            var constructors = runnableType.GetConstructors();
            var defaultConstructor = constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 0);
            if (defaultConstructor != null)
            {
                runnable = defaultConstructor.Invoke(Enumerable.Empty<Object>().ToArray()) as IRunnable;
            }
            else
            { 
                throw new Exception("Cannot find default constructor of Runnable instance!");
            }

            // update properties via [BindTo] attribute
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

            return runnable;
        }
    }
}