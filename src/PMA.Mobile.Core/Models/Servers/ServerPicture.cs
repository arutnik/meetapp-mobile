using System;
using Newtonsoft.Json;

namespace PMA.Mobile.Core.Models.Servers
{
	public class ServerPicture
	{
		public ServerPicture ()
		{
		}

		[JsonProperty("uri")]
		public string Uri { get; set; }


	}
}

