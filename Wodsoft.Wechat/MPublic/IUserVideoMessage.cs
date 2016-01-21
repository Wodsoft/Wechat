using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户视频消息。
    /// </summary>
    public interface IUserVideoMessage : IUserMediaMessage
    {
        /// <summary>
        /// 获取缩略媒体Id。
        /// </summary>
        string ThumbMediaId { get; }
    }
}
