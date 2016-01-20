using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserShortVideoMessage : UserMediaMessage, IUserShortVideoMessage
    {
        public UserShortVideoMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            ThumbMediaId = dictionary["ThumbMediaId"];
        }

        public string ThumbMediaId { get; private set; }
    }
}
