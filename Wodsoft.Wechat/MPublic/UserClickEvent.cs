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
    public class UserClickEvent : UserEvent, IUserClickEvent
    {
        /// <summary>
        /// 实例化用户点击事件。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserClickEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            EventKey = dictionary["EventKey"];
        }

        /// <summary>
        /// 事件关键字。
        /// </summary>
        public string EventKey { get; private set; }
    }
}
