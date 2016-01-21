using Newtonsoft.Json;
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
    public class UserToken : IUserToken
    {
        /// <summary>
        /// 获取或设置用户令牌。
        /// </summary>
        [JsonProperty("access_token")]
        public string Token { get; set; }

        /// <summary>
        /// 获取或设置刷新令牌。
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 获取或设置过期时间。
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiredTime { get; set; }

        /// <summary>
        /// 获取或设置用户OpenId。
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 获取或设置类型。
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// 获取或设置通用Id。
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
