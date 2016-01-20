using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Open
{
    public class OpenService : ServiceBase
    {
        public OpenService(string appId, string appKey)
            : base(appId, appKey)
        { }

        public string GetQrSignInUrl(string returnUrl, string state)
        {
            return "https://open.weixin.qq.com/connect/qrconnect?appid=" + AppId + "&redirect_uri=" + Uri.EscapeDataString(returnUrl) + "&scope=snsapi_login&response_type=code&state=" + state + "#wechat_redirect";
        }
    }
}
