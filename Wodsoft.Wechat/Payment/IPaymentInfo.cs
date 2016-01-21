using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 交易信息。
    /// </summary>
    public interface IPaymentInfo : IOpenId, ITradeNumber
    {
        /// <summary>
        /// 获取设备信息。
        /// </summary>
        string Device { get; }

        /// <summary>
        /// 获取附加数据。
        /// </summary>
        string Comment { get; }

        /// <summary>
        /// 获取交易状态。
        /// </summary>
        TradeState State { get; }

        /// <summary>
        /// 获取付款银行。
        /// </summary>
        string Bank { get; }

        /// <summary>
        /// 获取交易金额。
        /// </summary>
        int Fee { get; }

        /// <summary>
        /// 获取交易货币类型。
        /// </summary>
        string Currency { get; }

        /// <summary>
        /// 获取现金金额。
        /// </summary>
        int Cash { get; }

        /// <summary>
        /// 获取现金货币类型。
        /// </summary>
        string CashCurrency { get; }

        /// <summary>
        /// 获取优惠券总支付金额。
        /// </summary>
        int Coupon { get; }

        /// <summary>
        /// 获取优惠券数量。
        /// </summary>
        int CouponCount { get; }

        /// <summary>
        /// 获取优惠券批次Id。
        /// </summary>
        string[] CouponBatch { get; }

        /// <summary>
        /// 获取优惠券Id。
        /// </summary>
        string[] CouponId { get; }

        /// <summary>
        /// 获取优惠券支付金额。
        /// </summary>
        int[] CouponEach { get; }

        /// <summary>
        /// 获取微信交易号。
        /// </summary>
        string TransactionId { get; }

        /// <summary>
        /// 获取支付完成时间。
        /// </summary>
        DateTime? CompletedDate { get; }

        /// <summary>
        /// 获取交易状态描述。
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 获取交易类型。
        /// </summary>
        string Type { get; }

        /// <summary>
        /// 获取是否已关注。
        /// </summary>
        bool IsSubscribe { get; }
    }
}
