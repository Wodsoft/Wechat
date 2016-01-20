using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserLinkMessage : UserMessage, IUserLinkMessage
    {
        public UserLinkMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Title = dictionary["Title"];
            Description = dictionary["Description"];
            Url = dictionary["Url"];
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string Url { get; private set; }
    }
}
