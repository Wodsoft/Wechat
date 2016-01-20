using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    public class ServiceError
    {
        public ServiceError()
        {

        }

        public ServiceError(int code, string msg)
        {
            ErrorCode = code;
            ErrorMessage = msg;
        }

        [JsonProperty("errcode")]
        public int ErrorCode { get; private set; }

        [JsonProperty("errmsg")]
        public string ErrorMessage { get; private set; }
    }
}
