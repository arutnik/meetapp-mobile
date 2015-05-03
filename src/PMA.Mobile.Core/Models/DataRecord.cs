using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    /// <summary>
    /// Base class for objects that can be saved locally.
    /// </summary>
    public abstract class DataRecord
    {
        public abstract object PrimaryKey
        { get; }
    }
}
