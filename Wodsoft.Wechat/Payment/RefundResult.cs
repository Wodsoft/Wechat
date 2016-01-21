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
    public class RefundResult : IRefundResult
    {
        /// <summary>
        /// 获取或设置退款单号。
        /// </summary>
        public string RefundId { get; set; }

        /// <summary>
        /// 获取或设置退款渠道。
        /// </summary>
        public string RefundChannel { get; set; }

        /// <summary>
        /// 获取或设置退款金额。
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>
        /// 获取或设置订单金额。
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 获取或设置货币类型。
        /// </summary>
        public string FeeCurrency { get; set; }

        /// <summary>
        /// 获取或设置现金支付金额。
        /// </summary>
        public int Cash { get; set; }

        /// <summary>
        /// 获取或设置现金退款。
        /// </summary>
        public int CashRefund { get; set; }

        /// <summary>
        /// 获取或设置优惠券退款金额。
        /// </summary>
        public int Coupon { get; set; }

        /// <summary>
        /// 获取或设置优惠券数量。
        /// </summary>
        public int CouponCount { get; set; }

        /// <summary>
        /// 获取或设置优惠券Id。
        /// </summary>
        public string CouponId { get; set; }
    }
}
