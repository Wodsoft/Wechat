using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户发送消息内容。
    /// </summary>
    public interface IUserMessage : IMessage
    {
        /// <summary>
        /// 获取消息Id。
        /// </summary>
        long Id { get; }
    }
}
