using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public interface IQrCode
    {
        string Ticket { get; }

        DateTime ExpiredDate { get; }

        string Url { get; }
    }
}
