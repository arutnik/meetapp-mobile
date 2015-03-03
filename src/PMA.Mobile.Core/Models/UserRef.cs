using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    /// <summary>
    /// A lightweight reference to a user.
    /// </summary>
    public class UserRef
    {
        /// <summary>
        /// The User's ID. TODO: type as guid?
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// URI to a thumbnail for the user's display picture.
        /// </summary>
        public string ThumbnailUri { get; set; }

        /// <summary>
        /// The name to display for the user.
        /// </summary>
        public PersonName DisplayName { get; set; }
    }
}
