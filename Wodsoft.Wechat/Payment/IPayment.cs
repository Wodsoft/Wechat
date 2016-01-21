using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 微信支付。
    /// </summary>
    public interface IPayment
    {
        /// <summary>
        /// 获取支付类型。
        /// </summary>
        string TradeType { get; }

        /// <summary>
        /// 获取预支付单号。
        /// </summary>
        string PrepayId { get; }
    }
}
