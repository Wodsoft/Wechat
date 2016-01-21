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
    public class PaymentInfo : IPaymentInfo
    {
        /// <summary>
        /// 获取设备信息。
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// 获取附加数据。
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 获取交易状态。
        /// </summary>
        public TradeState State { get; set; }

        /// <summary>
        /// 获取付款银行。
        /// </summary>
        public string Bank { get; set; }

        /// <summary>
        /// 获取交易金额。
        /// </summary>
        public int Fee { get; set; }

        /// <summary>
        /// 获取交易货币类型。
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 获取现金金额。
        /// </summary>
        public int Cash { get; set; }

        /// <summary>
        /// 获取现金货币类型。
        /// </summary>
        public string CashCurrency { get; set; }

        /// <summary>
        /// 获取优惠券总支付金额。
        /// </summary>
        public int Coupon { get; set; }

        /// <summary>
        /// 获取优惠券数量。
        /// </summary>
        public int CouponCount { get; set; }

        /// <summary>
        /// 获取优惠券批次Id。
        /// </summary>
        public string[] CouponBatch { get; set; }

        /// <summary>
        /// 获取优惠券Id。
        /// </summary>
        public string[] CouponId { get; set; }

        /// <summary>
        /// 获取优惠券支付金额。
        /// </summary>
        public int[] CouponEach { get; set; }

        /// <summary>
        /// 获取微信交易号。
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 获取支付完成时间。
        /// </summary>
        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// 获取交易状态描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取交易类型。
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 获取是否已关注。
        /// </summary>
        public bool IsSubscribe { get; set; }

        /// <summary>
        /// 获取或设置用户OpenId。
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 获取或设置商户订单号。
        /// </summary>
        public string TradeNo { get; set; }
    }
}
