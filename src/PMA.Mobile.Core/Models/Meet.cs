using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    /// <summary>
    /// This models the basic properties of a Meet. 
    /// </summary>
    public class Meet
    {
        /// <summary>
        /// The user that is hosting the Meet.
        /// </summary>
        public UserRef HostUser { get; set; }
    }
}
