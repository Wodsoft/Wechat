using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户图片消息。
    /// </summary>
    public interface IUserImageMessage : IUserMediaMessage
    {
        /// <summary>
        /// 获取图片地址。
        /// </summary>
        string ImageUrl { get; }
    }
}
