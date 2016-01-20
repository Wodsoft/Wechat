using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public interface IUserLinkMessage : IUserMessage
    {
        string Title { get; }

        string Description { get; }

        string Url { get; }
    }
}
