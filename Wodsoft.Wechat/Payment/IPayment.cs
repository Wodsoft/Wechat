using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public interface IPayment
    {
        string TradeType { get; }

        string PrepayId { get; }
    }
}
