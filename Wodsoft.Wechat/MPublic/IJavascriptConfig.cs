using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public interface IJavascriptConfig
    {
        string Nonce { get; }

        int TimeStamp { get; }

        string Signature { get; }

        string Url { get; }
    }
}
