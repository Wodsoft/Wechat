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
    public abstract class UserEvent : Message, IUserEvent
    {
        /// <summary>
        /// 实例化用户事件信息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            EventType = dictionary["Event"];
        }

        /// <summary>
        /// 获取时间类型。
        /// </summary>
        public string EventType { get; private set; }
    }
}
