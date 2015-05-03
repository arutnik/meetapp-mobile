using PMA.Mobile.Core.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore.IoC;

namespace PMA.Mobile.Core.Services.Core
{
    public class ReflectionService : IReflectionService
    {
        private IEnumerable<Assembly> Assemblies
        {
            get { return new[] { GetType().GetTypeInfo().Assembly }; }
        }

        public Type[] GetAllTypesAssignableTo(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return Assemblies
                .SelectMany(s => s.CreatableTypes())
                .Where(t => typeInfo.IsAssignableFrom(t.GetTypeInfo()))
                .ToArray();
        }
    }
}
