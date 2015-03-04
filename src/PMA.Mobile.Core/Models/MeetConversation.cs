using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    public class MeetConversation
    {
        public Guid ConversationId { get; set;}

        public UserRef Poster { get; set; }

        public string PostText { get; set; }

        public DateTime PostTimeUtc { get; set; }

        
    }
}
