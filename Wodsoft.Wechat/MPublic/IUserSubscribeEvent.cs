using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户订阅事件。
    /// </summary>
    public interface IUserSubscribeEvent : IUserEvent
    {
        /// <summary>
        /// 获取是否已订阅。
        /// </summary>
        bool IsSubscribed { get; }
    }
}
