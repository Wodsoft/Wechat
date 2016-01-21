using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Open
{
    /// <summary>
    /// 微信开放平台服务。
    /// </summary>
    public class OpenService : ServiceBase
    {
        /// <summary>
        /// 实例化微信开放平台服务。
        /// </summary>
        /// <param name="appId">开放平台Id。</param>
        /// <param name="appKey">开放平台密钥。</param>
        public OpenService(string appId, string appKey)
            : base(appId, appKey)
        { }

        /// <summary>
        /// 获取二维码登录地址。
        /// </summary>
        /// <param name="returnUrl">回调地址。</param>
        /// <param name="state">随机码。</param>
        /// <returns></returns>
        public string GetQrSignInUrl(string returnUrl, string state)
        {
            return "https://open.weixin.qq.com/connect/qrconnect?appid=" + AppId + "&redirect_uri=" + Uri.EscapeDataString(returnUrl) + "&scope=snsapi_login&response_type=code&state=" + state + "#wechat_redirect";
        }
    }
}
