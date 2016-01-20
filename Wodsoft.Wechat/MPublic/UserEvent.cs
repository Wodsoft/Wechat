using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public abstract class UserEvent : Message, IUserEvent
    {
        public UserEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            EventType = dictionary["Event"];
        }

        public string EventType { get; private set; }
    }
}
