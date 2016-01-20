using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户扫描二维码事件。
    /// </summary>
    public interface IUserScanEvent : IUserEvent
    {
        /// <summary>
        /// 获取二维码Key。
        /// </summary>
        string EventKey { get; }

        /// <summary>
        /// 获取二维码Ticket。
        /// </summary>
        string Ticket { get; }
    }
}
