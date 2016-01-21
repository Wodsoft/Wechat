using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户消息。
    /// </summary>
    public abstract class UserMessage : Message, IUserMessage
    {
        /// <summary>
        /// 实例化用户消息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Id = long.Parse(dictionary["MsgId"]);
        }

        /// <summary>
        /// 获取消息Id。
        /// </summary>
        public long Id { get; private set; }
    }
}
