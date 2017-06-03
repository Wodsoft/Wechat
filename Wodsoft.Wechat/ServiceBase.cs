using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 服务基类。
    /// </summary>
    public class ServiceBase
    {
        /// <summary>
        /// 实例化服务基类。
        /// </summary>
        /// <param name="appId">公众号Id。</param>
        /// <param name="appKey">公众号密钥。</param>
        public ServiceBase(string appId, string appKey) : this(new ServiceToken(), appId, appKey) { }

        /// <summary>
        /// 实例化服务基类。
        /// </summary>
        /// <param name="serviceToken">服务令牌。</param>
        /// <param name="appId">公众号Id。</param>
        /// <param name="appKey">公众号密钥。</param>
        public ServiceBase(ServiceToken serviceToken, string appId, string appKey)
        {
            if (serviceToken == null)
                throw new ArgumentNullException("serviceToken");
            if (appId == null)
                throw new ArgumentNullException("appId");
            if (appKey == null)
                throw new ArgumentNullException("appKey");
            ServiceToken = serviceToken;
            AppId = appId;
            AppKey = appKey;
        }

        /// <summary>
        /// 获取服务令牌。
        /// </summary>
        public ServiceToken ServiceToken { get; private set; }

        /// <summary>
        /// 获取公众号Id。
        /// </summary>
        public string AppId { get; private set; }

        /// <summary>
        /// 获取公众号密钥。
        /// </summary>
        public string AppKey { get; private set; }

        /// <summary>
        /// 检查服务令牌。
        /// </summary>
        /// <returns></returns>
        public async Task CheckServiceToken()
        {
            if (ServiceToken.ExpiredDate > DateTime.Now)
                return;
            await ServiceToken.RefreshToken(AppId, AppKey);
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSignature(object arguments, string key)
        {
            var type = arguments.GetType();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var property in type.GetRuntimeProperties())
                dictionary.Add(property.Name, property.GetValue(arguments).ToString());
            return GetSignature(dictionary, key);
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSignature(IDictionary<string, string> dictionary, string key)
        {
            string data = string.Join("&", dictionary.Where(t => t.Key != "sign" && !string.IsNullOrEmpty(t.Value)).OrderBy(t => t.Key).Select(t => t.Key + "=" + t.Value)) + "&key=" + key;
            using (MD5 md5 = MD5.Create())
                return string.Concat(md5.ComputeHash(Encoding.UTF8.GetBytes(data)).Select(t => t.ToString("X2")));
        }

        /// <summary>
        /// 从XML数据转换为字典。
        /// </summary>
        /// <param name="xml">XML数据。</param>
        /// <returns>返回字典。</returns>
        protected virtual IDictionary<string, string> GetDataFromXml(string xml)
        {
            var doc = XDocument.Parse(xml);
            return doc.Element("xml").Elements().ToDictionary(t => t.Name.LocalName, t => t.Value);
        }

        /// <summary>
        /// 从XML数据转换为字典。
        /// </summary>
        /// <param name="stream">XML流。</param>
        /// <returns>返回字典。</returns>
        protected virtual async Task<IDictionary<string, string>> GetDataFromXml(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string xml = await reader.ReadToEndAsync();
            var doc = XDocument.Parse(xml);
            return doc.Element("xml").Elements().ToDictionary(t => t.Name.LocalName, t => t.Value);
        }

        private static DateTime _StartDate = new DateTime(1970, 1, 1);
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public virtual int GetTimestamp()
        {
            return (int)(DateTime.Now - _StartDate).TotalSeconds;
        }
    }
}
