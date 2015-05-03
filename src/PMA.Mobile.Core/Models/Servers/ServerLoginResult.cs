using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models.Servers
{
    public class ServerLoginResult
    {
        /// <summary>
        /// User Id used for authentication
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User password used for authentication
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Your public profile Id.
        /// </summary>
        public string UserProfileId { get; set; }
    }
}
