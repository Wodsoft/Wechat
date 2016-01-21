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
    public class JavascriptConfig : IJavascriptConfig
    {
        /// <summary>
        /// 获取随机码。
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 获取时间戳。
        /// </summary>
        public int TimeStamp { get; set; }

        /// <summary>
        /// 获取签名。
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 获取页面地址。
        /// </summary>
        public string Url { get; set; }
    }
}
