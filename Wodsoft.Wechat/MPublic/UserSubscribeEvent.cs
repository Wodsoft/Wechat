using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserSubscribeEvent : UserEvent, IUserSubscribeEvent
    {
        public UserSubscribeEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            IsSubscribed = EventType == "subscribe";
        }

        public bool IsSubscribed { get; private set; }
    }
}
