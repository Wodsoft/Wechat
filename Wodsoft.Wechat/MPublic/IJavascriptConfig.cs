using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// JS SDK配置。
    /// </summary>
    public interface IJavascriptConfig
    {
        /// <summary>
        /// 获取随机码。
        /// </summary>
        string Nonce { get; }

        /// <summary>
        /// 获取时间戳。
        /// </summary>
        int TimeStamp { get; }

        /// <summary>
        /// 获取签名。
        /// </summary>
        string Signature { get; }

        /// <summary>
        /// 获取页面地址。
        /// </summary>
        string Url { get; }
    }
}
