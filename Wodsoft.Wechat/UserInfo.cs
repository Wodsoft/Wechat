using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 用户信息。
    /// </summary>
    public class UserInfo : IUserInfo
    {
        /// <summary>
        /// 获取或设置用户昵称。
        /// </summary>
        [JsonProperty("nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置用户的性别。值为1时是男性，值为2时是女性，值为0时是未知。
        /// </summary>
        [JsonProperty("sex")]
        public string Gender { get; set; }

        /// <summary>
        /// 获取或设置用户个人资料填写的省份。
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        /// 获取或设置普通用户个人资料填写的城市。
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 获取或设置国家。如中国为CN。
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// 获取或设置用户头像。最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。
        /// </summary>
        [JsonProperty("headimgurl")]
        public string Avatar { get; set; }

        /// <summary>
        /// 获取或设置用户特权信息。如微信沃卡用户为“chinaunicom”。
        /// </summary>
        [JsonProperty("privilege")]
        public string[] Privilege { get; set; }

        /// <summary>
        /// 获取或设置用户OpenId。
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 获取或设置通用Id。
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}
