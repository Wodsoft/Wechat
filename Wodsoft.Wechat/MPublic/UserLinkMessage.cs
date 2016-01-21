using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户链接消息。
    /// </summary>
    public class UserLinkMessage : UserMessage, IUserLinkMessage
    {
        /// <summary>
        /// 实例化用户链接消息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserLinkMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Title = dictionary["Title"];
            Description = dictionary["Description"];
            Url = dictionary["Url"];
        }

        /// <summary>
        /// 获取标题。
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 获取简介。
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 获取链接地址。
        /// </summary>
        public string Url { get; private set; }
    }
}
