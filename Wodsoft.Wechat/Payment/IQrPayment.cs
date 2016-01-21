using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 二维码交易。
    /// </summary>
    public interface IQrPayment : IPayment
    {
        /// <summary>
        /// 获取二维码地址。
        /// </summary>
        string QrUrl { get; }
    }
}
