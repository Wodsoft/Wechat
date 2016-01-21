using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 微信异常。
    /// </summary>
    public class WechatException : Exception
    {
        /// <summary>
        /// 实例化微信异常。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        public WechatException(string errorMessage)
            : this(-1, errorMessage, null) { }

        /// <summary>
        /// 实例化微信异常。
        /// </summary>
        /// <param name="error">服务错误。</param>
        public WechatException(ServiceError error)
            : this(error.ErrorCode, error.ErrorMessage) { }

        /// <summary>
        /// 实例化微信异常。
        /// </summary>
        /// <param name="errorCode">错误代码。</param>
        /// <param name="errorMessage">错误信息。</param>
        public WechatException(int errorCode, string errorMessage)
            : this(errorCode, errorMessage, null) { }

        /// <summary>
        /// 实例化微信异常。
        /// </summary>
        /// <param name="errorCode">错误代码。</param>
        /// <param name="errorMessage">错误信息。</param>
        /// <param name="innerException">内部错误。</param>
        public WechatException(int errorCode, string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
            HResult = errorCode;
        }
    }
}
