using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class ReplyErrorMessage : IReplyMessage
    {
        private static byte[] _Success = Encoding.UTF8.GetBytes("error");

        public void WriteResponseText(Stream stream)
        {
            stream.Write(_Success, 0, _Success.Length);
        }
    }
}
