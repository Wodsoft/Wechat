using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public interface IUserTemplateSendEvent : IUserEvent
    {
        string MessageId { get; }

        string Status { get; }
    }
}
