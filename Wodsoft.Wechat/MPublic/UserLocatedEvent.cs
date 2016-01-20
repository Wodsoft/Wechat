using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class UserLocatedEvent : UserEvent, IUserLocatedEvent
    {
        public UserLocatedEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Latitude = double.Parse(dictionary["Latitude"]);
            Longitude = double.Parse(dictionary["Longitude"]);
            Precision = double.Parse(dictionary["Precision"]);
        }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public double Precision { get; private set; }
    }
}
