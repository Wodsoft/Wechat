using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserScanEvent : UserEvent, IUserScanEvent
    {
        public UserScanEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            EventKey = dictionary["EventKey"];
            Ticket = dictionary["Ticket"];
        }

        public string EventKey { get; private set; }

        public string Ticket { get; private set; }
    }
}
