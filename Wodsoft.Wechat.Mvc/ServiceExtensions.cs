using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Wodsoft.Wechat
{
    public static class ServiceExtensions
    {
        public static string GetWechatCode(this Controller controller)
        {
            string code = controller.Request.QueryString["code"];
            if (code == "" || code == "authdeny")
                return null;
            string state = controller.Request.QueryString["state"];
            if (state != (string)controller.Session["wechatState"])
                return null;
            return code;
        }

        public static bool IsWechatBrowser(this HttpContext context)
        {
            return context.Request.UserAgent.ToLower().Contains("micromessenger");
        }

        public static bool IsWechatBrowser(this Controller controller)
        {
            return controller.Request.UserAgent.ToLower().Contains("micromessenger");
        }

        public static bool IsWechatBrowser(this WebPageRenderingBase page)
        {
            return page.Request.UserAgent.ToLower().Contains("micromessenger");
        }
    }
}
