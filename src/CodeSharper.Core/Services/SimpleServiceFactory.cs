using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Services
{
    public class SimpleServiceFactory : IServiceFactory
    {
        private readonly Dictionary<Type, Object> registeredServices;

        public SimpleServiceFactory()
        {
            this.registeredServices = new Dictionary<Type, Object>();
        }

        public void RegisterService<TService>(TService service) where TService : class
        {
            Assume.IsRequired(service, nameof(service));

            var serviceType = typeof (TService);
            if (registeredServices.ContainsKey(serviceType))
                throw new ArgumentException($"This service type has already been registered: {serviceType.FullName}.");

            registeredServices.Add(serviceType, service);
        }

        public TService GetService<TService>() where TService : class
        {
            if (!registeredServices.ContainsKey(typeof (TService)))
                throw new ArgumentException($"Service type ({typeof (TService).FullName}) is not registered.");

            return registeredServices[typeof (TService)] as TService;
        }
    }
}