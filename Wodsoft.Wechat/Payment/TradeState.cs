using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 交易状态。
    /// </summary>
    public enum TradeState
    {
        /// <summary>
        /// 已成功。
        /// </summary>
        SUCCESS,
        /// <summary>
        /// 已退款。
        /// </summary>
        REFUND,
        /// <summary>
        /// 未支付。
        /// </summary>
        NOTPAY,
        /// <summary>
        /// 已关闭。
        /// </summary>
        CLOSED,
        /// <summary>
        /// 已取消。
        /// </summary>
        REVOKED,
        /// <summary>
        /// 正在支付。
        /// </summary>
        USERPAYING,
        /// <summary>
        /// 支付错误。
        /// </summary>
        PAYERROR
    }
}
