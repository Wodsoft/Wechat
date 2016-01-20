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
    public interface IUserLocatedEvent : IUserEvent
    {
        /// <summary>
        /// 获取地理位置纬度 。
        /// </summary>
        double Latitude { get; }

        /// <summary>
        /// 获取地理位置经度 。
        /// </summary>
        double Longitude { get; }

        /// <summary>
        /// 获取地理位置精度 。
        /// </summary>
        double Precision { get; }
    }
}
