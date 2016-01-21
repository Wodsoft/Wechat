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
    public interface IUserVoiceMessage : IUserMediaMessage
    {
        /// <summary>
        /// 获取语音格式。
        /// </summary>
        string Format { get; }

        /// <summary>
        /// 获取语音识别结果。
        /// </summary>
        string Recognition { get; }
    }
}
