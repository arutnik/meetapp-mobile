using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Interfaces.Core
{
    public interface IReflectionService
    {
        /// <summary>
        /// Gets all types that can be assigned to the given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Type[] GetAllTypesAssignableTo(Type type);
    }
}
