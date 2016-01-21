using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 微信服务错误。
    /// </summary>
    public class ServiceError
    {
        /// <summary>
        /// 实例化微信服务错误。
        /// </summary>
        public ServiceError() { }

        /// <summary>
        /// 实例化微信服务错误。
        /// </summary>
        /// <param name="code">错误代码。</param>
        /// <param name="msg">错误信息。</param>
        public ServiceError(int code, string msg)
        {
            ErrorCode = code;
            ErrorMessage = msg;
        }

        /// <summary>
        /// 获取或设置错误代码。
        /// </summary>
        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 获取或设置错误信息。
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }
    }
}
