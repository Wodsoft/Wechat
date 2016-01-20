using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    public class UserToken : IUserToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiredTime { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
