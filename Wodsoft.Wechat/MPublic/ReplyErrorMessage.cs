using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 回复错误消息。
    /// </summary>
    public class ReplyErrorMessage : IReplyMessage
    {
        private static byte[] _Success = Encoding.UTF8.GetBytes("error");

        /// <summary>
        /// 将消息写入流。
        /// </summary>
        /// <param name="stream">流。</param>
        public void WriteResponseText(Stream stream)
        {
            stream.Write(_Success, 0, _Success.Length);
        }
    }
}
