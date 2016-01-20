using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Wodsoft.Wechat.Open
{
    public static class OpenServiceExtensions
    {
        public static ActionResult QrSignIn(this Controller controller, OpenService service, string returnUrl)
        {
            var state = new Random().Next(100000000, 1000000000);
            controller.Session["wechatState"] = state;
            string url = service.GetQrSignInUrl(returnUrl, state.ToString());
            return new RedirectResult(url);
        }
    }
}
