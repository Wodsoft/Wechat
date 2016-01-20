using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wodsoft.Wechat.MPublic
{
    public class ReplyTextMessage : ReplyMessage
    {
        public ReplyTextMessage()
        {
            Type = "text";
        }

        public string Content { get; set; }

        protected override XElement[] GetResponseNode()
        {
            return new XElement[]{
                new XElement("Content", Content)
            };
        }
    }
}
