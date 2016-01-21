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
    public interface IUserMediaMessage : IUserMessage
    {
        /// <summary>
        /// 获取媒体Id。
        /// </summary>
        string MediaId { get; }
    }
}
