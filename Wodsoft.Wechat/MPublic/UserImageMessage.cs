using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserImageMessage : UserMediaMessage, IUserImageMessage
    {
        public UserImageMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            ImageUrl = dictionary["PicUrl"];
        }

        public string ImageUrl { get; private set; }

    }
}
