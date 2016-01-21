using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信消息。
    /// </summary>
    public abstract class Message : IMessage
    {
        /// <summary>
        /// 实例化微信消息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public Message(IDictionary<string, string> dictionary)
        {
            ToUser = dictionary["ToUserName"];
            FromUser = dictionary["FromUserName"];
            CreateDate = new DateTime(1970, 1, 1).AddSeconds(int.Parse(dictionary["CreateTime"])).ToLocalTime();
            Type = dictionary["MsgType"];
        }

        /// <summary>
        /// 获取接收方。
        /// </summary>
        public string ToUser { get; private set; }

        /// <summary>
        /// 获取发送方。
        /// </summary>
        public string FromUser { get; private set; }


        /// <summary>
        /// 获取消息创建时间。
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// 获取消息类型。
        /// </summary>
        public string Type { get; private set; }

        string IOpenId.OpenId { get { return FromUser; } }
    }
}
