using PMA.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Interfaces.Data
{
    /// <summary>
    /// Stores and retrieves data records locally on device
    /// </summary>
    /// <remarks>
    /// Assume that calls to this will be blocking.
    /// Calls to this are thread safe.
    /// </remarks>
    public interface ILocalData
    {
        T FindOne<T>(Expression<Func<T, bool>> predicate) where T: DataRecord, new();

        IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : DataRecord, new();

        IEnumerable<T> All<T>() where T : DataRecord, new();

        void Save<T>(IEnumerable<T> items) where T : DataRecord, new();

        void Remove<T>(IEnumerable<T> items) where T : DataRecord, new();

        void RemoveAll<T>() where T : DataRecord, new();
    }
}
