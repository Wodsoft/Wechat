using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 回复消息。
    /// </summary>
    public abstract class ReplyMessage : IReplyMessage, IMessage
    {
        public ReplyMessage()
        {
            CreateDate = DateTime.Now;
        }

        public void WriteResponseText(Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            XDocument doc = new XDocument();
            var root = new XElement("xml");

            root.Add(new XElement("ToUserName", ToUser));
            root.Add(new XElement("FromUserName", FromUser));
            root.Add(new XElement("CreateTime", (int)(CreateDate.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds));
            root.Add(new XElement("MsgType", Type));

            var nodes = GetResponseNode();
            if (nodes != null)
                root.Add(nodes);
            doc.Add(root);
            doc.Save(writer);
        }

        protected virtual XElement[] GetResponseNode()
        {
            return null;
        }

        /// <summary>
        /// 获取或设置接收用户OpenId。
        /// </summary>
        public string ToUser { get; set; }

        /// <summary>
        /// 获取或设置发送公众号。
        /// </summary>
        public string FromUser { get; set; }

        /// <summary>
        /// 获取或设置创建时间。
        /// </summary>
        public DateTime CreateDate { get; protected set; }

        public string Type { get; protected set; }

        string IOpenId.OpenId { get { return ToUser; } }
    }
}
