using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 二维码。
    /// </summary>
    public interface IQrCode
    {
        /// <summary>
        /// 二维码标签。
        /// </summary>
        string Ticket { get; }

        /// <summary>
        /// 二维码过期时间。
        /// </summary>
        DateTime ExpiredDate { get; }

        /// <summary>
        /// 链接地址。
        /// </summary>
        string Url { get; }
    }
}
