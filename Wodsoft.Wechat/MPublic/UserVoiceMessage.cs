using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户语音消息。
    /// </summary>
    public class UserVoiceMessage : UserMediaMessage, IUserVoiceMessage
    {
        /// <summary>
        /// 实例化用户语音消息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserVoiceMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Format = dictionary["Format"];
            if (dictionary.ContainsKey("Recognition"))
                Recognition = dictionary["Recognition"];
        }

        /// <summary>
        /// 获取语音格式。
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// 获取语音识别结果。
        /// </summary>
        public string Recognition { get; private set; }
    }
}
