using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public class PaymentBase : IPayment
    {
        public string TradeType { get; set; }

        public string PrepayId { get; set; }
        
        public string Nonce { get; set; }
    }
}
