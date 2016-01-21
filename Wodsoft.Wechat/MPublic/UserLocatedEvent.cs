using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户定位事件。
    /// </summary>
    public class UserLocatedEvent : UserEvent, IUserLocatedEvent
    {
        /// <summary>
        /// 实例化用户定位事件。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserLocatedEvent(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            Latitude = double.Parse(dictionary["Latitude"]);
            Longitude = double.Parse(dictionary["Longitude"]);
            Precision = double.Parse(dictionary["Precision"]);
        }

        /// <summary>
        /// 获取地理位置纬度 。
        /// </summary>
        public double Latitude { get; private set; }

        /// <summary>
        /// 获取地理位置经度 。
        /// </summary>
        public double Longitude { get; private set; }

        /// <summary>
        /// 获取地理位置精度 。
        /// </summary>
        public double Precision { get; private set; }
    }
}
