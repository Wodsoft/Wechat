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
    public class QrPayment : PaymentBase, IQrPayment
    {
        /// <summary>
        /// 获取或设置二维码地址。
        /// </summary>
        public string QrUrl { get; set; }
    }
}
