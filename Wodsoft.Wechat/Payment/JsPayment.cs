using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public class JsPayment : PaymentBase, IJsPayment
    {
        public int TimeStamp { get; set; }

        public string Signature { get; set; }
    }
}
