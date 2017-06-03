using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 微信服务令牌。
    /// </summary>
    public class ServiceToken : IServiceToken
    {
        /// <summary>
        /// 实例化微信服务令牌。
        /// </summary>
        public ServiceToken()
        {
            ExpiredDate = DateTime.Now;
            HttpHelper = new HttpHelper();            
        }

        public HttpHelper HttpHelper { get; private set; }

        /// <summary>
        /// 获取或设置过期时间。
        /// </summary>
        public DateTime ExpiredDate { get; protected set; }

        /// <summary>
        /// 获取或设置令牌。
        /// </summary>
        public string Token { get; protected set; }

        /// <summary>
        /// 刷新令牌。
        /// </summary>
        /// <param name="appId">公众号Id。</param>
        /// <param name="appKey">公众号密钥。</param>
        /// <returns></returns>
        public virtual async Task RefreshToken(string appId, string appKey)
        {
            string result = await HttpHelper.GetHttp("https://api.weixin.qq.com/cgi-bin/token", new
            {
                grant_type = "client_credential",
                appid = appId,
                secret = appKey
            });
            if (result.Contains("errcode"))
            {
                var error = JsonConvert.DeserializeObject<ServiceError>(result);
                throw new WechatException(error);
            }
            else
            {
                var doc = JsonConvert.DeserializeXNode(result, "xml").Element("xml");
                Token = doc.Element("access_token").Value;
                ExpiredDate = DateTime.Now.AddSeconds(int.Parse(doc.Element("expires_in").Value)).AddMinutes(-5);
            }
        }
    }
}
