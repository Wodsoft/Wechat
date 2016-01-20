using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public interface IRefundResult
    {
        string RefundId { get; }

        string RefundChannel { get; }

        int RefundFee { get; }

        int TotalFee { get; }

        string FeeCurrency { get; }

        int Cash { get; }

        int CashRefund { get; }

        int Coupon { get; }

        int CouponCount { get; }

        string CouponId { get; }
    }
}
