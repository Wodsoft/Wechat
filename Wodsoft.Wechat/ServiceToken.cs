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
        public static string TokenUrl = "https://api.weixin.qq.com/cgi-bin/token";

        /// <summary>
        /// 实例化微信服务令牌。
        /// </summary>
        public ServiceToken(string appId, string appKey)
        {
            AppId = appId ?? throw new ArgumentNullException(nameof(appId));
            AppKey = appKey ?? throw new ArgumentNullException(nameof(appKey));
            ExpiredDate = DateTime.Now;
            HttpHelper = new HttpHelper();            
        }

        /// <summary>
        /// 获取公众号Id。
        /// </summary>
        public string AppId { get; private set; }

        /// <summary>
        /// 获取公众号密钥。
        /// </summary>
        public string AppKey { get; private set; }


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
        /// <returns></returns>
        public virtual async Task RefreshTokenAsync()
        {
            string result = await HttpHelper.GetHttp(TokenUrl, new
            {
                grant_type = "client_credential",
                appid = AppId,
                secret = AppKey
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
