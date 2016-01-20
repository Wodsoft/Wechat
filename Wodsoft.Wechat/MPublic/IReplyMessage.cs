using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public interface IReplyMessage
    {
        void WriteResponseText(Stream stream);
    }
}
