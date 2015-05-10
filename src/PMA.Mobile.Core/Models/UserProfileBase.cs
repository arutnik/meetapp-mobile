using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    public class UserProfileBase : DataRecord
    {
        [PrimaryKey]
        public string UserProfileId { get; set; }

        public override object PrimaryKey
        {
            get { return UserProfileId; }
        }

        [Ignore]
        public PersonName RealName
        {
            get { return new PersonName(_FirstName, _LastName); }
            set { _FirstName = value.FirstName; _LastName = value.LastName; }
        }

        Interest[] _interests;
        [Ignore]
        public Interest[] Interests
        {
            get { return _interests; }
            set
            {
                _interests = value;
                _Interests = JsonConvert.SerializeObject(value.Select(s => s.Id).ToArray());
            }
        }

        Gender _gender;
        [Ignore]
        public Gender Gender
        {
            get { return _gender; }
            set { _gender = value; _GenderId = value.Id; }
        }

        public ushort Age { get; set; }

        [Ignore]
        public string[] UserPictureUris { get; set; }

        #region Serialization Properties

        public string _LastName { get; set; }

        public string _FirstName { get; set; }

        public string _Interests { get; set; }

        public string _GenderId { get; set;}

        #endregion
    }
}
