using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// Http助手。
    /// </summary>
    public class HttpHelper
    {
        public HttpHelper()
        {
#if NET451
            Handler = new WebRequestHandler();
#else
        Handler = new HttpClientHandler();
#endif
            Client = new HttpClient(Handler);
        }

        public HttpClient Client { get; private set; }

#if NET451
        public WebRequestHandler Handler { get; private set; }
#else
        public HttpClientHandler Handler { get; private set; }
#endif

        /// <summary>
        /// Get获取内容。
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时（毫秒）。</param>
        /// <returns>返回响应内容。</returns>
        public async Task<string> GetHttp(Uri uri, Encoding encoding, int timeout)
        {
            var response = await Client.GetAsync(uri);
            //HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            //request.Timeout = timeout;
            //request.Method = "GET";
            //request.AllowAutoRedirect = true;
            //var response = await request.GetResponseAsync();
            var stream = await response.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream, encoding);
            string result = await reader.ReadToEndAsync();
            return result;
        }

        /// <summary>
        /// Get获取内容。
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <param name="encoding">编码。</param>
        /// <returns>返回响应内容。</returns>
        public Task<string> GetHttp(Uri uri, Encoding encoding)
        {
            return GetHttp(uri, encoding, 5000);
        }

        /// <summary>
        /// Get获取内容。
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <param name="timeout">超时（毫秒）。</param>
        /// <returns>返回响应内容。</returns>
        public Task<string> GetHttp(Uri uri, int timeout)
        {
            return GetHttp(uri, Encoding.UTF8, timeout);
        }

        /// <summary>
        /// Get获取内容。
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <returns>返回响应内容。</returns>
        public Task<string> GetHttp(Uri uri)
        {
            return GetHttp(uri, Encoding.UTF8, 5000);
        }

        /// <summary>
        /// Get获取内容。
        /// </summary>
        /// <param name="url">地址。</param>
        /// <returns>返回响应内容。</returns>
        public Task<string> GetHttp(string url)
        {
            return GetHttp(new Uri(url));
        }

        /// <summary>
        /// Get获取内容。
        /// </summary>
        /// <param name="url">地址。</param>
        /// <param name="querystring"></param>
        /// <returns>返回响应内容。</returns>
        public Task<string> GetHttp(string url, object querystring)
        {
            var type = querystring.GetType();
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var property in type.GetRuntimeProperties())
            {
                data.Add(property.Name, property.GetValue(querystring).ToString());
            }
            url += "?" + GetQueryString(data);
            return GetHttp(url);
        }

        /// <summary>
        /// Post提交数据。
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <param name="rawData">原始数据。</param>
        /// <param name="contentType">内容类型。</param>
        /// <param name="encoding">编码。</param>
        /// <returns>返回响应内容。</returns>
        public async Task<string> PostHttp(Uri uri, byte[] rawData, string contentType, Encoding encoding)
        {
            ByteArrayContent content = new ByteArrayContent(rawData);
            content.Headers.Add("Content-Type", contentType);
            var response = await Client.PostAsync(uri, content);
            //HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            //request.Timeout = timeout;
            //request.Method = "POST";
            //request.ContentType = contentType;
            //request.ContentLength = rawData.Length;
            //if (cert != null)
            //    request.ClientCertificates.Add(cert);
            //request.AllowAutoRedirect = true;
            {
                var stream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(stream, encoding);
                string result = await reader.ReadToEndAsync();
                return result;
            }
        }
        /// <summary>
        /// Post提交数据。
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <param name="formData">表单数据。</param>
        /// <param name="encoding">编码。</param>
        /// <returns>返回响应内容。</returns>
        public Task<string> PostHttp(Uri uri, object formData, Encoding encoding)
        {
            var type = formData.GetType();
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var property in type.GetProperties())
            {
                data.Add(property.Name, property.GetValue(formData).ToString());
            }
            return PostHttp(uri, data, encoding);
        }

        /// <summary>
        /// Post提交数据。
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <param name="formData">表单数据。</param>
        /// <returns>返回响应内容。</returns>
        public Task<string> PostHttp(Uri uri, object formData)
        {
            return PostHttp(uri, formData, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <param name="formData"></param>
        /// <param name="encoding">编码。</param>
        /// <returns>返回响应内容。</returns>
        public Task<string> PostHttp(Uri uri, IDictionary<string, string> formData, Encoding encoding)
        {
            return PostHttp(uri, encoding.GetBytes(GetFormString(formData)), "application/x-www-form-urlencoded", encoding);
        }

        /// <summary>
        /// Post提交数据。
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <param name="formData">表单数据。</param>
        /// <returns>返回响应内容。</returns>
        public Task<string> PostHttp(Uri uri, IDictionary<string, string> formData)
        {
            return PostHttp(uri, formData, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">地址。</param>
        /// <param name="formData"></param>
        /// <returns>返回响应内容。</returns>
        public Task<string> PostHttp(string url, object formData)
        {
            return PostHttp(new Uri(url), formData);
        }

        /// <summary>
        /// Post提交数据。
        /// </summary>
        /// <param name="url">地址。</param>
        /// <param name="formData">表单数据。</param>
        /// <returns>返回响应内容。</returns>
        public Task<string> PostHttp(string url, IDictionary<string, string> formData)
        {
            return PostHttp(new Uri(url), formData);
        }

        /// <summary>
        /// Post提交文件。
        /// </summary>
        /// <param name="uri">地址。</param>
        /// <param name="parts">内容部分。</param>
        /// <returns>返回响应内容。</returns>
        public async Task<string> PostHttp(Uri uri, params HttpPart[] parts)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            foreach (var part in parts)
            {
                MemoryStream stream = new MemoryStream();
                await part.WriteContent(stream);
                content.Add(new ByteArrayContent(stream.ToArray()), part.Name);
            }
            var response = await Client.PostAsync(uri, content);
            {
                var stream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string result = await reader.ReadToEndAsync();
                return result;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">地址。</param>
        /// <param name="parts"></param>
        /// <returns>返回响应内容。</returns>
        public Task<string> PostHttp(string url, params HttpPart[] parts)
        {
            return PostHttp(new Uri(url), parts);
        }

        /// <summary>
        /// 获取查询字符串。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        /// <returns>返回查询字符串。</returns>
        public string GetQueryString(IDictionary<string, string> dictionary)
        {
            return string.Join("&", dictionary.Select(t => t.Key + "=" + Uri.EscapeUriString(t.Value)));
        }

        /// <summary>
        /// 获取表单字符串。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        /// <returns>返回表单字符串。</returns>
        public string GetFormString(IDictionary<string, string> dictionary)
        {
            return string.Join("&", dictionary.Select(t => t.Key + "=" + Uri.EscapeDataString(t.Value)));
        }
    }

    /// <summary>
    /// Http部分。
    /// </summary>
    public abstract class HttpPart
    {
        /// <summary>
        /// 获取新行字节数据。
        /// </summary>
        protected readonly byte[] NewLine = Encoding.UTF8.GetBytes("\r\n");

        /// <summary>
        /// 实例化Http部分。
        /// </summary>
        /// <param name="name">表单名称。</param>
        public HttpPart(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 获取表单名称。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 将数据写入Http流。
        /// </summary>
        /// <param name="stream">Http流。</param>
        /// <returns></returns>
        public abstract Task WriteContent(Stream stream);
    }

    /// <summary>
    /// Http表单部分。
    /// </summary>
    public class HttpFormPart : HttpPart
    {
        /// <summary>
        /// 实例化Http表单部分。
        /// </summary>
        /// <param name="name">表单名称。</param>
        /// <param name="value">表单值。</param>
        public HttpFormPart(string name, string value)
            : base(name)
        {
            Value = value;
        }

        /// <summary>
        /// 获取表单值。
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 将数据写入Http流。
        /// </summary>
        /// <param name="stream">Http流。</param>
        /// <returns></returns>
        public override async Task WriteContent(Stream stream)
        {
            var disposition = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"" + Name + "\"");
            await stream.WriteAsync(disposition, 0, disposition.Length);
            await stream.WriteAsync(NewLine, 0, NewLine.Length);
            var data = Encoding.UTF8.GetBytes(Value);
            await stream.WriteAsync(data, 0, data.Length);
        }
    }

    /// <summary>
    /// Http文件部分。
    /// </summary>
    public class HttpFilePart : HttpPart
    {
        /// <summary>
        /// 实例化Http文件部分。
        /// </summary>
        /// <param name="name">表单名称。</param>
        /// <param name="stream">文件流。</param>
        public HttpFilePart(string name, Stream stream)
            : this(name, stream, "UploadFile")
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">表单名称。</param>
        /// <param name="stream">文件流。</param>
        /// <param name="filename">文件名。</param>
        public HttpFilePart(string name, Stream stream, string filename)
            : this(name, stream, filename, "application/octet-stream")
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">表单名称。</param>
        /// <param name="stream">文件流。</param>
        /// <param name="filename">文件名。</param>
        /// <param name="mimetype">文件类型。</param>
        public HttpFilePart(string name, Stream stream, string filename, string mimetype)
            : base(name)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (filename == null)
                throw new ArgumentNullException("filename");
            if (mimetype == null)
                throw new ArgumentNullException("mimetype");
            Mimetype = mimetype;
        }

        /// <summary>
        /// 获取文件流。
        /// </summary>
        public Stream File { get; private set; }

        /// <summary>
        /// 获取文件名。
        /// </summary>
        public string Filename { get; private set; }

        /// <summary>
        /// 获取文件类型。
        /// </summary>
        public string Mimetype { get; private set; }

        /// <summary>
        /// 将数据写入Http流。
        /// </summary>
        /// <param name="stream">Http流。</param>
        /// <returns></returns>
        public override async Task WriteContent(Stream stream)
        {
            var disposition = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"" + Name + "\"; filename=\"" + Filename + "\"");
            await stream.WriteAsync(disposition, 0, disposition.Length);
            await stream.WriteAsync(NewLine, 0, NewLine.Length);
            var type = Encoding.UTF8.GetBytes("Content-Type: " + Mimetype);
            await stream.WriteAsync(NewLine, 0, NewLine.Length);
            await File.CopyToAsync(stream);
        }
    }
}
