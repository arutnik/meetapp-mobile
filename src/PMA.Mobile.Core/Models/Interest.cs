using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    /// <summary>
    /// Models an interest.
    /// </summary>
    public class Interest : DataRecord
    {
        public string Id { get; set; }

        public override object PrimaryKey
        {
            get { return Id; }
        }
    }
}
