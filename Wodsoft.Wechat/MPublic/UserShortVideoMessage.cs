using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户短视频消息。
    /// </summary>
    public class UserShortVideoMessage : UserMediaMessage, IUserShortVideoMessage
    {
        /// <summary>
        /// 实例化用户短视频消息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserShortVideoMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            ThumbMediaId = dictionary["ThumbMediaId"];
        }

        /// <summary>
        /// 获取缩略媒体Id。
        /// </summary>
        public string ThumbMediaId { get; private set; }
    }
}
