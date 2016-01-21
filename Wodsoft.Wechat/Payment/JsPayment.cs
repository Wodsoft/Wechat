using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 内置浏览器交易。
    /// </summary>
    public class JsPayment : PaymentBase, IJsPayment
    {
        /// <summary>
        /// 获取或设置随机码。
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// 获取或设置时间戳。
        /// </summary>
        public int TimeStamp { get; set; }

        /// <summary>
        /// 获取或设置签名。
        /// </summary>
        public string Signature { get; set; }
    }
}
