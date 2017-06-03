using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信公众服务扩展。
    /// </summary>
    public static class PublicServiceExtensions
    {
        /// <summary>
        /// 微信登录跳转。
        /// </summary>
        /// <param name="service">微信公众服务。</param>
        /// <param name="controller">Mvc控制器。</param>
        /// <param name="returnUrl">回调地址。</param>
        /// <returns>返回跳转结果。</returns>
        public static IActionResult SignIn(this PublicService service, Controller controller, string returnUrl)
        {
            var state = new Random().Next(100000000, 1000000000);
            controller.HttpContext.Session.SetString("wechatState", state.ToString());
            string url = service.GetAuthUrlWithInfo(returnUrl, state.ToString());
            return new RedirectResult(url);
        }

        /// <summary>
        /// 处理微信被动消息。
        /// </summary>
        /// <param name="service">微信公众服务。</param>
        /// <param name="controller">Mvc控制器。</param>
        /// <returns>返回处理结果。</returns>
        public static async Task<IActionResult> HandleMessage(this PublicService service, Controller controller)
        {
            IReplyMessage message;
            if (controller.Request.Query.Keys.Contains("msg_signature"))
                message = await service.HandleEncryptedMessage(controller.Request.Body, controller.Request.Query["timestamp"], controller.Request.Query["nonce"], controller.Request.Query["msg_signature"]);
            else
                message = await service.HandleMessage(controller.Request.Body);
            if (message == null)
                return new ContentResult() { Content = "success" };
            MemoryStream stream = new MemoryStream();
            message.WriteResponseText(stream);
            if (controller.Request.Query.Keys.Contains("msg_signature"))
            {
                var encryptedMessage = service.EncryptMessage(Encoding.UTF8.GetString(stream.ToArray()));
                var timestamp = service.GetTimestamp().ToString();
                var nonce = Guid.NewGuid().ToString().Replace("-", "");

                List<string> data = new List<string> { service.AppToken, timestamp, nonce, encryptedMessage };
                data.Sort();

                string signature;
                using (SHA1 sha = SHA1.Create())
                {
                    signature = string.Concat(sha.ComputeHash(Encoding.ASCII.GetBytes(string.Concat(data))).SelectMany(t => t.ToString("x2")));
                }

                XmlDocument doc = new XmlDocument();
                var xml = doc.CreateElement("xml");
                var xmlEncrypt = doc.CreateElement("Encrypt");
                xmlEncrypt.InnerText = encryptedMessage;
                var xmlSignature = doc.CreateElement("MsgSignature");
                xmlSignature.InnerText = signature;
                var xmlTimeStamp = doc.CreateElement("TimeStamp");
                xmlTimeStamp.InnerText = timestamp;
                var xmlNonce = doc.CreateElement("Nonce");
                xmlNonce.InnerText = nonce;
                xml.AppendChild(xmlEncrypt);
                xml.AppendChild(xmlSignature);
                xml.AppendChild(xmlTimeStamp);
                xml.AppendChild(xmlNonce);
                stream.Position = 0;
                doc.Save(stream);
            }
            stream.Position = 0;
            return new FileStreamResult(stream, "text/xml");
        }
    }
}
