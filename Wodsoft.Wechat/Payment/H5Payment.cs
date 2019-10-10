using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// H5支付交易。
    /// </summary>
    public class H5Payment : PaymentBase, IH5Payment
    {
        /// <summary>
        /// 重定向地址。
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}
