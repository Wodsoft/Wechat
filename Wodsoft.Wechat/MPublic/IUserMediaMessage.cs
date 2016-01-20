using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public interface IUserMediaMessage : IUserMessage
    {

        string MediaId { get; }
    }
}
