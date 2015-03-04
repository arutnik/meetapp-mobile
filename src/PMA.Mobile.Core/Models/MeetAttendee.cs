using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    public class MeetAttendee
    {
        public UserRef User { get; set; }

        public MeetAttendeeType Type { get; set; }
    }
}
