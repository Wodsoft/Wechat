using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public interface IUserLocationMessage : IUserMessage
    {
        double LocationX { get; }

        double LocationY { get; }

        int Scale { get; }

        string Label { get; }
    }
}
