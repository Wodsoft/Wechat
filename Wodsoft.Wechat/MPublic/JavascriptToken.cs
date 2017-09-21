using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// JS SDK令牌。
    /// </summary>
    public class JavascriptToken : IServiceToken
    {
        /// <summary>
        /// 获取过期时间。
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// 获取令牌。
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 刷新令牌。
        /// </summary>
        /// <returns></returns>
        public Task RefreshTokenAsync()
        {
            throw new NotSupportedException();
        }
    }
}
