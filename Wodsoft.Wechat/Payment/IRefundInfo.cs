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
    public interface IRefundInfo
    {
        /// <summary>
        /// 获取退款项。
        /// </summary>
        IRefundResult[] Items { get; }
    }
}
