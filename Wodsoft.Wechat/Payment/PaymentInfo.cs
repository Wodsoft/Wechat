using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public class PaymentInfo : IPaymentInfo
    {
        public string Device { get; set; }

        public string Comment { get; set; }

        public TradeState State { get; set; }

        public string Bank { get; set; }

        public int Cash { get; set; }

        public string CashCurrency { get; set; }

        public int Coupon { get; set; }

        public int CouponCount { get; set; }

        public string[] CouponBatch { get; set; }

        public string[] CouponId { get; set; }

        public int[] CouponEach { get; set; }

        public string TransactionId { get; set; }

        public DateTime CompletedDate { get; set; }

        public string Description { get; set; }

        public string OpenId { get; set; }

        public string Type { get; set; }

        public int Fee { get; set; }

        public string Currency { get; set; }

        public string TradeNo { get; set; }
        
        public bool IsSubscribe { get; set; }
    }
}
