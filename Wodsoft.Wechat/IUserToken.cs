using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 用户令牌。
    /// </summary>
    public interface IUserToken : IUser, IToken
    {
        /// <summary>
        /// 获取刷新令牌。
        /// </summary>
        string RefreshToken { get; }

        /// <summary>
        /// 获取过期时间。
        /// </summary>
        int ExpiredTime { get; }
    }
}
