using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    public interface ITradeNumber
    {
        /// <summary>
        /// 获取交易订单号。
        /// </summary>
        [Required]
        [MaxLength(32)]
        string TradeNo { get; }
    }
}
