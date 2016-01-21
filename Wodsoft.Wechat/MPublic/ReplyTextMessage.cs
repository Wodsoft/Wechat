using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 回复文本消息。
    /// </summary>
    public class ReplyTextMessage : ReplyMessage
    {
        /// <summary>
        /// 实例化回复文本消息。
        /// </summary>
        public ReplyTextMessage()
        {
            Type = "text";
        }

        /// <summary>
        /// 获取或设置文本内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取回复节点。
        /// </summary>
        /// <returns>返回XML Linq节点数组。</returns>
        protected override XElement[] GetResponseNode()
        {
            return new XElement[]{
                new XElement("Content", Content)
            };
        }
    }
}
