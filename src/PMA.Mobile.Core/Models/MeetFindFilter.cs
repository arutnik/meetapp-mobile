using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    public class MeetFindFilter
    {
        public string UserId { get; set; }

        public Interest[] WantedInterests { get; set; }

        public double MaxDistance { get; set; }

        public Gender[] WantedHostGenders { get; set; }

        public bool AllowFee { get; set; }

        public double StartTimeMinHoursAway { get; set; }

        public double StartTimeMaxHoursAway { get; set; }

        public ushort HostMinAge { get; set; }

        public ushort HostMaxAge { get; set; }

        public DistanceCalculationType MaxDistanceCalculationType { get; set; }
    }
}
