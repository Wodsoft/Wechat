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
    public interface IUserTextMessage : IUserMessage
    {
        /// <summary>
        /// 获取文本内容。
        /// </summary>
        string Content { get; }
    }
}
