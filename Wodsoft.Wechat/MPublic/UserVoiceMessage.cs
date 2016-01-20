using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserVoiceMessage : UserMediaMessage, IUserVoiceMessage
    {
        public UserVoiceMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Format = dictionary["Format"];
            if (dictionary.ContainsKey("Recognition"))
                Recognition = dictionary["Recognition"];
        }

        public string Format { get; private set; }

        public string Recognition { get; private set; }
    }
}
