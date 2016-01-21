using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户模板消息事件。
    /// </summary>
    public interface IUserTemplateSendEvent : IUserEvent
    {
        /// <summary>
        /// 获取消息Id。
        /// </summary>
        string MessageId { get; }

        /// <summary>
        /// 获取状态。
        /// </summary>
        string Status { get; }
    }
}
