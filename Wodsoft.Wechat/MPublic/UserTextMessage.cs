using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户文本消息。
    /// </summary>
    public class UserTextMessage : UserMessage, IUserTextMessage
    {
        /// <summary>
        /// 实例化用户文本消息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserTextMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Content = dictionary["Content"];
        }

        /// <summary>
        /// 获取文本内容。
        /// </summary>
        public string Content { get; private set; }
    }
}
