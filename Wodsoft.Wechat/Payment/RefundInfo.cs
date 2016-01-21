using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 微信退款信息。
    /// </summary>
    public class RefundInfo : IRefundInfo
    {
        /// <summary>
        /// 获取或设置退款项。
        /// </summary>
        public IRefundResult[] Items { get; set; }
    }
}
