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
    public class UserScanEvent : UserEvent, IUserScanEvent
    {
        /// <summary>
        /// 实例化用户扫描二维码事件。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserScanEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            EventKey = dictionary["EventKey"];
            Ticket = dictionary["Ticket"];
        }

        /// <summary>
        /// 获取二维码Key。
        /// </summary>
        public string EventKey { get; private set; }

        /// <summary>
        /// 获取二维码Ticket。
        /// </summary>
        public string Ticket { get; private set; }
    }
}
