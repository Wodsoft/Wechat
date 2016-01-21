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
    public class UserSubscribeEvent : UserEvent, IUserSubscribeEvent
    {
        /// <summary>
        /// 实例化用户订阅事件。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserSubscribeEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            IsSubscribed = EventType == "subscribe";
        }

        /// <summary>
        /// 获取是否已订阅。
        /// </summary>
        public bool IsSubscribed { get; private set; }
    }
}
