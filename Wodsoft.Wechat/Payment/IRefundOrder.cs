using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 退款订单。
    /// </summary>
    public interface IRefundOrder : ITradeNumber, ITransactionId
    {
        /// <summary>
        /// 获取商户退款单号。
        /// </summary>
        string RefundNo { get; }

        /// <summary>
        /// 获取终端设备号。
        /// </summary>
        string Device { get; }

        /// <summary>
        /// 获取订单总金额。（单位为分。）
        /// </summary>
        int TotalFee { get; }

        /// <summary>
        /// 获取退款金额。
        /// </summary>
        int RefundFee { get; }

        /// <summary>
        /// 获取退款货币类型。
        /// </summary>
        string RefundCurrency { get; }

        /// <summary>
        /// 获取操作员。
        /// </summary>
        string Operator { get; }
    }
}
