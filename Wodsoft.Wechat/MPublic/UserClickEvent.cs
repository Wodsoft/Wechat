using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserClickEvent : UserEvent, IUserClickEvent
    {
        public UserClickEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            EventKey = dictionary["EventKey"];
        }

        public string EventKey { get; private set; }
    }
}
