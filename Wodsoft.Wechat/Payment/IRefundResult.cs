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
    public interface IRefundResult : IRefundId, IRefundNumber
    {
        /// <summary>
        /// 获取退款渠道。
        /// </summary>
        string RefundChannel { get; }

        /// <summary>
        /// 获取退款金额。
        /// </summary>
        int RefundFee { get; }
        
        /// <summary>
        /// 获取现金退款。
        /// </summary>
        int RefundCash { get; }

        /// <summary>
        /// 获取优惠券退款金额。
        /// </summary>
        int Coupon { get; }

        /// <summary>
        /// 获取优惠券退款项。
        /// </summary>
        Coupon[] CouponItems { get; }

        /// <summary>
        /// 获取退款状态。
        /// </summary>
        RefundStatus Status { get; }

        /// <summary>
        /// 获取退款账户。
        /// </summary>
        string Account { get; }
    }
}
