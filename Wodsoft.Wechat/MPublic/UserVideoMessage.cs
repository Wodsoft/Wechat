using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserVideoMessage : UserMediaMessage, IUserVideoMessage
    {
        public UserVideoMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            ThumbMediaId = dictionary["ThumbMediaId"];
        }

        public string ThumbMediaId { get; private set; }
    }
}
