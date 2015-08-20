using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Services;

namespace CodeSharper.Playground.GUI.Services
{
    public static class ServiceFactoryHelpers
    {
        public static SimpleServiceFactory RegisterInteractiveService(this SimpleServiceFactory serviceFactory, IInteractiveService interactiveService)
        {
            serviceFactory.RegisterService(interactiveService);
            return serviceFactory;
        }
    }
}