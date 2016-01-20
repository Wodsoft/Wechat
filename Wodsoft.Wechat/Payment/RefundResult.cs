using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public class RefundResult : IRefundResult
    {
        public string RefundId { get; set; }

        public string RefundChannel { get; set; }

        public int RefundFee { get; set; }

        public int TotalFee { get; set; }

        public string FeeCurrency { get; set; }

        public int Cash { get; set; }

        public int CashRefund { get; set; }

        public int Coupon { get; set; }

        public int CouponCount { get; set; }

        public string CouponId { get; set; }
    }
}
