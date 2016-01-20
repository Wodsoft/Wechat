using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public abstract class Message : IMessage
    {
        public Message(IDictionary<string, string> dictionary)
        {
            ToUser = dictionary["ToUserName"];
            FromUser = dictionary["FromUserName"];
            CreateDate = new DateTime(1970, 1, 1).AddSeconds(int.Parse(dictionary["CreateTime"])).ToLocalTime();
            Type = dictionary["MsgType"];
        }

        public string ToUser { get; private set; }

        public string FromUser { get; private set; }

        public DateTime CreateDate { get; private set; }
        
        public string Type { get; private set; }

        string IOpenId.OpenId { get { return FromUser; } }
    }
}
