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
        /// 获取或设置商户退款单号。
        /// </summary>
        public string RefundNo { get; set; }

        /// <summary>
        /// 获取或设置退款金额。
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>
        /// 获取或设置现金退款。
        /// </summary>
        public int RefundCash { get; set; }

        /// <summary>
        /// 获取或设置优惠券退款金额。
        /// </summary>
        public int Coupon { get; set; }

        /// <summary>
        /// 获取或设置优惠券退款项。
        /// </summary>
        public Coupon[] CouponItems { get; set; }

        /// <summary>
        /// 获取或设置退款状态。
        /// </summary>
        public RefundStatus Status { get; set; }

        /// <summary>
        /// 获取或设置退款账户。
        /// </summary>
        public string Account { get; set; }
    }

    /// <summary>
    /// 微信退款状态。
    /// </summary>
    public enum RefundStatus
    {
        /// <summary>
        /// 成功。
        /// </summary>
        SUCCESS,
        /// <summary>
        /// 失败。
        /// </summary>
        FAIL,
        /// <summary>
        /// 处理中。
        /// </summary>
        PROCESSING,
        /// <summary>
        /// 未确定。（需要商户退款单重新发起退款）
        /// </summary>
        NOTSURE,
        /// <summary>
        /// 转入代发。
        /// </summary>
        CHANGE
    }
}
