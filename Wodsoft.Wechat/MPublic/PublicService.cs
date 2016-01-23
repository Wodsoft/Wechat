using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 公众服务。
    /// </summary>
    public class PublicService : ServiceBase
    {
        /// <summary>
        /// 实例化公众服务。
        /// </summary>
        /// <param name="serviceToken">服务令牌。</param>
        /// <param name="appId">公众号Id。</param>
        /// <param name="appKey">公众号密钥。</param>
        /// <param name="appToken">消息令牌。</param>
        /// <param name="appAESKey">消息AES密钥。</param>
        public PublicService(ServiceToken serviceToken, string appId, string appKey, string appToken, string appAESKey)
            : this(serviceToken, appId, appKey)
        {
            AppToken = appToken;
            AppAESKey = appAESKey;
        }

        /// <summary>
        /// 实例化公众服务。
        /// </summary>
        /// <param name="serviceToken">服务令牌。</param>
        /// <param name="appId">公众号Id。</param>
        /// <param name="appKey">公众号密钥。</param>
        public PublicService(ServiceToken serviceToken, string appId, string appKey)
            : base(serviceToken, appId, appKey)
        {
            MessageManager = new MessageManager();
        }

        /// <summary>
        /// 获取消息令牌。
        /// </summary>
        public string AppToken { get; private set; }

        /// <summary>
        /// 获取消息AES密钥。
        /// </summary>
        public string AppAESKey { get; private set; }

        /// <summary>
        /// 处理Json错误。
        /// </summary>
        /// <param name="json">Json内容。</param>
        /// <exception cref="Wodsoft.Wechat.WechatException">微信异常错误。</exception>
        protected void HandleJsonError(string json)
        {
            if (json.Contains("errcode") && !json.Contains("errcode\":0"))
            {
                var error = JsonConvert.DeserializeObject<ServiceError>(json);
                throw new WechatException(error);
            }
        }

        /// <summary>
        /// 获取授权地址并且不获取用户信息。
        /// 该地址不需要用户进行授权。
        /// </summary>
        /// <param name="returnUrl">回调地址。</param>
        /// <param name="state">随机码。</param>
        /// <returns>返回拼接后的授权地址。</returns>
        public string GetAuthUrlWithoutInfo(string returnUrl, string state)
        {
            return "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppId + "&redirect_uri=" + Uri.EscapeDataString(returnUrl) + "&response_type=code&scope=snsapi_base&state=" + state + "#wechat_redirect";
        }

        /// <summary>
        /// 获取授权地址并且能进一步获取用户信息。
        /// 该地址会提示用户进行授权。
        /// </summary>
        /// <param name="returnUrl">回调地址。</param>
        /// <param name="state">随机码。</param>
        /// <returns>返回拼接后的授权地址。</returns>
        public string GetAuthUrlWithInfo(string returnUrl, string state)
        {
            return "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppId + "&redirect_uri=" + Uri.EscapeDataString(returnUrl) + "&response_type=code&scope=snsapi_userinfo&state=" + state + "#wechat_redirect";
        }

        #region 被动消息

        /// <summary>
        /// 获取或设置消息管理器。
        /// </summary>
        public MessageManager MessageManager { get; set; }

        /// <summary>
        /// 处理明文被动消息。
        /// </summary>
        /// <param name="stream">消息流。</param>
        /// <returns>返回回复内容。</returns>
        public virtual async Task<IReplyMessage> HandleMessage(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (MessageManager == null)
                return null;
            IDictionary<string, string> dictionary = await GetDataFromXml(stream);
            return await MessageManager.ExecuteMessage(dictionary);
        }

        /// <summary>
        /// 处理加密被动消息。
        /// </summary>
        /// <param name="stream">消息流。</param>
        /// <param name="timestamp">时间戳。</param>
        /// <param name="nonce">随机码。</param>
        /// <param name="signature">签名。</param>
        /// <returns>返回回复内容。</returns>
        public virtual async Task<IReplyMessage> HandleEncryptedMessage(Stream stream, string timestamp, string nonce, string signature)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (timestamp == null)
                throw new ArgumentNullException("timestamp");
            if (nonce == null)
                throw new ArgumentNullException("nonce");
            if (signature == null)
                throw new ArgumentNullException("signature");
            if (MessageManager == null)
                return null;
            var doc = XDocument.Load(stream);
            var encryptedMessage = doc.Element("xml").Element("Encrypt").Value;

            List<string> data = new List<string> { AppToken, timestamp, nonce, encryptedMessage };
            data.Sort();

            using (SHA1 sha = SHA1.Create())
            {
                var hash = string.Concat(sha.ComputeHash(Encoding.ASCII.GetBytes(string.Concat(data))).SelectMany(t => t.ToString("x2")));
                if (hash != signature)
                    throw new ArgumentException("签名验证失败。");
            }

            var dictionary = GetDataFromXml(DecryptMessage(encryptedMessage));
            return await MessageManager.ExecuteMessage(dictionary);
        }

        /// <summary>
        /// 解密消息。
        /// </summary>
        /// <param name="encryptedMessage">加密后的消息。</param>
        /// <returns>返回解密后的消息。</returns>
        public virtual string DecryptMessage(string encryptedMessage)
        {
            var data = Convert.FromBase64String(encryptedMessage);

            byte[] key;
            key = Convert.FromBase64String(AppAESKey + "=");
            byte[] iv = new byte[16];
            Array.Copy(key, iv, 16);

            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = key;
            aes.IV = iv;
            ICryptoTransform transform = aes.CreateDecryptor();
            data = transform.TransformFinalBlock(data, 0, data.Length);
            var pad = data[data.Length - 1];
            if (pad < 1 || pad > 32)
                pad = 0;
            data = data.Take(data.Length - pad).ToArray();

            int length = BitConverter.ToInt32(data, 16);
            length = IPAddress.NetworkToHostOrder(length);
            data = data.Skip(20).Take(length).ToArray();
            return Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// 加密消息。
        /// </summary>
        /// <param name="message">消息明文。</param>
        /// <returns>返回加密后的消息。</returns>
        public virtual string EncryptMessage(string message)
        {
            byte[] key;
            key = Convert.FromBase64String(AppAESKey + "=");
            byte[] iv = new byte[16];
            Array.Copy(key, iv, 16);

            List<byte> data = new List<byte>();
            byte[] rndData = new byte[16];
            byte[] msgData = Encoding.UTF8.GetBytes(message);
            Random rnd = new Random();
            rnd.NextBytes(rndData);
            data.AddRange(rndData);
            Int32 outval = 0;
            for (int i = 0; i < 4; i++)
                outval = (outval << 8) + ((msgData.Length >> (i * 8)) & 0xFF);
            data.AddRange(BitConverter.GetBytes(outval));
            data.AddRange(msgData);
            data.AddRange(Encoding.UTF8.GetBytes(AppId));

            int pad = 32 - (data.Count % 32);
            if (pad == 0)
                pad = 32;
            data.AddRange(Encoding.UTF8.GetBytes("".PadLeft(pad, (char)(byte)(pad & 0xFF))));

            var aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = iv;
            ICryptoTransform transform = aes.CreateEncryptor();
            byte[] buffer = transform.TransformFinalBlock(data.ToArray(), 0, data.Count);

            return Convert.ToBase64String(buffer);
        }

        #endregion

        #region JSSDK

        /// <summary>
        /// 获取JS SDK令牌。
        /// </summary>
        public IServiceToken JavascriptToken { get; private set; }

        /// <summary>
        /// 刷新JS SDK令牌。
        /// </summary>
        /// <returns>返回JS SDK令牌。</returns>
        public virtual async Task<IServiceToken> RefreshJavascriptToken()
        {
            await CheckServiceToken();
            string remoteToken = ServiceToken.Token;
            string result = await HttpHelper.GetHttp("https://api.weixin.qq.com/cgi-bin/ticket/getticket", new
            {
                access_token = remoteToken,
                type = "jsapi"
            });
            HandleJsonError(result);
            var item = new JavascriptToken();
            var doc = JsonConvert.DeserializeXNode(result, "xml").Element("xml");
            item.Token = doc.Element("ticket").Value;
            item.ExpiredDate = DateTime.Now.AddSeconds(int.Parse(doc.Element("expires_in").Value)).AddMinutes(-1);
            JavascriptToken = item;
            return item;
        }

        /// <summary>
        /// 检查JS SDK令牌。
        /// </summary>
        /// <returns></returns>
        public async Task CheckJavascriptToken()
        {
            if (JavascriptToken == null || JavascriptToken.ExpiredDate < DateTime.Now)
                JavascriptToken = await RefreshJavascriptToken();
        }

        /// <summary>
        /// 获取JS SDK配置。
        /// </summary>
        /// <param name="url">页面地址。</param>
        /// <returns>返回JS SDK配置。</returns>
        public async Task<IJavascriptConfig> GetJavascriptConfig(string url)
        {
            await CheckJavascriptToken();
            var jsToken = JavascriptToken.Token;
            JavascriptConfig config = new JavascriptConfig();
            config.Nonce = Guid.NewGuid().ToString().Replace("-", "");
            config.TimeStamp = GetTimestamp();
            config.Url = url;
            using (SHA1 sha1 = SHA1.Create())
            {
                string text = "jsapi_ticket=" + jsToken;
                text += "&noncestr=" + config.Nonce;
                text += "&timestamp=" + config.TimeStamp;
                text += "&url=" + config.Url;
                config.Signature = string.Concat(sha1.ComputeHash(Encoding.UTF8.GetBytes(text)).SelectMany(t => t.ToString("x2")));
            }
            return config;
        }

        #endregion

        #region 用户

        /// <summary>
        /// 获取用户令牌。
        /// </summary>
        /// <param name="code">用户代码。</param>
        /// <returns></returns>
        public virtual async Task<IUserToken> GetUserToken(string code)
        {
            string result = await HttpHelper.GetHttp("https://api.weixin.qq.com/sns/oauth2/access_token", new
            {
                appid = AppId,
                secret = AppKey,
                code = code,
                grant_type = "authorization_code"
            });
            return GetUserTokenCore(result);
        }

        /// <summary>
        /// 刷新用户令牌。
        /// </summary>
        /// <param name="userToken">用户令牌。</param>
        /// <returns>返回新用户令牌。</returns>
        public virtual async Task<IUserToken> RefreshUserToken(IUserToken userToken)
        {
            string result = await HttpHelper.GetHttp("https://api.weixin.qq.com/sns/oauth2/refresh_token", new
            {
                appid = AppId,
                grant_type = "refresh_token",
                refresh_token = userToken.RefreshToken
            });
            if (result == null)
                return null;
            return GetUserTokenCore(result);
        }

        /// <summary>
        /// 获取用户令牌。
        /// </summary>
        /// <param name="json">Json数据。</param>
        /// <returns>返回用户令牌。</returns>
        protected virtual IUserToken GetUserTokenCore(string json)
        {
            HandleJsonError(json);
            var item = JsonConvert.DeserializeObject<UserToken>(json);
            return item;
        }

        /// <summary>
        /// 获取用户信息。
        /// </summary>
        /// <param name="userToken">用户令牌。</param>
        /// <returns>返回用户信息。</returns>
        public virtual async Task<IUserInfo> GetUserInfo(IUserToken userToken)
        {
            string result = await HttpHelper.GetHttp("https://api.weixin.qq.com/sns/userinfo", new
            {
                access_token = userToken.Token,
                openid = userToken.OpenId
            });
            HandleJsonError(result);
            var item = JsonConvert.DeserializeObject<UserInfo>(result);
            return item;
        }

        /// <summary>
        /// 获取用户信息。
        /// </summary>
        /// <param name="openId">用户OpenId。</param>
        /// <returns>返回用户信息。</returns>
        public virtual async Task<IUserInfo> GetUserInfo(IOpenId openId)
        {
            await CheckServiceToken();
            string result = await HttpHelper.GetHttp("https://api.weixin.qq.com/cgi-bin/user/info", new
            {
                access_token = ServiceToken.Token,
                openid = openId.OpenId
            });
            HandleJsonError(result);
            var item = JsonConvert.DeserializeObject<UserInfo>(result);
            return item;
        }

        #endregion

        #region 二维码

        /// <summary>
        /// 创建临时二维码。
        /// </summary>
        /// <param name="expired">过期时间（秒）。</param>
        /// <param name="sceneId">场景Id。</param>
        /// <returns>返回二维码。</returns>
        public virtual async Task<IQrCode> CreateQrCode(int expired, uint sceneId)
        {
            string jsonData = JsonConvert.SerializeObject(new
            {
                expire_seconds = expired,
                action_name = "QR_SCENE",
                action_info = new { scene = new { scene_id = sceneId } }
            });
            await CheckServiceToken();
            string result = await HttpHelper.PostHttp(new Uri("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + ServiceToken.Token), Encoding.UTF8.GetBytes(jsonData), "text/json", Encoding.UTF8);
            return GetQrCode(result);
        }

        /// <summary>
        /// 创建永久二维码。
        /// </summary>
        /// <param name="sceneId">场景Id。</param>
        /// <returns>返回二维码。</returns>
        public virtual async Task<IQrCode> CreateQrCode(int sceneId)
        {
            if (sceneId < 1)
                throw new ArgumentOutOfRangeException("sceneId", "Id不能小于1。");
            if (sceneId > 100000)
                throw new ArgumentOutOfRangeException("sceneId", "Id不能大于100000。");
            string jsonData = JsonConvert.SerializeObject(new
            {
                action_name = "QR_LIMIT_SCENE",
                action_info = new { scene = new { scene_id = sceneId } }
            });
            await CheckServiceToken();
            string result = await HttpHelper.PostHttp(new Uri("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + ServiceToken.Token), Encoding.UTF8.GetBytes(jsonData), "text/json", Encoding.UTF8);
            return GetQrCode(result);
        }

        /// <summary>
        /// 创建永久二维码。
        /// </summary>
        /// <param name="scene">场景值。</param>
        /// <returns>返回二维码。</returns>
        public virtual async Task<IQrCode> CreateQrCode(string scene)
        {
            if (scene == null)
                throw new ArgumentNullException("scene");
            if (scene.Length < 1)
                throw new ArgumentOutOfRangeException("scene", "字符串长度不能小于1。");
            if (scene.Length > 64)
                throw new ArgumentOutOfRangeException("scene", "字符串长度不能大于64。");
            string jsonData = JsonConvert.SerializeObject(new
            {
                action_name = "QR_LIMIT_STR_SCENE",
                action_info = new { scene = new { scene_str = scene } }
            });
            await CheckServiceToken();
            string result = await HttpHelper.PostHttp(new Uri("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + ServiceToken.Token), Encoding.UTF8.GetBytes(jsonData), "text/json", Encoding.UTF8);
            return GetQrCode(result);
        }

        /// <summary>
        /// 获取二维码。
        /// </summary>
        /// <param name="jsonData">Json数据。</param>
        /// <returns>返回二维码。</returns>
        protected virtual IQrCode GetQrCode(string jsonData)
        {
            HandleJsonError(jsonData);
            var item = new QrCode();
            var doc = JsonConvert.DeserializeXNode(jsonData, "xml").Element("xml");
            item.Ticket = doc.Element("ticket").Value;
            if (doc.Element("expire_seconds") != null)
                item.ExpiredDate = DateTime.Now.AddSeconds(int.Parse(doc.Element("expire_seconds").Value));
            item.Url = doc.Element("url").Value;
            return item;
        }

        /// <summary>
        /// 获取二维码地址。
        /// </summary>
        /// <param name="ticket">二维码标签。</param>
        /// <returns>返回二维码地址。</returns>
        public virtual string GetQrCodeUrl(string ticket)
        {
            return "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + Uri.EscapeDataString(ticket);
        }

        #endregion

        #region 素材

        /// <summary>
        /// 上传临时素材。
        /// </summary>
        /// <param name="stream">内容流。</param>
        /// <param name="type">素材类型。</param>
        /// <param name="filename">文件名。</param>
        /// <returns>返回素材Id。</returns>
        public virtual async Task<string> UploadTemperatureMedia(Stream stream, MediaType type, string filename)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            switch (type)
            {
                case MediaType.Image:
                    if (stream.Length > 1024 * 1024 * 2)
                        throw new ArgumentException("文件长度超过2M限制。");
                    break;
                case MediaType.Thumb:
                    if (stream.Length > 1024 * 64)
                        throw new ArgumentException("文件长度超过64KB限制。");
                    break;
                case MediaType.Video:
                    if (stream.Length > 1024 * 1024 * 10)
                        throw new ArgumentException("文件长度超过10M限制。");
                    break;
                case MediaType.Voice:
                    if (stream.Length > 1024 * 1024 * 2)
                        throw new ArgumentException("文件长度超过2M限制。");
                    break;
            }
            var result = await HttpHelper.PostHttp("https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + ServiceToken.Token + "&type=" + type.ToString(), new HttpFilePart("media", stream, filename));
            HandleJsonError(result);
            return JsonConvert.DeserializeXNode(result, "xml").Element("xml").Element("media_id").Value;
        }

        #endregion

        #region 模板消息

        /// <summary>
        /// 获取模板Id。
        /// </summary>
        /// <param name="templateShortId">模板短Id。</param>
        /// <returns>返回模板Id。</returns>
        public async Task<string> GetTemplateId(string templateShortId)
        {
            byte[] jsonData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                template_id_short = templateShortId
            }));
            await CheckServiceToken();
            var result = await HttpHelper.PostHttp(new Uri("https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token=" + ServiceToken.Token), jsonData, "text/json", Encoding.UTF8);
            HandleJsonError(result);
            return JsonConvert.DeserializeXNode(result, "xml").Element("xml").Element("template_id").Value;
        }

        /// <summary>
        /// 删除模板。
        /// </summary>
        /// <param name="templateId">模板Id。</param>
        /// <returns></returns>
        public async Task RemoveTemplate(string templateId)
        {
            byte[] jsonData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                template_id = templateId
            }));
            await CheckServiceToken();
            var result = await HttpHelper.PostHttp(new Uri("https://api,weixin.qq.com/cgi-bin/template/del_private_template?access_token=" + ServiceToken.Token), jsonData, "text/json", Encoding.UTF8);
            HandleJsonError(result);
        }

        /// <summary>
        /// 发送模板消息。
        /// </summary>
        /// <param name="openId">用户OpendId。</param>
        /// <param name="templateId">模板Id。</param>
        /// <param name="url">链接地址。</param>
        /// <param name="parameters">模板参数。</param>
        /// <returns>返回消息Id。</returns>
        public async Task<string> SendTemplateMessage(IOpenId openId, string templateId, string url, object parameters)
        {
            Dictionary<string, object> parameterData = new Dictionary<string, object>();
            foreach (var p in parameters.GetType().GetProperties())
            {
                object value = p.GetValue(parameters);
                if (value is string)
                    parameterData.Add(p.Name, new
                    {
                        value = value
                    });
                else
                    parameterData.Add(p.Name, value);
            }
            byte[] jsonData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                touser = openId.OpenId,
                template_id = templateId,
                url = url,
                data = parameterData
            }));
            await CheckServiceToken();
            var result = await HttpHelper.PostHttp(new Uri("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + ServiceToken.Token), jsonData, "text/json", Encoding.UTF8);
            HandleJsonError(result);
            return JsonConvert.DeserializeXNode(result, "xml").Element("xml").Element("msgid").Value;
        }

        #endregion

        #region 自定义菜单

        /// <summary>
        /// 设置微信菜单。
        /// </summary>
        /// <param name="menus">菜单项。</param>
        /// <returns></returns>
        public async Task SetMenu(Menu[] menus)
        {
            if (menus == null)
                throw new ArgumentNullException("menus");
            if (menus.Length == 0)
                throw new ArgumentException("至少需要1个菜单项。");
            if (menus.Length > 3)
                throw new ArgumentException("不支持3个以上的菜单项。");

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                button = menus
            }));
            await CheckServiceToken();
            var result = await HttpHelper.PostHttp(new Uri("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + ServiceToken.Token), data, "text/json", Encoding.UTF8);
            HandleJsonError(result);
        }

        /// <summary>
        /// 清除微信菜单项。
        /// </summary>
        /// <returns></returns>
        public async Task ClearMenu()
        {
            await CheckServiceToken();
            var result = await HttpHelper.GetHttp("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=" + ServiceToken.Token);
            HandleJsonError(result);
        }

        /// <summary>
        /// 获取微信菜单项。
        /// </summary>
        /// <returns>返回微信菜单项。</returns>
        public async Task<MenuGeneralItem[]> GetMenu()
        {
            await CheckServiceToken();
            var result = await HttpHelper.GetHttp("https://api.weixin.qq.com/cgi-bin/menu/get?access_token=" + ServiceToken.Token);
            HandleJsonError(result);
            var model = JsonConvert.DeserializeObject<MenuQuery>(result);
            return model.Menu.Items;
        }

        #endregion
    }
}
