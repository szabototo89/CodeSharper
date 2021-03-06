﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.Common.NameMatchers;
using CodeSharper.Core.Common.Runnables.ValueConverters;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Services;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.Runnables
{
    public class DefaultRunnableFactory : IRunnableFactory
    {
        /// <summary>
        /// Gets or sets the value converter.
        /// </summary>
        public IValueConverter ValueConverter { get; }

        /// <summary>
        /// Gets or sets the service factory.
        /// </summary>
        public IServiceFactory ServiceFactory { get; }

        /// <summary>
        /// Gets or sets the available runnables.
        /// </summary>
        public IEnumerable<Type> AvailableRunnables { get; }

        /// <summary>
        /// Gets or sets the name matcher.
        /// </summary>
        public INameMatcher RunnableNameMatcher { get; }

        /// <summary>
        /// Gets or sets the parameter name matcher.
        /// </summary>
        public INameMatcher ParameterNameMatcher { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRunnableFactory"/> class.
        /// </summary>
        public DefaultRunnableFactory(IEnumerable<Type> availableRunnables, IValueConverter valueConverter = null, INameMatcher runnableNameMatcher = null, INameMatcher parameterNameMatcher = null,
                                      IServiceFactory serviceFactory = null)
        {
            AvailableRunnables = availableRunnables ?? Enumerable.Empty<Type>();
            RunnableNameMatcher = runnableNameMatcher ?? new EqualityNameMatcher();
            ParameterNameMatcher = parameterNameMatcher ?? new EqualityNameMatcher();
            ValueConverter = valueConverter ?? new EmptyValueConverter();
            ServiceFactory = serviceFactory;
        }

        /// <summary>
        /// Creates a runnable with the specified name and actual arguments
        /// </summary>
        public virtual IRunnable Create(String runnableName, IEnumerable<KeyValuePair<String, Object>> actualArguments)
        {
            Assume.NotNull(runnableName, nameof(runnableName));
            Assume.NotNull(actualArguments, nameof(actualArguments));

            // instantiate runnable
            var runnableType = AvailableRunnables.FirstOrDefault(type => RunnableNameMatcher.Match(type.Name, runnableName));
            if (runnableType == null)
                throw new Exception($"Runnable ({runnableName}) is not available!");

            IRunnable runnable = null;

            // get its default constructor
            var constructors = runnableType.GetConstructors();
            var defaultConstructor = constructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 0);
            if (defaultConstructor != null)
                runnable = defaultConstructor.Invoke(Enumerable.Empty<Object>().ToArray()) as IRunnable;
            if (ServiceFactory != null)
            {
                // instantiate with service factory
                var constructorWithService = constructors.FirstOrDefault(constructor => {
                    var parameters = constructor.GetParameters();
                    if (parameters.Length != 1)
                        return false;

                    var isServiceFactory = typeof (IServiceFactory).IsAssignableFrom(parameters[0].ParameterType);

                    return isServiceFactory;
                });

                if (constructorWithService != null)
                    runnable = constructorWithService.Invoke(new[] {ServiceFactory}) as IRunnable;
            }

            if (runnable == null)
                throw new Exception("Cannot find proper constructor of Runnable instance!");

            // update properties via [Parameter] attribute
            var properties = runnableType.GetProperties()
                                         .Select(property => new
                                         {
                                             Property = property,
                                             BindTo = property.GetCustomAttributes(typeof (ParameterAttribute), true)
                                                              .SingleOrDefault() as ParameterAttribute
                                         })
                                         .Where(prop => prop.BindTo != null);

            foreach (var property in properties)
            {
                var argument = actualArguments.FirstOrDefault(arg => ParameterNameMatcher.Match(arg.Key, property.BindTo.PropertyName));

                var value = argument.Value;
                if (ValueConverter.CanConvert(value, property.Property.PropertyType))
                    value = ValueConverter.Convert(value);
                property.Property.SetValue(runnable, value);
            }

            return runnable;
        }
    }
}