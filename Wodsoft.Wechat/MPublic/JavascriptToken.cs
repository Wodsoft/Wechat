using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class JavascriptToken : IServiceToken
    {
        public DateTime ExpiredDate { get; set; }

        public string Token { get; set; }
    }
}
