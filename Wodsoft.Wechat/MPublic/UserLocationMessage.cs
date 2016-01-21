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
    public class UserLocationMessage : UserMessage, IUserLocationMessage
    {
        /// <summary>
        /// 实例化用户地理位置消息。
        /// </summary>
        /// <param name="dictionary">字典数据。</param>
        public UserLocationMessage(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
            LocationX = double.Parse(dictionary["Location_X"]);
            LocationY = double.Parse(dictionary["Location_Y"]);
            Scale = int.Parse(dictionary["Scale"]);
            Label = dictionary["Label"];
        }

        /// <summary>
        /// 获取地址位置维度。
        /// </summary>
        public double LocationX { get; private set; }

        /// <summary>
        /// 获取地址位置经度。
        /// </summary>
        public double LocationY { get; private set; }

        /// <summary>
        /// 获取地址位置精度。
        /// </summary>
        public int Scale { get; private set; }

        /// <summary>
        /// 获取地址位置标签。
        /// </summary>
        public string Label { get; private set; }
    }
}
