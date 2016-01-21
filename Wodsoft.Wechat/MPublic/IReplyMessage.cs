using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 被动回复消息。
    /// </summary>
    public interface IReplyMessage
    {
        /// <summary>
        /// 将消息写入流。
        /// </summary>
        /// <param name="stream">流。</param>
        void WriteResponseText(Stream stream);
    }
}
