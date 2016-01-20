using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信消息。
    /// </summary>
    public interface IMessage : IOpenId
    {
        /// <summary>
        /// 获取接收方。
        /// </summary>
        string ToUser { get; }

        /// <summary>
        /// 获取发送方。
        /// </summary>
        string FromUser { get; }

        /// <summary>
        /// 获取消息创建时间。
        /// </summary>
        DateTime CreateDate { get; }

        /// <summary>
        /// 获取消息类型。
        /// </summary>
        string Type { get; }
    }
}
