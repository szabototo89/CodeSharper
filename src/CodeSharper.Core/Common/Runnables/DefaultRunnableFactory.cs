﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.Common.NameMatchers;
using CodeSharper.Core.Common.Runnables.ValueConverters;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    public class DefaultRunnableFactory : IRunnableFactory
    {
        public IValueConverter ValueConverter { get; set; }

        /// <summary>
        /// Gets or sets the available runnables.
        /// </summary>
        public IEnumerable<Type> AvailableRunnables { get; protected set; }

        /// <summary>
        /// Gets or sets the name matcher.
        /// </summary>
        public INameMatcher RunnableNameMatcher { get; protected set; }

        /// <summary>
        /// Gets or sets the parameter name matcher.
        /// </summary>
        public INameMatcher ParameterNameMatcher { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRunnableFactory"/> class.
        /// </summary>
        public DefaultRunnableFactory(IEnumerable<Type> availableRunnables, IValueConverter valueConverter = null, INameMatcher runnableNameMatcher = null, INameMatcher parameterNameMatcher = null)
        {
            AvailableRunnables = availableRunnables ?? Enumerable.Empty<Type>();
            RunnableNameMatcher = runnableNameMatcher ?? new EqualityNameMatcher();
            ParameterNameMatcher = parameterNameMatcher ?? new EqualityNameMatcher();
            ValueConverter = valueConverter ?? new EmptyValueConverter();
        }

        /// <summary>
        /// Creates a runnable with the specified name and actual arguments
        /// </summary>
        public virtual IRunnable Create(String runnableName, IEnumerable<KeyValuePair<String, Object>> actualArguments)
        {
            Assume.NotNull(runnableName, "runnableName");
            Assume.NotNull(actualArguments, "actualArguments");

            // instantiate runnable
            var runnableType = AvailableRunnables.FirstOrDefault(type => RunnableNameMatcher.Match(type.Name, runnableName));
            if (runnableType == null)
            {
                throw new Exception(String.Format("Runnable ({0}) is not available!", runnableName));
            }

            IRunnable runnable = null;

            // get its default constructor
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

            // update properties via [Parameter] attribute
            var properties = runnableType.GetProperties()
                                         .Select(property => new {
                                             Property = property,
                                             BindTo = property.GetCustomAttributes(typeof(ParameterAttribute), true)
                                                              .SingleOrDefault() as ParameterAttribute
                                         })
                                         .Where(prop => prop.BindTo != null);

            foreach (var property in properties)
            {
                var argument = actualArguments.FirstOrDefault(arg => ParameterNameMatcher.Match(arg.Key, property.BindTo.PropertyName));

                var value = argument.Value;
                if (ValueConverter.CanConvert(value, property.Property.PropertyType))
                {
                    value = ValueConverter.Convert(value);
                }
                property.Property.SetValue(runnable, value);
            }

            return runnable;
        }
    }
}