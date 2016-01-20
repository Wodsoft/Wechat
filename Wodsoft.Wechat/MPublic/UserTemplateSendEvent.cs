using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserTemplateSendEvent : UserEvent, IUserTemplateSendEvent
    {
        public UserTemplateSendEvent(IDictionary<string, string> dictionary) :
            base(dictionary)
        {
            MessageId = dictionary["MsgID"];
            Status = dictionary["Status"];
        }

        public string MessageId { get; set; }

        public string Status { get; set; }
    }
}
