using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 微信交易号。
    /// </summary>
    public interface ITransactionId
    {
        /// <summary>
        /// 获取微信交易号。
        /// </summary>
        string TransactionId { get; }
    }
}
