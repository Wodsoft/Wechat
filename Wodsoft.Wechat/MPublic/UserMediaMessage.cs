using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户媒体消息。
    /// </summary>
    public abstract class UserMediaMessage : UserMessage, IUserMediaMessage
    {
        /// <summary>
        /// 实例化用户媒体消息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserMediaMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            MediaId = dictionary["MediaId"];
        }

        /// <summary>
        /// 获取媒体Id。
        /// </summary>
        public string MediaId { get; private set; }
    }
}
