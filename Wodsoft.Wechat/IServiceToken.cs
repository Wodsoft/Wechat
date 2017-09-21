using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 微信远程令牌。
    /// </summary>
    public interface IServiceToken : IToken
    {
        /// <summary>
        /// 获取过期时间。
        /// </summary>
        DateTime ExpiredDate { get; }

        /// <summary>
        /// 刷新令牌。
        /// </summary>
        /// <returns></returns>
        Task RefreshTokenAsync();
    }
}
