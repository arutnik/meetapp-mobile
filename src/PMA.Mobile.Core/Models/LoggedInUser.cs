using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    public class LoggedInUser : DataRecord
    {
        [PrimaryKey]
        public string SingleId { get; set; }

        /// <summary>
        /// Stores the user profile id of last logged in user.
        /// </summary>
        public string UserProfileId { get; set; }
        
        public LoggedInUser()
        {
            SingleId = "id";
        }

        public override object PrimaryKey
        {
            get { return SingleId; }
        }
    }
}
