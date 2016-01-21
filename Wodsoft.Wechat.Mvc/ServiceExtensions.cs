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
    /// <summary>
    /// 微信服务扩展。
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// 获取登录回调用户代码。
        /// </summary>
        /// <param name="controller">Mvc控制器。</param>
        /// <returns>返回用户代码。</returns>
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

        /// <summary>
        /// 判断是否是微信内置浏览器。
        /// </summary>
        /// <param name="context">Http上下文。</param>
        /// <returns>返回是与否。</returns>
        public static bool IsWechatBrowser(this HttpContext context)
        {
            return context.Request.UserAgent.ToLower().Contains("micromessenger");
        }

        /// <summary>
        /// 判断是否是微信内置浏览器。
        /// </summary>
        /// <param name="controller">Mvc控制器。</param>
        /// <returns>返回是与否。</returns>
        public static bool IsWechatBrowser(this Controller controller)
        {
            return controller.Request.UserAgent.ToLower().Contains("micromessenger");
        }

        /// <summary>
        /// 判断是否是微信内置浏览器。
        /// </summary>
        /// <param name="page">Web页面视图。</param>
        /// <returns>返回是与否。</returns>
        public static bool IsWechatBrowser(this WebPageRenderingBase page)
        {
            return page.Request.UserAgent.ToLower().Contains("micromessenger");
        }
    }
}
