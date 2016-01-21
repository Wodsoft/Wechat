using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 微信优惠券。
    /// </summary>
    public class Coupon
    {
        /// <summary>
        /// 获取或设置优惠券金额。
        /// </summary>
        public int Fee { get; set; }

        /// <summary>
        /// 获取或设置优惠券批次。
        /// </summary>
        public string Batch { get; set; }
        
        /// <summary>
        /// 获取或设置优惠券Id。
        /// </summary>
        public string Id { get; set; }
    }
}
