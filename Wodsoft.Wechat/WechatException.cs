using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    public class WechatException : Exception
    {
        public WechatException(string errorMessage)
            : this(-1, errorMessage, null) { }

        public WechatException(ServiceError error)
            : this(error.ErrorCode, error.ErrorMessage) { }

        public WechatException(int errorCode, string errorMessage)
            : this(errorCode, errorMessage, null) { }

        public WechatException(int errorCode, string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
            HResult = errorCode;
        }
    }
}
