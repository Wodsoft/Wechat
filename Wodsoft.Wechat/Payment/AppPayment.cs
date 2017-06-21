using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.Wechat.Payment
{
    public class AppPayment : PaymentBase, IAppPayment
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
