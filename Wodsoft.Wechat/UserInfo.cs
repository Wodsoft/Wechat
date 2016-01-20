using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    public class UserInfo : IUserInfo
    {
        [JsonProperty("nickname")]
        public string NickName { get; set; }

        [JsonProperty("sex")]
        public string Gender { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("headimgurl")]
        public string Avatar { get; set; }

        [JsonProperty("privilege")]
        public string[] Privilege { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }

        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
