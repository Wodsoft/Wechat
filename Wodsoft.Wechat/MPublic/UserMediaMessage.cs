using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public abstract class UserMediaMessage : UserMessage, IUserMediaMessage
    {
        public UserMediaMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            MediaId = dictionary["MediaId"];
        }

        public string MediaId { get; private set; }
    }
}
