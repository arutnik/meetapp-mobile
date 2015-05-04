using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;

namespace PMA.Mobile.Core.Utility.Reflection
{
    public static class IocHelper
    {
        /// <summary>
        /// Gets all classes that implement the given interfaces.
        /// Those that have been registed as singleton on at least one interface
        /// will have their singleton returned. Otherwise new instances will
        /// be created.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="createableTypes">Types that are creatable</param>
        /// <returns></returns>
        public static IList<T> GetAllInstancesOf<T>(IEnumerable<Type> createableTypes)
        {
            var typeInfo = typeof(T).GetTypeInfo();

            var types = createableTypes.Select(t => t.GetTypeInfo())
                .Where(t => typeInfo.IsAssignableFrom(t))
                ;

            List<T> results = new List<T>();

            foreach (var type in types)
            {
                object impl = null;

                foreach (var serviceInterface in type.ImplementedInterfaces)
                {
                    if (Mvx.TryResolve(serviceInterface, out impl))
                    {
                        break;
                    }
                }

                if (impl == null)
                {
                    impl = Mvx.IocConstruct(type.AsType());
                }

                if (impl is T)
                {
                    results.Add((T)impl);
                }
            }

            return results;
        }

        /// <summary>
        /// Registers the implementing a type as a singleton
        /// for all the given interfaces - ensuring that only a
        /// single instance of the service type will be created.
        /// </summary>
        /// <param name="interfaceTypes">The interface types implemented by the service</param>
        /// <param name="serviceType">The concrete service type</param>
        public static void LazyConstructAndRegisterSingletonForAll(List<Type> interfaceTypes, Type serviceType)
        {
            var pair = new MvxTypeExtensions.ServiceTypeAndImplementationTypePair(interfaceTypes, serviceType);
            MvxTypeExtensions.RegisterAsLazySingleton(new[] { pair });
        }
    }
}
