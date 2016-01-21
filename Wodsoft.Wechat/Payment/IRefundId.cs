using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 退款单号。
    /// </summary>
    public interface IRefundId
    {
        /// <summary>
        /// 获取微信退款单号。
        /// </summary>
        string RefundId { get; }
    }
}
