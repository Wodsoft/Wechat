using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public abstract class UserMessage : Message, IUserMessage
    {
        public UserMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Id = long.Parse(dictionary["MsgId"]);
        }

        public long Id { get; private set; }
    }
}
