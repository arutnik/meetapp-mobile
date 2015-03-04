using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    class MeetHubData
    {
        public AttendeeRequirement AttendeeRequirements { get; set; }

        public MeetAttendee[] Attendees { get; set; }

        public MeetAttendee[] BannedAttendees { get; set; }
    }
}
