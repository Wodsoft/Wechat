using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class QrCode : IQrCode
    {
        public string Ticket { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string Url { get; set; }
    }
}
