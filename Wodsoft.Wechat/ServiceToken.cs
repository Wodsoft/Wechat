using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    public class ServiceToken : IServiceToken
    {
        public ServiceToken()
        {
            ExpiredDate = DateTime.Now;
        }

        public DateTime ExpiredDate { get; protected set; }

        public string Token { get; protected set; }

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
