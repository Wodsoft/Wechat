using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public interface IOrderInfo : ITradeNumber
    {
        /// <summary>
        /// 获取交易标题。
        /// </summary>
        [Required]
        [MaxLength(32)]
        string Title { get; }

        /// <summary>
        /// 获取交易详情。
        /// </summary>
        [MaxLength(8192)]
        string Detail { get; }

        /// <summary>
        /// 获取交易注释。
        /// </summary>
        [MaxLength(127)]
        string Comment { get; }
        
        /// <summary>
        /// 获取交易货币类型。
        /// </summary>
        [MaxLength(16)]
        string Currency { get; }

        /// <summary>
        /// 获取交易金额。
        /// 以分为单位。
        /// </summary>
        [Required]
        int Fee { get; }

        /// <summary>
        /// 获取客户端IP。
        /// </summary>
        [Required]
        [MaxLength(16)]
        string UserIp { get; }

        /// <summary>
        /// 获取交易时间。
        /// </summary>
        DateTime? StartDate { get; }

        /// <summary>
        /// 获取支付过期时间。
        /// </summary>
        DateTime? ExpiredDate { get; }

        /// <summary>
        /// 获取交易标记。
        /// </summary>
        [MaxLength(32)]
        string Tag { get; }

        /// <summary>
        /// 获取设备信息。
        /// </summary>
        [MaxLength(32)]
        string Device { get; }
    }
}
