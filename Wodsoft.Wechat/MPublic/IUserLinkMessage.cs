using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户链接消息。
    /// </summary>
    public interface IUserLinkMessage : IUserMessage
    {
        /// <summary>
        /// 获取标题。
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 获取简介。
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 获取链接地址。
        /// </summary>
        string Url { get; }
    }
}
