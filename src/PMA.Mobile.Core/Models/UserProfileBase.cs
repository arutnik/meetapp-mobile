using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    public class UserProfileBase
    {
        /// <summary>
        /// TBD: Type
        /// </summary>
        public string UserId { get; set; }

        public PersonName DisplayName { get; set; }

        public Interest[] Interests { get; set; }

        public Gender Gender { get; set; }

        public ushort Age { get; set; }

        public string[] UserPictureUris { get; set; }
    }
}
