using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class JavascriptConfig : IJavascriptConfig
    {
        public string Nonce { get; set; }

        public int TimeStamp { get; set; }

        public string Signature { get; set; }
        
        public string Url { get; set; }
    }
}
