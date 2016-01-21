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
    public class UserTemplateSendEvent : UserEvent, IUserTemplateSendEvent
    {
        /// <summary>
        /// 实例化用户模板消息事件。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserTemplateSendEvent(IDictionary<string, string> dictionary) :
            base(dictionary)
        {
            MessageId = dictionary["MsgID"];
            Status = dictionary["Status"];
        }

        /// <summary>
        /// 获取消息Id。
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 获取状态。
        /// </summary>
        public string Status { get; set; }
    }
}
