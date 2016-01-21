using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 未知消息异常。
    /// </summary>
    public class UnknownMessageException : Exception
    {
        /// <summary>
        /// 实例化未知消息异常。
        /// </summary>
        /// <param name="msgType">消息类型。</param>
        public UnknownMessageException(string msgType)
        {
            MessageType = msgType;
        }

        /// <summary>
        /// 获取消息类型。
        /// </summary>
        public string MessageType { get; private set; }
    }
}
