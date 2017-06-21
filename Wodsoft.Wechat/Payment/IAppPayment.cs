using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// APP交易。
    /// </summary>
    public interface IAppPayment : IPayment
    {
        /// <summary>
        /// 获取随机码。
        /// </summary>
        string Nonce { get; }

        /// <summary>
        /// 获取时间戳。
        /// </summary>
        int TimeStamp { get; }

        /// <summary>
        /// 获取签名。
        /// </summary>
        string Signature { get; }
    }
}
