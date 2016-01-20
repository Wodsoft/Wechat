using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户事件信息。
    /// </summary>
    public interface IUserEvent : IMessage
    {
        /// <summary>
        /// 获取时间类型。
        /// </summary>
        string EventType { get; }
    }
}
