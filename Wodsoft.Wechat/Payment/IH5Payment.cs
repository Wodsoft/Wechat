using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// H5支付交易。
    /// </summary>
    public interface IH5Payment : IPayment
    {
        /// <summary>
        /// 重定向地址。
        /// </summary>
        string RedirectUrl { get; }
    }
}
