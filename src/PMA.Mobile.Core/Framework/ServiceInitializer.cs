using PMA.Mobile.Core.Interfaces.Core;
using PMA.Mobile.Core.Utility.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Framework
{
    public static class ServiceInitializer
    {

        public static async Task<IList<IApplicationService>> InitializeAppServices(IReflectionService reflectionService)
        {
            var types = reflectionService.GetAllTypesAssignableTo(typeof(IApplicationService));

            var services = IocHelper.GetAllInstancesOf<IApplicationService>(types);

            foreach (var service in services)
            {
                await service.Initialize();
            }

            return services;
        }
    }
}
