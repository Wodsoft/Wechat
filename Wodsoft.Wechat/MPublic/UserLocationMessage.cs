using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserLocationMessage : UserMessage, IUserLocationMessage
    {
        public UserLocationMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            LocationX = double.Parse(dictionary["Location_X"]);
            LocationY = double.Parse(dictionary["Location_Y"]);
            Scale = int.Parse(dictionary["Scale"]);
            Label = dictionary["Label"];
        }

        public double LocationX { get; private set; }

        public double LocationY { get; private set; }

        public int Scale { get; private set; }

        public string Label { get; private set; }
    }
}
