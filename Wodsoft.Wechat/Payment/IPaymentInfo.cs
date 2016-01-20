using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public interface IPaymentInfo : IOpenId, ITradeNumber
    {
        string Device { get; }

        string Comment { get; }

        TradeState State { get; }

        string Bank { get; }

        int Fee { get; }

        string Currency { get; }

        int Cash { get; }

        string CashCurrency { get; }

        int Coupon { get; }

        int CouponCount { get; }

        string[] CouponBatch { get; }

        string[] CouponId { get; }

        int[] CouponEach { get; }

        string TransactionId { get; }

        DateTime CompletedDate { get; }

        string Description { get; }

        string Type { get; }

        bool IsSubscribe { get; }
    }
}
