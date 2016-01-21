using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 微信支付基类。
    /// </summary>
    public class PaymentBase : IPayment
    {
        /// <summary>
        /// 获取或设置支付类型。
        /// </summary>
        public string TradeType { get; set; }

        /// <summary>
        /// 获取或设置预支付单号。
        /// </summary>
        public string PrepayId { get; set; }
    }
}
