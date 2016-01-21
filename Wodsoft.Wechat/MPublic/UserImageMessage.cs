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
    public class UserImageMessage : UserMediaMessage, IUserImageMessage
    {
        /// <summary>
        /// 实例化用户图片消息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserImageMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            ImageUrl = dictionary["PicUrl"];
        }

        /// <summary>
        /// 获取图片地址。
        /// </summary>
        public string ImageUrl { get; private set; }

    }
}
