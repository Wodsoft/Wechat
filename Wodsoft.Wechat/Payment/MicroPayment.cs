using System;
using System.Collections.Generic;
using System.Text;

namespace Wodsoft.Wechat.Payment
{
    public class MicroPayment : IMicroPayment
    {
        public TradeState State { get; set; }

        public string TransactionId { get; set; }

        public string TradeNo { get; set; }

        public string Message { get; set; }
    }
}
