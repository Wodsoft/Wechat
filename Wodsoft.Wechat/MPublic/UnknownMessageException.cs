using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UnknownMessageException : Exception
    {
        public UnknownMessageException(string msgType)
        {
            MessageType = msgType;
        }

        public string MessageType { get; private set; }
    }
}
