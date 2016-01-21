using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 用户地理位置消息。
    /// </summary>
    public interface IUserLocationMessage : IUserMessage
    {
        /// <summary>
        /// 获取地址位置维度。
        /// </summary>
        double LocationX { get; }

        /// <summary>
        /// 获取地址位置经度。
        /// </summary>
        double LocationY { get; }

        /// <summary>
        /// 获取地址位置精度。
        /// </summary>
        int Scale { get; }

        /// <summary>
        /// 获取地址位置标签。
        /// </summary>
        string Label { get; }
    }
}
