using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserTextMessage : UserMessage, IUserTextMessage
    {
        public UserTextMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Content = dictionary["Content"];
        }

        public string Content { get; private set; }
    }
}
