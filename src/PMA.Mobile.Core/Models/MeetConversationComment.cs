using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Models
{
    public class MeetConversationComment
    {
        public Guid ConversationId { get; set; }

        public UserRef Commenter { get; set; }

        public DateTime CommentTimeUtc { get; set; }

        public string CommentText { get; set; }
    }
}
