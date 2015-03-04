using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    /// <summary>
    /// The Profile data for the user that is using
    /// the app locally.
    /// </summary>
    public class LocalUserProfile : UserProfileBase
    {
        public object UserLocation { get; set; }

        public Meet[] HostedMeets { get; set; }


    }
}
