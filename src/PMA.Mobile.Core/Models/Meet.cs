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

        public string Title { get; set; }

        public MeetStatus Status { get; set; }

        public DateTime StartTimeUtc { get; set; }

        public TimeSpan Length { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public object Location { get; set; }

        

        public double FeeMinimum { get; set; }

        public double FeeMaximum { get; set; }

        public Interest[] Interests { get; set; }

        public string MainPictureUri { get; set; }
    }
}
