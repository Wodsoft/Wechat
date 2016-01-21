using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 微信退款商户订单号。
    /// </summary>
    public interface IRefundNumber
    {
        /// <summary>
        /// 获取商户退款单号。
        /// </summary>
        string RefundNo { get; }
    }
}
