using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models.Servers
{
    public class ServerUser
    {
        [JsonProperty("realName")]
        public ServerName RealName { get; set; }

        [JsonProperty("dob")]
        public DateTime Dob { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

		[JsonProperty("pictures")]
		public ServerPicture[] Pictures { get; set; }

        [JsonProperty("searchCriteria")]
        public ServerSearchCriteria SearchCriteria { get; set; }

		public ServerUser()
		{
			Pictures = new ServerPicture[0];
		}
    }

    public class ServerName
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }

    public class ServerSearchCriteria
    {
        [JsonProperty("minHostAge")]
        public int MinHostAge { get; set; }

        [JsonProperty("maxHostAge")]
        public int MaxHostAge { get; set; }

        [JsonProperty("maxDistanceKm")]
        public double MaxDistanceKm { get; set; }

        [JsonProperty("matchesInterests")]
        public string[] MatchingInterests { get; set; }

        [JsonProperty("acHostGenders")]
        public string[] AcceptableHostGenders { get; set; }

        [JsonProperty("minHoursToEventStart")]
        public int MinHoursToEventStart { get; set; }

        [JsonProperty("maxHoursToEventStart")]
        public int MaxHoursToEventStart { get; set; }
    }
}
