using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户点击事件。
    /// </summary>
    public interface IUserClickEvent : IUserEvent
    {
        /// <summary>
        /// 获取事件Key值。
        /// </summary>
        string EventKey { get; }
    }
}
