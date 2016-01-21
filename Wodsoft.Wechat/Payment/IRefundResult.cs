using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 退款结果。
    /// </summary>
    public interface IRefundResult
    {
        /// <summary>
        /// 获取退款单号。
        /// </summary>
        string RefundId { get; }

        /// <summary>
        /// 获取退款渠道。
        /// </summary>
        string RefundChannel { get; }

        /// <summary>
        /// 获取退款金额。
        /// </summary>
        int RefundFee { get; }

        /// <summary>
        /// 获取订单金额。
        /// </summary>
        int TotalFee { get; }

        /// <summary>
        /// 获取货币类型。
        /// </summary>
        string FeeCurrency { get; }

        /// <summary>
        /// 获取现金支付金额。
        /// </summary>
        int Cash { get; }

        /// <summary>
        /// 获取现金退款。
        /// </summary>
        int CashRefund { get; }

        /// <summary>
        /// 获取优惠券退款金额。
        /// </summary>
        int Coupon { get; }

        /// <summary>
        /// 获取优惠券数量。
        /// </summary>
        int CouponCount { get; }

        /// <summary>
        /// 获取优惠券Id。
        /// </summary>
        string CouponId { get; }
    }
}
