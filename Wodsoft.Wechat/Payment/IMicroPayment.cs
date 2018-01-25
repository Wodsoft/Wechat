using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 刷卡支付交易。
    /// </summary>
    public interface IMicroPayment : ITransactionId, ITradeNumber
    {
        /// <summary>
        /// 交易状态。
        /// </summary>
        TradeState State { get; set; }

        /// <summary>
        /// 结果信息。
        /// </summary>
        string Message { get; set; }
    }
}
