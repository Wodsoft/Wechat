using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    public static class HttpHelper
    {
        public static async Task<string> GetHttp(Uri uri, Encoding encoding, int timeout)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Timeout = timeout;
            request.Method = "GET";
            request.AllowAutoRedirect = true;
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, encoding);
            string result = await reader.ReadToEndAsync();
            return result;
        }

        public static Task<string> GetHttp(Uri uri, Encoding encoding)
        {
            return GetHttp(uri, encoding, 5000);
        }

        public static Task<string> GetHttp(Uri uri, int timeout)
        {
            return GetHttp(uri, Encoding.UTF8, timeout);
        }

        public static Task<string> GetHttp(Uri uri)
        {
            return GetHttp(uri, Encoding.UTF8, 5000);
        }

        public static Task<string> GetHttp(string url)
        {
            return GetHttp(new Uri(url));
        }

        public static Task<string> GetHttp(string url, object querystring)
        {
            var type = querystring.GetType();
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var property in type.GetProperties())
            {
                data.Add(property.Name, property.GetValue(querystring).ToString());
            }
            url += "?" + GetQueryString(data);
            return GetHttp(url);
        }

        public static async Task<string> PostHttp(Uri uri, byte[] rawData, string contentType, Encoding encoding, int timeout, X509Certificate2 cert)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Timeout = timeout;
            request.Method = "POST";
            request.ContentType = contentType;
            request.ContentLength = rawData.Length;
            if (cert != null)
                request.ClientCertificates.Add(cert);
            request.AllowAutoRedirect = true;
            {
                var stream = await request.GetRequestStreamAsync();
                await stream.WriteAsync(rawData, 0, rawData.Length);
                stream.Close();
            }
            {
                var response = await request.GetResponseAsync();
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, encoding);
                string result = await reader.ReadToEndAsync();
                return result;
            }
        }

        public static Task<string> PostHttp(Uri uri, byte[] rawData, string contentType, Encoding encoding)
        {
            return PostHttp(uri, rawData, contentType, encoding, 5000, null);
        }

        public static Task<string> PostHttp(Uri uri, object formData, Encoding encoding)
        {
            var type = formData.GetType();
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var property in type.GetProperties())
            {
                data.Add(property.Name, property.GetValue(formData).ToString());
            }
            return PostHttp(uri, data, encoding);
        }

        public static Task<string> PostHttp(Uri uri, object formData)
        {
            return PostHttp(uri, formData, Encoding.UTF8);
        }

        public static Task<string> PostHttp(Uri uri, IDictionary<string, string> formData, Encoding encoding)
        {
            return PostHttp(uri, encoding.GetBytes(GetFormString(formData)), "application/x-www-form-urlencoded", encoding);
        }

        public static Task<string> PostHttp(Uri uri, IDictionary<string, string> formData)
        {
            return PostHttp(uri, formData, Encoding.UTF8);
        }

        public static Task<string> PostHttp(string url, object formData)
        {
            return PostHttp(new Uri(url), formData);
        }

        public static Task<string> PostHttp(string url, IDictionary<string, string> formData)
        {
            return PostHttp(new Uri(url), formData);
        }

        public static async Task<string> PostHttp(Uri uri, int timeout, X509Certificate2 cert, params HttpPart[] parts)
        {
            var boundary = "--" + Guid.NewGuid().ToString();
            var startBoundary = Encoding.UTF8.GetBytes("--" + boundary);
            var endBoundary = Encoding.UTF8.GetBytes("--" + boundary + "--");

            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Timeout = timeout;
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            if (cert != null)
                request.ClientCertificates.Add(cert);
            request.AllowAutoRedirect = true;
            {
                var stream = await request.GetRequestStreamAsync();
                foreach (var part in parts)
                {
                    await stream.WriteAsync(startBoundary, 0, startBoundary.Length);
                    await part.WriteContent(stream);
                }
                await stream.WriteAsync(endBoundary, 0, endBoundary.Length);
                stream.Close();
            }
            {
                var response = await request.GetResponseAsync();
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string result = await reader.ReadToEndAsync();
                return result;
            }
        }

        public static Task<string> PostHttp(string url, int timeout, params HttpPart[] parts)
        {
            return PostHttp(new Uri(url), timeout, null, parts);
        }

        public static Task<string> PostHttp(string url, params HttpPart[] parts)
        {
            return PostHttp(new Uri(url), 10000, null, parts);
        }

        public static string GetQueryString(IDictionary<string, string> dictionary)
        {
            return string.Join("&", dictionary.Select(t => t.Key + "=" + Uri.EscapeUriString(t.Value)));
        }

        public static string GetFormString(IDictionary<string, string> dictionary)
        {
            return string.Join("&", dictionary.Select(t => t.Key + "=" + Uri.EscapeDataString(t.Value)));
        }
    }

    public abstract class HttpPart
    {
        protected static byte[] NewLine = Encoding.UTF8.GetBytes("\r\n");

        public HttpPart(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract Task WriteContent(Stream stream);
    }

    public class HttpFormPart : HttpPart
    {
        public HttpFormPart(string name, string value)
            : base(name)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public override async Task WriteContent(Stream stream)
        {
            var disposition = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"" + Name + "\"");
            await stream.WriteAsync(disposition, 0, disposition.Length);
            await stream.WriteAsync(NewLine, 0, NewLine.Length);
            var data = Encoding.UTF8.GetBytes(Value);
            await stream.WriteAsync(data, 0, data.Length);
        }
    }

    public class HttpFilePart : HttpPart
    {
        public HttpFilePart(string name, Stream stream)
            : this(name, stream, "UploadFile")
        { }


        public HttpFilePart(string name, Stream stream, string filename)
            : this(name, stream, filename, "application/octet-stream")
        { }

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

        public Stream File { get; private set; }

        public string Filename { get; private set; }

        public string Mimetype { get; private set; }

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
