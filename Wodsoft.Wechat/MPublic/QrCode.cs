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
    public class QrCode : IQrCode
    {
        /// <summary>
        /// 二维码标签。
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 二维码过期时间。
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// 链接地址。
        /// </summary>
        public string Url { get; set; }
    }
}
