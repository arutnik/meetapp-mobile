using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    public class MeetExtraDetails
    {
        public string[] AdditionalPictureURIs { get; set; }

        public string Description { get; set; }

        public ushort MaxCapacity { get; set; }

        /// <summary>
        /// Amount attending including the host
        /// </summary>
        public ushort CurrentAttendeeCount { get; set; }
    }
}
