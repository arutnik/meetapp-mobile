using Cirrious.MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA.Mobile.Core.Events
{
    public class EventBase : MvxMessage
    {
        private static object s_defaultSender = new object();

        public EventBase()
            : base(s_defaultSender)
        {

        }

        public EventBase(object sender)
            : base(sender)
        {

        }
    }
}
