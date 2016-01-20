using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public interface IRefundInfo : ITradeNumber, ITransactionId
    {
        string RefundNo { get; }

        string Device { get; }

        int TotalFee { get; }

        int RefundFee { get; }

        string RefundCurrency { get; }

        string Operator { get; }
    }
}
