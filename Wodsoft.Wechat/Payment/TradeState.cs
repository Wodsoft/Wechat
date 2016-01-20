using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public enum TradeState
    {
        SUCCESS,
        REFUND,
        NOTPAY,
        CLOSED,
        REVOKED,
        USERPAYING,
        PAYERROR
    }
}
