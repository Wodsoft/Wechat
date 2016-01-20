using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 微信令牌。
    /// </summary>
    public interface IToken
    {
        /// <summary>
        /// 获取令牌。
        /// </summary>
        string Token { get; }
    }
}
